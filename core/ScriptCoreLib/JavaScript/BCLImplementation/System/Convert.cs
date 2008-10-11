using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
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

		public static byte ToByte(int value)
		{
			return (byte)(value & 0xff);

		}

		public static byte ToByte(double value)
		{
			return (byte)(((int)global::System.Math.Floor(value)) & 0xff);
		}


		public static double ToDouble(int value)
		{
			return value;
		}


        public static string ToString(char value)
        {
            return __String.FromCharCode(value);
        }
    }
}
