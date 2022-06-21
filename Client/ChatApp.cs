﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ChatApp : Form
    {
        public ChatApp()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Connect();
        }

        IPEndPoint ip;
        Socket client;

        //Gửi nhận tin dạng byte
        byte[] Serialize(object obj)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            return stream.ToArray();
        }
        object Deserialize(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }
        void AddMessage(string s)
        {
            lvMessage.Items.Add(new ListViewItem() { Text = s });
            tbMessage.Clear();
        }

        void SendMessage()
        {
            if (tbMessage.Text != string.Empty)
            {
                client.Send(Serialize("tbName.Text" + ":" + tbMessage.Text));
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
            AddMessage(tbMessage.Text);
        }

        void Connect()
        {
            //ip server
            ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                client.Connect(ip);
            }
            catch
            {
                MessageBox.Show("Không kết nối được tới server", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Thread listen = new Thread(Receive);
            listen.IsBackground = true;
            listen.Start();
        }

        //Đóng kết nối hiện thời
        void CloseClient()
        {
            client.Close();
        }

        //Nhận tin
        void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] data = new byte[1024 * 5000];// Khoảng cách nhận 5M byte
                    client.Receive(data);

                    string message = (string)Deserialize(data);
                    AddMessage(message);
                }
            }
            catch
            {
                Close();
            }

        }
    }
}
