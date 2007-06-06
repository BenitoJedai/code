using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{

    [Script(Implements = typeof(global::System.Action<>))]
    internal delegate void __Action<T>(T t);
    
}
