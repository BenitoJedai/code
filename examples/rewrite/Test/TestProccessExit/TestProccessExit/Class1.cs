using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace TestProccessExit
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("hi");

            var p = Process.Start(
                new ProcessStartInfo("cmd", "/Q /C call pause")
                {
                    // The process tried to write to a nonexistent pipe.

                    // The Process object must have the UseShellExecute property set to false in order to redirect IO streams.

                    UseShellExecute = false,
                    //CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    //RedirectStandardError = true,
                    //RedirectStandardInput = true
                }
            );

            // WaitForInputIdle failed.  This could be because the process does not have a graphical interface.
            //p.WaitForInputIdle();

            try
            {
                foo();
            }
            finally
            {
                Console.WriteLine("will exit now..");
                Thread.Sleep(1000);
                p.Kill();
            }
        }

        private static void foo()
        {
            //The runtime has encountered a fatal error. The address of the error was at 0x983f45cc, on thread 0x858. The error code is 0x80131623. This error may be a bug in the CLR or in the unsafe or non-verifiable portions of user code. Common sources of this bug include user marshaling errors for COM-interop or PInvoke, which may corrupt the stack.
            //Environment.FailFast("foo");
            Console.WriteLine("\njust before error");
            Environment.Exit(-2);

        }
    }
}
