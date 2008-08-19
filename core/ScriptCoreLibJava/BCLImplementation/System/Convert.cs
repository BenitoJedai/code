using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Convert))]
	internal class __Convert
	{
		public static int ToInt32(int e)
		{
			return (int)Math.Floor((double)e);
		}

		public static int ToInt32(double e)
		{
			return (int)Math.Floor((double)e);
		}
	}
}
