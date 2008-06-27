using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Converter<,>))]
    internal delegate TOutput __Converter<TInput, TOutput>(TInput input);
}
