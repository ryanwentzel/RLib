using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RLib.Extensions
{
    public static class StreamExtensions
    {
        public static byte[] ReadFully(this Stream stream, int initialLength = 32768)
        {
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;
            int chunk;

            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }

            byte[] result = new byte[read];
            Array.Copy(buffer, result, read);

            return result;
        }
    }
}
