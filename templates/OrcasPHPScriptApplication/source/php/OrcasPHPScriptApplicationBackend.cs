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
			Console.WriteLine("<body>");

			Console.WriteLine("<link rel='stylesheet' type='text/css' href='assets/OrcasPHPScriptApplication/WebPage.css' />");

			Console.WriteLine("<p><img src='" + "assets/OrcasPHPScriptApplication/tongue.gif" + "' /> hello world (php)</p>");

			Native.Link("see html for javascript OrcasPHPScriptApplicationDocument", "OrcasPHPScriptApplicationDocument.htm");

			Native.Dump(
				new { hello = "world" }
			);

			var kk = new StringBuilder();
			var k = kk.Append("hello").Append(" world");

			Console.WriteLine(
				k.ToString()
			);

			Console.WriteLine(
				new { hello = "world" }
			);

			Console.WriteLine("<hr />");

			Native.API.phpinfo();

			Console.WriteLine("</body>");
			Console.WriteLine("</html>");
		}
	}
}
