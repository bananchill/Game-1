using System;
using UnityEngine;

namespace Assets.Scrypts.Client
{
    class ConsoleHelper
    {
        public static void writeMessage(string message)
        {
            Debug.Log(message);
        }

        public static string readString()
        {
            string message;

            while (true)
            {
                try
                {
                    message = Console.ReadLine();
                    break;
                }
                catch (Exception)
                {
                    writeMessage("Произошла ошибка при попытке ввода текста. Попробуйте еще раз.");
                }
            }
            return message;
        }
    }
}
