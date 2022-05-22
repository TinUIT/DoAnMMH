using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }
        SocketData login = new SocketData();
        SocketManager socket = new SocketManager();

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (tbPassword != null && tbUserName != null)
            {
                login.UserName = tbUserName.Text;
                login.Password = tbPassword.Text;
                socket.Send(login);
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {

        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
