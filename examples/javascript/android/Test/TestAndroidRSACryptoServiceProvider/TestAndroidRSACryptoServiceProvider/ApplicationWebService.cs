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

namespace TestAndroidRSACryptoServiceProvider
{

    //using EncryptedBytes = Byte[];
    // add CallSite? or callback delegate to undo?

    public class EncryptedBytes(public byte[] bytes) { }
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProvider\JVMCLRRSACryptoServiceProvider\Program.cs
        // X:\jsc.svn\examples\javascript\appengine\Test\TestAppEngineRSACryptoServiceProvider\TestAppEngineRSACryptoServiceProvider\ApplicationWebService.cs

        // http://www.w3.org/2012/webcrypto/wiki/KeyWrap_Proposal
        static RSACryptoServiceProvider RSA;

        static ApplicationWebService()
        {
            var dwKeySize = (0x100) * 8;
            var MaxData = (dwKeySize - 384) / 8 + 37;

            // JVM multithreading, which thread will generate the key, and is it thread safe later?
            RSA = new RSACryptoServiceProvider(
                  dwKeySize: dwKeySize,
                  parameters: new CspParameters { }
              );
        }

        public async Task<EncryptedBytes> Encrypt(byte[] data)
        {
            Console.WriteLine("enter Encrypt");

            return new EncryptedBytes(RSA.Encrypt(data, fOAEP: false));
        }

        public async Task<byte[]> Decrypt(EncryptedBytes data)
        {
            Console.WriteLine("enter Decrypt");

            return RSA.Decrypt(data.bytes, fOAEP: false);
        }

    }

}
