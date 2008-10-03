using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Media;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace AvalonPipeMania.Code
{
	[Script]
	static class MyExtensions
	{
		public static IEnumerable<Color> ToGradient(this Color[] source, int count)
		{
			var value = new Color[0].AsEnumerable();

			if (source.Length > 1)
			{
				var c = count / (source.Length - 1);

				source.ForEachWithPrevious(
					(previous, item) =>
					{
						value = value.Concat(previous.ToGradient(item, c));
					}
				);
			}

			return value;
		}

		public static void ForEachWithPrevious<T>(this IEnumerable<T> source, Action<T, T> handler)
		{
			var previous = default(T);
			var ready = false;

			foreach (var item in source.AsEnumerable())
			{
				if (ready)
				{
					handler(previous, item);
				}

				ready = true;
				previous = item;
			}
		}
	}

}
