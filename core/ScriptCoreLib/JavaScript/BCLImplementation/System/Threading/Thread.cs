using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.CompilerServices;
using System.Threading;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Threading.Thread))]
    internal class __Thread
    {
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

        public int ManagedThreadId { get; set; }

        public bool IsAlive { get; set; }

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

        public __Thread(ParameterizedThreadStart t)
        {
            InternalParameterizedThreadStart = t;
        }

        public void Start(object e)
        {
            // WebWorker?
            // did jsc rewriter detect the threadstart correctly?

            IsAlive = true;
            InternalParameterizedThreadStart(e);
            IsAlive = false;
        }
        #endregion
    }


}
