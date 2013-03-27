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

namespace FileGeneratorExperimentForKen
{

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            System.Console.WriteLine("!! Visual Studio may redirect to Output Window !!");

            System.Console.WriteLine("running in jvm");
            System.Console.Out.Flush();

            //java.lang.JavaSystem.@out.println("java.lang.System.println('running in jvm');");


            // whatif .NET is not available?
            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
                yield:
                    clr_to_jvm =>
                    {
                        System.Console.WriteLine(new { clr_to_jvm });
                    }
            );

            //X:\jsc.svn\examples\java\FileGeneratorExperimentForKen\FileGeneratorExperimentForKen\bin\Debug>FileGeneratorExperimentForKen.exe
            //running in jvm
            //<hello>world</hello>
            //{ clr_to_jvm = hi from CLR }

            //X:\jsc.svn\examples\java\FileGeneratorExperimentForKen\FileGeneratorExperimentForKen\bin\Debug>

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
        public static void CLRMain(
             StringAction yield = null
            )
        {
            System.Console.WriteLine(XML);

            MessageBox.Show("CLR breakpoint");

            yield("hi from CLR");
        }
    }


}
