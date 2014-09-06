using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace TestWebCryptoKeyImport
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyExport\JVMCLRCryptoKeyExport\Program.cs
        // X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProviderExport\JVMCLRRSACryptoServiceProviderExport\Program.cs

        // shall we encrypt the public key by a shared secret too?
        public readonly byte[] e = p.Exponent;
        public readonly byte[] m = p.Modulus;

        // will this work for android now?

        static readonly RSACryptoServiceProvider RSA;
        static readonly RSAParameters p;

        static ApplicationWebService()
        {
            var sw = Stopwatch.StartNew();

            Console.WriteLine("enter ApplicationWebService cctor");
            var dwKeySize = (0x100) * 8;
            RSA = new RSACryptoServiceProvider(
                  dwKeySize: dwKeySize,
                  parameters: new CspParameters { }
              );

            var MaxData = (dwKeySize - 384) / 8 + 7;
            p = RSA.ExportParameters(includePrivateParameters: false);

            Console.WriteLine("enter exit ApplicationWebService cctor " + new { sw.ElapsedMilliseconds });
        }

        // ebytes = {byte[256]}
        public async Task UploadEncryptedString(byte[] ebytes)
        {
            var xdata = RSA.Decrypt(
                 //ebytes, fOAEP: false
                 ebytes, fOAEP: true
                );

            // xstring = "hello from client"

            var xstring = Encoding.UTF8.GetString(xdata);

            Console.WriteLine(new { xstring });
        }
    }
}
