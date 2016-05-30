using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTN
{
    class CBuffer
    {
        private int bufferSize;
        private int currentIndex;
        private byte[] buffer;

        public byte[] GetBuffer()
        {
            return this.buffer;
        }

        public void SetBuffer(byte[] data)
        {
            this.buffer = data;
        }

        public void CreateBuffer(int bufSize)
        {
            this.currentIndex = 0;
            this.bufferSize = bufSize;
            this.buffer = new byte[bufSize];
        }

        public int WriteToBufferForString(string data)
        {
            int len = data.Length;
            for (int i = 0; i < len; i++ )
            {
                this.buffer[this.currentIndex + i] = Convert.ToByte(data[i]);
            }
            this.currentIndex += len;

            return this.currentIndex;
        }

        public int WriteToBufferForInt32(int integer32)
        {
            int len = 4;
            byte[] temp = BitConverter.GetBytes((Int32)integer32);
            for (int i = 0; i < len; i++ )
            {
                this.buffer[this.currentIndex + i] = temp[i];
            }
            this.currentIndex += len;

            return this.currentIndex;
        }

        public string ReadToBufferForString(int range_start, int range_end)
        {
            byte[] temp = new byte[range_start - range_end + 1];
            int cnt = 0;
            for(int i = range_start; i < range_end; i++)
            {
                temp[cnt] = this.buffer[i];
                cnt++;
            }

            return Encoding.Default.GetString(temp);
        }

        public int ReadToBufferForInt32(int range_start, int range_end)
        {
            int[] temp = new int[4];
            int cnt = 0;
            for(int i = range_start; i < range_end; i++)
            {
                temp[cnt] = this.buffer[i] & 0xFF;
            }

            return ((temp[0] << 24) + (temp[1] << 16) + (temp[2] << 8) + (temp[3] << 0));
        }
    }
}
