using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Double),
		ImplementationType = typeof(java.lang.Double))]
	internal class __Double
	{
		[Script(ExternalTarget = "parseDouble")]
		public static double Parse(string e)
		{
			return default(double);
		}


        [Script(DefineAsStatic = true)]
        public int CompareTo(double value)
        {
            var v = (double)(object)this;

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
