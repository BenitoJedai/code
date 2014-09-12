using ScriptCoreLib.Shared.BCLImplementation.System.IO;
using ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Security.Cryptography.X509Certificates
{
    // http://msdn.microsoft.com/en-us/library/system.security.cryptography.cryptostream(v=vs.110).aspx
    // https://github.com/mono/mono/tree/master/mcs/class/corlib/System.Security.Cryptography/CryptoStream.cs
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/CryptoStream.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Security\Cryptography\CryptoStream.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\CryptoStream.cs


    // FEATURE_ASYNC_IO

    // ? shared 

    [Script(Implements = typeof(global::System.Security.Cryptography.CryptoStream))]
    internal class __CryptoStream : __Stream
    {
        // tested by ?

        // will this allow async rsa encryption?

        // public override Task<int> ReadAsync (byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        // public override Task WriteAsync (byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        public override void Flush()
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

        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, global::System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
