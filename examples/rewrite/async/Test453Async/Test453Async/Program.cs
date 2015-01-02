using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test453Async
{
    class Program
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

        // STAThread changes Task.Yield
        [STAThread]
        static void Main(string[] args)
        {
            Debugger.Launch();
            Debugger.Break();


            Method1();

            Thread.Yield();



        }
    }
}
