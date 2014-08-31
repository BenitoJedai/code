using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Security.Cryptography;
using ScriptCoreLib.Shared.BCLImplementation.System.Security.Cryptography;
using System.Threading.Tasks;
using System.Diagnostics;
using java.security;
using javax.crypto;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/rsacryptoserviceprovider.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSACryptoServiceProvider.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Security.Cryptography/RSACryptoServiceProvider.cs
    // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx



    //  !FEATURE_CORECLR    
    [Script(Implements = typeof(global::System.Security.Cryptography.RSACryptoServiceProvider))]
    internal class __RSACryptoServiceProvider : __RSA
    {
        private KeyPair InternalKeyPair;


        // http://www.windows-tech.info/13/edd925e63a09c709.php

        // If anyone out there is planning on using the .Net implementation of RSA for cross application/platform development for the 
        // exchange of RSA encrypted data, I would urge you don't unless you have solid knowledge of, or the capacity (mental, time, financial) 
        // for many hours of studying information on topics including: The RSA algorithm itself, RSACryptoServiceProvider and it's limitations,
        // RSAParameter, byte arrays (manipulations and conversions), big-endian encoding, UTF-8 encoding, big integer classes, OAEP, PKCS1v15
        // padding and signature schemes, to name a few.


        // X:\jsc.svn\examples\javascript\appengine\Test\TestCryptoKeyGenerate\TestCryptoKeyGenerate\ApplicationWebService.cs
        // http://www.jensign.com/JavaScience/dotnet/RSAEncrypt/

        //public async Task<byte[]> EncryptAsync(byte[] rgb, bool fOAEP)
        public byte[] Encrypt(byte[] rgb, bool fOAEP)
        {
            ///     This method can only encrypt (keySize - 88 bits) of data, so should not be used for encrypting
            ///     arbitrary byte arrays. Instead, encrypt a symmetric key with this method, and use the symmetric
            ///     key to encrypt the sensitive data.

            // fOAEP ?
            // true to use OAEP padding (PKCS #1 v2), false to use PKCS #1 type 2 padding

            // WebCrypte wiill need Async pattern!
            //   GetKeyPair();

            var value = default(byte[]);
            try
            {
                var rsaCipher = Cipher.getInstance("RSA");

                rsaCipher.init(Cipher.ENCRYPT_MODE, this.InternalKeyPair.getPublic());
                value = (byte[])(object)rsaCipher.doFinal((sbyte[])(object)rgb);
            }
            catch
            {
                throw;
            }

            return value;
        }

        //public async Task<byte[]> DecryptAsync(byte[] rgb, bool fOAEP)
        public byte[] Decrypt(byte[] rgb, bool fOAEP)
        {
            // WebCrypte wiill need Async pattern!
            //   GetKeyPair();


            var value = default(byte[]);
            try
            {

                var rsaCipher = Cipher.getInstance("RSA");


                //Decrypt
                rsaCipher.init(Cipher.DECRYPT_MODE, this.InternalKeyPair.getPrivate());
                value = (byte[])(object)rsaCipher.doFinal((sbyte[])(object)rgb);
            }
            catch
            {
                throw;
            }


            return value;

        }


        public override int KeySize
        {
            get
            {
                //   GetKeyPair();
                // what if it was imported key?

                return dwKeySize;
            }
        }


        int dwKeySize;
        CspParameters parameters;


        public __RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
        {
            // If this is not a random container we generate, create it eagerly 
            // in the constructor so we can report any errors now.

            //  Environment.GetCompatibilityFlag(CompatibilityFlag.EagerlyGenerateRandomAsymmKeys)
            //    GetKeyPair();


            // We only attempt to generate a random key on desktop runtimes because the CoreCLR
            // RSA surface area is limited to simply verifying signatures.  Since generating a
            // random key to verify signatures will always lead to failure (unless we happend to
            // win the lottery and randomly generate the signing key ...), there is no need
            // to add this functionality to CoreCLR at this point.

            // ? what

            this.dwKeySize = dwKeySize;
            this.parameters = parameters;

            // when would we want to delay key gen?
            // lets gen it early.

            // X:\jsc.svn\examples\javascript\appengine\Test\TestCryptoKeyGenerate\TestCryptoKeyGenerate\ApplicationWebService.cs

            try
            {
                // it works.
                // can we now wrap rsa for all platforms
                // and use it as a generic nuget?

                var sw = Stopwatch.StartNew();
                Console.WriteLine("RSACryptoServiceProvider before generateKeyPair " + new { sw.ElapsedMilliseconds });

                var keyGen = KeyPairGenerator.getInstance("RSA");

                keyGen.initialize(2048);

                this.InternalKeyPair = keyGen.generateKeyPair();
                Console.WriteLine("RSACryptoServiceProvider after generateKeyPair " + new { sw.ElapsedMilliseconds });

                //before generateKeyPair { { ElapsedMilliseconds = 2 } }
                //after generateKeyPair { { ElapsedMilliseconds = 1130 } }

            }
            catch
            {
                throw;
            }
        }



        // x:\jsc.svn\examples\javascript\forms\test\testrsacryptoserviceprovider\testrsacryptoserviceprovider\applicationcontrol.cs

        // we should defenetly be doing something more around here...


        public override RSAParameters ExportParameters(bool includePrivateParameters)
        {
            // used by?

            if (includePrivateParameters)
                return (RSAParameters)(object)new __RSAParameters
                {
                    D = InternalParameters.D,
                    DP = InternalParameters.DP,
                    DQ = InternalParameters.DQ,
                    Exponent = InternalParameters.Exponent,
                    InverseQ = InternalParameters.InverseQ,
                    Modulus = InternalParameters.Modulus,
                    P = InternalParameters.P,
                    Q = InternalParameters.Q,
                };

            return (RSAParameters)(object)new __RSAParameters
            {
                Exponent = InternalParameters.Exponent,
                Modulus = InternalParameters.Modulus,
            };
        }

        RSAParameters InternalParameters;

        public override void ImportParameters(RSAParameters parameters)
        {
            // tested by ?

            InternalParameters = parameters;
        }
    }
}
