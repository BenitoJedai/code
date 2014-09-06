using java.util.zip;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Cryptography;
using System.Diagnostics;

namespace JVMCLRRSACryptoServiceProviderExport
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // http://bouncy-castle.1462172.n4.nabble.com/FW-RSA-ECB-OAEP-Encryption-From-JAVA-to-NET-td1463697.html

            // http://stackoverflow.com/questions/17110217/is-rsa-pkcs1-oaep-padding-supported-in-bouncycastle
            // http://www.java2s.com/Tutorial/Java/0490__Security/RSAexamplewithOAEPPaddingandrandomkeygeneration.htm

            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyExport\JVMCLRCryptoKeyExport\Program.cs



            var sw = Stopwatch.StartNew();

            // MaxSize = 16384
            var dwKeySize = (0x100) * 8;
            var RSA = new RSACryptoServiceProvider(
                   dwKeySize: dwKeySize,
                   parameters: new CspParameters { }
               );
            //RSA.cre
            //var MaxData = (RSA.KeySize - 384) / 8 + 37;
            // not correct for OLEP?!
            var MaxData = (dwKeySize - 384) / 8 + 7;

            // http://stackoverflow.com/questions/1199058/how-to-use-rsa-to-encrypt-files-huge-data-in-c-sharp

            Console.WriteLine(new { dwKeySize, sw.ElapsedMilliseconds, MaxData });

            var bytes = Encoding.UTF8.GetBytes("hello world".PadRight(MaxData));


            RSAParameters p = RSA.ExportParameters(includePrivateParameters: false);

            // Modulus = {byte[256]}

            // { m = 256, ElapsedMilliseconds = 2756 }
            // {{ m = 256, ElapsedMilliseconds = 4939 }}

            Console.WriteLine(
                new
            {
                m = p.Modulus.Length,

                sw.ElapsedMilliseconds
            }
                );


            var ebytes = CLRProgram.CLRMain(
                 m: p.Modulus,
                 e: p.Exponent
            );

            try
            {
                // {{ Message = Cannot find any provider supporting RSA/NONE/OAEPWITHSHA1ANDMGF1PADDING, StackTrace = java.lang.RuntimeException: Cannot find any provider supporting RSA/NONE/OAEPWITHSHA1ANDMGF1PADDING
                // {{ Message = Cannot find any provider supporting RSA/ECB/OAEP,

                var xdata = RSA.Decrypt(
                 //ebytes, fOAEP: false
                 ebytes, fOAEP: true
                 );

                var xstring = Encoding.UTF8.GetString(xdata);

                Console.WriteLine(new { xstring });
            }
            catch (Exception err)
            {
                // {{ Message = Blocktype mismatch: -65, StackTrace = java.lang.RuntimeException: Blocktype mismatch: -65
                //{
                //    Message = Bad Data.
                //, StackTrace = at System.Security.Cryptography.CryptographicException.ThrowCryptographicException(Int32 hr)
                //   at System.Security.Cryptography.RSACryptoServiceProvider.DecryptKey(SafeKeyHandle pKeyContext, Byte[] pbEncryptedKey, Int32 cbEncryptedKey, Boolean fOAEP, ObjectHandleOnStack ohRetDecryptedKey)

                Console.WriteLine(new { err.Message, err.StackTrace });


            }


        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static byte[] CLRMain(
            byte[] e,
            byte[] m
                )
        {
            // jsc is not yet rewriting pdb

            // { e = System.Byte[], m = System.Byte[] }
            // { e = 3, m = 257 }

            // x:\jsc.svn\examples\javascript\test\testwebcryptokeyexport\testwebcryptokeyexport\applicationwebservice.cs

            // http://security.stackexchange.com/questions/42268/how-do-i-get-the-rsa-bit-length-with-the-pubkey-and-openssl
            //So the key has type RSA, and its modulus has length 257 bytes, except that the first byte has value "00", so the real length is 256 bytes (that first byte was added so that the value is considered positive, because the internal encoding rules call for signed integers, the first bit defining the sign). 256 bytes is 2048 bits.




            System.Console.WriteLine(
                new
            {
                e = e.Length,
                m = m.Length,
            }
            );


            //         Unhandled Exception: System.Security.Cryptography.CryptographicException: The parameter is incorrect.

            //at System.Security.Cryptography.CryptographicException.ThrowCryptographicException(Int32 hr)
            //at System.Security.Cryptography.RSACryptoServiceProvider.EncryptKey(SafeKeyHandle pKeyContext, Byte[] pbKey, Int32 cbKey, Boolean fOAEP, ObjectHandleOnStack ohRetEncryptedKey)
            //at System.Security.Cryptography.RSACryptoServiceProvider.Encrypt(Byte[] rgb, Boolean fOAEP)
            //at JVMCLRCryptoKeyExport.CLRProgram.CLRMain(Byte[] e, Byte[] m)

            var n = new RSACryptoServiceProvider(2048);

            n.ImportParameters(
                new RSAParameters { Exponent = e, Modulus = m }
            );

            // http://stackoverflow.com/questions/9839274/rsa-encryption-by-supplying-modulus-and-exponent
            //javax.crypto.IllegalBlockSizeException: Data must not be longer than 256 bytes

            var value = n.Encrypt(
                Encoding.UTF8.GetBytes("hello from server"), fOAEP: true
            //Encoding.UTF8.GetBytes("hello from server"), fOAEP: false
            );

            // { value = 257 }

            Console.WriteLine(new { value = value.Length });

            //MessageBox.Show("click to close");

            return value;


        }
    }



}
