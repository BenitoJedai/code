using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Comparison<>))]
    public delegate int __Comparison<T>(T x, T y);
}
