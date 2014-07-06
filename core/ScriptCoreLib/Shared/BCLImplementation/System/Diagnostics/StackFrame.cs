using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Diagnostics
{
    // http://referencesource.microsoft.com/#mscorlib/system/diagnostics/stackframe.cs
    [Script(Implements = typeof(global::System.Diagnostics.StackFrame))]
    internal class __StackFrame
    {
        // X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\Application.cs
        //script: error JSC1000: No implementation found for this native method, please implement [System.Diagnostics.StackFrame.GetFileLineNumber()]

        public virtual int GetFileLineNumber()
        {
            return 0;
        }

        public virtual string GetFileName()
        {
            return "?";
        }
    }
}
