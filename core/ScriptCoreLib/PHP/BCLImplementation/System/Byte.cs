using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Byte))]
    internal class __Byte
    {
		public static __Byte Parse(string e)
        {
			return (__Byte)((object)Native.API.intval(e));
        }
    }
}
