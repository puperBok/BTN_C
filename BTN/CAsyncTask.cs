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
                // 자료를 수신하고, 수신받은 바이트를 가져옵니다.
                recvBytes = this.workingSocket.EndReceive(ar);
            }
            catch
            {
                // 예외가 발생하면 함수 종료!
                return;
            }

            // 수신받은 자료의 크기가 1 이상일 때에만 자료 처리
            if (recvBytes > 0)
            {
                
            }

            try
            {
                // 자료 처리가 끝났으면~
                // 이제 다시 데이터를 수신받기 위해서 수신 대기를 해야 합니다.
                // Begin~~ 메서드를 이용해 비동기적으로 작업을 대기했다면
                // 반드시 대리자 함수에서 End~~ 메서드를 이용해 비동기 작업이 끝났다고 알려줘야 합니다!
                this.workingSocket.BeginReceive(this.buf, 0, this.buf.Length, SocketFlags.None, this.recvHandler, this);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                Console.WriteLine("recv error {0}", ex.Message);
                return;
            }
        }

        public void handlerOfSend(IAsyncResult ar)
        {

            // 넘겨진 추가 정보를 가져옵니다.
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 보낸 바이트 수를 저장할 변수 선언
            Int32 sentBytes;

            try
            {
                // 자료를 전송하고, 전송한 바이트를 가져옵니다.
                sentBytes = ao.WorkingSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                // 예외가 발생하면 예외 정보 출력 후 함수를 종료한다.
                Console.WriteLine("자료 송신 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }

            if (sentBytes > 0)
            {
                // 여기도 마찬가지로 보낸 바이트 수 만큼 배열 선언 후 복사한다.
                Byte[] msgByte = new Byte[sentBytes];
                Array.Copy(ao.Buffer, msgByte, sentBytes);

                Console.WriteLine("메세지 보냄: {0}", Encoding.Unicode.GetString(msgByte));
            }
        }
    }
}
