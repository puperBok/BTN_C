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
        private string ipAddress = "192.168.0.8";
        private int port = 9080;
        private string gameKey;
        private CUser user;
        private CTCPClient conn;
        
        public string GetError()
        {
            return this.conn.GetError();
        }

        public CBattleNet(string g_key, CUser user)
        {
            this.gameKey = g_key;
            this.user = user;
            this.conn = new CTCPClient();
        }
        
        public void SetUser()
        {

        }

        public bool ConnectToServer(CMessagePool messagePool)
        {
            if(!this.conn.CreateSocket(ipAddress, port))
            {
                return false;
            }

            if (!this.conn.ConnectToServer(messagePool))
            {
                return false;
            }

            return true;
        }

        public bool DisconnectToServer()
        {
            conn.DestroySocket();
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

        public bool TestEcho(List<string> datas)
        {
            byte[] row_packet = CPacket.EncodedPacketForXml(user.userName, user.loginSession, PROTOCOL.TEST_ECHO, datas);
            if (!conn.RequestToServer(row_packet))
                return false;

            return true;
        }
    }
}
