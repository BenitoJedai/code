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



			Console.WriteLine("<title>AvalonGalleryExample (PHP + JavaScript)</title>");



			Console.WriteLine("<style> body { background: url('assets/AvalonExampleGallery/bg.png'); } </style>");


			Console.WriteLine("</head>");
			Console.WriteLine("<body>");




			var Container = CreateOptions();

			Console.WriteLine(
				new IHTMLElement
				{
					Name = "center",
					Content = Container
				}
			);


			ScriptCoreLib.PHP.IO.FileInfo.OfPath(Filename + ".js").WriteToStream();


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
