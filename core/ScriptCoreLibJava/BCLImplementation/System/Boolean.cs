using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Boolean),
		ImplementationType = typeof(java.lang.Boolean))]
	internal class __Boolean
	{
		//[Script(ExternalTarget = "parseBoolean")]
		public static bool Parse(string e)
		{
			// java 1.4.2 does not have it yet...

			if (e == null)
				return false;

			if (e.ToLower() == "true")
				return true;

			return false;
			//return java.lang.Boolean.parseBoolean(e);
		}

        [Script(DefineAsStatic = true)]
        public int CompareTo(bool value)
        {
            var v = (bool)(object)this;

            if (v == value)
            {
                return 0;
            }
            if (!v)
            {
                return -1;
            }
            return 1;
        }

	}

}
