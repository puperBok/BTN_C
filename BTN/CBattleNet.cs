using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace BTN
{
    public class CBattleNet
    {
        private string ipAddress = "203.230.103.45";
        private int port = 9080;
        private string gameKey;
        private CUser user;
        private CTCPClient conn;
        private CMessagePool msgPool;
        
        public string GetError()
        {
            return this.conn.GetError();
        }

        public CBattleNet(string g_key, CUser user, string ipAddress)
        {
            this.gameKey = g_key;
            this.user = user;
            this.ipAddress = ipAddress;
            this.conn = new CTCPClient();
            this.msgPool = new CMessagePool();
        }

        public CBattleNet(string g_key, CUser user)
        {
            this.gameKey = g_key;
            this.user = user;
            this.conn = new CTCPClient();
            this.msgPool = new CMessagePool();
        }
        
        public void SetUser()
        {

        }
        public bool ConnectToServer()
        {
            if(!this.conn.CreateSocket(ipAddress, port))
            {
                return false;
            }

            if(!this.conn.ConnectToServer(msgPool))
            {
                return false;
            }

            return true;
        }
        public void RequestRoomList()
        {

        }
        public void CreateRoom()
        {

        }
        public void JoinRoom()
        {

        }
        public void GameReadyOK()
        {

        }
        public void GameReadyCancle()
        {

        }

        public bool TestSendToServerForString(string msg)
        {
            byte[] str = Encoding.Default.GetBytes(msg);
            if (!this.conn.RequestToServer(str))
                return false;

            return true;
        }

        public bool TestSendToServerForPacket(string data)
        {
            byte[] str = CPacket.EncodedPacket_ver2(PROTOCOL.TEST_ECHO, data);
            if (!this.conn.RequestToServer(str))
                return false;

            return true;
        }

    }
}
