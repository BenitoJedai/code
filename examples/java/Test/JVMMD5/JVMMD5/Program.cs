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

namespace JVMMD5
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

            //0001 02000002 JVMMD5__i__d.jvm::JVMMD5.Program
            //script: error JSC1000: Java : Opcode not implemented: div.un at MD5.MD5Type.CalculateMD5Value
            //script: error JSC1000: Java : unable to emit stloc.1 at 'MD5.MD5Type.CalculateMD5Value'#0051: Java : Opcode not implemented: div.un at MD5.MD5Type.CalculateMD5Value


            //Implementation not found for type import :
            //type: System.UInt32
            //method: System.String ToString(System.String)
            //Did you forget to add the [Script] attribute?
            //Please double check the signature!


            ulong u = 3614090360;

            // { u = -680876936 }
            Console.WriteLine(new { u });


            var a = new MD5.MD5Type();

            //a.FingerPrint
            a.ValueAsByte = Encoding.UTF8.GetBytes("hello world");


            //var hash = Encoding.UTF8.GetBytes(e.ToLower()).ToMD5Bytes().ToHexString();
            var hash = a.FingerPrint.ToLower();

            Console.WriteLine(new { hash });

            // { hash = 5eb63bbbe01eeed093cb22bb8f5acdc3 }
            // { hash = 5eb63bbbe01eeed093cb22bb8f5acdc3 }

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
