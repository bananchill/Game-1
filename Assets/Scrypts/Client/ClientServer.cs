using UnityEngine;
using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine.UI;

namespace Assets.Scrypts
{
    public class ClientServer : MonoBehaviour
    {
        /*
         * ошибка в отключении другого клиента, если он отключается,
         * то прилетает ошибочное сообщение,
         * после чего данный клиент думает, что сервер упал
         */

        protected Connection connection;
        public volatile bool clientConnected = false;
        private bool online = false;
        Thread receiveThread, checkThread;

        public void StartClient()
        {
            checkThread = new Thread(new ThreadStart(StartCheck));
            checkThread.Start();
            try
            {
                TcpClient client = new TcpClient("localhost", 3000);
                connection = new Connection(client);
                ClientHandshake();
                StartMain();
            }
            catch (Exception)
            {
                NotifyConnectionStatusChanged(false);
            }
        }

        private void StartMain()
        {
            receiveThread = new Thread(ClientMainLoop);
            receiveThread.Start();
        }

        private void StopMain()
        {
            receiveThread.Abort();
        }

        private void StartCheck()
        {
            while (true)
            {
                try
                {
                    if (!online)
                    {
                        Thread.Sleep(30000);
                        if (!online)
                        {
                            ServerClose();
                            return;
                        }
                    }
                    connection.Send(new Message(MessageType.TEST_WORK));
                    online = false;
                }
                catch (Exception) { }
                Thread.Sleep(30000);
            }
        }

        public void ClientHandshake()
        {
            int count = 0;
            while (true)
            {
                Message message = connection.Receive();
                if (message == null)
                {
                    ServerClose();
                    return;
                }
                if (message.Type() == MessageType.NAME_REQUEST && count == 0)
                {
                    connection.Send(new Message(MessageType.USER_NAME, "DoctorC"));
                    count = 1;
                }
                else if (message.Type() == MessageType.NAME_ACCEPTED)
                {
                    NotifyConnectionStatusChanged(true);
                    ConsoleHelper.WriteMessage("Соединение установлено.");
                    return;
                }
                online = true;
            }
        }

        public void SendTextMessage(Message message)
        {
            try
            {
                StopMain();
                connection.Send(message);
                StartMain();
            }
            catch (Exception)
            {
                ConsoleHelper.WriteMessage("Ошибка отправки");
                clientConnected = false;
            }
        }

        public void ClientMainLoop()
        {
            while (true)
            {
                Message message = connection.Receive();

                if (message != null)
                {
                    if (message.Type() == MessageType.TEXT)
                    {
                        ProcessIncomingMessage(message.DataFirst());
                    }
                    if (message.Type() == MessageType.AUTHORIZATION)
                    {
                        SetAccount(message);
                    }
                    online = true;
                }
                else
                {
                    ServerClose();
                    return;
                }
            }
        }

        private void ServerClose()
        {
            connection.Close();
            ConsoleHelper.WriteMessage("Error happened, server disconnected");
        }

        protected void ProcessIncomingMessage(string message)
        {
            Debug.Log(message);
        }

        public void NotifyConnectionStatusChanged(bool clientConnected)
        {
            this.clientConnected = clientConnected;
        }

        public void GetAccount(string identifier, string password)
        {
            SendTextMessage(new Message(MessageType.AUTHORIZATION, identifier, password));
        }

        public void SetAccount(Message message)
        {
            Account.character = new Character(int.Parse(message.DataFirst()), message.DataSecond(), message.DataThird(),
                message.DataFourth(), int.Parse(message.DataFifth()), int.Parse(message.DataSixth()));
        }

        public void AddCharacter(Character character)
        {
            SendTextMessage(new Message(MessageType.REGISTRATION, character.Nickname(), character.Password(), character.Mail()));
        }
    }
}