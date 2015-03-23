using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.security;
using ScriptCoreLibJava.BCLImplementation.System.IO;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.X509Certificates
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/x509certificates/x509certificate.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Security/Cryptography/X509Certificates/X509Certificate.cs
    // http://msdn.microsoft.com/en-us/library/system.security.cryptography.x509certificates.x509certificate(v=vs.110).aspx
    // https://github.com/mono/mono/tree/master/mcs/class/corlib/System.Security.Cryptography.X509Certificates/X509Certificate.cs
    // x:\jsc.svn\core\scriptcorelib\javascript\bclimplementation\system\security\cryptography\x509certificates\x509certificate.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\X509Certificates\X509Certificate.cs


    [Script(Implements = typeof(global::System.Security.Cryptography.X509Certificates.X509Certificate))]
    internal class __X509Certificate
    {
        // can we extract rsakey from .cer?

        public virtual string Subject { get; set; }
    }
}
