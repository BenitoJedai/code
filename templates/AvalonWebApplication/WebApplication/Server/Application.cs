using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.PHP;
using System;
using System.Text;
using System.IO;
using WebApplication.Client.Avalon;

namespace WebApplication.Server
{
	[Script]
	static class Application
	{
		public const string Filename = "index.php";

		// change: C:\util\xampplite\apache\conf\httpd.conf

		// http://localhost/jsc/WebApplication

		//Alias /jsc/WebApplication "C:\work\jsc.svn\templates\AvalonWebApplication\WebApplication\bin\Release\web"
		//<Directory "C:\work\jsc.svn\templates\AvalonWebApplication\WebApplication\bin\Release\web">
		//       Options Indexes FollowSymLinks ExecCGI
		//       AllowOverride All
		//       Order allow,deny
		//       Allow from all
		//</Directory>

		/// <summary>
		/// php script will invoke this method
		/// </summary>
		[Script(NoDecoration = true)]
		public static void Application_Entrypoint()
		{

			Console.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
			Console.WriteLine("<html>");
			Console.WriteLine("<head>");
			Console.WriteLine("<link rel='stylesheet' type='text/css' href='assets/WebApplication/WebPage.css' />");
			Console.WriteLine("</head>");
			Console.WriteLine("<body>");

			Console.WriteLine("<img src='assets/WebApplication/jsc.png' />");
			Console.WriteLine("<h1>Congratulations!</h1><h2>You are using jsc compiler to convert your C# Application to PHP Application!</h2>");

			Native.Link("Visit jsc.sourceforge.net", "http://jsc.sourceforge.net");

			Console.WriteLine("<br />");

			Native.Link("See actionscript in action", "?actionscript");
			Console.WriteLine("<br />");
			Native.Link("See javascript in action", "?javascript");
			Console.WriteLine("<br />");
			Native.Link("See javascript and actionscript in action", "?javascript_actionscript");
			Console.WriteLine("<br />");
			Console.WriteLine("<br />");

			Native.Link("See more of this application over here!", "?more");

			Console.WriteLine("<br />");

			Action ShowActionScript =
				delegate
				{
					Console.WriteLine("<iframe style='border: 0;' width='" + AvalonCanvas.DefaultWidth + "' height='" + AvalonCanvas.DefaultHeight + "' src='AvalonFlash.htm' ></iframe>");
				};

			Action ShowJavaScript =
				delegate
				{
					Console.WriteLine("<iframe style='border: 0;' width='" + AvalonCanvas.DefaultWidth + "' height='" + AvalonCanvas.DefaultHeight + "' src='AvalonDocument.htm' ></iframe>");
				};

			if (Native.QueryString == "actionscript")
			{
				ShowActionScript();
			}

			if (Native.QueryString == "javascript")
			{
				ShowJavaScript();
			}

			if (Native.QueryString == "javascript_actionscript")
			{
				ShowJavaScript();
				ShowActionScript();

			}

			if (Native.QueryString == "more")
			{
				var path = "assets/WebApplication/description.txt";

				Console.WriteLine("<h2>" + path + "</h2>");
				Console.WriteLine("<p>");
				if (File.Exists(path))
				{
					Console.WriteLine(File.ReadAllText(path));
				}
				Console.WriteLine("</p>");

				Console.WriteLine("<p><img src='" + "assets/WebApplication/tongue.gif" + "' /> hello world (php)</p>");

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

			}

			Console.WriteLine("<hr />");

			//Native.API.phpinfo();

			Console.WriteLine("</body>");
			Console.WriteLine("</html>");

		}
	}
}
