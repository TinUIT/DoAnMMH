using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace DoAnMMH
{
    public partial class Server : Form
    {
        //Khai báo các biến toàn cục
        SqlConnection con;//Khai báo đối tượng thực hiện kết nối đến cơ sở dữ liệu
        SqlCommand cmd;//Khai báo đối tượng thực hiện các câu lệnh truy vấn
        SqlDataAdapter dap;//Khai báo đối tượng gắn kết DataSource với DataSet
        DataSet ds;//Đối tượng chứa dữ liệu tại local
        public Server()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            Connect();
        }

        IPEndPoint IP;
        Socket server;
        List<Socket> clientList;

        void Connect()
        {
            clientList = new List<Socket>();
            
            IP = new IPEndPoint(IPAddress.Any, 9999);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(IP);
            server.Listen(100);
            Thread Listen = new Thread(() =>
            {
                try
                {
                    while (true)
                    {                       
                        Socket client = server.Accept();
                        clientList.Add(client);

                        Thread receive = new Thread(Receive);
                        receive.IsBackground = true;
                        receive.Start(client);
                    }
                }
                catch
                {
                    //MessageBox.Show("loi connect");
                    IP = new IPEndPoint(IPAddress.Any, 9999);
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
            })
            {
                IsBackground = true
            };
            Listen.Start();
        }

        void Receive(Object obj)
        {
            Socket client = (Socket)obj; 
  
            while (true)
            {
                try
                {
                    byte[] data = new byte[1024];
                    client.Receive(data);
                    string receive = (string)Deserialize(data);
                    string[] arrListStr = receive.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);
                    SocketData login = new SocketData(arrListStr[0], arrListStr[1]);
                    MessageBox.Show(login.getPassword());

                    
                }
                catch (Exception ex)
                {
                    clientList.Remove(client);
                    client.Close();
                    //MessageBox.Show(ex.ToString());

                }
            }
        }

        byte[] Serialize(SocketData obj)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        object Deserialize(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            BinaryFormatter bf = new BinaryFormatter();
            //ms.Seek(0, SeekOrigin.Begin);
            return bf.Deserialize(ms);

        }

        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }

        private void Server_Shown(object sender, EventArgs e)
        {
            tbIP.Text = GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            if (string.IsNullOrEmpty(tbIP.Text))
            {
                tbIP.Text = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }

        private void Server_Load(object sender, EventArgs e)
        {
            Database();
        }





        // 2 này show trên server để biết là có những tài khoản nào có sẳn trên server
        public void Database()
        {
            //Tạo đối tượng Connection
            con = new SqlConnection();
            //Truyền vào chuỗi kết nối tới cơ sở dữ liệu
            //Gọi Application.StartupPath để lấy đường dẫn tới thư mục chứa file chạy chương trình 
            con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Source\Repos\DoAnMMH28\5\Account.mdf;Integrated Security=True";
            //Gọi phương thức Load dự liệu
            LoadDuLieu("Select * from tbUser");
        }
        private void LoadDuLieu(String sql)
        {
            //tạo đối tượng DataSet
            ds = new DataSet();
            //Khởi tạo đối tượng DataAdapter và cung cấp vào câu lệnh SQL và đối tượng Connection
            dap = new SqlDataAdapter(sql, con);
            //Dùng phương thức Fill của DataAdapter để đổ dữ liệu từ DataSource tới DataSet
            dap.Fill(ds);
            //Gắn dữ liệu từ DataSet lên DataGridView
            dgvServer.DataSource = ds.Tables[0];
        }

        //Hàm kiểm tra xem thông tin client đăng nhập đúng hay không
        public bool CheckLogin(string UserName, string Password)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Source\Repos\DoAnMMH28\5\Account.mdf;Integrated Security=True");


            string name = UserName;  //login.getUsername();
            string pass = Password; //login.getPassword();

            string sql = "Select * from tbUser where Name = '" + name + "' and Password = '" + pass + "'";
            connect.Open();
            SqlCommand cmd = new SqlCommand(sql, connect);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() == true)
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        // Hàm kiểm tra việc đăng ký tài khoản được hay không
        public bool CheckRegister(string UserName, string Password)
        {
            //Nếu đăng ký thành công thì sẽ return true
            try
            {
                SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\Source\Repos\DoAnMMH28\5\Account.mdf;Integrated Security=True");


                string name = UserName;
                string pass = Password;

                string sql = "Insert into tbUser values ('" + name + "', '" + pass + "', 0, 0)";


                SqlCommand cmd = new SqlCommand(sql, connect);
                connect.Open();
                cmd.ExecuteNonQuery();
                connect.Close();
                cmd.Dispose();
                return true;
            }
            //Có lỗi xãy ra thì return false
            catch(Exception)
            {
                return false;
            }     
            
        }
    }

}
