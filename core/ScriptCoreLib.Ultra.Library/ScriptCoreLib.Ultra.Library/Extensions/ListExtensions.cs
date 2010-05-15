using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.Extensions
{
	public static class ListExtensions
	{
		public static void AddDistinct<T>(this List<T> source, T value)
		{
			if (source.Contains(value))
				return;

			source.Add(value);
		}
	}
}
