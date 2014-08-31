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
using System.Diagnostics;
using System.Security.Cryptography;

namespace JVMCLRRSACryptoServiceProvider
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // http://www.jensign.com/JavaScience/dotnet/RSAEncrypt/

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            // x:\jsc.svn\examples\javascript\appengine\test\testcryptokeygenerate\testcryptokeygenerate\applicationwebservice.cs




            // X:\jsc.svn\examples\javascript\forms\Test\TestRSACryptoServiceProvider\TestRSACryptoServiceProvider\ApplicationControl.cs
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140831


            var sw = Stopwatch.StartNew();

            // MaxSize = 16384
            var dwKeySize = (0x100) * 8;
            var RSA = new RSACryptoServiceProvider(
                   dwKeySize: dwKeySize,
                   parameters: new CspParameters { }
               );

            //var MaxData = (RSA.KeySize - 384) / 8 + 37;
            var MaxData = (dwKeySize - 384) / 8 + 37;

            Console.WriteLine(new { dwKeySize, sw.ElapsedMilliseconds, MaxData });

            var bytes = Encoding.UTF8.GetBytes("hello world".PadRight(MaxData));

            var ebytes = RSA.Encrypt(
               bytes, false
           );

            Console.WriteLine(new { RSA.KeySize, sw.ElapsedMilliseconds, MaxData, ebytes.Length });

            var xdata = RSA.Decrypt(
             ebytes, false
         );

            var xstring = Encoding.UTF8.GetString(xdata);

            Console.WriteLine(new { xstring });
            //{ KeySize = 2048, ElapsedMilliseconds = 1736, MaxData = 245, Length = 256 }
            //    xstring = hello world


            CLRProgram.CLRMain();
        }


    }


    public delegate XElement XElementFunc();

    [SwitchToCLRContext]
    static class CLRProgram
    {
        public static XElement XML { get; set; }

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
