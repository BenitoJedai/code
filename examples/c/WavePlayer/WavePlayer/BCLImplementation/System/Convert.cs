using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WavePlayer.BCLImplementation.System
{
	[Script(Implements = typeof(global::System.Convert))]
	internal  class __Convert
	{
		public static int ToInt32(long value)
		{
			return (int)value;
		}

		public static short ToInt16(int value)
		{
			return (short)value;
		}

		public static short ToInt16(double value)
		{
			return (short)value;
		}
	}
}
