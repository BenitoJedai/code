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

namespace TestAsyncYield
{

    static class Program
    {
        // X:\jsc.svn\examples\javascript\async\Test\TestAsyncMouseOver\TestAsyncMouseOver\Application.cs
        // X:\jsc.svn\examples\rewrite\async\Test453Async\Test453Async\Program.cs

        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2015/20150101/async

        // Show Details	Severity	Code	Description	Project	File	Line
        //Error CS0246  The type or namespace name 'async' could not be found(are you missing a using directive or an assembly reference?)	Test453Async Program.cs  13

        // http://developer.telerik.com/featured/whats-new-c-6-0-inside-visual-studio-2015-preview/
        public static async void Method1()
        {
            //Console.WriteLine("enter \{nameof(Method1)}");
            Console.WriteLine("enter {nameof(Method1)}");

            await Task.Yield();

            //Console.WriteLine("exit \{nameof(Method1)}");
            Console.WriteLine("exit {nameof(Method1)}");
        }


//        0001 02000002 TestAsyncYield__i__d.jvm::TestAsyncYield.Program
//0001 02000003 TestAsyncYield__i__d.jvm::TestAsyncYield.Program+<Method1>d__1
//System.NotImplementedException: {
//            ParameterType = TestAsyncYield.Program +< Method1 > d__1 &,


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // jsc needs to see args to make Main into main for javac..

            //java.lang.Object, rt
            //enter { nameof(Method1)}
            //exit { nameof(Method1)}

            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            Method1();


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
