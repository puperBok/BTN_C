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
        private byte[] buffer;

        public PROTOCOL GetPacketProtocol() { return this.packet_protocol; }
        public string GetPacketData()
        {
            if (packet_data != null) return Encoding.Default.GetString(this.packet_data);
            else return null;
        }

        public static byte[] EncodedPacket(PROTOCOL protocol, string data)
        {
            byte[] protocolBytes = new byte[4];
            protocolBytes[0] |= (byte)(((int)protocol & 0xFF000000) >> 24);
            protocolBytes[1] |= (byte)(((int)protocol & 0xFF0000) >> 16);
            protocolBytes[2] |= (byte)(((int)protocol & 0xFF00) >> 8);
            protocolBytes[3] |= (byte)((int)protocol & 0xFF);

            byte[] dataLengthBytes = new byte[4];
            dataLengthBytes[0] |= (byte)(((int)data.Length & 0xFF000000) >> 24);
            dataLengthBytes[1] |= (byte)(((int)data.Length & 0xFF0000) >> 16);
            dataLengthBytes[2] |= (byte)(((int)data.Length & 0xFF00) >> 8);
            dataLengthBytes[3] |= (byte)((int)data.Length & 0xFF);

            byte[] dataBytes = Encoding.Default.GetBytes(data);

            byte[] packet = new byte[4 + 4 + dataBytes.Length];
            System.Buffer.BlockCopy(protocolBytes, 0, packet, 0, protocolBytes.Length);
            System.Buffer.BlockCopy(dataLengthBytes, 0, packet, protocolBytes.Length, dataLengthBytes.Length);
            System.Buffer.BlockCopy(dataBytes, 0, packet, protocolBytes.Length + dataLengthBytes.Length, dataBytes.Length);

            if (BitConverter.IsLittleEndian == true)
            {
                Array.Reverse(packet);
            }

            return packet;
        }

        public static byte[] EncodedPacket_ver2(PROTOCOL protocol, string data)
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

            /*
            if(BitConverter.IsLittleEndian == true)
            {
                Array.Reverse(packet);
            }
            */

            return packet;
        }

        public static string EncodedPacket_ver3(PROTOCOL protocol, string data)
        {
            string xml = "";

            return xml;
        }

        public int DecodedPacket()
        {
            if(buffer == null)
            {
                return -1;
            }

            int protocol = -1;
            protocol |= (buffer[0] & (int)0xFF) << 24;
            protocol |= (buffer[1] & (int)0xFF) << 16;
            protocol |= (buffer[2] & (int)0xFF) << 8;
            protocol |= (buffer[3] & (int)0xFF);
            this.packet_protocol = (PROTOCOL)protocol;

            int dataLength = -1;
            dataLength |= (buffer[4] & (int)0xFF) << 24;
            dataLength |= (buffer[5] & (int)0xFF) << 16;
            dataLength |= (buffer[6] & (int)0xFF) << 8;
            dataLength |= (buffer[7] & (int)0xFF);
            this.packet_data_size = dataLength;

            byte[] data = new byte[dataLength];
            System.Buffer.BlockCopy(buffer, 8, data, 0, dataLength);
            this.packet_data = data;

            return buffer.Length;
        }

        public int DecodedPacket_ver2()
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
    }
}
