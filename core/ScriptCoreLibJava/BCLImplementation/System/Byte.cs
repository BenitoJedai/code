using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/byte.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Byte.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Byte.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Byte.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Byte.cs


	[Script(Implements = typeof(global::System.Byte)
		, ImplementationType = typeof(java.lang.Byte)
		)]
	internal class __Byte
	{
        [Script]
        public class __ByteByRef
        {
            // X:\jsc.svn\examples\javascript\Test\Test453RefByteLocal\Test453RefByteLocal\Test453RefByteLocal.csproj

            public byte[] array;
            public int index;
        }

		[Script(ExternalTarget = "parseByte")]
		public static byte Parse(string e)
		{
			return default(byte);
		}

		[Script(DefineAsStatic = true)]
		public string ToString(string format)
		{
			if (format != "x2")
				throw new NotImplementedException();

			return ToHexString((byte)(object)this);
		}

		public static string ToHexString(byte e)
		{
			const string u = "0123456789abcdef";

			return u.Substring((e >> 4) & 0xF, 1) + u.Substring((e >> 0) & 0xF, 1);
		}

     
	}

    // Error	716	An attribute argument must be a constant expression, typeof expression or array creation expression of an attribute parameter type	X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Byte.cs	44	26	ScriptCoreLibJava
    //[Script(Implements = typeof(global::System.Byte).MakeByRefType())]
    //public class __ByteByRef
    //{
    //    // X:\jsc.svn\examples\javascript\Test\Test453RefByteLocal\Test453RefByteLocal\Test453RefByteLocal.csproj
    //}
}
