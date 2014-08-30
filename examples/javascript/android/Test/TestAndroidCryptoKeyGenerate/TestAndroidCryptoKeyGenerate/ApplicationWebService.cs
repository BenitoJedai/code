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
            // X:\jsc.svn\core\ScriptCoreLib.Ultra.Library\ScriptCoreLib.Ultra.Library\Ultra\WebService\InternalWebMethodInfo.cs

            //I/System.Console( 9232): #4 POST /xml/Decrypt HTTP/1.1
            //I/System.Console( 9232): enter InternalURLDecode { Value = /xml/Decrypt }

            //I/System.Console( 9232): enter InternalURLDecode { Value = %26lt%3B_02000005%26gt%3B%26lt%3B_04000004%26gt%3Bz1o%2bbB8d7y/wWXcrIqGf78g9/Boe4vZVxXWxZ96FT0kelREgmwg0LOKAapbCpaPSHSrX9/d5iUmOiiYsvxqoJO2YvjYiwns3p2NV0xa90RpLQAHzczq%2b%2b%2bts%2bSZDa2u4b/N/OH2GzYSRgNkRjoO6EbD9rW1KD6dzuAVMrfc0pIIz5peVJohpdzDgkIl/CYHXlgZSUjvDEzl1A8lhxH%2brstNyjwJVNg8iX5Vfq0OumWz4ZyPia0mWS4v4ECIN4RcZ7T1NB/K1ZH2byVC%2bfce7plZ/drDyCXbHGt2gvIVdems9ozv5Fjw7imP%2bviwYqQZcCYjvSHBlr4YLp8eSMRQ2UQ%3D%3D%26lt%3B/_04000004%26gt%3B%26lt%3B/_02000005%26gt%3B }

            //I/System.Console( 9232): enter InternalURLDecode { Value = 06000013 }
            //I/System.Console( 9232): enter InternalURLDecode { Value = Decrypt }
            //I/System.Console( 9232): enter invoke { WebMethod = { IsConstructor = false, MetadataToken = 06000013, Name = Decrypt, TypeFullName = TestAndroidCryptoKeyGenerate.ApplicationWebService, Parameters = 1 } }
            //I/System.Console( 9232): check NewGlobalInvokeMethod { Name = Encrypt }
            //I/System.Console( 9232): check NewGlobalInvokeMethod { Name = Decrypt }
            //I/System.Console( 9232): enter NewGlobalInvokeMethod { Name = Decrypt }
            //I/System.Console( 9232): GetParameterValue: { name = data, Length = 1 }
            //I/System.Console( 9232): GetParameterValue: { key = _06000013_data }
            //I/System.Console( 9232): GetParameterValue: { value = &lt;_02000005&gt;&lt;_04000004&gt;z1o%2bbB8d7y/wWXcrIqGf78g9/Boe4vZVxXWxZ96FT0kelREgmwg0LOKAapbCpaPSHSrX9/d5iUmOiiYsvxqoJO2YvjYiwns3p2NV0xa90RpLQAHzczq%2b%2b%2bts%2bSZDa2u4b/N/OH2GzYSRgNkRjoO6EbD9rW1KD6dzuAVMrfc0pIIz5peVJohpdzDgkIl/CYHXlgZSUjvDEzl1A8lhxH%2brstNyjwJVNg8iX5Vfq0OumWz4ZyPia0mWS4v4ECIN4RcZ7T1NB/K1ZH2byVC%2bfce7plZ/drDyCXbHGt2gvIVdems9ozv5Fjw7imP%2bviwYqQZcCYjvSHBlr4YLp8eSMRQ2UQ==&lt;/_04000004&gt;&lt;/_02000005&gt; }

            // http://stackoverflow.com/questions/8946307/character-is-converted-to-2b-in-http-post

            //I/System.Console( 9232): GetParameterValue: { r = <_02000005><_04000004>z1o%2bbB8d7y/wWXcrIqGf78g9/Boe4vZVxXWxZ96FT0kelREgmwg0LOKAapbCpaPSHSrX9/d5iUmOiiYsvxqoJO2YvjYiwns3p2NV0xa90RpLQAHzczq%2b%2b%2bts%2bSZDa2u4b/N/OH2GzYSRgNkRjoO6EbD9rW1KD6dzuAVMrfc0pIIz5peVJohpdzDgkIl/CYHXlgZSUjvDEzl1A8lhxH%2brstNyjwJVNg8iX5Vfq0OumWz4ZyPia0mWS4v4ECIN4RcZ7T1NB/K1ZH2byVC%2bfce7plZ/drDyCXbHGt2gvIVdems9ozv5Fjw7imP%2bviwYqQZcCYjvSHBlr4YLp8eSMRQ2UQ==</_04000004></_02000005> }
            // I/System.Console(10141): GetParameterValue: { r = <_02000005><_04000004>MOKwz/L4D+vavq3FmpvcMUNWklOCwt91Y+cL7yocaAcC+6E6QzPbvefn/h252ntNf4bGCPkRdjva/GQ/CbIPMKQWvw8HU6lMuy7hErTWkjzfiLRzh0nvy2DD8amAxnrt29nYIjFK4Q1U95wqymJiVOLDgp6XxhvtRhO/Fn2Qmi+g5F3bm7UC2RvTdsdhr0/es5rg34XMURWp5AyRT6OgC1kHa+A3o1rpjkYwPC7eG/QuJdVDMGSYvxiIwcxss0EeWmSKuHd+UzPn5e3cXfYu0MJaeZTjyP9aQx9olN9MGCQhvjFt+1+s46LnBteLlV6nb8Zr8Fyu7XlKnVuwA2blPA==</_04000004></_02000005> }

            //I/System.Console( 9232): enter Decrypt {{ Length = 268 }}
            //D/dalvikvm( 9232): GC_CONCURRENT freed 4490K, 53% free 4423K/9376K, paused 3ms+1ms, total 34ms
            //D/dalvikvm( 9232): WAIT_FOR_CONCURRENT_GC blocked 22ms


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
