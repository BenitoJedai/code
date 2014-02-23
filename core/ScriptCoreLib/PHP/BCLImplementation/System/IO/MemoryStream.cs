using ScriptCoreLib.Shared.BCLImplementation.System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.MemoryStream))]
    internal class __MemoryStream : __Stream
    {
        // see also: http://www.phpclasses.org/browse/file/34867.html

        internal string Buffer = "";


        public override void Flush()
        {
            // ?
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

        public override void WriteByte(byte value)
        {
            if (this.Position < this.Length)
                throw new NotImplementedException();


            this.Buffer += new string((char)(value & 0xff), 1);
            this.Position++;
        }

        public virtual byte[] ToArray()
        {
            // http://stackoverflow.com/questions/885597/string-to-byte-array-in-php
            return (byte[])Native.API.array_values(Native.API.unpack("C*", this.Buffer));
        }

        public override long Length
        {
            get { return Buffer.Length; }
        }

        public override long Position { get; set; }


        public override long Seek(long offset, global::System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
