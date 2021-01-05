using System;
using System.IO;

namespace Vanir.Utilities.Helpers
{
    public class StreamHelper
    {
        public static byte[] ReadToEnd(Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                var readBuffer = new byte[4096];
                var totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        var nextByte = stream.ReadByte();

                        if (nextByte != -1)
                        {
                            var temporaryBytes = new byte[readBuffer.Length * 2];

                            Buffer.BlockCopy(readBuffer, 0, temporaryBytes, 0, readBuffer.Length);
                            Buffer.SetByte(temporaryBytes, totalBytesRead, (byte)nextByte);

                            readBuffer = temporaryBytes;
                            totalBytesRead++;
                        }
                    }
                }

                var buffer = readBuffer;

                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }

                return buffer;
            }
            finally
            {
                if (stream.CanSeek) stream.Position = originalPosition;
            }
        }
    }
}