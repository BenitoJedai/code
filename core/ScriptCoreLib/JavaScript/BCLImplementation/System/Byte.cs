using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.Runtime;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System
{

	[Script(Implements = typeof(global::System.Byte))]
	internal class __Byte
	{

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
