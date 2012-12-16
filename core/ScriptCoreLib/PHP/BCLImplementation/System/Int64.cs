using ScriptCoreLib;
using ScriptCoreLib.PHP.Runtime;
using System;
using System.Globalization;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System
{
    [Script(Implements = typeof(global::System.Int64))]
    internal class __Int64
    {
     
        public static long Parse(string e)
        {
            return Native.API.intval(e);
        }


        public static long Parse(string s, NumberStyles style)
		{
			if (style == NumberStyles.HexNumber)
				return Native.API.intval(s, 16);

			return Native.API.intval(s);
		}


        [Script(DefineAsStatic = true)]
        public string ToString(string format)
        {
            var value = (int)(object)this;


            return InternalToString(format, value);
        }

        internal static string InternalToString(string format, int value)
        {
            if (format != "x8")
                throw new Exception("format");


            var s = new StringBuilder();

            s.Append(ToHexString((byte)(value >> 0x18)));
            s.Append(ToHexString((byte)(value >> 0x10)));
            s.Append(ToHexString((byte)(value >> 8)));
            s.Append(ToHexString((byte)(value)));

            return s.ToString();
        }

        public static string ToHexString(byte e)
        {
            const string u = "0123456789abcdef";

            return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
        }
    }
}
