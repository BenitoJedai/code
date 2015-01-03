using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{
    // http://referencesource.microsoft.com/#mscorlib/system/byte.cs
    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Byte.cs
    // https://github.com/mono/mono/blob/master/mcs/class/corlib/System/Byte.cs

	[Script(Implements = typeof(global::System.Byte))]
	internal class __Byte
	{
        // shared Memory between threads?
        // X:\jsc.svn\examples\javascript\Test\Test453RefByteLocal\Test453RefByteLocal\Class1.cs

        [Script(DefineAsStatic = true)]
        static public bool TryParse(string e, out byte result)
        {
            // tested by ?
            // X:\jsc.svn\examples\javascript\Test\TestInlineTryParse\TestInlineTryParse\Application.cs

            var x = __Int32.parseInt(e);
            var nan = __Int32.isNaN(x);

            if (nan)
                result = 0;
            else
                result = (byte)x;

            return !nan;
        }

		[Script(OptimizedCode = "return parseInt(e);")]
		static public __Byte Parse(string e)
		{
			return default(__Byte);
		}

		[Script(DefineAsStatic = true)]
		public int CompareTo(__Byte e)
		{
			return Expando.Compare(this, e);

		}

		[Script(DefineAsStatic = true)]
		public string ToString(string format)
		{
			var value = (byte)(object)this;


			var w = new StringBuilder();

			if (format == "x2")
			{
				AppendByteAsHexString(value, w);
			}
			else
			{
				w.Append(value);
			}

			return w.ToString();
		}

		private static void AppendByteAsHexString(byte value, StringBuilder w)
		{
			w.Append((string)__Byte.NibbleToHexString((value & 0xF0) >> 4));
			w.Append((string)__Byte.NibbleToHexString(value & 0xF));
		}

		private static string NibbleToHexString(int p)
		{
			return "0123456789abcdef".Substring(p, 1);
		}
	}
}
