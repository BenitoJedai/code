using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using System.Threading;
using System.Diagnostics;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/thread.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/Thread.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Threading/Thread.cs

    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Thread.cs
    // X:\jsc.svn\core\ScriptCoreLibNative\ScriptCoreLibNative\BCLImplementation\System\Threading\Thread.cs

    [Script(Implements = typeof(global::System.Threading.Thread))]
    internal class __Thread
    {
        // can we make threads from service workers?

        // "C:\Program Files (x86)\SketchUp\SketchUp 2014\Tools\RubyStdLib\thread.rb"
        // X:\jsc.svn\examples\c\Test\TestThreadStart\TestThreadStart\BCLImplementation.cs
        // tested by x:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs

        static __Thread InternalCurrentThreadValue;

        public static __Thread InternalCurrentThread
        {
            get
            {
                // Web Worker needs to be told which it is

                if (InternalCurrentThreadValue == null)
                {
                    InternalCurrentThreadValue = new __Thread(default(ThreadStart));


                    if (Native.window != null)
                        InternalCurrentThreadValue.ManagedThreadId = 1;


                }

                return InternalCurrentThreadValue;
            }
        }

        public static Thread CurrentThread
        {
            get
            {
                return (Thread)(object)InternalCurrentThread;
            }
        }

        public static void Sleep(int ms)
        {
            // unless jsc now has learned how to do a global async?
            // fake sleep. keep cpu busy!

            Console.WriteLine("sleep " + new { ms, Thread.CurrentThread.ManagedThreadId });

            var e = new Stopwatch();
            e.Start();

            if (ms > 0)
                while (e.ElapsedMilliseconds < ms)
                {
                    Thread.Yield();
                }

            Console.WriteLine("end of sleep " + new { ms, e.ElapsedMilliseconds, Thread.CurrentThread.ManagedThreadId });
        }


        public static bool Yield()
        {
            // how do we yield? :P

            return false;
        }

        public int ManagedThreadId { get; set; }

        public bool IsAlive { get; set; }

        public bool IsBackground { get; set; }

        #region ThreadStart
        public ThreadStart InternalMethod;

        public __Thread(ThreadStart t)
        {
            InternalMethod = t;
        }

        public void Start()
        {
            // WebWorker?
            // did jsc rewriter detect the threadstart correctly?
            IsAlive = true;
            InternalMethod();
            IsAlive = false;

        }
        #endregion

        #region ParameterizedThreadStart
        public ParameterizedThreadStart InternalParameterizedThreadStart;


        // tested by x:\jsc.svn\examples\javascript\Test\TestThreadStartAsWebWorker\TestThreadStartAsWebWorker\Application.cs
        public __Thread(ParameterizedThreadStart t)
        {
            // it seems we should support scope sharing
            // yet we implemented it in Task.Run instead.
            // do we need to move it around?
            // check with java and actionscript

            InternalParameterizedThreadStart = t;
        }

        public void Start(object e)
        {
            // what about serviceworker?

            // WebWorker?
            // did jsc rewriter detect the threadstart correctly?

            IsAlive = true;
            InternalParameterizedThreadStart(e);
            IsAlive = false;
        }
        #endregion
    }


}
