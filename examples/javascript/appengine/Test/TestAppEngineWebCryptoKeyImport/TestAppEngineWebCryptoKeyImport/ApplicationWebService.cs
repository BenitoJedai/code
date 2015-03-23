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

namespace TestAppEngineWebCryptoKeyImport
{
    /// <summary>
    /// Methods defined in this type can be used from JavaScript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public class ApplicationWebService
    {
		// Unhandled Exception: System.TypeLoadException: Could not load type 'java.sql.Clob' from assembly 'ScriptCoreLibJava, Version=4.5.0.0, Culture=neutral, PublicKeyToken=null'.

		// https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/201503/20150323

		// X:\jsc.svn\examples\java\hybrid\JVMCLRCryptoKeyExport\JVMCLRCryptoKeyExport\Program.cs
		// X:\jsc.svn\examples\java\hybrid\JVMCLRRSACryptoServiceProviderExport\JVMCLRRSACryptoServiceProviderExport\Program.cs
		// X:\jsc.svn\examples\javascript\Test\TestWebCryptoKeyImport\TestWebCryptoKeyImport\ApplicationWebService.cs
		// X:\jsc.svn\examples\javascript\appengine\test\TestAppEngineWebCryptoKeyImport\TestAppEngineWebCryptoKeyImport\ApplicationWebService.cs
		// X:\jsc.svn\examples\javascript\android\test\TestAndroidWebCryptoKeyImport\TestAndroidWebCryptoKeyImport\ApplicationWebService.cs

		// shall we encrypt the public key by a shared secret too?
		[Obsolete("jsc should not try to resync fields if readonly!")]
        public byte[] e;
        public byte[] m;

        public ApplicationWebService()
        {
            //            IL_0008: ldarg.0
            //  IL_0009: ldsflda valuetype[mscorlib]System.Security.Cryptography.RSAParameters TestAppEngineWebCryptoKeyImport.ApplicationWebService::p
            //IL_000e:  ldfld uint8[] [mscorlib]
            //        System.Security.Cryptography.RSAParameters::Exponent
            //IL_0013:  stfld uint8[] TestAppEngineWebCryptoKeyImport.ApplicationWebService::e


            var pp = p;


            e = pp.Exponent;
            m = pp.Modulus;
        }

        // will this work for android now?

        static RSACryptoServiceProvider RSA;
        static RSAParameters p;

        //        Y:\TestAppEngineWebCryptoKeyImport.ApplicationWebService\staging.java\web\java\TestAppEngineWebCryptoKeyImport\ApplicationWebService.java:49: error: cannot assign a value to final variable p
        //        p = ApplicationWebService.RSA.ExportParameters(false);
        //        ^
        //Y:\TestAppEngineWebCryptoKeyImport.ApplicationWebService\staging.java\web\java\TestAppEngineWebCryptoKeyImport\Global.java:162: error: cannot assign a value to final variable e
        //            this.service.e = StringConversions.FromBase64StringOrDefault(InternalWebMethodInfo.GetParameterValue(_arg0, "field_e"));
        //                        ^
        //Y:\TestAppEngineWebCryptoKeyImport.ApplicationWebService\staging.java\web\java\TestAppEngineWebCryptoKeyImport\Global.java:163: error: cannot assign a value to final variable m
        //            this.service.m = StringConversions.FromBase64StringOrDefault(InternalWebMethodInfo.GetParameterValue(_arg0, "field_m"));
        //                        ^

        //        found 1222 types to be compiled
        //script: error JSC1000: Java : Opcode not implemented: ldsflda at TestAppEngineWebCryptoKeyImport.ApplicationWebService..ctor
        //script: error JSC1000: Java : unable to emit ldfld at 'TestAppEngineWebCryptoKeyImport.ApplicationWebService..ctor'#0006: Java : Opcode not implemented: ldsflda at TestAppEngineWebCryptoKeyImport.ApplicationWebService..ctor



        static ApplicationWebService()
        {
            var sw = Stopwatch.StartNew();

            Console.WriteLine("enter ApplicationWebService cctor");
            var dwKeySize = (0x100) * 8;
            RSA = new RSACryptoServiceProvider(
                  dwKeySize: dwKeySize,
                  parameters: new CspParameters { }
              );

            var MaxData = (dwKeySize - 384) / 8 + 7;
            p = RSA.ExportParameters(includePrivateParameters: false);

            Console.WriteLine("enter exit ApplicationWebService cctor " + new { sw.ElapsedMilliseconds });
        }

        // ebytes = {byte[256]}
        public async Task UploadEncryptedString(byte[] ebytes)
        {
            var sw = Stopwatch.StartNew();

            Console.WriteLine("enter UploadEncryptedString " + new { sw.ElapsedMilliseconds });
            var xdata = RSA.Decrypt(
                 //ebytes, fOAEP: false
                 ebytes, fOAEP: true
                );

            // xstring = "hello from client"

            var xstring = Encoding.UTF8.GetString(xdata);

            Console.WriteLine(new { xstring });
            // {{ xstring = hello from client }}

            Console.WriteLine("exit UploadEncryptedString " + new { sw.ElapsedMilliseconds });
        }

    }
}
