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

namespace TestWebCryptoKeyExport
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
        //static RSACryptoServiceProvider RSA;

        //public static void ExportParameters()
        //{
        //    var publicKey = RSA.ExportParameters(false);

        //    //            +Exponent    { byte[3]}
        //    //+Modulus { byte[256]}


        //}


        public async Task<byte[]> Encrypt(byte[] Exponent, byte[] Modulus)
        {
            // encrypted state sharing.

            // what about import?

            // http://bouncy-castle.1462172.n4.nabble.com/Interoperability-issue-with-SunJCE-OAEP-td4656157.html

            // http://www.w3.org/TR/WebCryptoAPI/#rsa-oaep
            // RSA/ECB/OAEPWithSHA-1AndMGF1Padding

            // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyExport\JVMCLRCryptoKeyExport\Program.cs

            //var n = new RSACryptoServiceProvider(2048);
            var n = new RSACryptoServiceProvider();

            n.ImportParameters(
                new RSAParameters { Exponent = Exponent, Modulus = Modulus }
            );

            // http://stackoverflow.com/questions/9839274/rsa-encryption-by-supplying-modulus-and-exponent

            var value = n.Encrypt(
                Encoding.UTF8.GetBytes("hello from server"), fOAEP: true
            );

            //Array.Reverse(value);

            return value;
        }

        //static ApplicationWebService()
        //{
        //    var dwKeySize = (0x100) * 8;
        //    var MaxData = (dwKeySize - 384) / 8 + 37;

        //    // JVM multithreading, which thread will generate the key, and is it thread safe later?
        //    RSA = new RSACryptoServiceProvider(
        //          dwKeySize: dwKeySize,
        //          parameters: new CspParameters { }
        //      );


        //}

    }
}
