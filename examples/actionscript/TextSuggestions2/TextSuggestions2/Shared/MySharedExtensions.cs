using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows.Media;

namespace TextSuggestions2.Shared
{
	[Script]
	public static class MySharedExtensions
	{
		public static IEnumerable<Color> ToGradient(this Color from, Color to, int count)
		{
			return Enumerable.Range(0, count).Select(
				i =>
				{
					var j = count - (i + 1);

					var r = Convert.ToByte( (from.R * j + to.R * i) / count);
					var g = Convert.ToByte((from.G * j + to.G * i) / count);
					var b = Convert.ToByte((from.B * j + to.B * i) / count);

					return Color.FromRgb(r, g, b);
				}
			);
		}
	}
}
