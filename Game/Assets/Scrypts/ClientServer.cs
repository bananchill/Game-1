using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using Assets.Scrypts;
using Assets.Scrypts.Client;
using UnityEngine.UI;
using System.Collections;
using System.Threading;

public class ClientServer : MonoBehaviour
{
    protected Connection connection;
    private volatile bool clientConnected = false;
    public Text text;
    public InputField input;
    private bool online = false;
    Thread checkThread;

    private void Start()
    {
        Thread checkThread = new Thread(new ThreadStart(startCheck));
        checkThread.Start();
        startClient();
    }
    
    IEnumerator First()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            clientMainLoop();
        }
    }

    private void startClient()
    {
        try
        {
            TcpClient client = new TcpClient("localhost", 3002);
            connection = new Connection(client);
            clientHandshake();
            checkThread = new Thread(clientMainLoop);
            checkThread.Start();
        }
        catch (IOException)
        {
            notifyConnectionStatusChanged(false);
        }
    }

    private void startCheck()
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
                        serverClose();
                        return;
                    }
                }
                connection.send(new Message(MessageType.TEST_WORK));
                online = false;
            }
            catch (Exception) { }
            Thread.Sleep(30000);
        }
    }

    public void clientHandshake()
    {
        while (true)
        {
            Message message = connection.receive();
            if (message == null)
            {
                serverClose();
                return;
            }
            if (message.getType() == MessageType.NAME_REQUEST)
            {
                connection.send(new Message(MessageType.USER_NAME, "DoctorC"));
            }
            else if (message.getType() == MessageType.NAME_ACCEPTED)
            {
                notifyConnectionStatusChanged(true);
                ConsoleHelper.writeMessage("Соединение установлено.");
                return;
            }
            online = true;
        }
    }

    public void SendMessage()
    {
        if (clientConnected)
        {
            string message;
            message = input.text;
            sendTextMessage(message);
        }
        else
        {
            ConsoleHelper.writeMessage("Произошла ошибка во время работы клиента.");
        }
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

            if (message != null)
            {
                if (message.getType() == MessageType.TEXT)
                {
                    processIncomingMessage(message.getData());
                }
                online = true;
            }
            else
            {
                serverClose();
                return;
            }
        }
    }

    private void serverClose()
    {
        connection.close();
        ConsoleHelper.writeMessage("Error happened, server disconnected");
    }

    protected void processIncomingMessage(string message)
    {
        text.text = message;
        Debug.Log(message);
    }

    public void notifyConnectionStatusChanged(bool clientConnected)
    {
        this.clientConnected = clientConnected;
    }

    //IEnumerator StartCheck()
    //{
    //    bool isTrue = true;
    //    while (isTrue)
    //    {
    //        try
    //        {
    //            if (!online)
    //            {
    //                Thread.Sleep(5000);
    //                if (!online)
    //                {
    //                    serverClose();
    //                    isTrue = false;
    //                }
    //            }
    //            connection.send(new Message(MessageType.TEST_WORK));
    //            online = false;
    //        }
    //        catch (Exception) { }
    //        yield return new WaitForSeconds(1);
    //    }
    //}
}
