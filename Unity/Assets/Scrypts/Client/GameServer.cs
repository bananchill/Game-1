using System;
using UnityEngine;

namespace Assets.Scrypts
{
    class GameServer : Client, SendMessage
    {
        bool loagingGame = false;
        public GameServer()
        {
            try
            {
                Debug.Log("Start app, client = " + online);
                StartClient("localhost", 3001);

                Debug.Log("send");
                string data = Account.character.Mail() + "#" + Account.character.Password();
                connection.Send(new Message(MessageType.AUTHORIZATION, data));
                Debug.Log("send1");

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

        public override void ClientMainLoop()
        {
            while (true)
            {
                Message message = connection.Receive();

                if (message != null)
                {
                    if (message.Type() == MessageType.LOADING_GAME && !loagingGame)
                    {
                        FirstGame.WaitTheGame();
                        loagingGame = true;
                    }
                    if (message.Type() == MessageType.SET_INFO)
                    {
                        FirstGame.SetInfo(message);
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
    }
}
