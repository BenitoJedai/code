using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;
using ScriptCoreLib.Delegates;
using System.Runtime.InteropServices;

namespace RewriteToJavaConsoleApplicationWithThreads
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            X00.InternalMain();
        }
    }

    class X00
    {
        public static void InternalMain()
        {
            Console.WriteLine("begin Generate");

            TupleAction AtEvent1 =
                n =>
                {
                    Console.WriteLine("event: " + n.Data1);
                    Console.WriteLine("3");
                    Thread.Sleep(500);
                    Console.WriteLine("2");
                    Thread.Sleep(500);
                    Console.WriteLine("1");
                    Thread.Sleep(500);
                };

            X01.Invoke(AtEvent1);

            Console.WriteLine("end Generate");
        }


    }
    class X01
    {

        public static void Invoke(TupleAction AtEvent1)
        {
            // this is our call site context..
            var AtEvent_Context = Marshal.AllocHGlobal(1);
            var t = X02.__Invoke(AtEvent_Context);

            ExtensionsToSwitchToCLRContext.AtEvent1_BeginAsync(AtEvent_Context);

            t.Start();

            X03.PollLoop(AtEvent1, AtEvent_Context, t);

            Marshal.FreeHGlobal(AtEvent_Context);
            t.Join();
        }

    }

    class X02
    {
        public static Thread __Invoke(IntPtr AtEvent_Context)
        {
            var t = new Thread(
                delegate()
                {
                    ExtensionsToSwitchToCLRContext.Generate(AtEvent_Context);
                    ExtensionsToSwitchToCLRContext.AtEvent1_EndAsync(AtEvent_Context);
                }
            );
            return t;
        }
    }

    class X03
    {
        public static void PollLoop(TupleAction AtEvent1, IntPtr AtEvent_Context, Thread t)
        {
            while (t.IsAlive)
            {
                var n = X04.__Poll(AtEvent_Context);

                if (n != null)
                {
                    AtEvent1(n);
                }
                else
                {
                    Console.WriteLine("no event");
                }
            }
        }
    }

    class X04
    {
        public static Tuple __Poll(IntPtr AtEvent_Context)
        {
            var n = ExtensionsToSwitchToCLRContext.AtEvent1_Poll(AtEvent_Context);
            return n;
        }
    }

    class Tuple
    {
        public string Data1;
    }

    delegate void TupleAction(Tuple e);

    [SwitchToCLRContext]
    static class ExtensionsToSwitchToCLRContext
    {

        static void AtEvent1_Invoke(IntPtr AtEvent1_Context, Tuple e)
        {
            AtEvent1_Data = e;
            // notify wait handler
            AtEvent1_PollWait.Set();

            AtEvent1_InvokeWait.WaitOne();
        }


        static Tuple AtEvent1_Data;
        static EventWaitHandle AtEvent1_PollWait;
        static EventWaitHandle AtEvent1_InvokeWait;
        static int AtEvent1_InvokeWait_Counter;

        public static Tuple AtEvent1_Poll(
            IntPtr AtEvent1_Context
            )
        {

            if (AtEvent1_InvokeWait_Counter == 0)
                AtEvent1_InvokeWait_Counter = 1;
            else
                AtEvent1_InvokeWait.Set();

            // wait for it...
            AtEvent1_PollWait.WaitOne();


            var r = AtEvent1_Data;

            AtEvent1_Data = null;

            
            return r;
        }

        public static void AtEvent1_BeginAsync(
            IntPtr AtEvent1_Context
            )
        {
            // create wait handler
            AtEvent1_PollWait = new EventWaitHandle(false, EventResetMode.AutoReset);
            AtEvent1_InvokeWait = new EventWaitHandle(false, EventResetMode.AutoReset);
            AtEvent1_InvokeWait_Counter = 0;
        }

        public static void AtEvent1_EndAsync(
            IntPtr AtEvent1_Context
            )
        {
            AtEvent1_Data = null;
            AtEvent1_PollWait.Set();
            AtEvent1_PollWait = null;
            AtEvent1_InvokeWait = null;
            // release wait handler
        }

        public static void Generate(
#if FEATURE_DELEGATE
            TupleAction AtEvent1
#else
            IntPtr AtEvent_Context
            // Event 2
            // Class for Events
#endif
        )
        {
            StringAction Notify =
                text =>
                {
                    Console.WriteLine(text);

                    var a = new Tuple { Data1 = text };

#if FEATURE_DELEGATES
                    AtEvent1(a);
#else
                    AtEvent1_Invoke(AtEvent_Context, a);
#endif
                };

            Notify("  beginning");

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine();

            Notify("  middle");

            for (int i = 0; i < 8; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine();

            Notify("  end");

        }
    }
}
