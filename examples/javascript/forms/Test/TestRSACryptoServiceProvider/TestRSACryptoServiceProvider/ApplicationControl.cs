using TestRSACryptoServiceProvider;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Diagnostics;
using System;

namespace TestRSACryptoServiceProvider
{
    public partial class ApplicationControl : UserControl
    {
        public ApplicationControl()
        {
            this.InitializeComponent();
        }

        private void ApplicationControl_Load(object sender, System.EventArgs e)
        {
            // http://stackoverflow.com/questions/1199058/how-to-use-rsa-to-encrypt-files-huge-data-in-c-sharp
            // http://msdn.microsoft.com/en-us/library/as0w18af(v=vs.110).aspx

            // private/public algorithms are not suitable for encrypting large data. 
            // In practice they are used to exchange a private secret key between 
            // the two parties that will be used to symmetrically encrypt/decrypt the large data

            // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx
            // http://social.msdn.microsoft.com/Forums/vstudio/en-US/4282dda1-4803-435a-b63a-65e2d5ac9941/get-modulus-exponent-from-certificate-public-key?forum=netfxbcl
            // BigInteger
            // http://stackoverflow.com/questions/5113498/can-rsacryptoserviceprovider-nets-rsa-use-sha256-for-encryption-not-signing
            // http://stackoverflow.com/questions/7539984/asymetric-cryptography-example-in-c-sharp
            // http://andrewlocatelliwoodcock.com/2011/08/01/implementing-rsa-asymmetric-public-private-key-encryption-in-c-encrypting-under-the-public-key/



            // HardwareDevice = false
            // KeySize = 2048
            // KeyExchangeAlgorithm = "RSA-PKCS1-KeyEx"

            var sw = Stopwatch.StartNew();

            //Generate a public/private key pair.

            var RSA = new RSACryptoServiceProvider(
                dwKeySize: (0x100) * 8,
                parameters: new CspParameters { }
            );


            //            You can calculate the max number of bytes which can be encrypted with a particular key size with the following:
            //((KeySize - 384) / 8) + 37
            // The legal key sizes are 384 thru 16384 with a skip size of 8.

            //            Step into: Stepping over method without symbols 'System.Security.Cryptography.Utils.GetKeyPairHelper'
            //A first chance exception of type 'System.Threading.ThreadAbortException' occurred in mscorlib.dll



            //[Managed to Native Transition]	
            //mscorlib.dll!System.Security.Cryptography.Utils.GetKeyPairHelper(System.Security.Cryptography.CspAlgorithmType keyType, System.Security.Cryptography.CspParameters parameters, bool randomKeyContainer, int dwKeySize, ref System.Security.Cryptography.SafeProvHandle safeProvHandle, ref System.Security.Cryptography.SafeKeyHandle safeKeyHandle) + 0x1db bytes	
            //mscorlib.dll!System.Security.Cryptography.RSACryptoServiceProvider.GetKeyPair() + 0x5d bytes	
            //mscorlib.dll!System.Security.Cryptography.RSACryptoServiceProvider.KeySize.get() + 0xe bytes	
            //var __KeyPair = RSA.GetKeyPair();


            //RSA.DecryptValue
            var MaxData = (RSA.KeySize - 384) / 8 + 37;
            // MaxData = 245

            Console.WriteLine(new { RSA.KeySize, sw.ElapsedMilliseconds, MaxData });
            // { KeySize = 2176, ElapsedMilliseconds = 1154 }

            var bytes = Encoding.UTF8.GetBytes("hello world".PadRight(MaxData));

            // A first chance exception of type 'System.Security.Cryptography.CryptographicException' occurred in mscorlib.dll
            // how can we choose which key are we using?


            // using public key!
            var ebytes = RSA.Encrypt(
                bytes, false
            );

            Console.WriteLine(new { RSA.KeySize, sw.ElapsedMilliseconds, MaxData, ebytes.Length });
            // { KeySize = 4096, ElapsedMilliseconds = 37258, MaxData = 501, Length = 512 }

            // ebytes = {byte[256]}
            // ebytes = {byte[0x00000107]}
            // ebytes = {byte[0x00000110]}

            var xdata = RSA.Decrypt(
                ebytes, false
            );

            var xstring = Encoding.UTF8.GetString(xdata);

            //Save the public key information to an RSAParameters structure.
            RSAParameters RSAKeyInfo = RSA.ExportParameters(false);
        }
    }
}
