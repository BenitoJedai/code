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

namespace JVMCLRByRefThis
{
    struct __Invoke
    {
        public int state;

        public static void __forwardref(ref __Invoke that)
        {
            that.state++;

            Console.WriteLine("exit __forwardref " + new { that.state });
        }


        public void MoveNext()
        {
            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRGenericByRefThis\TestJVMCLRGenericByRefThis\Program.cs

            //java.lang.Object, rt
            //enter MoveNext { state = 5 }
            //exit __forwardref { state = 6 }
            //exit MoveNext { state = 6 }


            Console.WriteLine("enter MoveNext " + new { state });

            var loc0 = this;
            __forwardref(ref loc0);

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRByRefThis\Program.java
            //Y:\staging\web\java\JVMCLRByRefThis\__Invoke.java:25: error: method __forwardref in class __Invoke cannot be applied to given types;
            //        __Invoke.__forwardref(this);
            //                ^

            // does JVM support it?
            // would jsc have to transform all struct methods into static byrefs?
            //__forwardref(ref this);

            Console.WriteLine("exit MoveNext " + new { state });

            //0:14ms enter MoveNext { state = 5 }
            //0:14ms exit __forwardref { state = 6 }
            //0:14ms exit MoveNext { state = 6 }

            // CLR:
            //enter MoveNext { state = 5 }
            //exit __forwardref { state = 6 }
            //exit MoveNext { state = 5 }

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
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            __Invoke loc0;

            loc0.state = 5;
            loc0.MoveNext();


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
