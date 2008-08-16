using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Convert))]
    internal class __Convert
    {
		public static int ToInt32(int value)
		{
			return (int)global::System.Math.Floor((double)value);

		}

        public static int ToInt32(double value)
        {
            return (int)global::System.Math.Floor(value);
        }
    }
}
