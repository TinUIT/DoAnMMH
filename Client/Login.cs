using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

        }
        SocketManager socket = new SocketManager();

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (tbPassword.Text != null && tbUserName.Text != null)
            {
                socket.ConnectServer();
                socket.Send(tbUserName.Text + "--" + tbPassword.Text);
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {

        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Login_Shown(object sender, EventArgs e)
        {

;       }
    }
}
