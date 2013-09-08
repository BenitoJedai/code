using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJava.BCLImplementation.System.IO
{
    [Script(Implements = typeof(global::System.IO.FileStream))]
    internal class __FileStream : __Stream
    {
        // see also:
        // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Web\HttpApplication.cs

        public Stream InternalStream;

        public static implicit operator FileStream(__FileStream s)
        {
            return (FileStream)(object)s;
        }

        public override void Close()
        {
            this.InternalStream.Close();
        }

        public override long Length
        {
            get
            {
                return this.InternalStream.Length;
            }
        }

        public override long Position
        {
            get
            {
                return this.InternalStream.Position;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.InternalStream.Read(
                buffer, offset, count
            );

        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
