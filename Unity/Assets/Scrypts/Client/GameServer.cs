using System;
using System.Net.Sockets;
using System.Threading;

namespace Assets.Scrypts
{
    class GameServer
    {
        protected static Connection connection;
        public static bool clientConnected = false;
        public static bool online = false;
        Thread receiveThread, checkThread;

        public GameServer()
        {
            checkThread = new Thread(new ThreadStart(StartCheck));
            checkThread.Start();
            try
            {
                TcpClient client = new TcpClient("localhost", 3001);
                connection = new Connection(client);
                ClientHandshake();
                string data = Account.character.Mail() + "#" + Account.character.Password();
                connection.Send(new Message(MessageType.AUTHORIZATION, data));
                while (true)
                {
                    Message message = connection.Receive();
                    if (message == null)
                    {
                        ServerClose();
                        return;
                    }
                    if (message.Type() == MessageType.AUTHORIZATION)
                    {
                        NotifyConnectionStatusChanged(true);
                        ConsoleHelper.WriteMessage("Соединение установлено.");
                        break;
                    }
                    online = true;
                }
                StartMain();
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
                        Thread.Sleep(1000);
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
                Thread.Sleep(1000);
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
                    count = 1;
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

        public void ClientMainLoop()
        {
            while (true)
            {
                Message message = connection.Receive();

                if (message != null)
                {
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

        public void NotifyConnectionStatusChanged(bool clientConnected)
        {
            ClientServer.clientConnected = clientConnected;
        }
    }
}
