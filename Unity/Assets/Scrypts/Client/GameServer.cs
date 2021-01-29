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
                        CharacterGame.WaitTheGame();
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
                                    //EnemyBot enemy = Converter.XmlToEnemy(message.data);
                                    //CharacterGame.listEnemy.Add(enemy);
                                }
                                if (message.Type() == MessageType.SET_END)
                                {
                                    CharacterGame.isSpawn = true;
                                    CharacterGame.isGame = true;
                                    break;
                                }
                            }
                        }
                    }
                    if (message.Type() == MessageType.GOT_CHEST)
                    {
                        string[] data = message.Data().Split('#');
                        Chest chest = CharacterGame.SearchChest(float.Parse(data[0]), float.Parse(data[1]));
                        if (chest != null)
                        {
                            foreach (Item item in chest.listItem)
                            {
                                ConsoleHelper.WriteMessage(item.ToString());
                            }
                        }
                    }
                    if (message.Type() == MessageType.GOT_ENEMY)
                    {
                        //string[] data = message.Data().Split('#');
                        //EnemyBot chest = CharacterGame.SearchEnemy(float.Parse(data[0]), float.Parse(data[1]));
                        //if (chest != null)
                        //{
                        //    ConsoleHelper.WriteMessage(chest.ToString());
                        //}
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

        public void SendStep(string step)
        {
            switch (step)
            {
                case "W":
                    SendTextMessage(new Message(MessageType.W));
                    break;
                case "A":
                    connection.Send(new Message(MessageType.A));
                    break;
                case "S":
                    connection.Send(new Message(MessageType.S));
                    break;
                case "D":
                    connection.Send(new Message(MessageType.D));
                    break;
            }
        }
    }
}
