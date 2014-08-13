using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;
using System.Threading;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/environment.cs

    [Script(Implements = typeof(global::System.Environment))]
    internal class __Environment
    {
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Environment.cs
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRYieldStatement\TestJVMCLRYieldStatement\Program.cs

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


        public static string CurrentDirectory
        {
            get
            {
                // http://www.devx.com/tips/Tip/13804

                var f = new java.io.File(".");
                var c = default(string);

                try
                {
                    c = f.getCanonicalPath();
                }
                catch
                {
                    throw;
                }

                return c;
            }
        }
    }
}
