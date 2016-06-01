using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BTN
{
    class CTCPClient : CSocketBase
    {
        public CAsyncTask asyncTask;
        public override Socket CreateSocket(string address, int port)
        {
            this.address = address;
            this.port = port;
            this.asyncTask = new CAsyncTask(4096);

            this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 10000);
            this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);

            return this.socket;
        }

        public Socket ConnectToServer()
        {
            try
            {
                IPAddress ipAddr = System.Net.IPAddress.Parse(this.address);
                IPEndPoint ipEndPoint = new System.Net.IPEndPoint(ipAddr, this.port);
                this.socket.Connect(ipEndPoint);
            }
            catch (SocketException SCE)
            {
                Console.WriteLine("Socket connect error! : " + SCE.ToString());
                return null;
            }

            this.asyncTask.workingSocket = this.socket;
            this.socket.BeginReceive(this.asyncTask.buf, 0, this.asyncTask.buf.Length, SocketFlags.None, this.asyncTask.recvHandler, this.asyncTask);

            return this.socket;
        }

        public void SendToServer(byte[] msg)
        {
            this.asyncTask.send(msg);
        }
    }
}
