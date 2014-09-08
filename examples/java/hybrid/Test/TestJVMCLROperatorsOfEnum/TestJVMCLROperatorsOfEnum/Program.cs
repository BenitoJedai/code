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

namespace TestJVMCLROperatorsOfEnum
{
    enum xKey { }
    enum zKey { }

    class foo(private string tag = "hello")
    {
        //public foo()
        //{
        //    Console.WriteLine(new { tag });
        //}

        //- javac
        //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestJVMCLROperatorsOfEnum\Program.java
        //Y:\staging\web\java\TestJVMCLROperatorsOfEnum\foo.java:26: error: method Toint(foo) is already defined in class foo
        //    public static int Toint(foo that)
        //                       ^

        public static implicit operator xKey(foo that)
        {
            //that.tag
            return default(xKey);
        }

        public static implicit operator zKey(foo that)
        {
            return default(zKey);
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201409/20140908

            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            var f = new foo();
            xKey x = f;
            zKey z = f;


            CLRProgram.CLRMain();
        }


    }



    [SwitchToCLRContext]
    static class CLRProgram
    {
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
