using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLibJavaCard.APDUProxyGenerator
{
	public static class PrimitiveNameProvider
	{
		public static string ToPrimitiveName(this Type e)
		{
			if (e == typeof(string))
				return "string";

			if (e == typeof(byte))
				return "byte";


			return e.Name;
		}
	}
}
