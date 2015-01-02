using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/UInt64.cs

    [Script(Implements = typeof(global::System.UInt64))]
    internal class __UInt64
    {
        #region OptimizedCode
        [Script(OptimizedCode = "return parseInt(e, radix);")]
        static public ulong parseInt(string e, long radix = 10)
        {
            // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/parseInt

            return default(ulong);
        }

        [Script(OptimizedCode = "return isNaN(d);")]
        public static bool isNaN(ulong d)
        {
            return default(bool);

        }
        #endregion




        [Script(DefineAsStatic = true)]
        public static ulong Parse(string s, NumberStyles style)
        {
            // X:\jsc.svn\examples\javascript\Test\TestULongShift\TestULongShift\ApplicationControl.cs

            var x = default(ulong);

            if (style == NumberStyles.HexNumber)
            {
                x = parseInt(s, 16);
            }
            else
            {
                x = parseInt(s);
            }

            if (isNaN(x))
                throw new InvalidOperationException();

            return x;
        }

        [Script(DefineAsStatic = true)]
        static public ulong Parse(string e)
        {
            var x = parseInt(e);

            if (isNaN(x))
                throw new InvalidOperationException();

            return x;
        }


        //[Script(DefineAsStatic = true)]
        //static public bool TryParse(string e, out ulong result)
        //{
        //    // script: error JSC1000: unknown opcode stind.i8 at TryParse + 0x001d

        //    // tested by
        //    // X:\jsc.svn\examples\javascript\forms\FormsDataGridViewDeleteRow\FormsDataGridViewDeleteRow\ApplicationControl.cs

        //    //parseInt('s')
        //    //NaN


        //    var x = parseInt(e);
        //    var nan = isNaN(x);

        //    if (nan)
        //        result = 0;
        //    else
        //        result = x;

        //    return !nan;
        //}

        [Script(DefineAsStatic = true)]
        public int CompareTo(__UInt32 e)
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
            //E:\jsc.svn\examples\javascript\Test\TestToStringCurrencyFormat\TestToStringCurrencyFormat\Application.cs
            if (format == "0.00")
            {
                var cent = value % 100;
                var t = value - cent;
                var to = t / 100;
                var total = to + "." + cent.ToString().PadLeft(2, '0');
                return total;
            }

            if (format.ToLower() != "x8")
                throw new NotImplementedException("format");


            var s = new StringBuilder();

            s.Append(ToHexString((byte)(value >> 0x18)));
            s.Append(ToHexString((byte)(value >> 0x10)));
            s.Append(ToHexString((byte)(value >> 8)));
            s.Append(ToHexString((byte)(value)));

            if (format == "x8")
                return s.ToString().ToLower();

            // X:\jsc.svn\examples\javascript\test\TestMD5Experiment\TestMD5Experiment\Library\helper.cs

            return s.ToString().ToUpper();
        }

        public static string ToHexString(byte e)
        {
            const string u = "0123456789abcdef";

            return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
        }
    }
}
