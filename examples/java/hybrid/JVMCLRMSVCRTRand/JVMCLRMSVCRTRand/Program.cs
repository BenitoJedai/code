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
using System.Runtime.InteropServices;

namespace JVMCLRMSVCRTRand
{

    static class Program
    {
        // X:\jsc.svn\examples\java\hybrid\JVMCLRLoadLibrary\JVMCLRLoadLibrary\Program.cs


        // http://www.pinvoke.net/default.aspx/msvcrt.rand
        [DllImport("msvcrt")]

        // int rand( void );
        static extern int rand();

//        enter CFunc { lib = msvcrt, fname = rand }
//        enter CFunc InternalTryLoadLibrary { lib = msvcrt, fname = rand
//    }
//{{ x = 41 }}
//enter CFunc
//{
//    lib = X:\jsc.svn\examples\java\hybrid\JVMCLRMSVCRTRand\JVMCLRMSVCRTRand\bin\Release\JVMCLRMSVCRTRand.exports, fname = export://0600002b }
//enter CFunc InternalTryLoadLibrary
//    {
//        lib = X:\jsc.svn\examples\java\hybrid\JVMCLRMSVCRTRand\JVMCLRMSVCRTRand\bin\Release\JVMCLRMSVCRTRand.exports, fname = export://0600002b }


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

            var x = rand();
            // x = 41
            Console.WriteLine(

                new { x }

                );


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
