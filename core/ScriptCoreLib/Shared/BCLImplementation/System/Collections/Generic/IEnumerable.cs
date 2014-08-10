using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic
{
    // http://referencesource.microsoft.com/#mscorlib/system/collections/generic/ienumerable.cs
    // see VM\compile.cpp.

    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerable<>))]
    public interface __IEnumerable<out T> : __IEnumerable
    {
        // X:\jsc.svn\examples\javascript\Test\TestAssignArrayToEnumerable\TestAssignArrayToEnumerable\Application.cs
        // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRAssignArrayToEnumerable\TestJVMCLRAssignArrayToEnumerable\Program.cs
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140810/asenumerable

        new IEnumerator<T> GetEnumerator();
    }
}
