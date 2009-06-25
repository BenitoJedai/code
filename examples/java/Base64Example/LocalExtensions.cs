using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace Base64Example
{
	[Script]
	public static class LocalExtensions
	{
		public static bool ByteArrayEquals(this byte[] e, byte[] value)
		{
			if (e.Length != value.Length)
				return false;

			var x = true;
			for (int i = 0; i < e.Length; i++)
			{
				if (e[i] != value[i])
				{
					x = false;
					break;
				}
			}
			return x;
		}

	}
}
