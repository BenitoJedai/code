using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Int64))]
    internal class __Int64
    {

        static public long Parse(string e)
        {
            var value = parseInt(e);

            var x = "" + value;

            if (x == e)
                return value;

            // tested by X:\jsc.svn\examples\javascript\Test\TestLongParse\TestLongParse\Application.cs

            throw new InvalidOperationException("parseInt failed for " + e);
        }

        [Script(OptimizedCode = "return parseInt(e);")]
        static public long parseInt(string e)
        {
            return default(long);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(long e)
        {
            return Expando.Compare(this, e);

        }

        [Script(DefineAsStatic = true)]
        public string ToString(string format)
        {
            var value = (long)(object)this;


            return InternalToString(format, value);
        }

        internal static string InternalToString(string format, long value)
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
