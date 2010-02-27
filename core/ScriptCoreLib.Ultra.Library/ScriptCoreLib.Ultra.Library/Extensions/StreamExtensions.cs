using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptCoreLib.Ultra.Library.Extensions
{
	public static class StreamExtensions
	{
		public static byte[] ToBytes(this Stream s)
		{
			var x = new byte[s.Length];

			s.Read(x, 0, x.Length);

			return x;
		}
	}
}
