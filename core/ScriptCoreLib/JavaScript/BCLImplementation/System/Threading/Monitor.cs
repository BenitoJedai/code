using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/threading/monitor.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System.Threading/Monitor.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Threading/Monitor.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Threading\Monitor.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Threading\Monitor.cs


    [Script(Implements = typeof(global::System.Threading.Monitor))]
    internal class __Monitor
    {
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140108-lock
        // X:\jsc.svn\examples\javascript\Test\TestLock\TestLock\Class1.cs

        // X:\jsc.svn\examples\javascript\android\AndroidRTC\AndroidRTC\ApplicationWebService.cs
        // "X:\jsc.svn\examples\java\LockKeyword\LockKeyword.sln"

        // how could one sync across multiple devices in async flow?

        public static void Enter(object obj, ref bool lockTaken)
        {
        
            // since we sync the console and static strings with background threads.
            // how about providing support for lock objects too?
        }

        public static void Enter(object e)
        {
        }

        public static void Exit(object obj)
        {
        }
    }
}
