using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Threading;

namespace ScriptCoreLibJava.BCLImplementation.System.Threading
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/ThreadPool.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/ThreadPool.cs



    /*
     * Below you'll notice two sets of APIs that are separated by the
     * use of 'Unsafe' in their names.  The unsafe versions are called
     * that because they do not propagate the calling stack onto the
     * worker thread.  This allows code to lose the calling stack and 
     * thereby elevate its security privileges.  Note that this operation
     * is much akin to the combined ability to control security policy
     * and control security evidence.  With these privileges, a person 
     * can gain the right to load assemblies that are fully trusted which
     * then assert full trust and can call any code they want regardless
     * of the previous stack information.
     */

    [Script(Implements = typeof(global::System.Threading.ThreadPool))]
    internal class __ThreadPool
    {
		// https://github.com/dotnet/coreclr/blob/master/src/vm/delegateinfo.h

		//        0001 0200002b ScriptCoreLibJava::ScriptCoreLibJava.BCLImplementation.System.Threading.__RegisteredWaitHandle
		//script: error JSC1000: Java : class import: no implementation for System.Threading.WaitOrTimerCallback at ScriptCoreLibJava.BCLImplementation.System.Threading.__ThreadPool

		// http://stackoverflow.com/questions/11381771/thread-sleep-vs-monitor-wait-vs-registeredwaithandle
		// http://www.java2s.com/Tutorial/CSharp/0420__Thread/QueuingataskforexecutionbyThreadPoolthreadswiththeRegisterWaitForSingleObjectmethod.htm

		// X:\jsc.svn\examples\c\Test\TestFunc\TestFunc\Program.cs
		// X:\jsc.svn\examples\java\hybrid\Test\JVMCLRThreadPool\JVMCLRThreadPool\Program.cs
		// can we do audio data processing in AIR ?
		// X:\jsc.svn\market\Abstractatech.ActionScript.Audio\Abstractatech.ActionScript.Audio\Application.cs

		// 
		public static bool QueueUserWorkItem(WaitCallback callBack)
        {

            return false;
        }

        public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
        {
            return (RegisteredWaitHandle)(object)new __RegisteredWaitHandle();
        }
    }
}
