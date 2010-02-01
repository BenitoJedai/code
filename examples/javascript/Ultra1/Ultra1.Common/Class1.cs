using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript;

namespace Ultra1.Common
{
	public class Class1
	{
		public static string ToString(string x, string y)
		{
			return "{ " + x + ": " + y + " }";
		}
	}


	public interface IUltraPolyglot
	{
		event Action Clicked;
		event Action MyLoaded;

		string Status { get; set; }
	}

	public static class js_extensions
	{
		public static void MessageStatusOnClick(this IUltraPolyglot e)
		{
			e.Clicked +=
				delegate
				{
					Native.Window.alert("MessageStatusOnClick: " + e.Status);
				};
		}
	}
}
