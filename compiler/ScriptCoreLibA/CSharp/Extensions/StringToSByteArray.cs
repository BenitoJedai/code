using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.CSharp.Extensions
{
	public static class StringToSByteArray
	{
		public static sbyte[] ToSBytes(this string e)
		{
			return Encoding.UTF8.GetBytes(e).Select(k => (sbyte)k).ToArray();
		}

	}
}
