using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

namespace ClientTest
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    TcpClient client = new TcpClient("localhost", 1133);

        //    //String name = Console.ReadLine();
        //    Message message = new Message(MessageType.TEXT, "HELLO SERVER!");
        //    String name = (message.getXml());

        //    // send name to server
        //    byte[] buf;
        //    // append newline as server expects a line to be read
        //    buf = Encoding.UTF8.GetBytes(name + "\n");

        //    NetworkStream stream = client.GetStream();
        //    stream.Write(buf, 0, name.Length + 1);

        //    //read xml from server
        //    buf = new byte[100];
        //    stream.Read(buf, 0, 100);
        //    string xml = Encoding.UTF8.GetString(buf);//принят нормальный вид xml

        //    // take only upto first null char
        //    xml = xml.Substring(0, xml.IndexOf(char.ConvertFromUtf32(0)));

        //    var serializer = new XmlSerializer(typeof(Message));
        //    Message obj;
        //    using (var xmlStream = new StringReader(xml))
        //    {
        //        obj = (Message)serializer.Deserialize(xmlStream);
        //    }
        //    Console.Write(obj.ToString());
        //}
    }
}
