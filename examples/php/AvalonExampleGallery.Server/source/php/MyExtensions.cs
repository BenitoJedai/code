using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptApplication.source.php
{
	[Script]
	public static class MyExtensions
	{
		public static T SizeTo<T>(this T e, int x, int y)
				where T : IHTMLElement
		{
			if (e.Style == null)
				e.Style = new IStyle();

			e.Style.width = x + "px";
			e.Style.height = y + "px";

			return e;
		}

		public static T MoveTo<T>(this T e, int x, int y)
			where T : IHTMLElement
		{
			if (e.Style == null)
				e.Style = new IStyle();

			e.Style.left = x + "px";
			e.Style.top = y + "px";
			e.Style.position = "absolute";

			return e;
		}

		public static string ToLink(this string href, string content)
		{
			return "<a href='" + href + "'>" + content + "</a>";
		}

		public static string ToImage(this string e)
		{
			return "<img src='" + e + "' />";
		}
	}
}
