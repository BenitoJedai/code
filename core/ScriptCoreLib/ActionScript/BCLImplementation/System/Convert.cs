using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Convert))]
    internal class __Convert
    {
		public static uint ToUInt32(long value)
		{
			return ((uint)value & 0xffffffff);

		}

		public static int ToInt32(long value)
		{
			return (int)((int)value & 0xffffffff);

		}

		public static int ToInt32(int value)
		{
			return (int)global::System.Math.Floor((double)value);

		}

		public static byte ToByte(int value)
		{
			return (byte)(value & 0xff);

		}

        public static int ToInt32(double value)
        {
            return (int)global::System.Math.Floor(value);
        }

		public static byte ToByte(double value)
		{
			return (byte)(((int)global::System.Math.Floor(value)) % 256);
		}
    }
}
