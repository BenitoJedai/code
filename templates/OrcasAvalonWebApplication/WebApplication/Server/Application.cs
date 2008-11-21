using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.PHP;
using System;
using System.Text;
using System.IO;
using WebApplication.Client.Avalon;
using WebApplication.Client.Java;
using WebApplication.Shared;

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

			Console.WriteLine("<meta name='description' content='jsc can convert your C# Application to PHP, JavaScript, Actionscript and Java' />");
			Console.WriteLine("<meta name='keywords' content='c# decompiler, cross compiler, flash, actionscript, php, java, javascript, ajax, web2, dhtml, jsc' />");
			Console.WriteLine("<link rel='shortcut icon' href='" + KnownAssets.Path.Assets + "/App.ico" + "' />");
			Console.WriteLine("<link rel='alternate' type='application/rss+xml' title='RSS 2.0' href='http://zproxy.wordpress.com/feed/' />");


			Console.WriteLine("<title>" + "AvalonWebApplication".WithBranding() + "</title>");

			Console.WriteLine("<link rel='stylesheet' type='text/css' href='assets/WebApplication/WebPage.css' />");
			Console.WriteLine("</head>");
			Console.WriteLine("<body>");

			(KnownAssets.Path.Assets + "/Preview.png").ToImageToConsole();
			(KnownAssets.Path.Assets + "/jsc.png").ToImageToConsole();

			Console.WriteLine("<h1>Congratulations!</h1><h2>You are using jsc compiler to convert your C# Application to PHP, JavaScript, Actionscript and Java!</h2>");
			Console.WriteLine("<h3>" + "C# To PHP".WithBranding() + "</h3>");


			(KnownAssets.Path.Assets + "/diagram_jsc.png").ToImageToConsoleWithStyle(
				"border: 1px solid gray; background: white; padding: 1em;"
			);

			Console.WriteLine("<br />");

			Native.Link(SharedExtensions.HomePageText, SharedExtensions.HomePage);
			Console.WriteLine("<br />");
			Console.WriteLine("<br />");
			Native.Link("View Source", SharedExtensions.TemplateSourceCode);
			Console.WriteLine("<hr />");

			//Native.Link("See java applet in action", "?javaapplet");
			//Console.WriteLine("<br />");
			//Native.Link("See actionscript in action", "?actionscript");
			//Console.WriteLine("<br />");
			//Native.Link("See javascript in action", "?javascript");
			//Console.WriteLine("<br />");
			//Native.Link("See javascript and actionscript in action", "?javascript_actionscript");
			//Console.WriteLine("<br />");
			//Native.Link("See javascript and actionscript, java applet in action", "?javascript_actionscript_javaapplet");
			//Console.WriteLine("<br />");
			//Console.WriteLine("<br />");

			Native.Link("See more of this application over here!", "?more");

			Console.WriteLine("<hr />");

			Action<int, int, string> CreateIFrame =
				(w, h, src) => Console.WriteLine("<iframe style='border: 0;' width='" + w + "' height='" + h + "' src='" + src + "' ></iframe>"); ;

			Action ShowActionScript = () => CreateIFrame(AvalonCanvas.DefaultWidth, AvalonCanvas.DefaultHeight, "AvalonFlash.htm");
			Action ShowJavaScript = () => CreateIFrame(AvalonCanvas.DefaultWidth, AvalonCanvas.DefaultHeight, "AvalonDocument.htm");
			Action ShowJavaApplet = () => CreateIFrame(ApplicationApplet.DefaultWidth, ApplicationApplet.DefaultHeight, "ApplicationApplet.htm");

			//Console.WriteLine("<h3>" + Native.QueryString + "</h3>");


			//if (Native.QueryString == "javaapplet")
			//{
			//    ShowJavaApplet();
			//}

			//if (Native.QueryString == "actionscript")
			//{
			//    ShowActionScript();
			//}

			//if (Native.QueryString == "javascript")
			//{
			//    ShowJavaScript();
			//}

			//if (Native.QueryString == "javascript_actionscript")
			//{
			//    ShowJavaScript();
			//    ShowActionScript();

			//}

			// sf.net does not work with querystring for some reason.

			Console.WriteLine("<h3>Java Applet</h3>");
			ShowJavaApplet();

			Console.WriteLine("<h3>ActionScript</h3>");
			ShowActionScript();

			Console.WriteLine("<h3>JavaScript</h3>");
			ShowJavaScript();

			Console.WriteLine("<h3>How to deploy this to a site?</h3>");
			Console.WriteLine("You need to copy the following folders and files from 'bin/Release/web/' folder:");
			Console.WriteLine(@"
				<ul>
					<li>assets/</li>
					<li>bin/</li>
					<li>inc/</li>
					<li>*.htm</li>
					<li>*.swf</li>
					<li>*.js</li>
				</ul>
			");


			//if (Native.QueryString == "more")
			//{
			//    var path = "assets/WebApplication/description.txt";

			//    Console.WriteLine("<h2>" + path + "</h2>");
			//    Console.WriteLine("<p>");
			//    if (File.Exists(path))
			//    {
			//        Console.WriteLine(File.ReadAllText(path));
			//    }
			//    Console.WriteLine("</p>");

			//    Console.WriteLine("<p><img src='" + "assets/WebApplication/tongue.gif" + "' /> hello world (php)</p>");

			//    Native.Link("see html for javascript OrcasPHPScriptApplicationDocument", "OrcasPHPScriptApplicationDocument.htm");

			//    Native.Dump(
			//        new { hello = "world" }
			//    );




			//    var kk = new StringBuilder();
			//    var k = kk.Append("hello").Append(" world");

			//    Console.WriteLine(
			//        k.ToString()
			//    );

			//    Console.WriteLine(
			//        new { hello = "world" }
			//    );

			//}

			Console.WriteLine("<hr />");

			//Native.API.phpinfo();

			Console.WriteLine("</body>");
			Console.WriteLine("</html>");

		}
	}
}
