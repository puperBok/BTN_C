using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTN
{
    public enum PROTOCOL : int
    {
        LOBBY_ENTER,
        ROOM_LIST_REQUEST,
        ROOM_OPEN,
        ROOM_CLOSE,
        ROOM_JOIN,
        ROOM_LEAVE,
        GAME_READY_OK,
        GAME_READY_CANCEL,
        GAME_START,
        TEST_ECHO
    }
}
