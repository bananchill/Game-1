using System;
using System.Net.Sockets;
using System.Threading;

namespace Assets.Scrypts
{
    public abstract class Client
    {
        protected static Connection connection;
        public static bool clientConnected = false;
        public static bool online = false;
        public Thread receiveThread, checkThread;

        public abstract void ClientMainLoop();

        public void StartClient(string address, int port)
        {
            checkThread = new Thread(new ThreadStart(StartCheck));
            checkThread.Start();
            try
            {
                TcpClient client = new TcpClient(address, port);
                connection = new Connection(client);
                ClientHandshake();
            }
            catch (Exception)
            {
                NotifyConnectionStatusChanged(false);
            }
        }

        public void StartCheck()
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

        public void NotifyConnectionStatusChanged(bool clientConnected)
        {
            ClientServer.clientConnected = clientConnected;
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

        public void ServerClose()
        {
            connection.Close();
            ConsoleHelper.WriteMessage("Error happened, server disconnected");
        }

        public void StopMain()
        {
            receiveThread.Abort();
        }

        public void StartMain()
        {
            receiveThread = new Thread(ClientMainLoop);
            receiveThread.Start();
        }
    }
}
