using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTN
{
    public class CMessagePool
    {
        static CMessagePool instance = new CMessagePool();
        private CMessagePool() { }
        public static CMessagePool GetInstance()
        {
            if(instance == null)
            {
                instance = new CMessagePool();
            }

            return instance;
        }

        private Queue<CPacket> packetPool = new Queue<CPacket>();

        public CPacket TakeMessage()
        {
            if(queIsEmpty() == true)
            {
                return null;
            }
            else
            {
                return packetPool.Dequeue();
            }
        }

        public void PushMessage(CPacket msg)
        {
            this.packetPool.Enqueue(msg);
        }

        private bool queIsEmpty()
        {
            if(this.packetPool.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
