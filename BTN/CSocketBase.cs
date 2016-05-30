using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BTN
{
    abstract class CSocketBase
    {
        protected Socket socket;
        protected string address;
        protected int port;
        
        public abstract Socket CreateSocket(string address, int port);
        public void DestroySocket()
        {
            this.socket.Close();
            this.socket = null;
        }
    }
}
