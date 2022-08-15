using System;
using System.Collections.Generic;
using System.Text;

namespace AGS_V2.Utils
{
    class BitWriter
    {
        List<byte> Bytes = new List<byte>();

        public void Write(byte[] Data)
        {
            foreach (var Byte in Data)
                Bytes.Add(Byte);

            Bytes.Add(Convert.ToByte('\0'));
        }
        public void Write(string Data) =>
            Write(Encoding.UTF8.GetBytes(Data));

        public void Write(float Data) =>
            Write(Encoding.UTF8.GetBytes(Data.ToString()));

        public void Write(int Data) =>
            Write(Encoding.UTF8.GetBytes(Data.ToString()));

        public byte[] Dump()
        {
            return Bytes.ToArray();
        }
    }
}
