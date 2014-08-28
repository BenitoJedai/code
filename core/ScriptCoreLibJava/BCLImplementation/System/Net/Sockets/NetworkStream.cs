using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Net;
using System.Net.Sockets;
using ScriptCoreLibJava.BCLImplementation.System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Net.Sockets
{
    // http://referencesource.microsoft.com/#System/net/System/Net/Sockets/NetworkStream.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Sockets/NetworkStream.cs


    [Script(Implements = typeof(global::System.Net.Sockets.NetworkStream))]
    internal class __NetworkStream : __Stream
    {
        public global::java.io.OutputStream InternalOutputStream;
        public global::java.io.InputStream InternalInputStream;

        public virtual bool DataAvailable
        {
            get
            {
                var flag = false;

                try
                {
                    flag = this.InternalInputStream.available() > 0;
                }
                catch
                {
                }

                return flag;
            }
        }

        public override void Close()
        {
            Flush();

            try
            {
                if (this.InternalOutputStream != null)
                    this.InternalOutputStream.close();

                if (this.InternalInputStream != null)
                    this.InternalInputStream.close();
            }
            catch
            {
                throw;
            }
        }

        public override void Flush()
        {
            try
            {
                if (this.InternalOutputStream != null)
                    this.InternalOutputStream.flush();
            }
            catch
            {
                throw;
            }
        }

        public override long Length
        {
            get
            {
                var i = 0;

                try
                {
                    i = this.InternalInputStream.available();
                }
                catch
                {
                }

                return i;
            }
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override long Seek(long offset, global::System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var i = -1;

            try
            {
                i = this.InternalInputStream.read((sbyte[])(object)buffer, offset, count);
            }
            catch
            {
                throw;
            }

            return i;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            try
            {
                this.InternalOutputStream.write((sbyte[])(object)buffer, offset, count);
            }
            catch
            {
                throw;
            }

        }

        public static implicit operator __NetworkStream(java.io.InputStream s)
        {
            return new __NetworkStream { InternalInputStream = s };
        }

        public static implicit operator NetworkStream(__NetworkStream s)
        {
            return (NetworkStream)(object)s;
        }
    }
}
