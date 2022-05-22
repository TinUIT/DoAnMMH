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
                socket.IP = tbIP.Text;
                socket.ConnectServer();
            }
            catch
            {
                MessageBox.Show("Vui lòng nhập IP");
            }
            if(socket.ConnectServer())
                MessageBox.Show("Đã kết nối", "Thông báo", MessageBoxButtons.OKCancel);
        }

    }
}
