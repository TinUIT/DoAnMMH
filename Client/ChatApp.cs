using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class ChatApp : Form
    {
        public ChatApp(SocketManager socketManager, string name)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            client = socketManager;
            tbname = name;
            Thread thread = new Thread(Receive);
            thread.IsBackground = true;
            thread.Start();
        }
        string tbname;
        public void Receive()
        {
            try
            {
                while (true)
                {
                    string r = (string)client.Receive();
                    AddMessage(r);
                }
            }
            catch (Exception ex)
            {
                client.Close();
            }
        }
        IPEndPoint ip;
        SocketManager client;

        //Gửi nhận tin dạng byte
        void AddMessage(string s)
        {
            lvMessage.Items.Add(new ListViewItem() { Text = s });
            tbMessage.Clear();
        }

        void SendMessage()
        {
            if (tbMessage.Text != string.Empty)
            {
                client.Send(tbname + "-.-" + ":" + tbMessage.Text + "-.-chat");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
            AddMessage(tbMessage.Text);
        }        
    }
}
