using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Connect : Form
    {
        public Connect()
        {
            InitializeComponent();
        }

        SocketManager socket;

        private void btConnect_Click(object sender, EventArgs e)
        {
            try
            {
                socket = new SocketManager();
                socket.setIP(tbIP.Text);
                if (socket.ConnectServer())
                {
                    this.Hide();
                    socket.Close();
                    Login login = new Login();
                    login.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Không connect", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void Connect_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
