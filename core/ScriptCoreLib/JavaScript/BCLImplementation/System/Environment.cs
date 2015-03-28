using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
	// http://referencesource.microsoft.com/#mscorlib/system/environment.cs
	// https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Environment.cs
	// https://github.com/Microsoft/referencesource/blob/master/mscorlib/system/environment.cs
	// https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Environment.cs

	// https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Environment.cs
	// https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Environment.cs
	// https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Environment.cs

	// X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Environment.cs
	// X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Environment.cs

	// System.Environment
	[Script(Implements = typeof(global::System.Environment))]
    internal static class __Environment
    {

        // by
        // X:\jsc.svn\examples\javascript\forms\AsyncTaskYieldViaProgress\AsyncTaskYieldViaProgress\ApplicationControl.cs
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRYieldStatement\TestJVMCLRYieldStatement\Program.cs

        public static int CurrentManagedThreadId
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId;
            }
        }

        public static int ProcessorCount
        {
            // PLinq?
            get
            {
                // can workers access it?


                // tested by
                // X:\jsc.svn\examples\javascript\async\Test\TestNavigatorCores\TestNavigatorCores\Application.cs
                // X:\jsc.svn\examples\javascript\WorkerMD5Experiment\WorkerMD5Experiment\Application.cs

                // whats the average/distribution histogram?
                return Native.window.navigator.hardwareConcurrency;
            }
        }

        public static string NewLine
        {
            get
            {
                return "\r\n";
            }
        }

        public static string StackTrace
        {
            get
            {
                // X:\jsc.svn\examples\javascript\test\TestChromeStackFrames\TestChromeStackFrames\Application.cs
                // X:\jsc.svn\examples\javascript\CodeTraceExperiment\CodeTraceExperiment\Application.cs
                // X:\jsc.svn\examples\javascript\Test\TestDelegateInvokeDisplayName\TestDelegateInvokeDisplayName\Application.cs

                // can we provide some good caller intel yet?
                // for code patching?
                var value = default(string);

                try
                {
                    //--TypeError: Cannot read property 'stack' of null
                    //throw null;
                    throw new Exception();
                }
                catch (Exception err)
                {
                    value = err.StackTrace;
                }

                return value;
            }
        }
    }
}
