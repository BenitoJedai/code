using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
	public static class QueryExtensions
	{
		public static List<T> ToEmptyList<T>(this T template)
		{
			return new List<T>();
		}
	}
}
