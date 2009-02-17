using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.PHP;
using System;
using System.Text;
using System.IO;
using MovieBlog.Client.Avalon;
using MovieBlog.Client.Java;
using MovieBlog.Shared;
using System.Net;
using System.Net.Sockets;

namespace MovieBlog.Server
{
	[Script]
	static class Application
	{
		public const string Filename = "index.php";

		// change: C:\util\xampplite\apache\conf\httpd.conf

		// http://localhost/jsc/MovieBlog

		//Alias /jsc/MovieBlog "C:\work\jsc.svn\examples\php\MovieBlog\MovieBlog\bin\Release\web"
		//<Directory "C:\work\jsc.svn\examples\php\MovieBlog\MovieBlog\bin\Release\web">
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
			var t = new TcpClient();

			t.Connect("www.google.ee", 80);

			var w = new StreamWriter(t.GetStream());

			w.WriteLine("GET / HTTP/1.0");
			w.WriteLine("Host: www.google.ee");
			w.WriteLine("Connection: Close");
			w.WriteLine();
			w.Flush();

			//var r = new StreamReader(t.GetStream());
			var r = t.GetStream();

			var i = r.ReadByte();

			while (i != -1)
			{

				var text = new string((char)i, 1);

				Console.Write(text);

				i = r.ReadByte();
			}

			//Console.WriteLine(r.ReadToEnd());


			t.Close();



			//var fp = Native.API.fsockopen("www.google.ee", 80);

			//if (fp == null)
			//{
			//    Console.WriteLine("error");
			//}
			//else
			//{
			//    Native.API.fwrite(fp, "GET / HTTP/1.0\r\n");
			//    Native.API.fwrite(fp, "Host: www.google.ee\r\n");
			//    Native.API.fwrite(fp, "Connection: Close\r\n\r\n");


			//    while (!Native.API.feof(fp))
			//    {
			//        Console.Write(Native.API.fgets(fp, 128));
			//    }

			//    Native.API.fclose(fp);

			//}


			//Console.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
			//Console.WriteLine("<html>");
			//Console.WriteLine("<head>");

			//Console.WriteLine("<meta name='description' content='jsc can convert your C# Application to PHP, JavaScript, Actionscript and Java' />");
			//Console.WriteLine("<meta name='keywords' content='c# decompiler, cross compiler, flash, actionscript, php, java, javascript, ajax, web2, dhtml, jsc' />");
			//Console.WriteLine("<link rel='shortcut icon' href='" + KnownAssets.Path.Assets + "/App.ico" + "' />");
			//Console.WriteLine("<link rel='alternate' type='application/rss+xml' title='RSS 2.0' href='http://zproxy.wordpress.com/feed/' />");


			//Console.WriteLine("<title>" + "AvalonWebApplication".WithBranding() + "</title>");

			//Console.WriteLine("<link rel='stylesheet' type='text/css' href='assets/MovieBlog/WebPage.css' />");
			//Console.WriteLine("</head>");
			//Console.WriteLine("<body>");

			//(KnownAssets.Path.Assets + "/jsc.png").ToImageToConsole();

			//Console.WriteLine("<h1>Congratulations!</h1><h2>You are using jsc compiler to convert your C# Application to PHP, JavaScript, Actionscript and Java!</h2>");
			//Console.WriteLine("<h3>" + "C# To PHP".WithBranding() + "</h3>");


			//(KnownAssets.Path.Assets + "/diagram_jsc.png").ToImageToConsoleWithStyle(
			//    "border: 1px solid gray; background: white; padding: 1em;"
			//);

			//Console.WriteLine("<br />");

			//Native.Link(SharedExtensions.HomePageText, SharedExtensions.HomePage);
			//Console.WriteLine("<br />");
			//Console.WriteLine("<br />");
			//Native.Link("View Source", SharedExtensions.TemplateSourceCode);
			//Console.WriteLine("<hr />");

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

			//Native.Link("See more of this application over here!", "?more");

			//Console.WriteLine("<hr />");

			//Action<int, int, string> CreateIFrame =
			//    (w, h, src) => Console.WriteLine("<iframe style='border: 0;' width='" + w + "' height='" + h + "' src='" + src + "' ></iframe>"); ;

			//Action ShowActionScript = () => CreateIFrame(AvalonCanvas.DefaultWidth, AvalonCanvas.DefaultHeight, "AvalonFlash.htm");
			//Action ShowJavaScript = () => CreateIFrame(AvalonCanvas.DefaultWidth, AvalonCanvas.DefaultHeight, "AvalonDocument.htm");
			//Action ShowJavaApplet = () => CreateIFrame(ApplicationApplet.DefaultWidth, ApplicationApplet.DefaultHeight, "ApplicationApplet.htm");



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

			//if (Native.QueryString == "javascript_actionscript_javaapplet")
			//{
			//    ShowJavaScript();
			//    ShowActionScript();
			//    ShowJavaApplet();

			//}

			//if (Native.QueryString == "more")
			//{
			//    var path = "assets/MovieBlog/description.txt";

			//    Console.WriteLine("<h2>" + path + "</h2>");
			//    Console.WriteLine("<p>");
			//    if (File.Exists(path))
			//    {
			//        Console.WriteLine(File.ReadAllText(path));
			//    }
			//    Console.WriteLine("</p>");

			//    Console.WriteLine("<p><img src='" + "assets/MovieBlog/tongue.gif" + "' /> hello world (php)</p>");

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

			//Console.WriteLine("<hr />");

			////Native.API.phpinfo();

			//Console.WriteLine("</body>");
			//Console.WriteLine("</html>");

		}
	}
}
