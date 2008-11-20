using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WebApplication.Server
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
	}
}
