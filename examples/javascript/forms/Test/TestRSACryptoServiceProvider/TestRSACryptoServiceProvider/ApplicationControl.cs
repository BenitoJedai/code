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
            // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx
            // http://social.msdn.microsoft.com/Forums/vstudio/en-US/4282dda1-4803-435a-b63a-65e2d5ac9941/get-modulus-exponent-from-certificate-public-key?forum=netfxbcl
            // BigInteger
            // http://stackoverflow.com/questions/5113498/can-rsacryptoserviceprovider-nets-rsa-use-sha256-for-encryption-not-signing


            // HardwareDevice = false
            // KeySize = 2048
            // KeyExchangeAlgorithm = "RSA-PKCS1-KeyEx"

            var sw = Stopwatch.StartNew();

            //Generate a public/private key pair.
            var RSA = new RSACryptoServiceProvider(
                dwKeySize: 2048,
                parameters: new CspParameters {  }
            );

            Console.WriteLine(new { sw.ElapsedMilliseconds });

            //Save the public key information to an RSAParameters structure.
            RSAParameters RSAKeyInfo = RSA.ExportParameters(false);
        }
    }
}
