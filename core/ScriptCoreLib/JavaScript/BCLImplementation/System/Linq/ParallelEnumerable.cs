using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Linq
{
    // https://github.com/dotnet/corefx/blob/master/src/System.Linq.Parallel/src/System/Linq/ParallelEnumerable.cs
    // https://github.com/mono/mono/blob/master/mcs/class/System.Core/System.Linq/ParallelEnumerable.cs

    [Script(Implements = typeof(global::System.Linq.ParallelEnumerable))]
    public static class __ParallelEnumerable
    {
        // service worker vs worker threads
        // what analysis data does jsc need to probvide to know how can we share data?

        // how does it relate to async?
        // how does it relate to sqlite?
    }
}
