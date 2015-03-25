using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using java.security;
using ScriptCoreLibJava.BCLImplementation.System.IO;
using java.security.cert;
using java.io;
using java.security.interfaces;
using ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography.X509Certificates;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography.X509Certificates
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/x509certificates/x509certificate.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Security/Cryptography/X509Certificates/X509Certificate.cs
    // http://msdn.microsoft.com/en-us/library/system.security.cryptography.x509certificates.x509certificate(v=vs.110).aspx
    // https://github.com/mono/mono/tree/master/mcs/class/corlib/System.Security.Cryptography.X509Certificates/X509Certificate.cs
    // x:\jsc.svn\core\scriptcorelib\javascript\bclimplementation\system\security\cryptography\x509certificates\x509certificate.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\X509Certificates\X509Certificate.cs


    [Script(Implements = typeof(global::System.Security.Cryptography.X509Certificates.X509Certificate2))]
    internal class __X509Certificate2 : __X509Certificate
    {
        // can we extract rsakey from .cer?

        public X509Certificate InternalElement;

        public __X509Certificate2(byte[] rawData)
        {
            try
            {
                var certFactory = CertificateFactory.getInstance("X.509");

                InputStream ins = new ByteArrayInputStream((sbyte[])(object)rawData);

                this.InternalElement = (X509Certificate)certFactory.generateCertificate(ins);
            }
            catch
            {
                throw;
            }
        }



        public string SerialNumber
        {
            get
            {
                // http://stackoverflow.com/questions/12582850/x509-serial-number-using-java

                var u = this.InternalElement.getSerialNumber();

                var w = new StringBuilder();
                var bytes = (byte[])(object)u.toByteArray();

                for (int i = 0; i < bytes.Length; i++)
                {
                    w.Append(bytes[i].ToString("x2"));
                }

                return w.ToString();
            }
        }

        public global::System.Security.Cryptography.X509Certificates.PublicKey PublicKey
        {
            get
            {
                // will we need non RSA?
                var InternalRSAPublicKey = (RSAPublicKey)this.InternalElement.getPublicKey();

                var Key = new __RSACryptoServiceProvider
                {
                    InternalRSAPublicKey = InternalRSAPublicKey
                };

                var value = new __PublicKey { Key = Key };

                return value;
            }
        }


        public string GetNameInfo(global::System.Security.Cryptography.X509Certificates.X509NameType nameType, bool forIssuer)
        {
            // http://jdk-source-code.googlecode.com/svn/trunk/jdk6u21_src/deploy/src/common/share/classes/com/sun/deploy/security/CertUtils.java
            // https://android.googlesource.com/platform/frameworks/base/+/master/core/java/android/net/http/SslCertificate.java
            // http://stackoverflow.com/questions/2914521/how-to-extract-cn-from-x509certificate-in-java

            // https://supportforums.cisco.com/discussion/11041586/extracting-username-x509-certificate
            var subjectName = extractFromQuote(Subject, "CN=");
            return subjectName;
        }

        private static string extractFromQuote(string s, string prefix)
        {
            if (s == null)
                return null;

            // Search for issuer name
            //
            int x = s.IndexOf(prefix);
            int y = 0;

            if (x >= 0)
            {
                x = x + prefix.Length;

                // Search for quote
                if (s[x] == '\"')
                {
                    // if quote is found, search another quote
                    // skip the first quote
                    x = x + 1;
                    y = s.IndexOf('\"', x);
                }
                else // quote is not found, search for comma
                {
                    var z = x;
                    var ok = true;
                    while (ok)
                    {
                        y = s.IndexOf(',', z);
                        // invalidate the comma if it is escaped. android.

                        if (y > z)
                        {
                            z = y + 1;

                            ok = s[y - 1] == '\\';
                        }
                        else
                            ok = false;
                    }
                }
                if (y < 0)
                    return s.Substring(x).Replace("\\,", ",");
                else
                    return s.Substring(x, y - x).Replace("\\,", ",");
            }
            else // No match
                return null;
        }

        public override string Subject
        {
            get
            {
                return this.InternalElement.getSubjectDN().getName();
            }
            set
            {

            }
        }
    }
}
