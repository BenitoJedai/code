using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System.Web;
using System;
using System.Net;
using ScriptCoreLib.Shared.Drawing;
using UltraTutorial08.HTML.Pages.FromWeb;

namespace UltraTutorial08
{
	public class SecondaryApplication
	{
		// jsc libraries referenced here are to be loaded ad hoc
	}

	public sealed partial class UltraApplication : IWebServiceEnabled
	{
		public UltraApplication(IHTMLElement e)
		{
			//{ HTML.Pages.FromWeb.Browsers x; }
			//{ HTML.Images.FromBase64._troll__by_GirlFlash x; }
			//{ new HTML.Images.FromBase64._troll__by_GirlFlash(); }
			//{ new HTML.Images.FromBase64._troll__by_GirlFlash().AttachToDocument(); }

			////new HTML.Images.FromBase64._troll__by_GirlFlash().AttachToDocument();
			Start1();
		}

		private void Start1()
		{
			var Title = new IHTMLDiv
			{

			};

			new Browsers
			{
			}.Container.AttachTo(Title);

			new HTML.Images.FromBase64.twitter_small().AttachToDocument();
			new HTML.Images.FromAssets.twitter_small().AttachToDocument();

			


			var TitleLogo = new IHTMLImage("assets/ScriptCoreLib/jsc.png");
			var TitleText = new IHTMLSpan("UltraApplication");
			TitleText.style.fontFamily = ScriptCoreLib.JavaScript.DOM.IStyle.FontFamilyEnum.Verdana;
			TitleText.style.paddingLeft = "2em";
			TitleText.style.fontSize = "xx-large";
			TitleLogo.style.verticalAlign = "middle";


			Title.appendChild(TitleLogo);
			Title.appendChild(TitleText);

			Title.style.height = "128px";

			Title.AttachToDocument();
			Title.FadeIn(2500, 1000,
				delegate
				{
					1500.AtDelay(
						delegate
						{
							ContinueBuildingApplication();
						}
					);
				}
			);
		}

		partial void ContinueBuildingApplication();


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

	public sealed partial class AlphaWebService : UltraTutorial08.IAlphaWebService
	{
		// should we support fields?
		// should we support POCO?

		public void DownloadData(string url, DownloadDataResult result)
		{
			var c = new WebClient();

			result(c.DownloadString(new Uri(url)));
		}

		// can we have yield return here?
		// can we have linq to sql?
	}

}
