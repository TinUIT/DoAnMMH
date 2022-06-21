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
    public partial class Register : Form
    {
        public Register(SocketManager socketM)
        {
            InitializeComponent();
            socket = socketM;
        }
        SocketManager socket = new SocketManager();

        Crypto cryp = new Crypto();
        private void btRegister_Click(object sender, EventArgs e)
        {
            if (tbPassword.Text == tbRetypePassword.Text)
            {
                if (tbPassword.Text != null && tbUserName.Text != null)
                {
                    socket.Send(tbUserName.Text + "-.-" + cryp.SHA256(tbPassword.Text) + "-.-register");
                    string response = (string)socket.Receive();

                    if (response == "Đã đăng nhập thành công")
                    {
                        this.Hide();
                        ChatApp chatApp = new ChatApp(socket, tbUserName.Text);
                        chatApp.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(response);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng nhập lại Password");
                }
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowPassword.Checked == true)
            {
                tbPassword.UseSystemPasswordChar = false;
                tbRetypePassword.UseSystemPasswordChar = false;
            }
            else
            {
                tbPassword.UseSystemPasswordChar = true;
                tbRetypePassword.UseSystemPasswordChar = true;
            }
        }
    }
}
