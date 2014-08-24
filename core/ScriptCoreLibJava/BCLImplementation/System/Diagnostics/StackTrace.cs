using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Diagnostics
{
    [Script(Implements = typeof(global::System.Diagnostics.StackTrace))]
    internal class __StackTrace
    {
        // conflict with
        // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Diagnostics\StackTrace.cs

        java.lang.Throwable t;

        public __StackTrace()
        {
            this.t = new java.lang.Throwable();

        }
        public override string ToString()
        {

            var ww = new java.io.StringWriter();

            t.printStackTrace(new java.io.PrintWriter(ww));

            return ww.ToString();
        }
    }
}
