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
        private string gameKey;
        private CUser user;
        private CTCPClient conn;
        
        public CBattleNet(string g_key, CUser user)
        {
            this.gameKey = g_key;
            this.user = user;
            this.conn = new CTCPClient();
        }
        
        public void SetUser()
        {

        }
        public bool ConnectToServer()
        {
            if(!this.conn.CreateSocket("127.0.0.1", 9080))
            {
                return false;
            }

            if(!this.conn.ConnectToServer())
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
        
        public void TestEcho(string msg)
        {
            CPacket pac = new CPacket();
            byte[] encodedPac = pac.EncodePacket(PROTOCOL.TEST_ECHO, msg);

            conn.RequestToServer(encodedPac);
        }
    }
}
