using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace MovieBlog.Server
{
	[Script]
	public static class ServerExtensions
	{
		public static void ToImageToConsole(this string src)
		{
			Console.WriteLine("<img src='" + src + "' />");
		}

		public static void ToImageToConsoleWithStyle(this string src, string style)
		{
			Console.WriteLine("<img src='" + src + "' style='" + style + "' />");
		}

		public static Func<T, T> ToChainedFunc<T>(this Func<T, T> e, int count)
		{
			return
				value =>
				{
					var p = e(value);


					for (int i = 1; i < count; i++)
					{
						p = e(p);
					}

					return p;
				};
		}

	}
}
