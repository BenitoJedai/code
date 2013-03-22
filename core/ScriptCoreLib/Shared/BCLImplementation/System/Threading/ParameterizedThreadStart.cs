using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Shared.BCLImplementation.System.Threading
{
    [Script(Implements = typeof(global::System.Threading.ParameterizedThreadStart))]
    internal delegate void __ParameterizedThreadStart(object e);
}
