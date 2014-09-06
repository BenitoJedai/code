using java.security;
using java.security.interfaces;
using java.util.zip;
using javax.crypto;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRCryptoKeyExport
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProviderExport\JVMCLRRSACryptoServiceProviderExport\Program.cs

            // jsc needs to see args to make Main into main for javac..

            // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\ApplicationWebService.cs

            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            #region Do a release build to create a hybrid jvmclr program.
            if (typeof(object).FullName != "java.lang.Object")
            {
                System.Console.WriteLine("Do a release build to create a hybrid jvmclr program.");
                Debugger.Break();
                return;
            }
            #endregion


            // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyGenerate\JVMCLRCryptoKeyGenerate\Program.cs

            try
            {
                var sw = Stopwatch.StartNew();

                KeyPairGenerator keyGen = KeyPairGenerator.getInstance("RSA");

                keyGen.initialize(2048);

                KeyPair keyPair = keyGen.generateKeyPair();
                Console.WriteLine("after generateKeyPair " + new { sw.ElapsedMilliseconds });
                // after generateKeyPair { ElapsedMilliseconds = 1791 }


                // namespace java.security
                PublicKey publicKey = keyPair.getPublic();

                RSAPublicKey rsapublicKey = publicKey as RSAPublicKey;

                //{ rsapublicKey = Sun RSA public key, 2048 bits
                //  modulus: 29949193980909979274189480704243019682286346329170638184791375272041884154536539019391076867658592793782845145577141321856957216387377877051532863326545210198967756169805262894313096770941282431904979238566400967478467777198159929565518234047418214901842157765701592238170194579437999716462637123573832853765849987776635905960851094995851522216218636702303980441225891149285848171423753401798137735808260588593837046934598499190528986687550800243662647128332067862280439741602381552218867646299789687315601743815760887214608692897973056730201700896528249989739260099353181532267384971060647420834129903424358272703969
                //  public exponent: 65537 }
                //4

                Console.WriteLine(

                    new { rsapublicKey }

                );

                var rsaModulusBytes = (byte[])(object)rsapublicKey.getModulus().toByteArray();
                var rsaPublicExponent = (byte[])(object)rsapublicKey.getPublicExponent().toByteArray();

                var encByte = (sbyte[])(object)CLRProgram.CLRMain(

                    m: rsaModulusBytes,
                    e: rsaPublicExponent
                    );

                //System.Console.WriteLine("Public Key - " + publicKey.ToString());
                //System.Console.WriteLine("Private Key - " + privateKey.ToString());  

                System.Console.WriteLine(encByte.Length.ToString());


                //Decrypt
                Cipher rsaCipher = Cipher.getInstance("RSA");
                PrivateKey privateKey = keyPair.getPrivate();
                rsaCipher.init(Cipher.DECRYPT_MODE, privateKey);
                sbyte[] decByte = rsaCipher.doFinal(encByte);
                System.Console.WriteLine(decByte.Length.ToString());

                var xstring = Encoding.UTF8.GetString(
                    (byte[])(object)decByte
                );

                Console.WriteLine(new { xstring });


            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
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

            Console.WriteLine(new { firstByte = m[0] });
            // { firstByte = 0 }

            var xm = m.Skip(1).ToArray();


            System.Console.WriteLine(
                new
                {
                    e = e.Length,
                    m = m.Length,
                    xm = xm.Length
                }
            );


            //         Unhandled Exception: System.Security.Cryptography.CryptographicException: The parameter is incorrect.

            //at System.Security.Cryptography.CryptographicException.ThrowCryptographicException(Int32 hr)
            //at System.Security.Cryptography.RSACryptoServiceProvider.EncryptKey(SafeKeyHandle pKeyContext, Byte[] pbKey, Int32 cbKey, Boolean fOAEP, ObjectHandleOnStack ohRetEncryptedKey)
            //at System.Security.Cryptography.RSACryptoServiceProvider.Encrypt(Byte[] rgb, Boolean fOAEP)
            //at JVMCLRCryptoKeyExport.CLRProgram.CLRMain(Byte[] e, Byte[] m)

            var n = new RSACryptoServiceProvider(2048);

            n.ImportParameters(
                new RSAParameters { Exponent = e, Modulus = xm }
            );

            // http://stackoverflow.com/questions/9839274/rsa-encryption-by-supplying-modulus-and-exponent
            //javax.crypto.IllegalBlockSizeException: Data must not be longer than 256 bytes

            var value = n.Encrypt(
                //Encoding.UTF8.GetBytes("hello from server"), fOAEP: true
                Encoding.UTF8.GetBytes("hello from server"), fOAEP: false
            );

            // { value = 257 }

            Console.WriteLine(new { value = value.Length });

            MessageBox.Show("click to close");

            return value;


        }
    }


}
