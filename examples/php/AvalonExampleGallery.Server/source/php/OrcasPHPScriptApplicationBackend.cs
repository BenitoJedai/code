extern alias pages;


using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.PHP;
using System;
using System.Text;

namespace ScriptApplication.source.php
{
	[Script]
	static class OrcasPHPScriptApplicationBackend
	{
		public const string Entrypoint = "WebPageEntry";
		public const string Filename = "MyWebPage.php";


		/// <summary>
		/// php script will invoke this method
		/// </summary>
		[Script(NoDecoration = true)]
		public static void WebPageEntry()
		{


			Console.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
			Console.WriteLine("<html>");
			Console.WriteLine("<head>");
			Console.WriteLine("<title>Hello world</title>");

			Console.WriteLine("</head>");
			Console.WriteLine("<body>");

			
			Console.WriteLine(
				new { hello = "world" }
			);

			Console.WriteLine(
				"<img src='" + pages::NavigationButtons.Assets.Shared.KnownAssets.Path.Assets + "/Preview.png' />"
			);

			

			Console.WriteLine("</body>");
			Console.WriteLine("</html>");
		}
	}
}
