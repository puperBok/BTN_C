using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BTN
{
    public class CPacket
    {
        public CPacket(byte[] row_packet)
        {
            this.buffer = row_packet;
        }
        private PROTOCOL packet_protocol;
        private int packet_data_size;
        private byte[] packet_data;
        public List<string> packet_datas;
        private int packet_data_count;
        private byte[] buffer;

        public PROTOCOL GetPacketProtocol() { return this.packet_protocol; }
        public string GetPacketData()
        {
            if (packet_data != null) return Encoding.Default.GetString(this.packet_data);
            else return null;
        }

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
            this.packet_protocol = (PROTOCOL)protocol;

            int dataLength = -1;
            dataLength = BitConverter.ToInt32(buffer, 4);
            this.packet_data_size = dataLength;

            byte[] data = new byte[dataLength];
            System.Buffer.BlockCopy(buffer, 8, data, 0, dataLength);
            this.packet_data = data;

            return buffer.Length;
        }

        public int DecodedPacketForXml()
        {
            if (buffer == null)
            {
                return -1;
            }
            string xml = Encoding.Default.GetString(buffer);
            CXmlManager.XMLPACKET xp = CXmlManager.ParseFromXml(xml);

            this.packet_protocol = xp.protocol;
            this.packet_data_count = xp.dataCount;
            this.packet_datas = xp.datas;

            return buffer.Length;
        }
    }
}
