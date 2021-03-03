using UnityEngine;

namespace Assets.Scrypts
{
    public class ClientServer : Client, SendMessage
    {
        /*
         * ошибка в отключении другого клиента, если он отключается,
         * то прилетает ошибочное сообщение,
         * после чего данный клиент думает, что сервер упал
         */

        public ClientServer() { }

        public override void ClientMainLoop()
        {
            while (true)
            {
                Message message = connection.Receive();

                if (message != null)
                {
                    if (message.type == MessageType.TEXT)
                    {
                        ConsoleHelper.WriteMessage(message.data);
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

        public static void SetAccount(Message message)
        {
            string[] data = message.data.Split('#');

            Account.character = new Character(int.Parse(data[0]), data[1], data[2], data[3], int.Parse(data[4]));
            Debug.Log("Account character was been set");
        }

        public static void AddCharacter(Character character)
        {
            string data = character.mail + "#" + character.password + "#" + character.nickname;
            connection.Send(new Message(MessageType.REGISTRATION, data));
            while (true)
            {
                Message message = connection.Receive();
                if (message != null)
                {
                    if (message.type == MessageType.REGISTRATION)
                    {
                        SetAccount(message);
                        Debug.Log("Got character before registration");
                        return;
                    }
                    if (message.type == MessageType.ERROR_REGISTRATION)
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
            //string data = character.Mail() + "#" + character.Password();
            //string data = "doctor@mail.ru" + "#" + "password1234";
            string data = "keosha@mail.ru" + "#" + "password1234";
            connection.Send(new Message(MessageType.AUTHORIZATION, data));
            while (true)
            {
                Message message = connection.Receive();
                if (message != null)
                {
                    if (message.type == MessageType.AUTHORIZATION)
                    {
                        SetAccount(message);
                        Debug.Log("Got character before entry");
                        return;
                    }
                    if (message.type == MessageType.ERROR_AUTHORIZATION)
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