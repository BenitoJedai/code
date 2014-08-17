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

namespace TestJVMCLRGenericByRefThis
{

    static class Program
    {
        // X:\jsc.svn\examples\javascript\Test\TestGenericByRefThis\TestGenericByRefThis\ApplicationControl.cs

        //- javac
        //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestJVMCLRGenericByRefThis\Program.java
        //Y:\staging\web\java\TestJVMCLRGenericByRefThis\Program_Foo_1.java:22: error: method Method2 in class Program_Foo_1<G> cannot be applied to given types;
        //        this.Method2(this);
        //            ^
        //  required: Program_Foo_1<G>[]
        //  found: Program_Foo_1<G>
        //  reason: actual argument Program_Foo_1<G> cannot be converted to Program_Foo_1<G>[] by method invocation conversion
        //  where G is a type-variable:
        //    G extends Object declared in class Program_Foo_1

        struct Foo<G>
        {
            // X:\jsc.svn\examples\java\Test\JVMCLRByRefThis\JVMCLRByRefThis\Program.cs
            // X:\jsc.svn\examples\java\Test\TestGenericByRefThis\TestGenericByRefThis\Class1.cs

            public string Text;

            public void Method1()
            {
                // Error	1	Cannot pass 'this' as a ref or out argument because it is read-only	X:\jsc.svn\examples\javascript\Test\TestGenericByRefThis\TestGenericByRefThis\ApplicationControl.cs	23	29	TestGenericByRefThis
                Method2(ref this);
            }


            void Method2(ref Foo<G> f)
            {
                //Text = "Method2";

                f.Text = "Method2";
            }
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

            var s = default(Foo<string>);

            s.Method1();

            Console.WriteLine(
                new { s.Text }.ToString()
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


            MessageBox.Show("click to close");

        }
    }


}
