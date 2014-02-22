using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLibJava.Extensions;
using System.Xml.Linq;
using java.net;
using java.util.zip;
using System.Collections;
using System.IO;
using ScriptCoreLib.GLSL;

namespace jDOSBoxHybridExperiment
{
    class x
    {
        public string Name;
        public string Value;
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..

            // generic parameter needs to be moved..
            //enumerable_10 = __Enumerable.AsEnumerable(__SZArrayEnumerator_1<String>.Of(stringArray3));

            Console.WriteLine("hi! vm:" + typeof(object).FullName);

            System.Console.WriteLine("loading... jdos.gui.MainFrame.main");

            //public static Config control;
            Console.WriteLine(
                new
                {
                    jdos.Dosbox.control
                }
                );


            // X:\opensource\sourceforge\jdosbox\src\jdos\Dosbox.java
            // Pbool = secprop.Add_bool("ipx",Property.Changeable.WhenIdle, true);
            //jdos.Dosbox.control.GetSectionFromProperty("ipx").



            // "X:\jsc.svn\examples\java\synergy\jDOSBoxHybridExperiment\jDOSBoxHybridExperiment\bin\Release\.dosbox\dosbox-0.74.conf"
            //[ipx]
            //# ipx: Enable ipx over UDP/IP emulation.

            //ipx=false


            jdos.gui.MainFrame.main(args);

            CLRProgram.XML = new XElement("hello", "world");
            CLRProgram.CLRMain(
            );

        }


    }

    static class XX
    {
        public static IEnumerable<int> GetIndecies(this string e, string f)
        {
            var a = new List<int>();

            var p = 0;
            var i = e.IndexOf(f, p);
            while (i >= 0)
            {
                p = i + 1;

                a.Add(i);

                i = e.IndexOf(f, p);
            }

            return a;
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
             StringAction ListMethods = null
            )
        {
            System.Console.WriteLine(XML);

            MessageBox.Show("it works?!?");
        }
    }

}
