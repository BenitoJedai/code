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
			Native.API.set_time_limit(3);

			//ScriptCoreLib.PHP.BCLImplementation.System.IO.__StreamReader_Test.Assert();
			var start = Native.API.microtime(true);

			Console.WriteLine("start: " + Native.API.time());

			//ShowExampleDotCom();



			
			var crawler = new BasicWebCrawler("thepiratebay.org", 80);

			var headers = 0;

			crawler.HeaderReceived += delegate
			{
				headers++;
				Console.WriteLine(".");

			};

			crawler.AllHeadersReceived +=
				delegate
				{
					Native.API.set_time_limit(6);

					{
						var stop = Native.API.microtime(true);

						Console.WriteLine("header stop: " + stop);
						Console.WriteLine("header elapsed: " + (stop - start));

						start = Native.API.microtime(true);
					}
				};

			crawler.DataReceived +=
				document =>
				{
					Console.WriteLine(document.Length + " bytes");


					var results = document.IndexOf("<table id=\"searchResult\">");
					var headend = document.IndexOf("</thead>", results);

					var DefaultFields = new
					{
						Type = "",
						Name = "",
						Time = "",
						Links = "",
						Size = "",
						Seeders = "",
						Leechers = "",
					};

					Func<int, int> ScanSingleResult =
						offset =>
						{
							var itemstart = document.IndexOf("<tr>", offset);
							var itemend = document.IndexOf("</tr>", itemstart);


							var itemdata = document.Substring(itemstart, itemend - itemstart);



							//<tr>
							//<td class="vertTh"><a href="/browse/205" title="More from this category">Video &gt; TV shows</a></td>
							//<td><a href="/torrent/4727946/Heroes.S03E16.HDTV.XviD-XOR.avi" class="detLink" title="Details for Heroes.S03E16.HDTV.XviD-XOR.avi">Heroes.S03E16.HDTV.XviD-XOR.avi</a></td>
							//<td>Today&nbsp;04:55</td>
							//<td><a href="http://torrents.thepiratebay.org/4727946/Heroes.S03E16.HDTV.XviD-XOR.avi.4727946.TPB.torrent" title="Download this torrent"><img src="http://static.thepiratebay.org/img/dl.gif" class="dl" alt="Download" /></a><img src="http://static.thepiratebay.org/img/icon_comment.gif" alt="This torrent has 22 comments." title="This torrent has 22 comments." /><img src="http://static.thepiratebay.org/img/vip.gif" alt="VIP" title="VIP" style="width:11px;" /></td>
							//<td align="right">348.97&nbsp;MiB</td>
							//<td align="right">47773</td>
							//<td align="right">60267</td>

							//Console.WriteLine("<h1>Most Popular video</h1>");
							//Console.WriteLine("<table>");

							// type, name, uploaded, links, size, se, le

							var Fields = DefaultFields;

							Action<string> SetField = null;

							SetField = Type =>
							SetField = Name =>
							SetField = Time =>
							SetField = Links =>
							SetField = Size =>
							SetField = Seeders =>
							SetField = Leechers =>
							{
								Fields = new { Type, Name, Time, Links, Size, Seeders, Leechers };

								SetField = delegate { };
							};


							var ep = new BasicElementParser();

							ep.AddContent +=
								(value, index) =>
								{
									//Console.WriteLine("AddContent start #" + index);
									SetField(value);
									//Console.WriteLine("AddContent stop #" + index);
								};

							ep.Parse(itemdata, "td");

							Console.WriteLine("<p>");
							Console.WriteLine(Fields.Name + "<br />");
							Console.WriteLine(Fields.Size + "<br />");
							Console.WriteLine(Fields.Seeders + "<br />");
							Console.WriteLine(Fields.Leechers + "<br />");
							Console.WriteLine("</p>");

							return itemend + 5;
						};


					Console.WriteLine("<h2>Top Movies</h2>");

					var resultend = ScanSingleResult.ToChainedFunc(3)(headend);


				};




			crawler.Crawl("/top/200");

			{
				var stop = Native.API.microtime(true);

				Console.WriteLine("stop: " + stop);
				Console.WriteLine("elapsed: " + (stop - start));
			}


			// http://thepiratebay.org/top/200


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

		private static void ShowExampleDotCom()
		{
			var crawler = new BasicWebCrawler("example.com", 80);

			var headers = 0;

			crawler.HeaderReceived += delegate { headers++; };

			crawler.DataReceived +=
				document =>
				{
					document = document.Replace(
						"reached this web page",
						"<b>received " + headers + " HTTP header(s)</b> and you have reached this web page"
					);

					document = document.Replace(
						"are reserved for use in documentation and",
						"are reserved for use in documentation <b>including examples</b> and"
					);

					Console.Write(document);
				};

		


			crawler.Crawl("/");
		}
	}
}
