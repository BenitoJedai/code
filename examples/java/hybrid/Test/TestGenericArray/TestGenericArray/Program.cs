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

namespace TestGenericArray
{

    static class Program
    {
        //- javac
        //"C:\Program Files (x86)\Java\jdk1.7.0_45\bin\javac.exe" -classpath "Y:\staging\web\java";release -d release java\TestGenericArray\Program.java
        //java\TestGenericArray\Program.java:47: error: generic array creation
        //        class1_13.a = new TResult[((int)(tasks.length))];
        //                      ^
        //Y:\staging\web\java\ScriptCoreLibJava\BCLImplementation\System\Threading\Tasks\__Task.java:116: error: generic array creation
        //        class1_13.a = new TResult[((int)(tasks.length))];
        //                      ^


        public static Task<TResult[]> WhenAll<TResult>(params Task<TResult>[] tasks)
        {
            // X:\jsc.svn\examples\java\Test\TestGenericParameterArray\TestGenericParameterArray\Class1.cs
            // X:\jsc.svn\examples\java\hybrid\Test\TestGenericArray\TestGenericArray\Program.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141209
            var x = new TaskCompletionSource<TResult[]>();

            var a = new TResult[tasks.Length];

            var i = tasks.Length;
            var j = 0;
            foreach (var item in tasks)
            {
                var jj = j;
                j++;

                item.ContinueWith(
                    task =>
                    {
                        i--;

                        a[jj] = task.Result;

                        if (i == 0)
                        {
                            x.SetResult(a);
                        }
                    }
                );
            }


            return x.Task;
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

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201412/20141209


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
