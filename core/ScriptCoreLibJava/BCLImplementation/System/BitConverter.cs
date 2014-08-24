using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/bitconverter.cs

    [Script(Implements = typeof(global::System.BitConverter))]
    internal class __BitConverter
    {
        // X:\jsc.svn\examples\java\Test\JVMUInt32Bytes\JVMUInt32Bytes\Program.cs
        // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\BitConverter.cs
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
            // jsc do you supprt int vs uint?

            var _buffer = new byte[4];
            _buffer[0] = (byte)value;
            _buffer[1] = (byte)(value >> 8);
            _buffer[2] = (byte)(value >> 0x10);
            _buffer[3] = (byte)(value >> 0x18);
            return _buffer;
        }

        public static long DoubleToInt64Bits(double e)
        {
            return global::java.lang.Double.doubleToLongBits(e);
        }
    }
}
