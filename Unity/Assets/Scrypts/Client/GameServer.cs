namespace Assets.Scrypts
{
    class GameServer : Client, SendMessage
    {
        bool loagingGame = false;

        public GameServer() { }

        public void ConnectToServer()
        {
            string data = Account.character.mail + "#" + Account.character.password;
            connection.Send(new Message(MessageType.AUTHORIZATION, data));

            while (true)
            {
                Message message = connection.Receive();
                if (message == null)
                {
                    ServerClose();
                    return;
                }
                if (message.type == MessageType.AUTHORIZATION)
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
                    if (message.type == MessageType.LOADING_GAME && !loagingGame)
                    {
                        CharacterGame.WaitTheGame();
                        message = connection.Receive();
                        loagingGame = true;
                    }
                    if (message.type == MessageType.SET_INFO)
                    {
                        while (true)
                        {
                            message = connection.Receive();
                            if (message != null)
                            {
                                if (message.type == MessageType.SET_CHEST)
                                {
                                    Chest chest = Converter.XmlToChest(message.data);
                                    CharacterGame.listChests.Add(chest);
                                }
                                if (message.type == MessageType.SET_ENEMY)
                                {
                                    EnemyBot enemy = Converter.XmlToEnemy(message.data);
                                    CharacterGame.listEnemy.Add(enemy);
                                }
                                if (message.type == MessageType.SET_END)
                                {
                                    CharacterGame.isSpawn = true;
                                    connection.Send(new Message(MessageType.READY));
                                    break;
                                }
                            }
                        }
                    }
                    if (message.type == MessageType.GOT_CHEST)
                    {
                        string[] data = message.data.Split('#');
                        Chest chest = CharacterGame.SearchChest(float.Parse(data[0]), float.Parse(data[1]));
                        if (chest != null)
                        {
                            foreach (Item item in chest.listItem)
                            {
                                ConsoleHelper.WriteMessage(item.ToString());
                            }
                        }
                    }
                    if (message.type == MessageType.GOT_ENEMY)
                    {
                        string[] data = message.data.Split('#');
                        EnemyBot enemy = CharacterGame.SearchEnemy(float.Parse(data[0]), float.Parse(data[1]));
                        if (enemy != null)
                        {
                            enemy.attack(Account.character);
                            ConsoleHelper.WriteMessage(Account.character.health + " HP");
                        }
                    }
                    if (message.type == MessageType.START)
                    {
                        CharacterGame.isGame = true;
                    }
                    if (message.type == MessageType.ROUND_END)
                    {
                        CharacterGame.isGame = false;
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
                case "DOWN_E":
                    connection.Send(new Message(MessageType.DOWN_E));
                    break;
                case "UP_E":
                    connection.Send(new Message(MessageType.UP_E));
                    break;
            }
        }
    }
}
