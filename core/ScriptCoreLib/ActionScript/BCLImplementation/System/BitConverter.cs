using System;
using System.Collections.Generic;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/bitconverter.cs
    // https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/BitConverter.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/BitConverter.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\BitConverter.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\BitConverter.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\BitConverter.cs


    [Script(Implements = typeof(global::System.BitConverter))]
    internal class __BitConverter
    {
        public static byte[] GetBytes(int value)
        {
            var _buffer = new byte[4];
            _buffer[0] = (byte)value;
            _buffer[1] = (byte)(value >> 8);
            _buffer[2] = (byte)(value >> 0x10);
            _buffer[3] = (byte)(value >> 0x18);
            return _buffer;
        }

        public static byte[] GetBytes(uint value)
        {
            var _buffer = new byte[4];
            _buffer[0] = (byte)value;
            _buffer[1] = (byte)(value >> 8);
            _buffer[2] = (byte)(value >> 0x10);
            _buffer[3] = (byte)(value >> 0x18);
            return _buffer;
        }

        //        arg[0] is typeof System.UInt32
        //script: error JSC1000: No implementation found for this native method, please implement [static System.BitConverter.GetBytes(System.UInt32)]

        public static byte[] GetBytes(ulong value)
        {
            // http://stackoverflow.com/questions/337355/javascript-bitwise-shift-of-long-long-number

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140314

            // um. if the vm is 32bit then the shift operators
            // will not let us do 64bit shifts.

            // https://developer.mozilla.org/en-US/docs/Mozilla/js-ctypes/js-ctypes_reference/Int64

            var _buffer = new byte[8];

            //(~~d): 65280
            //c[0]: 255
            //(~~d) & 0xff: 0

            // X:\jsc.svn\examples\javascript\test\TestULongToByteCast\TestULongToByteCast\Program.cs

            var xvalue = value;

            _buffer[0] = (byte)xvalue;
            //xvalue = xvalue >> 8;
            xvalue = xvalue / 0x100;
            //xvalue = xvalue >> 8;
            _buffer[1] = (byte)xvalue;
            xvalue = xvalue / 0x100;
            _buffer[2] = (byte)xvalue;
            xvalue = xvalue / 0x100;
            _buffer[3] = (byte)xvalue;

            xvalue = xvalue / 0x100;
            _buffer[4] = (byte)xvalue;
            xvalue = xvalue / 0x100;
            _buffer[5] = (byte)xvalue;
            xvalue = xvalue / 0x100;
            _buffer[6] = (byte)xvalue;
            xvalue = xvalue / 0x100;
            _buffer[7] = (byte)xvalue;
            return _buffer;
        }

        //public static long DoubleToInt64Bits(double e)
        //{
        //    return global::java.lang.Double.doubleToLongBits(e);
        //}
    }
}
