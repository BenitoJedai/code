using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/environment.cs

    // System.Environment
    [Script(Implements = typeof(global::System.Environment))]
    internal static class __Environment
    {
        // by
        // X:\jsc.svn\examples\javascript\forms\AsyncTaskYieldViaProgress\AsyncTaskYieldViaProgress\ApplicationControl.cs
        public static int CurrentManagedThreadId
        {
            get
            {
                return Thread.CurrentThread.ManagedThreadId;
            }
        }

        public static string NewLine
        {
            get
            {
                return "\r\n";
            }
        }
    }
}
