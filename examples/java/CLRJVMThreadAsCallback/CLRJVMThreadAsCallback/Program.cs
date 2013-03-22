using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using ScriptCoreLibJava.Extensions;
using System.Threading;

namespace CLRJVMThreadAsCallback
{


    public class Program
    {
        public static void Main(string[] args)
        {

            var done = new EventWaitHandle(false, EventResetMode.AutoReset);

            var yield = new Thread(
                new ParameterizedThreadStart(
                    data =>
                    {
                        Console.WriteLine(new { data });

                        done.Set();
                    }
                )
            );

            Console.WriteLine("before wait " + DateTime.Now);

            // Additional information: Thread has not been started.
            done.WaitOne(2100);

            Console.WriteLine("after wait " + DateTime.Now);

            yield.Start(new { foo = "bar" });

            done.WaitOne();

            Console.WriteLine("done");

            CLRProgram.CLRMain();
        }


    }

    [SwitchToCLRContext]
    static class CLRProgram
    {
        [STAThread]
        public static void CLRMain()
        {
            Console.WriteLine("running inside CLR");

            Console.WriteLine(Path.GetFileName(Assembly.GetExecutingAssembly().Location));

            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                Console.WriteLine(Path.GetFullPath(item.Location) + " types: " + item.GetTypes().Length);
            }

            //Debugger.Break();
        }
    }
}
