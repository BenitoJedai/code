using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.BCLImplementation.System.Runtime.ExceptionServices;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/exception.cs


    [Script(InternalConstructor = true, Implements = typeof(global::System.Exception))]
    internal class __Exception
    {

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
            // X:\jsc.svn\examples\javascript\async\AsyncWindowUncaughtError\AsyncWindowUncaughtError\Application.cs

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201405/20140524
            // X:\jsc.svn\examples\javascript\forms\async\AsyncFinally\AsyncFinally\ApplicationControl.cs

            // ?
            //Console.WriteLine();
        }
    }
}
