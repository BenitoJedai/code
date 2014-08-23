using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Security.Cryptography.X509Certificates
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/x509certificates/x509certificate.cs
    // http://msdn.microsoft.com/en-us/library/system.security.cryptography.x509certificates.x509certificate(v=vs.110).aspx
    // https://github.com/mono/mono/blob/a31c107f59298053e4ff17fd09b2fa617b75c1ba/mcs/class/corlib/System.Security.Cryptography.X509Certificates/X509Certificate.cs


    //A public key certificate, usually just called a certificate, is a digitally signed statement 
    // that binds the value of a public key to the identity of the person, device, or service 
    // that holds the corresponding private key. One of the main benefits of certificates is 
    // that hosts no longer have to maintain a set of passwords for individual subjects who 
    // need to be authenticated as a prerequisite to access. Instead, the host merely establishes trust in a certificate issuer.

    //Most certificates in common use are based on the X.509 v3 certificate standard. 


    // FEATURE_CORECLR
    [Script(Implements = typeof(global::System.Security.Cryptography.X509Certificates.X509Certificate))]
    internal class __X509Certificate
    {
        // Certificate: paper establishing an ownership claim


        // X:\jsc.svn\examples\java\hybrid\JVMCLRRSA\JVMCLRRSA\Program.cs

        // X:\jsc.svn\core\ScriptCoreLibJava\java\security\cert\X509Certificate.cs
        // will .cer help us with securing our hybrid apps?
        // http://www.sparxeng.com/blog/software/x-509-self-signed-certificate-for-cryptography-in-net


        // http://webserver.codeplex.com/wikipage?title=HTTPS&referringTitle=Home
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\DOM\ServiceWorker.cs

        // https://developer.chrome.com/extensions/enterprise_platformKeys
        // chrome.enterprise.platformKeys API
        // http://www.w3.org/TR/WebCryptoAPI/

        // http://webserver.codeplex.com/wikipage?title=HTTPS&referringTitle=Home

        // X:\opensource\codeplex\webserver\HttpServer\SecureHttpListener.cs
        // X:\opensource\codeplex\webserver\HttpServer\Transports\ClientCertificate.cs

    }
}
