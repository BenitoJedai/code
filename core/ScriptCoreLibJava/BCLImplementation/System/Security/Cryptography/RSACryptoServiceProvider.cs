﻿using System;
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
using java.security.interfaces;
using java.math;
using java.security.spec;

namespace ScriptCoreLibJava.BCLImplementation.System.Security.Cryptography
{
    // http://referencesource.microsoft.com/#mscorlib/system/security/cryptography/rsacryptoserviceprovider.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Security/Cryptography/RSACryptoServiceProvider.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\RSACryptoServiceProvider.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Security.Cryptography/RSACryptoServiceProvider.cs
    // http://msdn.microsoft.com/en-us/library/5e9ft273(v=vs.110).aspx

    // http://msdn.microsoft.com/en-us/library/system.security.cryptography.rsacryptoserviceprovider.decrypt(v=vs.110).aspx


    //  !FEATURE_CORECLR    
    [Script(Implements = typeof(global::System.Security.Cryptography.RSACryptoServiceProvider))]
    internal class __RSACryptoServiceProvider : __RSA
    {
        // http://www.drdobbs.com/windows/programming-public-key-cryptostreams-par/184416907

        // http://lukieb.blogspot.com/2014/01/rsa-public-key-encryption-between-net.html


        public RSAPublicKey InternalRSAPublicKey;

        //  We only attempt to generate a random key on desktop runtimes because the CoreCLR
        // RSA surface area is limited to simply verifying signatures. 
        private KeyPair InternalKeyPair;


        // http://www.windows-tech.info/13/edd925e63a09c709.php

        // If anyone out there is planning on using the .Net implementation of RSA for cross application/platform development for the 
        // exchange of RSA encrypted data, I would urge you don't unless you have solid knowledge of, or the capacity (mental, time, financial) 
        // for many hours of studying information on topics including: The RSA algorithm itself, RSACryptoServiceProvider and it's limitations,
        // RSAParameter, byte arrays (manipulations and conversions), big-endian encoding, UTF-8 encoding, big integer classes, OAEP, PKCS1v15
        // padding and signature schemes, to name a few.

        // X:\jsc.svn\examples\javascript\android\Test\TestAndroidCryptoKeyGenerate\TestAndroidCryptoKeyGenerate\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\android\Test\TestAndroidRSACryptoServiceProvider\TestAndroidRSACryptoServiceProvider\ApplicationWebService.cs
        // X:\jsc.svn\examples\javascript\appengine\Test\TestCryptoKeyGenerate\TestCryptoKeyGenerate\ApplicationWebService.cs
        // http://www.jensign.com/JavaScience/dotnet/RSAEncrypt/



        //public async Task<byte[]> DecryptAsync(byte[] rgb, bool fOAEP)
        public byte[] Decrypt(byte[] rgb, bool fOAEP)
        {
            //[Obsolete("getPrivate")]

            // X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProviderExport\JVMCLRRSACryptoServiceProviderExport\Program.cs
            // X:\jsc.svn\examples\javascript\appengine\test\TestAppEngineWebCryptoKeyImport\TestAppEngineWebCryptoKeyImport\ApplicationWebService.cs
            // You don't need BC for RSA support

            // http://www.example-code.com/android/rsa_oaepPadding.asp
            //  Optimal Asymmetric Encryption Padding (OAEP) 

            // SunPKCS11 doesn't support RSA-OAEP, so the only thing you can do with SunPKCS11 is RSA-NOPADDING. Which is also listed as workaround in the link
            // SunPKCS11 provider doesn't support OAEP padding. If OAEP is crucial for you, then there is a need to make OAEP padding removal additionally after decryption.
            // SunPKCS11 provider doesn't support OAEP padding, making it more difficult. Encryption still can be done with BouncyCastle, but decryption can be done with no padding and SunPKCS11 provider. keyLength parameter is RSA key modulus length in bits (1024,2048 etc).

            // // choose between OAEP or PKCS#1 v.1.5 padding


            // For those of you who will get this problem, it was related to the fact that the Java Cryptography Extension (JCE) Unlimited Strength Jurisdiction Policy Files was not installed and it was not letting me use encryption better than AES-128. Replacing the policy files with the JCE policy files, I was able to successfully decrypt my encrypted assertion.
            // http://stackoverflow.com/questions/9422545/decrypting-encrypted-assertion-using-saml-2-0-in-java-using-opensaml


            // WebCrypte wiill need Async pattern!
            //   GetKeyPair();


            var value = default(byte[]);
            try
            {
                // https://www.dlitz.net/software/pycrypto/api/2.6/Crypto.Cipher.PKCS1_OAEP.PKCS1OAEP_Cipher-class.html
                // http://stackoverflow.com/questions/17110217/is-rsa-pkcs1-oaep-padding-supported-in-bouncycastle
                // http://www.corpsecurityservices.com/security.jsp
                // http://docs.oracle.com/javase/7/docs/technotes/guides/security/SunProviders.html
                // NOPADDING, 
                // PKCS1PADDING, 
                // OAEPWITHMD5ANDMGF1PADDING, 
                // OAEPWITHSHA1ANDMGF1PADDING, 
                // OAEPWITHSHA-1ANDMGF1PADDING, 
                // OAEPWITHSHA-256ANDMGF1PADDING, 
                // OAEPWITHSHA-384ANDMGF1PADDING, 
                // OAEPWITHSHA-512ANDMGF1PADDING
                // which is the one .net is using?
                // and web crypto?

                // https://code.google.com/p/chromium/issues/detail?id=372917


                // http://stackoverflow.com/questions/5113498/can-rsacryptoserviceprovider-nets-rsa-use-sha256-for-encryption-not-signing

                var RSACipher = InternalGetRSACipher(fOAEP);

                //Decrypt
                RSACipher.init(
                    Cipher.DECRYPT_MODE,
                    this.InternalKeyPair.getPrivate()
                );

                value = (byte[])(object)RSACipher.doFinal((sbyte[])(object)rgb);
            }
            catch
            {
                throw;
            }


            return value;

        }

