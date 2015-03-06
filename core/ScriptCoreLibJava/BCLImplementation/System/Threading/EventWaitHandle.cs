using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
	// http://referencesource.microsoft.com/#mscorlib/system/threading/EventWaitHandle.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/EventWaitHandle.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Threading/EventWaitHandle.cs

	[Script(Implements = typeof(global::System.Threading.EventWaitHandle))]
    internal class __EventWaitHandle : __WaitHandle
    {
        // see: http://www.koders.com/java/fid403F7FA980A2B7384C906BF2C6C3E15FB62A1A2F.aspx?s=file:semap*.java
        // see: http://www.javaworld.com/javaworld/javaqa/1999-11/02-qa-semaphore.html
        // see: http://gee.cs.oswego.edu/dl/classes/EDU/oswego/cs/dl/util/concurrent/Semaphore.java
        // see: http://stackoverflow.com/questions/1064596/what-is-javas-equivalent-of-manualresetevent
        // see: http://stackoverflow.com/questions/1091973/javas-equivalent-to-nets-autoresetevent
        // see: http://www.javamex.com/tutorials/synchronization_wait_notify.shtml
        // see: http://forums.techguy.org/software-development/589709-java-lang-illegalmonitorstateexception-current-thread.html

        bool initialState;
        EventResetMode mode;

        readonly global::java.lang.Object Context = new global::java.lang.Object();

        // this implementation needs to be updated
        public __EventWaitHandle(bool initialState, EventResetMode mode)
        {
            this.initialState = initialState;
            this.mode = mode;
        }


        public bool Set()
        {
			// roslyn 4.6 changes it?
			// X:\jsc.svn\examples\java\hybrid\JVMCLRNIC\JVMCLRNIC\Program.cs
			// X:\jsc.svn\examples\java\test\Test46LockField\Test46LockField\Class1.cs

			lock (this.Context)
                this.Context.notify();

            return false;
        }

        public override bool WaitOne(int millisecondsTimeout)
        {
            // X:\jsc.svn\examples\javascript\android\com.abstractatech.appmanager\com.abstractatech.appmanager\ApplicationWebService.cs

            try
            {
                // As in the one argument version, interrupts and spurious wakeups 
                // are possible, and this method should always be used in a loop:
                // ... fixme: we should do a check?

                lock (this.Context)
                    this.Context.wait(millisecondsTimeout);

            }
            catch
            {
                // now what?
                throw;
            }

            //Context

            return false;
        }

        public override bool WaitOne()
        {
            // IllegalMonitorStateException is thrown when you invoke the wait(), 
            // notify() or notifyAll() method of an object without holding a 
            // lock on that object. 

            try
            {
                // As in the one argument version, interrupts and spurious wakeups 
                // are possible, and this method should always be used in a loop:
                // ... fixme: we should do a check?

                lock (this.Context)
                    this.Context.wait();

            }
            catch
            {
                // now what?
                throw;
            }

            //Context

            return false;
        }
    }
}
