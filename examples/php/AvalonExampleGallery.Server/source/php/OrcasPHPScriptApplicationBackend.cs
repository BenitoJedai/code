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

			Console.WriteLine("<style> body { background: url('assets/AvalonExampleGallery/bg.png'); } </style>");

			Console.WriteLine(
				new { hello = "world" }
			);

			var ShadowSource = "assets/AvalonExampleGallery/PreviewShadow.png";

			var OptionWidth = 166 + 9;
			var OptionHeight = 90 + 18 + 4 + 20;

			Func<string, string, IHTMLElement> CreateOption =
				(Path, Text) =>
				new IHTMLElement
				{
					
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
								color = "white"
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

			Console.WriteLine(
				CreateOption(pages::NavigationButtons.Assets.Shared.KnownAssets.Path.Assets, "NavigationButtons").MoveTo(300, 200).ToString()
			);

			Console.WriteLine(
				CreateOption(pages::TextSuggestions.Shared.KnownAssets.Path.Assets, "TextSuggestions").MoveTo(300 + 140, 200).ToString()
			);


			Console.WriteLine(

				"AvalonExampleGalleryDocument.htm".ToLink(
					(pages::NavigationButtons.Assets.Shared.KnownAssets.Path.Assets + "/Preview.png").ToImage()
				)
			);



			Console.WriteLine("</body>");
			Console.WriteLine("</html>");
		}
	}
}