        // called by? Encrypt, Decrypt
        private static Cipher InternalGetRSACipher(bool fOAEP)
        {
            var RSACipher = default(Cipher);

            try
            {
                if (fOAEP)
                {
                    // !!! JVM does not seem to know about OAEP ??

                    // iOS/WebCrypto

                    //rsaCipher = Cipher.getInstance("RSA/NONE/OAEPWITHSHA1ANDMGF1PADDING", "BC");

                    // this will likely fail?
                    // http://javadoc.iaik.tugraz.at/iaik_jce/current/iaik/pkcs/pkcs1/RSACipher.html
                    //rsaCipher = Cipher.getInstance("RSA/NONE/OAEPWITHSHA1ANDMGF1PADDING");

                    // http://www.ietf.org/mail-archive/web/jose/current/msg04138.html

                    //            Caused by: javax.crypto.BadPaddingException: lHash mismatch
                    //at sun.security.rsa.RSAPadding.unpadOAEP(Unknown Source)

                    //rsaCipher = Cipher.getInstance("RSA/ECB/OAEP");
                    //rsaCipher = Cipher.getInstance("RSA/ECB/OAEPWithSHA-256AndMGF1Padding");

                    // .net seems to be fixed to sha1?
                    RSACipher = Cipher.getInstance("RSA/ECB/OAEPWithSHA-1AndMGF1Padding");
                }
                else
                {
                    RSACipher = Cipher.getInstance("RSA");
                }
            }
            catch { throw; }

            return RSACipher;
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

        // ctor()?
        public __RSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
        {


            // what if ctor is here for import instead of gen?
            // X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProviderExport\JVMCLRRSACryptoServiceProviderExport\Program.cs

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
                Console.WriteLine("RSACryptoServiceProvider before generateKeyPair " + new { dwKeySize });

                var keyGen = KeyPairGenerator.getInstance("RSA");

                keyGen.initialize(dwKeySize);

                this.InternalKeyPair = keyGen.generateKeyPair();
                this.InternalRSAPublicKey = (RSAPublicKey)this.InternalKeyPair.getPublic();

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

            // X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProviderExport\JVMCLRRSACryptoServiceProviderExport\Program.cs
            // X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyExport\JVMCLRCryptoKeyExport\Program.cs
            // X:\jsc.svn\examples\javascript\android\Test\TestAndroidWebCryptoKeyImport\TestAndroidWebCryptoKeyImport\ApplicationWebService.cs


            // did we generate the key, so we can export it?
            try
            {
                PublicKey publicKey = this.InternalKeyPair.getPublic();

                RSAPublicKey rsapublicKey = publicKey as RSAPublicKey;


                //x.Exponent = ?

                var rsaModulusBytes = (byte[])(object)rsapublicKey.getModulus().toByteArray();
                var rsaPublicExponent = (byte[])(object)rsapublicKey.getPublicExponent().toByteArray();

                // {{ m = 257, ElapsedMilliseconds = 9381 }}

                // http://security.stackexchange.com/questions/42268/how-do-i-get-the-rsa-bit-length-with-the-pubkey-and-openssl
                //So the key has type RSA, and its modulus has length 257 bytes, except that the first byte has value "00", so the real length is 256 bytes (that first byte was added so that the value is considered positive, because the internal encoding rules call for signed integers, the first bit defining the sign). 256 bytes is 2048 bits.

                #region firstByte
                var firstByte = rsaModulusBytes[0];

                if (firstByte == 0x0)
                {
                    // CLR does not have it. so we need to remove it in JVM too.

                    //rsaModulusBytes = rsaModulusBytes.Skip(1).ToArray();

                    //- javac
                    //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRRSACryptoServiceProviderExport\Program.java
                    //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\__RSACryptoServiceProvider.java:129: error: method Of in class __SZArrayEnumerator_1<T#2> cannot be applied to given types;
                    //                byteArray2 = __60002f4_0052__generic_array_creation(__Enumerable.<Byte>ToArray(__Enumerable.<Byte>Skip(__SZArrayEnumerator_1.<Byte>Of(byteArray2), 1)));
                    //                                                                                                                                            ^
                    //  required: T#1[]
                    //  found: byte[]
                    //  reason: actual argument byte[] cannot be converted to Byte[] by method invocation conversion
                    //  where T#1,T#2 are type-variables:
                    //    T#1 extends Object declared in method <T#1>Of(T#1[])
                    //    T#2 extends Object declared in class __SZArrayEnumerator_1
                    //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Security\Cryptography\__RSACryptoServiceProvider.java:174: error: possible loss of precision
                    //            x[i] = ((Short)e[i]).shortValue();
                    //                                           ^
                    //  required: byte
                    //  found:    short

                    var old = rsaModulusBytes;
                    rsaModulusBytes = new byte[old.Length - 1];
                    Array.Copy(
                        old,
                        1,

                        rsaModulusBytes,
                        0,

                        rsaModulusBytes.Length
                    );

                }
                #endregion


                this.InternalParameters = new RSAParameters
                {
                    Exponent = rsaPublicExponent,
                    Modulus = rsaModulusBytes
                };
            }
            catch
            {
                throw;
            }


            // tested by?
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

        // used by?
        // set by
        // ExportParameters
        public RSAParameters InternalParameters;


        public __RSACryptoServiceProvider()
        {
            // X:\jsc.svn\examples\java\hybrid\crypto\JVMCLRRSADuplex\JVMCLRRSADuplex\Program.cs
            // next call should be to ImportParameters, then Encrypt
        }

        public override void ImportParameters(RSAParameters parameters)
        {
            try
            {
                // http://developer.android.com/reference/java/security/KeyFactory.html

                // X:\jsc.svn\core\ScriptCoreLibJava\java\security\interfaces\RSAPublicKey.cs
                // https://gist.github.com/manzke/1068441
                // http://stackoverflow.com/questions/11410770/java-load-rsa-public-key-from-file

                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140829
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323
                // X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyExport\TestWebCryptoKeyExport\ApplicationWebService.cs
                // tested by ?

                var rsa = KeyFactory.getInstance("RSA");


                var rsaModulusBytes = parameters.Modulus;

                #region firstByte
                var firstByte = rsaModulusBytes[0];
                if (firstByte != 0)
                {
                    // jvm likes a leading 0 ?

                    rsaModulusBytes = new byte[parameters.Modulus.Length + 1];

                    Array.Copy(
                         parameters.Modulus,
                         0,

                         rsaModulusBytes,
                         1,

                         parameters.Modulus.Length
                     );
                }
                #endregion

                var Modulus = new BigInteger((sbyte[])(object)rsaModulusBytes);
                var Exponent = new BigInteger((sbyte[])(object)parameters.Exponent);

                //Console.WriteLine("RSACryptoServiceProvider.ImportParameters " + new { m = parameters.Modulus.Length, e = parameters.Exponent.Length });

                var s = new RSAPublicKeySpec(Modulus, Exponent);

                this.InternalRSAPublicKey = (RSAPublicKey)rsa.generatePublic(s);
                this.InternalParameters = parameters;
            }
            catch
            {
                throw;
            }
        }


        //public async Task<byte[]> EncryptAsync(byte[] rgb, bool fOAEP)
        public byte[] Encrypt(byte[] rgb, bool fOAEP)
        {
            // https://www.openssl.org/docs/apps/rsautl.html

            //-pkcs, -oaep, -ssl, -raw
            //the padding to use: PKCS#1 v1.5 (the default), PKCS#1 OAEP, special padding used in SSL v2 backwards compatible handshakes, or no padding, respectively.
            // For signatures, only -pkcs and -raw can be used.


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
                var RSACipher = InternalGetRSACipher(fOAEP);

                RSACipher.init(Cipher.ENCRYPT_MODE, this.InternalRSAPublicKey);

                value = (byte[])(object)RSACipher.doFinal((sbyte[])(object)rgb);
            }
            catch
            {
                throw;
            }

            return value;
        }

    }
}
