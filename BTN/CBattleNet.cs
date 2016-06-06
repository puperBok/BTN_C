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

        public bool LobbyEnter(List<string> datas)
        {
            byte[] row_packet = CPacket.EncodedPacketForXml(user.userName, user.loginSession, this.gameKey, PROTOCOL.LOBBY_ENTER, datas);
            if (!conn.RequestToServer(row_packet))
                return false;

            return true;
        }

        public bool RequestRoomList(List<string> datas)
        {
            byte[] row_packet = CPacket.EncodedPacketForXml(user.userName, user.loginSession, this.gameKey, PROTOCOL.ROOM_LIST_REQ, datas);
            if (!conn.RequestToServer(row_packet))
                return false;

            return true;
        }

        public bool CreateRoom(List<string> datas)
        {
            byte[] row_packet = CPacket.EncodedPacketForXml(user.userName, user.loginSession, this.gameKey, PROTOCOL.ROOM_OPEN, datas);
            if (!conn.RequestToServer(row_packet))
                return false;

            return true;
        }

        public bool JoinRoom(List<string> datas)
        {
            byte[] row_packet = CPacket.EncodedPacketForXml(user.userName, user.loginSession, this.gameKey, PROTOCOL.ROOM_JOIN, datas);
            if (!conn.RequestToServer(row_packet))
                return false;

            return true;
        }

        public bool LeaveRoomForGuest(List<string> datas)
        {
            byte[] row_packet = CPacket.EncodedPacketForXml(user.userName, user.loginSession, this.gameKey, PROTOCOL.GUEST_ROOM_LEAVE, datas);
            if (!conn.RequestToServer(row_packet))
                return false;

            return true;
        }

        public bool LeaveRoomForHost(List<string> datas)
        {
            byte[] row_packet = CPacket.EncodedPacketForXml(user.userName, user.loginSession, this.gameKey, PROTOCOL.HOST_ROOM_LEAVE, datas);
            if (!conn.RequestToServer(row_packet))
                return false;

            return true;
        }

        public void GameReadyOK()
        {

        }

        public void GameReadyCancle()
        {

        }

        public void GameStart()
        {

        }

        public bool TestEcho(List<string> datas)
        {
            byte[] row_packet = CPacket.EncodedPacketForXml(user.userName, user.loginSession, this.gameKey, PROTOCOL.TEST_ECHO, datas);
            if (!conn.RequestToServer(row_packet))
                return false;

            return true;
        }
    }
}
