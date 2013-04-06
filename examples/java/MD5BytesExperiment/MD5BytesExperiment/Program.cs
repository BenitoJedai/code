using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.io;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;
using System.Text;
using ScriptCoreLib.Ultra.Library.Extensions;

namespace MD5BytesExperiment
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {



            System.Console.WriteLine("jvm");

            var e = "foo";

            var hash = Encoding.UTF8.GetBytes(e.ToLower()).ToMD5Bytes().ToHexString();

            //X:\jsc.svn\examples\java\MD5BytesExperiment\MD5BytesExperiment\bin\Release>MD5BytesExperiment.exe
            //jvm
            //acbd18db4cc2f85cedef654fccc4a4d8


            CLRProgram.CLRMain(hash);


        }


    }


    [SwitchToCLRContext]
    static class CLRProgram
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void CLRMain(
          string hash
            )
        {
            System.Console.WriteLine(hash);

        }
    }


}
