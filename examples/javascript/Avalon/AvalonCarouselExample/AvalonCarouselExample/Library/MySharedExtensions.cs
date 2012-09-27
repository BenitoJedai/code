using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace CarouselExample.Shared
{
	[Script]
	public static class MySharedExtensions
	{
		public static List<T> ToEmptyList<T>(this T e)
		{
			return new List<T>();
		}
	}
}
