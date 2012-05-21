using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Int32))]
    internal class __Int32
    {

        [Script(OptimizedCode = "return parseInt(e);")]
        static public __Int32 Parse(string e)
        {
            return default(__Int32);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(__Int32 e)
        {
            return Expando.Compare(this, e);
           
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
                throw new NotImplementedException("format");


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
