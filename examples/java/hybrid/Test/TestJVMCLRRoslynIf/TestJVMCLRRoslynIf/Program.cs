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

namespace TestJVMCLRRoslynIf
{

    static class Program
    {

        public static object Invoke(Iobject a)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140813

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestJVMCLRRoslynIf\Program.java
            //java\TestJVMCLRRoslynIf\Program.java:41: error: bad operand types for binary operator '>'
            //        if (!(iobject0 > null))
            //                       ^
            //  first type:  Program_Iobject
            //  second type: <null>

            var e = a;

            if (e != null)
                return null;

            if (e == null)
                return null;

            return null;
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

            Invoke(null);


            CLRProgram.CLRMain();
        }

        public interface Iobject
        {
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
