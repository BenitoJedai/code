using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace InteractiveOrdering.Shared
{
	[Script]
	public static class MyExtensions
	{
		public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, Func<IEnumerable<T>, T> selector)
		{
			var Result = new List<T>();

			var Resources = source.ToList();

			while (Resources.Count > 0)
			{
				if (Resources.Count == 1)
				{
					var n = Resources.First();

					Result.Add(n);

					Resources.Remove(n);
				}
				else
				{
					var n = selector(Resources);

					Result.Add(n);

					Resources.Remove(n);
				}
			}

			return Result;
		}
	}
}
