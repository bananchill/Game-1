using UnityEngine;
using System;
using System.Net.Sockets;
using System.Threading;

namespace Assets.Scrypts
{
    public class ClientServer
    {
        /*
         * ошибка в отключении другого клиента, если он отключается,
         * то прилетает ошибочное сообщение,
         * после чего данный клиент думает, что сервер упал
         */

        protected static Connection connection;
        public static bool clientConnected = false;
        public static bool online = false;
        Thread receiveThread, checkThread;

        public ClientServer() { }

        public void StartClient()
        {
            checkThread = new Thread(new ThreadStart(StartCheck));
            checkThread.Start();
            try
            {
                TcpClient client = new TcpClient("localhost", 3000);
                connection = new Connection(client);
                ClientHandshake();
            }
            catch (Exception)
            {
                NotifyConnectionStatusChanged(false);
            }
        }

        public void StartMain()
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
                if (message.Type() == MessageType.CONNECTION_REQUEST && count == 0)
                {
                    connection.Send(new Message(MessageType.CONNECTION_ACCEPTED));
                    count++;
                }
                else if (message.Type() == MessageType.CONNECTION_ACCEPTED)
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
            catch (Exception e)
            {
                ConsoleHelper.WriteMessage("Ошибка отправки " + e);
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
                        ProcessIncomingMessage(message.Data());
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
            ClientServer.clientConnected = clientConnected;
        }

        public static void SetAccount(Message message)
        {
            string[] data = message.Data().Split('#');

            Account.character = new Character(int.Parse(data[0]), data[1], data[2], data[3], int.Parse(data[4]), int.Parse(data[5]));
            Debug.Log("Account character was been set");
        }

        public static void AddCharacter(Character character)
        {
            string data = character.Mail() + "#" + character.Password() + "#" + character.Nickname();
            connection.Send(new Message(MessageType.REGISTRATION, data));
            while (true)
            {
                Message message = connection.Receive();
                if (message != null)
                {
                    if (message.Type() == MessageType.REGISTRATION)
                    {
                        SetAccount(message);
                        Debug.Log("Got character before registration");
                        return;
                    }
                    if (message.Type() == MessageType.ERROR_REGISTRATION)
                    {
                        Debug.Log("Error registration");
                    }
                    online = true;
                }
                else { Debug.Log("Message = null"); }
            }
        }

        public static void Entry(Character character)
        {
            string data = character.Mail() + "#" + character.Password();
            connection.Send(new Message(MessageType.AUTHORIZATION, data));
            while (true)
            {
                Message message = connection.Receive();
                if (message != null)
                {
                    if (message.Type() == MessageType.AUTHORIZATION)
                    {
                        SetAccount(message);
                        Debug.Log("Got character before entry");
                        return;
                    }
                    if (message.Type() == MessageType.ERROR_AUTHORIZATION)
                    {
                        Debug.Log("Error authorization");
                    }
                    online = true;
                }
                else { Debug.Log("Message = null"); }
            }
        }
    }
}