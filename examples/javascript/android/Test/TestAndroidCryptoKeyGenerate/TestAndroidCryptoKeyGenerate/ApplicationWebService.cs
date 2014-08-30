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
using java.security;
using javax.crypto;

namespace TestAndroidCryptoKeyGenerate
{

    //using EncryptedBytes = Byte[];
    // add CallSite? or callback delegate to undo?
    public class EncryptedBytes(public byte[] bytes) { }

    public class ApplicationWebService
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140830
        // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyGenerate\JVMCLRCryptoKeyGenerate\Program.cs
        // X:\jsc.svn\examples\javascript\android\Test\TestAndroidCryptoKeyGenerate\TestAndroidCryptoKeyGenerate\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\appengine\Test\TestCryptoKeyGenerate\TestCryptoKeyGenerate\ApplicationWebService.cs

        private static readonly KeyPair keyPair;

        static ApplicationWebService()
        {
            try
            {
                // it works.
                // can we now wrap rsa for all platforms
                // and use it as a generic nuget?

                var sw = Stopwatch.StartNew();
                Console.WriteLine("before generateKeyPair " + new { sw.ElapsedMilliseconds });

                var keyGen = KeyPairGenerator.getInstance("RSA");

                keyGen.initialize(2048);

                keyPair = keyGen.generateKeyPair();
                Console.WriteLine("after generateKeyPair " + new { sw.ElapsedMilliseconds });
                // samsung yoga? I/System.Console( 6966): after generateKeyPair {{ ElapsedMilliseconds = 8633 }}
                // I/System.Console( 7337): after generateKeyPair {{ ElapsedMilliseconds = 11281 }}
                // should the server do it in a background thread?


                //before generateKeyPair { { ElapsedMilliseconds = 2 } }
                //after generateKeyPair { { ElapsedMilliseconds = 1130 } }

            }
            catch
            {
                throw;
            }
        }

        // jsc shall auto rethrow java methods that throw!
        // async rewriter causes issues for javac otherwise?

        //public async Task<EncryptedBytes> Encrypt(byte[] data)
        public Task<EncryptedBytes> Encrypt(byte[] data)
        {
     

            Console.WriteLine("enter Encrypt");

            var value = default(EncryptedBytes);
            try
            {
                var rsaCipher = Cipher.getInstance("RSA");


                //Encrypt
                rsaCipher.init(Cipher.ENCRYPT_MODE, keyPair.getPublic());
                var encByte = (byte[])(object)rsaCipher.doFinal((sbyte[])(object)data);

                value = new EncryptedBytes(encByte);
            }
            catch
            {
                throw;
            }

            Console.WriteLine("exit Encrypt " + new { value.bytes.Length });

            return value.AsResult();
        }

        // http://stackoverflow.com/questions/15806145/getting-error-java-lang-arrayindexoutofboundsexception-too-much-data-for-rsa-bl

        //public async Task<byte[]> Decrypt(EncryptedBytes data)
        public Task<byte[]> Decrypt(EncryptedBytes data)
        {
            // i think there is some byte escaping going on.
            // why?

            // why do we have more data here on android?
            // I/System.Console( 7337): enter Decrypt {{ Length = 265 }}

            Console.WriteLine("enter Decrypt " + new { data.bytes.Length });

            foreach (var item in data.bytes)
            {
                Console.Write(
                    " 0x" + item.ToString("x2")
                );

            }

            Console.WriteLine();



            //I / System.Console(6966): Caused by: java.lang.ArrayIndexOutOfBoundsException: too much data for RSA block
            // I / System.Console(6966):        at com.android.org.bouncycastle.jcajce.provider.asymmetric.rsa.CipherSpi.engineDoFinal(CipherSpi.java:457)
            // I / System.Console(6966):        at javax.crypto.Cipher.doFinal(Cipher.java:1204)

            var value = default(byte[]);
            try
            {

                var rsaCipher = Cipher.getInstance("RSA");


                //Decrypt
                rsaCipher.init(Cipher.DECRYPT_MODE, keyPair.getPrivate());
                value = (byte[])(object)rsaCipher.doFinal((sbyte[])(object)data.bytes);
            }
            catch
            {
                throw;
            }


            return value.AsResult();
        }
    }
}
