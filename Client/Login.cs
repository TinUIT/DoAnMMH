﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

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

        Crypto cryp = new Crypto();

        private void btLogin_Click(object sender, EventArgs e)
        {
            if (tbPassword.Text != null && tbUserName.Text != null)
            {
                socket.ConnectServer();
                socket.Send(tbUserName.Text + "-.-" + cryp.SHA256(tbPassword.Text) + "-.-login");
                string response = (string)socket.Receive();
                //
                if(response=="Đã đăng nhập thành công")
                {
                    this.Hide();
                    ChatApp chatApp = new ChatApp();
                    chatApp.ShowDialog();
                }    
                else
                {
                    MessageBox.Show(response);
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
            }
            else
            {
                tbPassword.UseSystemPasswordChar = true;
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            socket.Close();
        }

        private void Login_Shown(object sender, EventArgs e)
        {

;       }
    }
}
