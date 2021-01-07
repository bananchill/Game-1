using System;

namespace ClientTest
{
    class ConsoleHelper
    {
        public static void writeMessage(string message)
        {
            Console.WriteLine(message);
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
                    Console.WriteLine("Произошла ошибка при попытке ввода текста. Попробуйте еще раз.");
                }
            }
            return message;
        }
    }
}
