using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLibNative.SystemHeaders;
using System.Threading;

namespace ScriptCoreLibNative.BCLImplementation.System.Threading
{
    [Script(Implements = typeof(global::System.Threading.Thread))]
    internal class __Thread
    {
        ThreadStart __ThreadStart;
        ParameterizedThreadStart __ParameterizedThreadStart;


        public __Thread(ParameterizedThreadStart start)
        {
            // X:\jsc.svn\examples\c\Test\TestThreadStart\TestThreadStart\Program.cs
            __ParameterizedThreadStart = start;
        }


        public void Start(object parameter)
        {
            Console.WriteLine("__Thread ParameterizedThreadStart");

            ///*newobj*/
            //malloc(4)[0] = parameter;
            //objectArray0 = ((void**)/*newobj*/ malloc(4));

            //var arglist = new object[1];

            //arglist[0] = parameter;



            //var arglist = new object[] { parameter };

            //process_h._beginthread(__ParameterizedThreadStart._method, stack_size: 0, arglist: parameter);
        }


        public __Thread(ThreadStart e)
        {
            Console.WriteLine("__Thread ");
            __ThreadStart = e;
        }



        public void Start()
        {
            Console.WriteLine("__Thread Start");

            //process_h._beginthread(__ThreadStart._method, stack_size: 0, arglist: null);
        }

        public static void Sleep(int p)
        {
            windows_h.Sleep(p);
        }
    }
}
