using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTN
{
    public enum PROTOCOL : int
    {
        LOBBY_ENTER,
        LOBBY_ENTER_SUCC,
        LOBBY_ENTER_FAIL,
        ROOM_LIST_REQ,
        ROOM_LIST_REQ_SUCC,
        ROOM_LIST_REQ_FAIL,
        ROOM_OPEN,
        ROOM_OPEN_SUCC,
        ROOM_OPEN_FAIL,
        ROOM_JOIN,
        ROOM_JOIN_SUCC,
        ROOM_JOIN_FAIL,
        HOST_ROOM_LEAVE,
        HOST_ROOM_LEAVE_SUCC,
        HOST_ROOM_LEAVE_FAIL,
        GUEST_ROOM_LEAVE,
        GUEST_ROOM_LEAVE_SUCC,
        GUEST_ROOM_LEAVE_FAIL,
        GUEST_COME_IN,
        GUEST_COME_IN_SUCC,
        GUEST_COME_IN_FAIL,
        GUEST_GO_OUT,
        GUEST_GO_OUT_SUCC,
        GUEST_GO_OUT_FAIL,
        GAME_READY_OK,
        GAME_READY_OK_SUCC,
        GAME_READY_OK_FAIL,
        GAME_READY_CANCEL,
        GAME_READY_CANCEL_SUCC,
        GAME_READY_CANCEL_FAIL,
        GAME_START,
        GAME_START_SUCC,
        GAME_START_FAIL,
        TEST_ECHO
    }
}
