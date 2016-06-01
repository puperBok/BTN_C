using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BTN
{
    class CAsyncTask
    {
        public byte[] buf;
        public int bufSize;
        public Socket workingSocket;

        public AsyncCallback recvHandler;
        public AsyncCallback sendHandler;

        public CAsyncTask(int size)
        {
            this.bufSize = size;
            this.buf = new byte[size];
        }

        public void send(byte[] msg)
        {
            this.buf = msg;

            try
            {
                this.workingSocket.BeginSend(this.buf, 0, this.buf.Length, SocketFlags.None, this.sendHandler, this);
            }
            catch(Exception ex)
            {
                Console.WriteLine("send error {0}", ex.Message);
            }
        }

        public void handlerOfRecv(IAsyncResult ar)
        {
            Int32 recvBytes;

            try
            {
                recvBytes = this.workingSocket.EndReceive(ar);
            }
            catch
            {
                return;
            }

            if (recvBytes > 0)
            {
                //자료처리
            }

            try
            {
                // 다시 수신대기
                // Begin을 사용해서 비동기 작업을 대기 시켰으면 End를 통해서 작업이 끝났다고 알려줘야함
                this.workingSocket.BeginReceive(this.buf, 0, this.buf.Length, SocketFlags.None, this.recvHandler, this);
            }
            catch (Exception ex)
            {
                Console.WriteLine("recv error {0}", ex.Message);
                return;
            }
        }

        public void handlerOfSend(IAsyncResult ar)
        {
            Int32 sentBytes;

            try
            {
                sentBytes = this.workingSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("send error {0}", ex.Message);
                return;
            }
        }
    }
}
