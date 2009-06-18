using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ExposedFunctions.js
{
	[Script]
	public static class Extensions
	{
		public static string ToLink(this string src)
		{
			return "<a href=\"" + src + "\">" + src + @"</a>";
			
		}

		public static int Random(this int i)
		{
			return new Random().Next(i);
		}
	}
}
