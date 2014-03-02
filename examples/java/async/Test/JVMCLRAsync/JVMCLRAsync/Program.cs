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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace JVMCLRAsync
{
    public class Class1
    {
        public Class1()
        {
        }
    }

    public class Class1<T>
    {
    }


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Runtime\CompilerServices\AsyncVoidMethodBuilder.cs
            // jsc needs to see args to make Main into main for javac..


            // see also>
            // X:\jsc.svn\examples\javascript\android\AndroidBroadcastLogger\AndroidBroadcastLogger\ApplicationWebService.cs

            System.Console.WriteLine(
               typeof(object).AssemblyQualifiedName
            );

            //- javac
            //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\JVMCLRAsync\Program.java
            //java\JVMCLRAsync\Program.java:33: error: generic array creation
            //        class1_1Array0 = new JVMCLRAsync.Class1_1<Class1>[1];
            //                         ^

            var a = new Class1<Class1>[1];
            //var a = (Class1<Class1>[])Array.CreateInstance(typeof(Class1<Class1>), 1);



            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201402/20140216
            // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToAssembly\RewriteToAssembly.WriteSwitchRewrite.cs
            // X:\jsc.svn\examples\java\Test\TestByRefAwaitUnsafeOnCompleted\TestByRefAwaitUnsafeOnCompleted\Class1.cs

            // jsc java does not understand our async/switch rewriter?



            // X:\jsc.svn\examples\java\Test\TestNestedTypeImport\TestNestedTypeImport\Class1.cs
            Action goo = async delegate
            {



                Console.WriteLine("hi from goo");
            };
            goo();



            Func<Task<string>> foo = async delegate
            {
                return "hi from foo";
            };

            Console.WriteLine(
                new { foo().Result }
                );

            Func<Task<string>> foo2 = async delegate
            {
                Console.WriteLine("enter foo2");
                var x = await foo();

                Console.WriteLine("exit foo2");
                return x;

            };



            Console.WriteLine(
                new { foo2().Result }
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
