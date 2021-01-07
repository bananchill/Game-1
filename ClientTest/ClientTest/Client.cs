using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ClientTest
{
    public class Client
    {
        protected Connection connection;
        private volatile bool clientConnected = false;
        static Client client;

        static void Main(string[] args)
        {
            client = new Client();
            client.startClient();
        }

        private void startClient()
        {
            try
            {
                TcpClient client = new TcpClient("localhost", 3000);

                connection = new Connection(client);

                clientHandshake();
                clientMainLoop();
            }
            catch (IOException)
            {
                notifyConnectionStatusChanged(false);
            }
        }

        public void clientHandshake()
        {
            while (true)
            {
                Message message = connection.receive();

                if (message.getType() == MessageType.NAME_REQUEST)
                {
                    connection.send(new Message(MessageType.USER_NAME, "DoctorC"));
                }
                else if (message.getType() == MessageType.NAME_ACCEPTED)
                {
                    notifyConnectionStatusChanged(true);
                    Thread myThread = new Thread(new ThreadStart(run));
                    myThread.Start();
                    return;
                }
                else
                {
                    throw new Exception("Unexpected MessageType");
                }
            }
        }

        public void run()
        {
            if (clientConnected)
            {
                ConsoleHelper.writeMessage("Соединение установлено.");

                while (clientConnected)
                {
                    string message;
                    message = ConsoleHelper.readString();
                    if (shouldSendTextFromConsole())
                    {
                        sendTextMessage(message);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else
            {
                ConsoleHelper.writeMessage("Произошла ошибка во время работы клиента.");
            }
        }

        protected bool shouldSendTextFromConsole()
        {
            return true;
        }

        protected void sendTextMessage(string text)
        {
            try
            {
                connection.send(new Message(MessageType.TEXT, text));
            }
            catch (Exception)
            {
                ConsoleHelper.writeMessage("Ошибка отправки");
                clientConnected = false;
            }
        }

        public void clientMainLoop()
        {
            while (true)
            {
                Message message = connection.receive();

                if (!(message.getType() == MessageType.TEXT) &&
                        !(message.getType() == MessageType.USER_ADDED) &&
                        !(message.getType() == MessageType.USER_REMOVED))
                {
                    throw new Exception("Unexpected MessageType");
                }

                if (message.getType() == MessageType.TEXT)
                    processIncomingMessage(message.getData());
            }
        }

        protected void processIncomingMessage(string message)
        {
            ConsoleHelper.writeMessage(message);
        }

        public void notifyConnectionStatusChanged(bool clientConnected)
        {
            this.clientConnected = clientConnected;
        }

        protected string getUserName()
        {
            ConsoleHelper.writeMessage("Введите имя пользователя: ");
            return ConsoleHelper.readString();
        }        
    }
}
