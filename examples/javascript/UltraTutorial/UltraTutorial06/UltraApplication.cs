using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Web;
using System;
using System.Net;
using ScriptCoreLib.Shared.Drawing;

namespace UltraTutorial06
{
	public sealed partial class UltraApplication : IWebServiceEnabled
	{
		public UltraApplication(IHTMLElement e)
		{
			var Title = new IHTMLDiv();

			var TitleLogo = new IHTMLImage("assets/ScriptCoreLib/jsc.png");
			var TitleText = new IHTMLSpan("UltraApplication");
			TitleText.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Helvetica;
			TitleText.style.paddingLeft = "2em";
			TitleLogo.style.verticalAlign = "middle";
			Title.appendChild(TitleLogo);
			Title.appendChild(new IHTMLSpan("UltraApplication"));

			Title.style.height = "64px";
			Title.style.fontSize = "large";

			Title.AttachToDocument();
			Title.FadeIn(2500, 1000,
				delegate
				{
					1500.AtDelay(ContinueBuildingApplication);
				}
			);
		}

		private void ContinueBuildingApplication()
		{

			var note1 = new IHTMLPre
			{
				innerHTML = @"Notice: If flash does not respond to events, you need to clear your cache. 
+ Chrome flash in 'localhost' on Cassini always fails?
  - If so Try http://127.0.0.1 or http://COMPUTERNAME
+ Opera does not pass delegates?
+ IE cannot return from javascript to flash
+ Web page could be delivered within flash package
+ Javascript rewrite could omit unused types and methods
"

			}.AttachToDocument();

			note1.style.whiteSpace = ScriptCoreLib.JavaScript.DOM.IStyle.WhiteSpaceEnum.pre;
			note1.style.fontSize = "small";
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

			// passing the interface to flash is delayed until it is loaded, using the getter will fault
			s.WebService = new AlphaWebService();
			s.WebServiceEnabled = this;

			s.AppendLine("This call is delayed until flash is loaded (4)");

			new IHTMLBreak().AttachToDocument();


			this.WebServiceEnabled = new IHTMLInput(ScriptCoreLib.Shared.HTMLInputTypeEnum.checkbox);

			var WebServiceEnabledLabel = new IHTMLLabel("WebService is enabled for flash", this.WebServiceEnabled);

			new IHTMLDiv(
				WebServiceEnabledLabel,
				WebServiceEnabled
			).AttachToDocument();

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


		public IHTMLInput WebServiceEnabled;

		#region IWebServiceEnabled Members

		public string IsEnabled
		{
			get { return Convert.ToString(WebServiceEnabled.@checked); }
		}

		#endregion
	}

	public delegate void DownloadDataResult(string e);

	public sealed partial class AlphaWebService : UltraTutorial06.IAlphaWebService
	{

		public void DownloadData(string url, DownloadDataResult result)
		{
			var c = new WebClient();

			result(c.DownloadString(url));
		}
	}

}
