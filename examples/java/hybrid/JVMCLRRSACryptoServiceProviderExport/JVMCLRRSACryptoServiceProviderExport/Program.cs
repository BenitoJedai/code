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

            //var MaxData = (RSA.KeySize - 384) / 8 + 37;
            // not correct for OLEP?!
            var MaxData = (dwKeySize - 384) / 8 + 37;

            Console.WriteLine(new { dwKeySize, sw.ElapsedMilliseconds, MaxData });

            var bytes = Encoding.UTF8.GetBytes("hello world".PadRight(MaxData));


            var p = RSA.ExportParameters(includePrivateParameters: false);

            // Modulus = {byte[256]}


            CLRProgram.CLRMain();
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain()
        {
            System.Console.WriteLine(
                typeof(object).AssemblyQualifiedName
            );



            MessageBox.Show("click to close");

        }
    }


}
