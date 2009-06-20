using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJavaCard.APDUProxyGenerator.Library
{
	public static class Extensions
	{
		public static string ToHexLiteral(this byte e)
		{
			return string.Format("0x{0:x2}", e);
		}
	}
}
