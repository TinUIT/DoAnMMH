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

            CheckForIllegalCrossThreadCalls = false;
        }

        SocketManager socket = new SocketManager();
        //
        private void btConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if(tbIP.Text != null )
                {
                    socket.setIP(tbIP.Text);
                    if (socket.ConnectServer())
                    {
                        Login login = new Login();
                        this.Hide();
                        socket.Close();
                        login.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Không connect", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void tbIP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
