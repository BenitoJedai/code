extern alias pages;


using ScriptCoreLib;
using ScriptCoreLib.Shared;

using ScriptCoreLib.PHP;
using System;
using System.Text;
using System.IO;

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
			// http://::1/jsc/AvalonExampleGallery

			Console.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
			Console.WriteLine("<html>");

			new IHTMLHead
			{
				Content =
					(IHTMLMeta.Description)"Avalon Gallery Example powered by jsc, C#, javascript and php"
					+ (IHTMLMeta.Keywords)"wpf, avalon, avalonexamplegallery, jsc, csharp, javascript, php"
					+ (IHTMLMeta.Generator)"jsc"
					+ new IHTMLMeta { MetaName = "ROBOTS", MetaContent = "ALL" }
					+ new IHTMLMeta { MetaName = "verify-v1", MetaContent = "daj8Q3pV1gvuI3RZ/5TMzUuZwqmEZDPVNBHrXJZ3sUU=" }
					+ (IHTMLLink.RSS)"http://zproxy.wordpress.com/feed/"
					+ (IHTMLTitle)"AvalonGalleryExample (PHP + JavaScript)"
					+ (IHTMLStyle)@"
						body { background: url('assets/AvalonExampleGallery/bg.png'); }
						a { text-decoration: none; color: white; }
						a:hover { text-decoration: underline; color: blue; }

					"
			}.ToConsole();


			Console.WriteLine("<body>");

			var VersionXBAP = new IHTMLAnchor { URL = "AvalonExampleGallery.XBAP.xbap", Content = "XBAP Version" };
			var VersionFlash = new IHTMLAnchor { URL = "AvalonExampleGalleryFlash.htm", Content = "Flash Version" };

			if (!File.Exists(VersionXBAP.URL))
				VersionXBAP.Content = "<s>" + VersionXBAP.Content + "</s>";

			if (!File.Exists(VersionFlash.URL))
				VersionFlash.Content = "<s>" + VersionFlash.Content + "</s>";


			new IHTMLElement
			{
				Content =
					VersionXBAP + new IHTMLBreak()
					+ VersionFlash + new IHTMLBreak()
					+ new IHTMLAnchor { URL = "AvalonExampleGalleryDocument.htm", Content = "JavaScript Version" } + new IHTMLBreak()
					+ new IHTMLBreak()
					+ new IHTMLAnchor { URL = "http://jsc.sourceforge.net", Content = "visit jsc" } + new IHTMLBreak()
					+ new IHTMLAnchor { URL = "http://zproxy.wordpress.com", Content = "visit blog" } + new IHTMLBreak()
					+ new IHTMLAnchor { URL = "http://groups.google.com/group/jscsharp?pli=1", Content = "visit groups" } + new IHTMLBreak()

			}.MoveTo(32, 32).ToConsole();


			var Container = CreateOptions();

			Console.WriteLine(
				new IHTMLElement
				{
					Name = "center",
					Content = Container
				}
			);

			var f = ScriptCoreLib.PHP.IO.FileInfo.OfPath(Filename + ".js");

			Console.WriteLine("<!-- " + f.Size + " -->");

			f.WriteToStream();

			#region stats
			new IHTMLScript
			{
				Content = @"
					var sc_project=1795466; 
					var sc_invisible=0; 
					var sc_partition=16; 
					var sc_security=""de368db1""; 
					var sc_text=3; 
				"
			}.ToConsole();

			new IHTMLElement.Hidden
			{
				Content = new IHTMLScript { URL = "http://www.statcounter.com/counter/counter.js" }
			}.ToConsole();

			new IHTMLScript { URL = "http://track2.mybloglog.com/js/jsserv.php?mblID=2006091009424673" }.ToConsole();
			new IHTMLScript
			{
				Content = @"
					var gaJsHost = ((""https:"" == document.location.protocol) ? ""https://ssl."" : ""http://www."");
					document.write(unescape(""%3Cscript src='"" + gaJsHost + ""google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E""));
				"
			}.ToConsole();

			new IHTMLScript
			{
				Content = @"
					var pageTracker = _gat._getTracker(""UA-578264-1"");
					pageTracker._trackPageview();
				"
			}.ToConsole();

			#endregion


			Console.WriteLine("</body>");
			Console.WriteLine("</html>");
		}

		private static IHTMLElement CreateOptions()
		{
			var ShadowSource = "assets/AvalonExampleGallery/PreviewShadow.png";

			var OptionWidth = 166 + 9;
			var OptionHeight = 90 + 18 + 4 + 20;

			#region CreateOption
			Func<string, string, IHTMLElement> CreateOption =
				(Path, Text) =>
				new IHTMLElement
				{
					Class = Text,
					Content =
						new IHTMLImage
						{
							Source = ShadowSource
						}.MoveTo(9, 9)
						+ new IHTMLImage
						{
							Source = (Path + "/Preview.png")
						}.MoveTo(9, 9)
						+ new IHTMLInput
						{
							Type = "text",
							Value = Text,
							IsReadOnly = true,
							Style = new IStyle
							{
								textAlign = "center",
								backgroundColor = "transparent",
								borderWidth = "0px",
								color = "white",
								paddingTop = "0"
							}
						}.MoveTo(0, 104).SizeTo(120 + 9 * 2, 20)
						+ new IHTMLElement
						{
							Style = new IStyle
							{
								backgroundColor = "black",
								opacity = 0
							}
						}.MoveTo(0, 0).SizeTo(OptionWidth, OptionHeight)

				}.SizeTo(OptionWidth, OptionHeight);
			#endregion


			var Container = new IHTMLElement
			{
				Style = new IStyle
				{
					//backgroundColor = "red",
					position = "relative"
				},
				Class = "AvalonExampleGalleryContainer",
				Content = ""
			}.SizeTo(
				800,
				640
			);

			var i = 0;

			CreateOption.StreamTo(
				Option =>
				{
					Container.Content +=
						Option.MoveTo(
							48 + (180) * (i % 4),
							48 + (int)Math.Floor((double)i / 4) * 140
						);

					i++;
				}
			);
			return Container;
		}
	}
}
