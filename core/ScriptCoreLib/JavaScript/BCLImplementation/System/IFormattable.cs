using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.IFormattable))]
    internal interface __IFormattable
    {
        string ToString(string format, IFormatProvider formatProvider);
    }
}
