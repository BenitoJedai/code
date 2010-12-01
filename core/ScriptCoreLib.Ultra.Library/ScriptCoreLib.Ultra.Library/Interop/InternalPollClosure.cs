using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptCoreLib.Interop
{
    internal class InternalPollClosure
    {
        EventWaitHandle InternalPollWait;
        EventWaitHandle InternalInvokeWait;
        int InternalInvokeWait_Counter;

        public InternalPollClosure()
        {

        }

        internal void InternalPoll()
        {

            if (InternalInvokeWait_Counter == 0)
                InternalInvokeWait_Counter = 1;
            else
                InternalInvokeWait.Set();

            // wait for it...
            InternalPollWait.WaitOne();
        }

        internal void InternalBeginAsync()
        {
            InternalPollWait = new EventWaitHandle(false, EventResetMode.AutoReset);
            InternalInvokeWait = new EventWaitHandle(false, EventResetMode.AutoReset);
            InternalInvokeWait_Counter = 0;
        }

        internal void InternalEndAsync()
        {
            InternalPollWait.Set();
            InternalPollWait = null;
            InternalInvokeWait = null;
        }

        internal void InternalInvokeCallback()
        {
            // notify wait handler
            InternalPollWait.Set();

            InternalInvokeWait.WaitOne();
        }
    }
}
