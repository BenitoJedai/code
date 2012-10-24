using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.UInt32)
		, ImplementationType = typeof(java.lang.Integer)
		)]
	internal class __UInt32
	{
		[Script(ExternalTarget = "parseInt")]
		public static uint Parse(string e)
		{
			return default(uint);
		}

        [Script(DefineAsStatic = true)]
        public int CompareTo(uint value)
        {
            var v = (uint)(object)this;

            if (v < value)
            {
                return -1;
            }
            if (v > value)
            {
                return 1;
            }
            return 0;
        }
	}
}
