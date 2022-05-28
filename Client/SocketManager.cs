using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class SocketManager
    {
        Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static string sIP = "127.0.0.1";
        public  int PORT = 9999;
        public const int BUFFER = 1024;
        
        public void setIP(string IP)
        {
           sIP = IP;
        }

        public string getIP()
        {
            return sIP;
        }
        public bool ConnectServer()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(sIP), PORT);
           
            try
            {
                client.Connect(iep);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Send(object data)
        {
            byte[] sendData = SerializeData(data);

            client.Send(sendData);
        }

        public object Receive()
        {
            byte[] receiveData = new byte[BUFFER*50];
            client.Receive(receiveData);

            return DeserializeData(receiveData);
        }

        public byte[] SerializeData(object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, o);
            return ms.ToArray();
        }

        public object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            //ms.Position = 0;
            //ms.Seek(0, SeekOrigin.Begin);
            return bf1.Deserialize(ms);
        }

        public void Close()
        {
            client.Close();
        }
    }
}
