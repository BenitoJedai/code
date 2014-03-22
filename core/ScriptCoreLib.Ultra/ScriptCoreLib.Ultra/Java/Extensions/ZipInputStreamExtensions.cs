extern alias jvm;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using jvm::java.util.zip;

namespace ScriptCoreLib.Java.Extensions
{
    public static class ZipInputStreamExtensions
    {
        public static MemoryStream ReadToMemoryStream(this ZipInputStream zip, int ChunkSize = 1024)
        {
            var Memory = new MemoryStream();

            try
            {
                var Bytes = new sbyte[ChunkSize];

                var DoRead = true;

                while (DoRead)
                {
                    var Size = zip.read(Bytes, 0, (int)Bytes.Length);

                    DoRead = Size > 0;

                    if (DoRead)
                    {
                        Memory.Write((byte[])(object)Bytes, 0, Size);
                    }
                }
            }
            catch
            {
                throw;
            }

            return Memory;
        }
    }
}
