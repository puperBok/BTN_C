using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTN
{
    public class CMessagePool
    {
        private Queue<CPacket> packetQue = new Queue<CPacket>();

        public void PushMessge(CPacket packet)
        {
            this.packetQue.Enqueue(packet);
        }

        public CPacket TakeMessage()
        {
            if(this.QueIsEmpty())
            {
                return null;
            }
            else
            {
                return this.packetQue.Dequeue();
            }
        }

        public bool QueIsEmpty()
        {
            if(this.packetQue.Count == 0)
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
