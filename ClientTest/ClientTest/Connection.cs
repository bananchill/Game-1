using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;

namespace ClientTest
{
    public class Connection
    {
        NetworkStream stream;
        byte[] buf;

        public Connection(TcpClient tcpClient)
        {
            stream = tcpClient.GetStream();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void send(Message message)
        {
            String mess = (message.getXml());
            buf = Encoding.UTF8.GetBytes(mess + "\n");
            stream.Write(buf, 0, mess.Length + 1);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Message receive()
        {
            Message message;

            buf = new byte[100];
            stream.Read(buf, 0, 100);
            string xml = Encoding.UTF8.GetString(buf);
            xml = xml.Substring(0, xml.IndexOf(char.ConvertFromUtf32(0)));
            var serializer = new XmlSerializer(typeof(Message));
            using (var xmlStream = new StringReader(xml))
            {
                message = (Message)serializer.Deserialize(xmlStream);
            }
            return message;
        }

        public void close()
        {
            stream.Close();
        }
    }
}
