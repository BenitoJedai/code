using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.BCLImplementation.System;

namespace ScriptCoreLib.Ultra.BCLImplementation.Microsoft.FSharp.Core
{
    [Script]
    internal class __Operators
    {
        public static T2 Snd<T1, T2>(__Tuple<T1, T2> tuple)
        {
            return tuple.Item2;
        }
    }
}
