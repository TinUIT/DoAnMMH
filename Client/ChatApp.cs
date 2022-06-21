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
        string tbname;

        public ChatApp(SocketManager socketManager, string name)
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            client = socketManager;
            btName.Text += name;
            tbname = name;
            Thread thread = new Thread(Receive);
            thread.IsBackground = true;
            thread.Start();
        }

        public void Receive()
        {
            try
            {
                while (true)
                {
                    string r = (string)client.Receive();
                    lvMessage.Items.Add("\t\t\t" + r);
                    tbMessage.Clear();
                }
            }
            catch (Exception ex)
            {
                client.Close();
            }
        }
        IPEndPoint ip;
        SocketManager client;

        void SendMessage()
        {
            if (tbMessage.Text != string.Empty)
            {
                client.Send(tbname + "-.-" + ": " + tbMessage.Text + "-.-chat");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
            lvMessage.Items.Add(tbname + ": " + tbMessage.Text);
            tbMessage.Clear();
        }        
    }
}
