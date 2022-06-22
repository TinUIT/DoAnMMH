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
        Crypto crypto = new Crypto();
        string key = "thisisasecretkeythisisasecretkey";

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
                    string receive = (string)client.Receive();
                    string[] arrListStr = receive.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    string check = crypto.HMAC(key, arrListStr[1]);
                    if (arrListStr[2] != check)
                    {
                        MessageBox.Show("tin nhắn đã bị sửa");
                    }
                    
                    lvMessage.Items.Add("\t\t\t" + arrListStr[0] + ": " + crypto.DecryptAES(arrListStr[1], key));
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
                string aes = crypto.EncryptAES(tbMessage.Text, key);
                string hmac = crypto.HMAC(key, aes);
                client.Send(tbname + "-.-" + ":" + aes  + "-.-chat" + "-.-" + hmac);
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
