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
using System.Threading.Tasks;

namespace TestJVMCLRAsync
{

    static class Program
    {
        //- javac
        //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestJVMCLRAsync\Program.java
        //Y:\staging\web\java\TestJVMCLRAsync\Program__Invoke_d__1.java:26: error: method __forwardref in class Program__Invoke_d__1__MoveNext_0600000e cannot be applied to given types;
        //        Program__Invoke_d__1__MoveNext_0600000e.__forwardref(this);
        //                                               ^
        //  required: Program__Invoke_d__1[]
        //  found: Program__Invoke_d__1
        //  reason: actual argument Program__Invoke_d__1 cannot be converted to Program__Invoke_d__1[] by method invocation conversion

        // is it ok for js yet not for java? didnt we already fix it?

        public static async Task Invoke()
        {
            // script: error JSC1000: Java : class import: no implementation for System.Runtime.CompilerServices.YieldAwaiter at TestJVMCLRAsync.Program+<Invoke>d__1
            //await Task.Yield();

        }
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

            Invoke();


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
