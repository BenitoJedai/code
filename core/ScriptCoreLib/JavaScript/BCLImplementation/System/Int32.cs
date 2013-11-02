using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    using ScriptCoreLib.JavaScript.Runtime;

    [Script(Implements = typeof(global::System.Int32))]
    internal class __Int32
    {
        #region OptimizedCode
        [Script(OptimizedCode = "return parseInt(e);")]
        static public int parseInt(string e)
        {
            return default(int);
        }

        [Script(OptimizedCode = "return isNaN(d);")]
        public static bool isNaN(int d)
        {
            return default(bool);

        }
        #endregion


        [Script(DefineAsStatic = true)]
        static public int Parse(string e)
        {
            var x = parseInt(e);

            if (isNaN(x))
                throw new InvalidOperationException();

            return x;
        }


        [Script(DefineAsStatic = true)]
        static public bool TryParse(string e, out int result)
        {
            // tested by
            // X:\jsc.svn\examples\javascript\forms\FormsDataGridViewDeleteRow\FormsDataGridViewDeleteRow\ApplicationControl.cs

            //parseInt('s')
            //NaN


            var x = parseInt(e);
            var nan = isNaN(x);

            if (nan)
                result = 0;
            else
                result = x;

            return !nan;
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
