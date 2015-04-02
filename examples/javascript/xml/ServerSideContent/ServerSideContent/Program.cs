using jsc.meta.Commands.Rewrite.RewriteToUltraApplication;
using ServerSideContent.HTML.Pages;
using System;
using System.Xml.Linq;

namespace ServerSideContent
{
	/// <summary>
	/// You can debug your application by hitting F5.
	/// </summary>
	internal static class Program
	{
		public static void Main(string[] args)
		{
			var Textures = XElement.Parse(TexturesImagesSource.Text);

			// Textures = {<html>
			//  <body id="body">
			//    <img src="assets/ServerSideContent/thumb.jpg" id="thumb" />
			//  </body>
			//</html>}

			RewriteToUltraApplication.AsProgram.Launch(typeof(Application));
		}

	}
}
