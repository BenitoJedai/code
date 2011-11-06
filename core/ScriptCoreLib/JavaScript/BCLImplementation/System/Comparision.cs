using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Comparison<>))]
    internal delegate int __Comparison<T>(T x, T y);
}
