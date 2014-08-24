using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Diagnostics
{
    // http://referencesource.microsoft.com/#mscorlib/system/diagnostics/stacktrace.cs
    [Script(Implements = typeof(global::System.Diagnostics.StackTrace))]
    internal class __StackTrace
    {
        // conflict with
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Diagnostics\StackTrace.cs

        // X:\jsc.svn\examples\javascript\LINQ\ComplexQueryExperiment\ComplexQueryExperiment\Application.cs
        // script: error JSC1000: Missing Script Attribute? Native constructor was invoked, please implement [System.Diagnostics.StackTrace..ctor(System.Boolean)]

        public __StackTrace()
            : this(0, true)
        {

        }

        //Implementation not found for type import :
        //type: System.Diagnostics.StackTrace
        //method: Void .ctor(Int32, Boolean)
        //Did you forget to add the [Script] attribute?
        //Please double check the signature!

        public __StackTrace(bool fNeedFileInfo)
            : this(0, fNeedFileInfo)
        {

        }

        public __StackTrace(int skipFrames, bool fNeedFileInfo)
        {
            // X:\jsc.svn\examples\javascript\WebCamAvatarsExperiment\WebCamAvatarsExperiment\Application.cs
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
