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

namespace JVMCLRThreadBackgroundTask
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
                "before: " +
                new
                {
                    Thread.CurrentThread.ManagedThreadId,
                    typeof(object).AssemblyQualifiedName
                }
            );

            //System.Threading.Tasks.Task.Run(

            //before: { ManagedThreadId = 1, AssemblyQualifiedName = System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 }
            //inside StartNew: { ManagedThreadId = 3, AssemblyQualifiedName = System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 }
            //inside ContinueWith: { ManagedThreadId = 3, AssemblyQualifiedName = System.Object, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089 }

            // what about Task Run?
            var u = System.Threading.Tasks.Task.Factory.StartNew(
                delegate
                {
                    System.Console.WriteLine(
                        "inside StartNew: " +
                        new
                        {
                            Thread.CurrentThread.ManagedThreadId,
                            typeof(object).AssemblyQualifiedName
                        }
                    );
                }
            );

            // top level scheduler missing causes that new thread to continue?
            u.ContinueWith(
                t =>
                {
                    System.Console.WriteLine(
                        "inside ContinueWith: " +
                        new
                        {
                            Thread.CurrentThread.ManagedThreadId,
                            typeof(object).AssemblyQualifiedName
                        }
                    );
                }
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

            // X:\jsc.svn\examples\javascript\forms\FormsNIC\FormsNIC\ApplicationWebService.cs

            MessageBox.Show("click to close");

        }
    }


}
