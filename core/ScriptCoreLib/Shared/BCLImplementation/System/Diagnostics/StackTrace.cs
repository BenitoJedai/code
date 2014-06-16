using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Diagnostics
{
    [Script(Implements = typeof(global::System.Diagnostics.StackTrace))]
    internal class __StackTrace
    {
        // X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\Application.cs
        // script: error JSC1000: Missing Script Attribute? Native constructor was invoked, please implement [System.Diagnostics.StackTrace..ctor(System.Boolean)]

        public __StackTrace()
            : this(true)
        {

        }

        public __StackTrace(bool fNeedFileInfo)
        {

        }

        public override string ToString()
        {
            return "<__StackTrace>";
        }

        public virtual StackFrame GetFrame(int index)
        {
            return new StackFrame();
        }
    }
}
