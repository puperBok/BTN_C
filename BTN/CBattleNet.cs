using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace BTN
{
    class CBattleNet
    {
        private string gameKey;
        private CUser user;
        private CTCPClient conn;

        public void SetUser();
        public void ConnectToServer();
        public void RequestRoomList();
        public void CreateRoom();
        public void JoinRoom();
        public void GameReadyOK();
        public void GameReadyCancle();
    }
}
