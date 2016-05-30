using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTN
{
    class CPacket
    {
        private CBuffer buf;
        private int dataLength;
        private PROTOCOL protocol;
        private string data;

        public string GetData()
        {
            return this.data;
        }

        public PROTOCOL GetProtocol()
        {
            return this.protocol;
        }

        public byte[] EncodePacket(PROTOCOL protocol, string data)
        {
            buf.WriteToBufferForInt32((int)protocol);
            buf.WriteToBufferForInt32(data.Length);
            buf.WriteToBufferForString(data);

            return buf.GetBuffer();
        }

        public int DecodePacket(byte[] packet)
        {
            buf.SetBuffer(packet);
            this.protocol = (PROTOCOL)buf.ReadToBufferForInt32(0, 3);
            this.dataLength = buf.ReadToBufferForInt32(4, 7);
            this.data = buf.ReadToBufferForString(8, this.dataLength);

            return this.dataLength;
        }
    }
}
