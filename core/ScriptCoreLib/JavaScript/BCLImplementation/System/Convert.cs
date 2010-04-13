using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Convert))]
    internal class __Convert
    {
		public static long ToInt64(double value)
		{
			// there is no int64 in javascript?
			return (long)global::System.Math.Floor((double)value);
		}

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

		public static string ToString(int value)
		{
			return "" + value;
		}

		public static string ToString(object value)
		{
			if (value == null)
				return null;

			var s = value as string;
			if (s != null)
				return s;

			return value.ToString();
		}


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

        public static string ToString(char value)
        {
            return __String.FromCharCode(value);
        }

		public static double ToDouble(string value)
		{
			return double.Parse(value);
			
		}

		public static bool ToBoolean(int value)
		{
			return value != 0;
		}

		public static int ToInt32(bool value)
		{
			if (value)
				return 1;

			return 0;
		}

		public static int ToInt32(string value)
		{
			return int.Parse(value);
		}
    }
}
