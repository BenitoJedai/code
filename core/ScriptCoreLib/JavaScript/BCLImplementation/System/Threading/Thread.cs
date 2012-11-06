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
        public static Thread InternalCurrentThread;

        public static Thread CurrentThread
        {
            get
            {
                // Web Worker?

                if (InternalCurrentThread == null)
                    InternalCurrentThread = (Thread)(object)new __Thread(null) { ManagedThreadId = 1 };

                return InternalCurrentThread;
            }
        }

        public int ManagedThreadId { get; set; }

        public ThreadStart InternalMethod;

        public __Thread(ThreadStart t)
        {
            InternalMethod = t;
        }

        public void Start()
        {
            // WebWorker?
            // did jsc rewriter detect the threadstart correctly?

            InternalMethod();
        }
    }


}
