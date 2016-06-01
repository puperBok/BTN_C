using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace BTN
{
    public class CAsyncTask
    {
        public byte[] buf;
        public Socket workingSocket;

        public CAsyncTask(int size)
        {
            this.buf = new byte[size];
        }
    }
}
