using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Convert))]
	internal class __Convert
	{
		public static string ToString(bool value)
		{
			if (value)
				return "true";

			return "false";
		}
		public static bool ToBoolean(string value)
		{
			if ("true" == value)
				return true;

			return false;
		}

		public static long ToInt64(double value)
		{
			return (long)global::System.Math.Floor(value);
		}

		public static int ToInt32(long value)
		{
			return (int)value;
		}


		public static int ToInt32(double value)
		{
			return (int)global::System.Math.Floor(value);
		}

		public static string ToString(char value)
		{
			return Native.API.chr(value);
		}

	}
}
