using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptCoreLib.Interop
{
    internal class InternalPollClosure
    {
        EventWaitHandle InternalFirstPollWait;
        EventWaitHandle InternalPollWait;
        EventWaitHandle InternalInvokeWait;
        int InternalInvokeWait_Counter;

        public InternalPollClosure()
        {

        }

        internal void InternalPoll()
        {
            //Console.WriteLine("InternalPoll");

            if (InternalPollWait == null)
                return;

            if (InternalInvokeWait == null)
                return;

            if (InternalInvokeWait_Counter == 0)
            {
                InternalInvokeWait_Counter = 1;
                InternalFirstPollWait.Set();
            }
            else
            {
                // notify wait handler
                InternalInvokeWait.Set();
            }

            // notify wait handler
            InternalPollWait.WaitOne();
        }

        internal void InternalBeginAsync()
        {
            //Console.WriteLine("InternalBeginAsync");

            InternalFirstPollWait = new EventWaitHandle(false, EventResetMode.AutoReset);
            InternalPollWait = new EventWaitHandle(false, EventResetMode.AutoReset);
            InternalInvokeWait = new EventWaitHandle(false, EventResetMode.AutoReset);
            InternalInvokeWait_Counter = 0;


        }

        internal void InternalEndAsync()
        {
            //Console.WriteLine("InternalEndAsync");

            var w = InternalPollWait;

            InternalPollWait = null;
            InternalInvokeWait = null;

            w.Set();
        }

        internal void InternalInvokeCallback()
        {
            //Console.WriteLine("InternalInvokeCallback");

            InternalPollWait.Set();
            InternalInvokeWait.WaitOne();
        }

        internal void InternalWaitForFirstPoll()
        {
            // we need to make sure the polling thread is ready..
            InternalFirstPollWait.WaitOne();
        }
        
    }


}
