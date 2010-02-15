using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Web;
using System;
using System.Net;
using ScriptCoreLib.Shared.Drawing;

namespace UltraTutorial04
{
	public sealed partial class UltraApplication
	{
		public UltraApplication(IHTMLElement e)
		{
			var note1 = new IHTMLDiv { innerHTML = "Notice: If flash does not respond to events, you need to clear your cache." }.AttachToDocument();

			note1.style.color = Color.Red;


			var s = new UltraSprite();

			s.AttachSpriteToDocument();

			var SayHello = new IHTMLButton { innerText = "Say Hello to flash!" }.AttachToDocument();

			SayHello.onclick +=
				delegate
				{
					s.AppendLine("Hello from javascript");

					s.WhenReady(
						delegate
						{
							s.AppendLine("What about using the web service?");
						}
					);
				};

			s.AppendLine("This call is delayed until flash is loaded (1)");
			s.AppendLine("This call is delayed until flash is loaded (2)");
			s.AppendLine("This call is delayed until flash is loaded (3)");

			s.WhenReady(
				delegate
				{
					s.AppendLine("What about using the web service?");
				}
			);

			s.AppendLine("This call is delayed until flash is loaded (4)");

			ButtonsForWebService();
		}

		private static void ButtonsForWebService()
		{
			AddButtonForGetTime();

			var url = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.text, "http://example.com");

			url.AttachToDocument();


			var DownloadData = new IHTMLButton { innerText = "DownloadData" }.AttachToDocument();

			DownloadData.onclick +=
				delegate
				{
					new AlphaWebService().DownloadData(url.value,
						x =>
						{
							new IHTMLPre { innerText = url.value + Environment.NewLine + Environment.NewLine + x }.AttachToDocument();
						}
					);
				};
		}


	}

	public delegate void DownloadDataResult(string e);

	public sealed partial class AlphaWebService : UltraTutorial04.IAlphaWebService
	{

		public void DownloadData(string url, DownloadDataResult result)
		{
			var c = new WebClient();

			result(c.DownloadString(url));
		}
	}

}
