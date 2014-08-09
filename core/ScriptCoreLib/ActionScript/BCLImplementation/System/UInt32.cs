using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    [Script(
        Implements = typeof(global::System.UInt32),
        ImplementationType = typeof(global::ScriptCoreLib.ActionScript.@uint))]
    internal class __UInt32
    {
        // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\UInt32.cs
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\UInt32.cs

        // http://livedocs.adobe.com/flash/9.0/ActionScriptLangRefV3/package.html#parseInt()
        [Script(OptimizedCode = "return uint(parseInt(e));")]
        static public uint Parse(string e)
        {
            return default(uint);
        }

        [Script(DefineAsStatic = true)]
        public int CompareTo(uint value)
        {
            var v = (uint)(object)this;

            if (v < value)
            {
                return -1;
            }
            if (v > value)
            {
                return 1;
            }
            return 0;
        }

 


        [Script(DefineAsStatic = true)]
        public int CompareTo(object value)
        {
            // tested by ?
            // X:\jsc.svn\examples\actionscript\Test\TestUInntCompareToNonUInt32\TestUInntCompareToNonUInt32\ApplicationSprite.cs

            if (value == null)
            {
                return 1;
            }

            // X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.CoreAudio\Application.cs
            var isUInt32 = value is uint;
            if (!isUInt32)
            {
                throw new ArgumentException("MustBeUInt32");
            }


            return CompareTo((uint)value);
        }







        #region ToString
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
        #endregion




    }
}
