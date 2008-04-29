using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Convert))]
    internal class __Convert
    {
        public static int ToInt32(double value)
        {
            return (int)global::System.Math.Floor(value);
        }

        public static string ToString(char value)
        {
            return __String.FromCharCode(value);
        }
    }
}
