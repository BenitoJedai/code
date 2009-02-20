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
using System.Threading;

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
			// 5 sec is the limit
			Native.API.set_time_limit(4);
			//Native.API.error_reporting(2048);

			//ScriptCoreLib.PHP.BCLImplementation.System.IO.__StreamReader_Test.Assert();
			var start = Native.API.microtime(true);

			Console.WriteLine("start: " + Native.API.time());

			//ShowGoogleVideo();

			//ShowExampleDotCom();

			ShowPirateBay();

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

		private static void ShowGoogleVideo()
		{
			var c = new BasicGoogleVideoCrawler();

			c.VideoSourceFound +=
				(video, src) =>
				{
					Console.WriteLine(src.ToLink(src));

				};

			c.Search("terminator+trailer");


		}

		private static void ShowPirateBay()
		{
			Console.WriteLine("<style>");
			Console.WriteLine("img { border: 0; }");
			//Console.WriteLine("ol { -moz-column-count: 2; }");
			Console.WriteLine(@"
embed {
width: 100%;
height: 100%;
position: absolute; 
left: 0;
top: 0;
z-index: 0;
}

ol
{
display: block;
width: 100%;
height: 100%;
position: absolute; 
left: 0;
top: 0;
z-index: 1;

overflow: scroll;
}

li
{
color: white;
cursor:hand;
}

body{
	text-align: center;
	font-family:Verdana, Arial, Helvetica, sans-serif;
	font-size:.7em;
	margin: 10px;
	color: #fff;
	background: #fff;
	min-width: 520px;
	overflow: hidden;
}


a{
	color: #009;
	text-decoration: none;
	border-bottom: 1px dotted #4040D9;
}
a:hover{
	text-decoration: none;
	border-bottom: 1px solid #009;
}
		li { 
text-align: left;
margin: 1em;}	
			
			");
			Console.WriteLine("</style>");

			var DefaultLink = new { Link = "", Title = "", Text = "" };
			var DefaultImage = new { Source = "", Alt = "", Title = "" };

			var ParseLink = DefaultLink.ToAnonymousConstructor(
				(string element) =>
				{
					var Link = "";
					var Title = "";
					var Text = "";

					element.
						ParseAttribute("href", value => Link = value).
						ParseAttribute("title", value => Title = value).
						ParseContent(value => Text = value).
						Parse();

					return new { Link, Title, Text };
				}
			);

			var ParseImage = DefaultImage.ToAnonymousConstructor(
				(string element) =>
				{
					var Source = "";
					var Alt = "";
					var Title = "";

					element.
						ParseAttribute("src", value => Source = value).
						ParseAttribute("alt", value => Alt = value).
						ParseAttribute("title", value => Title = value).
						ParseContent(null).
						Parse();

					return new { Source, Alt, Title };
				}
			);


			var crawler = new BasicWebCrawler("thepiratebay.org", 80);
			var search = new BasicPirateBaySearch(crawler);

			crawler.AllHeadersReceived +=
				delegate
				{
					Native.API.set_time_limit(10);
				};

			search.Loaded +=
				ForEachEntry =>
				{


					//Console.WriteLine("<hr />");

					//var logo = "http://static.thepiratebay.org/img/tpblogo_sm_ny.gif";

					//Console.WriteLine(logo.ToImage().ToLink("http://tineye.com/search?url=" + logo));

					// http://code.google.com/apis/youtube/chromeless_example_1.html
					Console.WriteLine("<embed wmode='transparent' id='tv' src='http://www.youtube.com/apiplayer?enablejsapi=1&playerapiid=tv' allowScriptAccess='always' width='400' height='300' />");

					//Console.WriteLine("<h2>Top Movies</h2>");

					Console.WriteLine("<ol>");

					ForEachEntry(
						(entry, entryindex) =>
						{
							//if (entryindex > 2)
							//    return;

							var Type = ParseLink(entry.Type);
							var Name = ParseLink(entry.Name);

							var SmartName = new BasicFileNameParser(Name.Text);

							var c = new BasicGoogleVideoCrawler();

							var Video = "";
							var VideoSource = "";

							c.VideoSourceFound +=
								(video, src) =>
								{
									Video = video;
									VideoSource = src;


								};

							Native.API.set_time_limit(6);

							//Thread.Sleep(1500);

							c.Search(SmartName.Title + " trailer");

							Console.WriteLine("<li onmouseover='if (getElementById(\"tv\").getPlayerState() != 1) getElementById(\"tv\").loadVideoById(\"" + Video + "\")'>");


							Console.WriteLine("<b>" + SmartName.Title.ToLink(k => "http://www.imdb.com/find?s=tt;site=aka;q=" + k) + "</b>");

							if (!string.IsNullOrEmpty(SmartName.Season))
							{
								Console.WriteLine(" | Season <i>" + SmartName.Season + "</i>");
							}

							if (!string.IsNullOrEmpty(SmartName.Episode))
							{
								Console.WriteLine(" | Episode <i>" + SmartName.Episode + "</i>");
							}


							if (!string.IsNullOrEmpty(SmartName.SubTitle))
							{
								Console.WriteLine(" | <b>" + SmartName.SubTitle + "</b>");
							}

							if (!string.IsNullOrEmpty(SmartName.Year))
							{
								Console.WriteLine(" | <i>" + SmartName.Year + "</i>");
							}



							Console.WriteLine(" | ");
							Console.WriteLine("<b>");
							Console.WriteLine("trailer".ToLink(VideoSource, Video));
							Console.WriteLine("</b>");





							Console.WriteLine("<br />");

							Console.WriteLine("<small>");
							Console.WriteLine(SmartName.ColoredText.ToString().ToLink("http://thepiratebay.org" + Name.Link) + "<br />");


							Console.WriteLine(Type.Text.ToLink("http://thepiratebay.org" + Type.Link));


							entry.Links.ParseElements(
								(tag, index, element) =>
								{
									if (tag == "a")
									{
										var a = ParseLink(element);

										Console.WriteLine(" | " + "torrent".ToLink(a.Link));
									}

									if (tag == "img")
									{
										var img = ParseImage(element);

										if (img.Title.Contains("comment"))
										{
											Console.WriteLine(" | " + img.Title.ToLink("http://thepiratebay.org" + Name.Link));
										}
										else
										{
											Console.WriteLine(" | " + img.Title);
										}
									}
								}
							);

							Console.WriteLine(" | " + entry.Size);
							Console.WriteLine(" | " + entry.Seeders);
							Console.WriteLine(" | " + entry.Leechers + "<br />");


							Console.WriteLine("</small>");

							//Console.WriteLine("</div>");
							Console.WriteLine("</li>");


						}
					);

					Console.WriteLine("</ol>");
				};

			crawler.Crawl("/top/200");
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
