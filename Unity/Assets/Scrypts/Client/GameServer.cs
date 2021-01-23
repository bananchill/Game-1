using System;
using System.IO;
using UnityEngine;

namespace Assets.Scrypts
{
    class GameServer : Client, SendMessage
    {
        bool loagingGame = false;

        public GameServer() { }

        public void ConnectToServer()
        {
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
                    ConsoleHelper.WriteMessage("Account accept.");
                    break;
                }
                online = true;
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
                        message = connection.Receive();
                        loagingGame = true;
                    }
                    if (message.Type() == MessageType.SET_INFO)
                    {
                        while (true)
                        {
                            message = connection.Receive();
                            if (message != null)
                            {
                                if (message.Type() == MessageType.SET_CHEST)
                                {
                                    Chest chest = Converter.XmlToChest(message.data);
                                    CharacterGame.listChests.Add(chest);
                                }
                                if (message.Type() == MessageType.SET_ENEMY)
                                {
                                }
                                if (message.Type() == MessageType.SET_END)
                                {

                                    break;
                                }
                            }
                        }
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
