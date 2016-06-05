using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BTN
{
    public struct BINARYPACKET
    {
        public PROTOCOL protocol;
        public int dataSize;
        public byte[] data;
    }
    public class CPacket
    {
        public CPacket(byte[] row_packet)
        {
            this.buffer = row_packet;
        }
        private byte[] buffer;
        
        public XMLPACKET xmlPacket;
        public BINARYPACKET binPacket;

        public static byte[] EncodedPacketForBinary(PROTOCOL protocol, string data)
        {
            byte[] protocolBytes = new byte[4];
            protocolBytes = BitConverter.GetBytes((int)protocol);

            byte[] dataLengthBytes = new byte[4];
            dataLengthBytes = BitConverter.GetBytes((int)data.Length);

            byte[] dataBytes = Encoding.Default.GetBytes(data);

            byte[] packet = new byte[4 + 4 + dataBytes.Length];
            System.Buffer.BlockCopy(protocolBytes, 0, packet, 0, protocolBytes.Length);
            System.Buffer.BlockCopy(dataLengthBytes, 0, packet, protocolBytes.Length, dataLengthBytes.Length);
            System.Buffer.BlockCopy(dataBytes, 0, packet, protocolBytes.Length + dataLengthBytes.Length, dataBytes.Length);

            if(BitConverter.IsLittleEndian == false)
            {
                Array.Reverse(packet);
            }
            
            return packet;
        }

        public static byte[] EncodedPacketForXml(PROTOCOL protocol, List<string> datas)
        {
            CXmlManager xml = new CXmlManager();

            xml.XML_FORM(protocol, datas);

            byte[] packet = Encoding.Default.GetBytes(xml.ByString());

            if(BitConverter.IsLittleEndian == false)
            {
                Array.Reverse(packet);
            }
            return packet;
        }

        public int DecodedPacketForBinary()
        {
            if (buffer == null)
            {
                return -1;
            }

            int protocol = -1;
            protocol = BitConverter.ToInt32(buffer, 0);
            this.binPacket.protocol = (PROTOCOL)protocol;

            int dataLength = -1;
            dataLength = BitConverter.ToInt32(buffer, 4);
            this.binPacket.dataSize = dataLength;

            byte[] data = new byte[dataLength];
            System.Buffer.BlockCopy(buffer, 8, data, 0, dataLength);
            this.binPacket.data = data;

            return buffer.Length;
        }

        public int DecodedPacketForXml()
        {
            if (buffer == null)
            {
                return -1;
            }
            string xml = Encoding.Default.GetString(buffer);
            XMLPACKET xp = CXmlManager.ParseFromXml(xml);

            this.xmlPacket.protocol = xp.protocol;
            this.xmlPacket.dataCount = xp.dataCount;
            this.xmlPacket.datas = xp.datas;

            return buffer.Length;
        }
    }
}
