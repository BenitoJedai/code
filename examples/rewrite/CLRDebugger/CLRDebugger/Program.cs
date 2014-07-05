extern alias rewrite;

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
using System.Diagnostics;
//using Microsoft.Diagnostics.Runtime;

    // jsc broke it somehow
//using rewrite::Microsoft.Diagnostics.Runtime;
using global::Microsoft.Diagnostics.Runtime;

namespace CLRDebugger
{

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


            // http://msdn.microsoft.com/en-us/library/bb161718.aspx
            // https://www.nuget.org/packages/Microsoft.Diagnostics.Runtime
            // http://blogs.msdn.com/b/dotnet/archive/2013/05/01/net-crash-dump-and-live-process-inspection.aspx?
            //Microsoft.Diagnostics.Runtime


            var pp = Process.GetProcesses();

            // Additional information: Could not attach to pid 17A0, HRESULT: 0x8000ffff
            int pid = pp.FirstOrDefault(x => x.ProcessName.Contains("jsc")).Id;
            using (DataTarget dataTarget = DataTarget.AttachToProcess(pid, 5000))
            {
                string dacLocation = dataTarget.ClrVersions[0].TryGetDacLocation();
                ClrRuntime runtime = dataTarget.CreateRuntime(dacLocation);

                // ...

                //ClrHeap heap = runtime.GetHeap();
                //foreach (ulong obj in heap.EnumerateObjects())
                //{
                //    ClrType type = heap.GetObjectType(obj);
                //    ulong size = type.GetSize(obj);
                //    Console.WriteLine("{0,12:X} {1,8:n0} {2}", obj, size, type.Name);
                //}


                //var stats = from o in heap.EnumerateObjects()
                //            let t = heap.GetObjectType(o)
                //            group o by t into g
                //            let size = g.Sum(o => (uint)g.Key.GetSize(o))
                //            orderby size
                //            select new
                //            {
                //                Name = g.Key.Name,
                //                Size = size,
                //                Count = g.Count()
                //            };

                //foreach (var item in stats)
                //    Console.WriteLine("{0,12:n0} {1,12:n0} {2}", item.Size, item.Count, item.Name);


                foreach (ClrThread thread in runtime.Threads)
                {
                    Console.WriteLine("ThreadID: {0:X}", thread.OSThreadId);
                    Console.WriteLine("Callstack:");

                    foreach (ClrStackFrame frame in thread.StackTrace)
                        Console.WriteLine("{0,12:X} {1,12:X} {2}", frame.InstructionPointer, frame.StackPointer, frame.DisplayString);

                    Console.WriteLine();
                }
            }

            MessageBox.Show("click to close");

        }
    }


}
