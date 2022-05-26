using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAnMMH
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            Connect();
        }

        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;

        void Connect()
        {
            clientList = new List<Socket>();
            
            IP = new IPEndPoint(IPAddress.Any, 9999);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(IP);
            server.Listen(100);
            Thread Listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {                       
                        Socket client = server.Accept();
                        clientList.Add(client);

                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start(client);
                    }
                }
                catch
                {
                    //MessageBox.Show("loi connect");
                    IP = new IPEndPoint(IPAddress.Any, 9999);
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
            })
            {
                IsBackground = true
            };
            Listen.Start();
        }

        void Receive(Object obj)
        {
            Socket client = (Socket)obj; 
  
            while (true)
            {
                try
                { 
                     byte[] data = new byte[1024];
                     client.Receive(data);
                     string receive = (string)Deserialize(data);
                     string[] arrListStr = receive.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
                    SocketData login = new SocketData(arrListStr[0], arrListStr[1]);
                    MessageBox.Show(login.getUsername());
                }
                catch(Exception ex)
                {
                    clientList.Remove(client);
                    client.Close();
                    //MessageBox.Show(ex.ToString());
                }
            }
        }

        byte[] Serialize(SocketData obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        object Deserialize(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter bf = new BinaryFormatter();
            //ms.Seek(0, SeekOrigin.Begin);
            return bf.Deserialize(ms);

        }

        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }

        private void Server_Shown(object sender, EventArgs e)
        {
            tbIP.Text = GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            if (string.IsNullOrEmpty(tbIP.Text))
            {
                tbIP.Text = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }

        private void Server_Load(object sender, EventArgs e)
        {

        }
    }
}
