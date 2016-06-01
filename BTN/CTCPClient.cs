using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace BTN
{
    public class CTCPClient : CSocketBase
    {
        private AsyncCallback recvHandler;
        private AsyncCallback sendHandler;

        public string Error
        {
            get
            {
                return Error;
            }
            set
            {
                Error = value;
            }
        }

        private void ErrorHandler(ERROR_CODE code, string error_msg)
        {
            switch(code)
            {
                case ERROR_CODE.SOCKET_CREATE:
                    this.Error = "SOCKET_CREATE_ERROR" + " : " + error_msg;
                    break;
                case ERROR_CODE.SOCKET_CONNECT:
                    this.Error = "SOCKET_CONNECT_ERROR" + " : " + error_msg;
                    break;
                case ERROR_CODE.SOCKET_SEND:
                    this.Error = "SOCKET_SEND_ERROR" + " : " + error_msg;
                    break;
                case ERROR_CODE.SOCKET_RECV:
                    this.Error = "SOCKET_RECV_ERROR" + " : " + error_msg;
                    break;
            }
        }

        public override bool CreateSocket(string address, int port)
        {
            this.address = address;
            this.port = port;

            try
            {
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            }
            catch (SocketException SCE)
            {
                this.ErrorHandler(ERROR_CODE.SOCKET_CREATE, SCE.ToString());
                return false;
            }
            this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 10000);
            this.socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 10000);

            return true;
        }

        public bool ConnectToServer()
        {
            try
            {
                IPAddress ipAddr = System.Net.IPAddress.Parse(this.address);
                IPEndPoint ipEndPoint = new System.Net.IPEndPoint(ipAddr, this.port);
                this.socket.Connect(ipEndPoint);
            }
            catch (SocketException SCE)
            {
                this.ErrorHandler(ERROR_CODE.SOCKET_CONNECT, SCE.ToString());
                return false;
            }

            CAsyncTask at = new CAsyncTask(4096);
            at.workingSocket = this.socket;
            
            this.socket.BeginReceive(at.buf, 0, at.buf.Length, SocketFlags.None, recvHandler, at);

            return true;
        }

        public bool RequestToServer(byte[] msg)
        {
            CAsyncTask at = new CAsyncTask(1);
            at.buf = msg;
            at.workingSocket = this.socket;
            
            try
            {
                this.socket.BeginSend(at.buf, 0, at.buf.Length, SocketFlags.None, sendHandler, at);
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ERROR_CODE.SOCKET_SEND, ex.ToString());
                return false;
            }

            return true;
        }

        private bool handlerOfRecv(IAsyncResult ar)
        {
            CAsyncTask at = (CAsyncTask)ar.AsyncState;
            Int32 recvBytes;

            try
            {
                recvBytes = at.workingSocket.EndReceive(ar);
            }
            catch
            {
                return false;
            }

            if (recvBytes > 0)
            {
                //자료처리
                CPacket packet = new CPacket(at.buf);
                CMessagePool.GetInstance().PushMessage(packet);
            }

            try
            {
                // 다시 수신대기
                // Begin을 사용해서 비동기 작업을 대기 시켰으면 End를 통해서 작업이 끝났다고 알려줘야함
                at.workingSocket.BeginReceive(at.buf, 0, at.buf.Length, SocketFlags.None, this.recvHandler, this);
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ERROR_CODE.SOCKET_RECV, ex.ToString());
                return false;
            }

            return true;
        }

        private bool handlerOfSend(IAsyncResult ar)
        {
            CAsyncTask at = (CAsyncTask)ar.AsyncState;
            Int32 sentBytes;

            try
            {
                sentBytes = at.workingSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                this.ErrorHandler(ERROR_CODE.SOCKET_SEND, ex.ToString());
                return false;
            }

            return true;
        }
    }
}
