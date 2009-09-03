using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.BitConverter))]
	internal class __BitConverter
	{
		public static long DoubleToInt64Bits(double e)
		{
			return global::java.lang.Double.doubleToLongBits(e);
		}
	}
}
