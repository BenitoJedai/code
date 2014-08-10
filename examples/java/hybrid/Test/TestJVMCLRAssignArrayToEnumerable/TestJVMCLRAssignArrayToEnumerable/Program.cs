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

namespace TestJVMCLRAssignArrayToEnumerable
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

            //System.Console.WriteLine(
            //   typeof(object).AssemblyQualifiedName
            //);


            //            0001 0200000d TestJVMCLRAssignArrayToEnumerable__i__d.jvm::< module >.SHA11d66b4cfe54cf0fbb67bce0aa80fe941953cb0df@1270592323$00000018$0000004c
            //              - javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" - classpath "Y:\staging\web\java"; release - d release java\TestJVMCLRAssignArrayToEnumerable\Program.java
            //   java\TestJVMCLRAssignArrayToEnumerable\Program.java:32: error:
            //            incompatible types
            //        enumerable_10 = objectArray1;
            //                        ^
            //  required: __IEnumerable_1<Object>
            //  found:    Object[]

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140810/asenumerable
            IEnumerable<object> e = new[] { new object() };
            //IEnumerable<object> e = new[] { new object() }.AsEnumerable();

            // how do the other jsc languages behave?


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
