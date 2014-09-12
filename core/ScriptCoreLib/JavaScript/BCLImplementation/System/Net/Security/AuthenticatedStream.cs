using ScriptCoreLib.Shared.BCLImplementation.System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Net.Security
{
    // http://referencesource.microsoft.com/#System/net/System/Net/SecureProtocols/AuthenticatedStream.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System/System.Net.Security/AuthenticatedStream.cs

    [Script(Implements = typeof(global::System.Net.Security.AuthenticatedStream))]
    internal abstract class __AuthenticatedStream : __Stream
    {

        // what about CryptoStream?
        // used by
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Net\Security\SslStream.cs


        public abstract bool IsMutuallyAuthenticated { get; set; }

        public override long Seek(long offset, global::System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
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

        public override void Flush()
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
