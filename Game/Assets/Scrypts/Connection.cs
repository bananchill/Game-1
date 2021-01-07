using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scrypts
{
    public class Connection
    {
        StreamWriter writer;
        StreamReader reader;
        NetworkStream stream;
        byte[] buf;
        byte[] receiveBuffer;
        int dataBufferSize = 2048;

        public Connection(TcpClient tcpClient)
        {
            stream = tcpClient.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void send(Message message)
        {
            //String mess = (message.getXml());
            //buf = Encoding.UTF8.GetBytes(mess + "\n");
            //stream.Write(buf, 0, mess.Length + 1);
            //stream.Flush();

            writer.WriteLine(message.getXml());
            writer.Flush();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Message receive()
        {
            Message message;

            //buf = new byte[2048];
            //try
            //{
            //    int bytesread = stream.Read(buf, 0, buf.Length);
            //    if (bytesread > -1)
            //    {
            //        string xml = Encoding.UTF8.GetString(buf);
            //        xml = xml.Substring(0, xml.IndexOf(char.ConvertFromUtf32(0)));
            //        var serializer = new XmlSerializer(typeof(Message));
            //        using (var xmlStream = new StringReader(xml))
            //        {
            //            message = (Message)serializer.Deserialize(xmlStream);
            //        }
            //        return message;
            //    }
            //    else return null;
            //}
            //catch (Exception)
            //{
            //    return null;
            //}

            string s = reader.ReadLine();
            var serializer = new XmlSerializer(typeof(Message));
            using (var xmlStream = new StringReader(s))
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