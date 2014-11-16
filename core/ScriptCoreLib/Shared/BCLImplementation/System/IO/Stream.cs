using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLib.Shared.BCLImplementation.System.IO
{
    // http://referencesource.microsoft.com/#mscorlib/system/io/stream.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.IO/Stream.cs

    [Script(Implements = typeof(global::System.IO.Stream))]
    public abstract class __Stream : __MarshalByRefObject, IDisposable
    {
        public virtual int ReadTimeout { get; set; }


        public abstract long Seek(long offset, SeekOrigin origin);

        public abstract void SetLength(long value);

        public abstract long Length { get; }

        public abstract long Position { get; set; }

        public abstract void Flush();

        public virtual void Close()
        {
            this.Flush();
        }

        public abstract int Read(byte[] buffer, int offset, int count);

        public virtual int ReadByte()
        {
            var buffer = new byte[1];
            var i = Read(buffer, 0, 1);

            if (i < 0)
                return i;

            return buffer[0];
        }

        public abstract void Write(byte[] buffer, int offset, int count);

        public virtual void WriteByte(byte value)
        {
            this.Write(new[] { value }, 0, 1);
        }

        public void CopyTo(Stream destination)
        {
            //Console.WriteLine("__Stream.CopyTo");

            var buffer = new byte[0x4000];

            var flag = true;
            while (flag)
            {
                flag = false;
                var c = this.Read(buffer, 0, buffer.Length);

                //Console.WriteLine("__Stream.CopyTo " + new { c });

                if (c > 0)
                {
                    destination.Write(buffer, 0, c);
                    flag = true;
                }
            }
        }

        public void Dispose()
        {
            //            Implementation not found for type import :
            //type: System.IO.Stream
            //method: Void Dispose()
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!

            this.Close();
        }
    }
}
