using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnMMH
{
    [Serializable]
    public class SocketData
    {
        private string UserName;
        private string Password;

        public SocketData(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string getUsername()
        {
            return UserName;
        }
    }
}
