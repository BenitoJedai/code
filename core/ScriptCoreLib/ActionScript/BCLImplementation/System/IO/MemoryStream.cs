using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.Extensions;
using System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.IO;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.MemoryStream))]
    internal class __MemoryStream : __Stream
    {
        // http://www.adobe.com/devnet/air/articles/faster-byte-array-operations.html

        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\IO\MemoryStream.cs

        // this cost use 1 man day to figure out!
        // http://forums.adobe.com/thread/659903
        // X:\jsc.svn\examples\actionscript\ReadFloat32\ReadFloat32\ApplicationSprite.cs
        internal ByteArray InternalBuffer = new ByteArray { endian = Endian.LITTLE_ENDIAN };

        public override void Flush()
        {
            // ?
        }

        public virtual int Capacity
        {
            get
            {
                return (int)InternalBuffer.length;

            }
            set
            {

            }
        }

        public __MemoryStream()
            : this(null)
        {

        }

        public __MemoryStream(byte[] buffer)
        {
            if (buffer != null)
            {
                this.Write(buffer, 0, buffer.Length);

                Position = 0;
            }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // http://livedocs.adobe.com/flex/2/langref/flash/utils/ByteArray.html#readBytes()
            var o = new ByteArray { endian = Endian.LITTLE_ENDIAN };


            InternalBuffer.readBytes(o, (uint)offset, (uint)count);

            o.position = 0;

            for (int i = 0; i < count; i++)
            {
                buffer[i] = (byte)((byte)(o.readByte()) & 0xff);
            }

            return buffer.Length;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            // http://livedocs.adobe.com/flex/2/langref/flash/utils/ByteArray.html#writeBytes()
            var o = new ByteArray { endian = Endian.LITTLE_ENDIAN };

            for (int i = 0; i < count; i++)
            {
                o.writeByte(buffer[offset + i] & 0xff);
            }

            InternalBuffer.writeBytes(o, 0, 0);
        }

        public virtual byte[] ToArray()
        {
            return this.InternalBuffer.ToArray();

            //var o = new byte[this.Buffer.length];

            //for (int i = 0; i < o.Length; i++)
            //{
            //    var b = (byte)(this.Buffer.readByte());

            //    o[i] = (byte)(b & 0xff);
            //}

            //return o;
        }

        public override long Length
        {
            get { return InternalBuffer.length; }
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override long Position
        {
            get
            {
                return InternalBuffer.position;
            }
            set
            {
                InternalBuffer.position = (uint)value;
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public void WriteTo(Stream s)
        {
            var b = new byte[this.Length];

            this.Position = 0;
            this.Read(b, 0, b.Length);

            s.Write(b, 0, b.Length);
        }
    }
}
