using System;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript;
using System.Net;
using ScriptCoreLib.Ultra.Library.Extensions;
using System.Xml.Linq;

namespace YoutubeCaptions
{

	//[Description("YoutubeCaptions. Write javascript, flash and java applets within a C# project.")]

	public sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "Ultra Application";

			new IHTMLDiv(
					"downloading..."
				).AttachToDocument();

			"".ToCaptions(
				x =>
				{
					new IHTMLDiv(
						"done!"
					).AttachToDocument();


					foreach (var item in x.Root.Elements("text"))
					{
						new IHTMLDiv(
							item.Value
						).AttachToDocument();
					}
				}
			);
		}


	}

	public static class MyExtensions
	{
		public static void ToCaptions(this string video, Action<XDocument> e)
		{

			new UltraWebService().GetCaptions(video,
				xml =>
				{
					e(XDocument.Parse(xml));
				}
			);
		}
	}

	public delegate void StringAction(string e);

	public sealed class UltraWebService
	{
		public void GetCaptions(string video, StringAction result)
		{
			if (string.IsNullOrEmpty(video))
				video = "LT_x9s67yWA";

			var uri = new Uri("http://www.youtube.com/watch?v=" + video);

			var c = new WebClient();

			var html = c.DownloadString(uri);

			var ttsurl = Uri.UnescapeDataString(html.SkipUntilIfAny("\"ttsurl\":").SkipUntilIfAny("\"").TakeUntilIfAny("\""));

			// http://video.google.com/timedtext?
			//	sparams=caps%2Cexpire%2Cv&
			//	expire=1268420400&
			//	caps=asr&
			//	key=yttt1&
			//	signature=8F7DADCF868F8302AD31C92F5D4F54532F24583E.9AD0492582C05330E9CFA753B1298D71BE71FD5F

			// http://video.google.com/timedtext?
			//	sparams=caps%2Cexpire%2Cv
			//	expire=1268420400&
			//	caps=asr&
			//	key=yttt1&
			//	signature=8F7DADCF868F8302AD31C92F5D4F54532F24583E.9AD0492582C05330E9CFA753B1298D71BE71FD5F&

			//	name=&
			//	v=LT_x9s67yWA&
			//	lang=en&
			//	type=track&
			//	hl=en&
			//	kind=asr&
			//	ts=1268397349057&

			ttsurl += "&name=" + "";
			ttsurl += "&v=" + video;
			ttsurl += "&lang=" + "en";
			ttsurl += "&type=" + "track";
			ttsurl += "&hl=" + "en";
			ttsurl += "&kind=" + "asr";
			ttsurl += "&ts=" + "";

			var tts = c.DownloadString(new Uri(ttsurl));

			result(tts);
		}
	}
}
