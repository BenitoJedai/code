using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.ExceptionServices;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/exception.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/Exception.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Exception.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Exception.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Exception.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Exception.cs

    [Script(InternalConstructor = true, Implements = typeof(global::System.Exception))]
    internal class __Exception
    {
		// https://github.com/dotnet/coreclr/blob/6d1715d68d8ca1f921534897828242bbcc4f00b6/src/pal/src/exception/signal.cpp

		// android NDK stores os errno at a pointer location 
		// X:\jsc.svn\core\ScriptCoreLibAndroidNDK\ScriptCoreLibAndroidNDK\SystemHeaders\errno.cs

		public string Message
        {
            [Script(DefineAsStatic = true)]
            get
            {
                return Expando<string, string>.Of(this)["message"];
            }
        }

        public string StackTrace
        {
            [Script(DefineAsStatic = true)]
            get
            {
                // X:\jsc.svn\examples\javascript\Test\TestDelegateInvokeDisplayName\TestDelegateInvokeDisplayName\Application.cs
                // how else could we get a stack trace?

                // https://code.google.com/p/v8/wiki/JavaScriptStackTraceApi
                return Expando<string, string>.Of(this)["stack"];
            }
        }

        #region Constructor

        public __Exception(string message) { }

        [Script(OptimizedCode = @"return new Error(e);")]
        internal static __Exception InternalConstructor(string e)
        {
            return default(__Exception);
        }

        public __Exception() { }

        [Script(OptimizedCode = @"return new Error();")]
        static __Exception InternalConstructor()
        {
            return default(__Exception);
        }

        #endregion





        [Script(DefineAsStatic = true)]
        public void RestoreExceptionDispatchInfo(__ExceptionDispatchInfo e)
        {
            // can the stacktrace be restored for the serverside?
            // we could perhaps even walk up the stack
            // and see readonly locals for the client side?


            // X:\jsc.svn\examples\javascript\async\AsyncWindowUncaughtError\AsyncWindowUncaughtError\Application.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140524
            // X:\jsc.svn\examples\javascript\forms\async\AsyncFinally\AsyncFinally\ApplicationControl.cs

            // ?
            //Console.WriteLine();
        }
    }
}
