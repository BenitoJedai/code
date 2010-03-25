using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using UltraApplicationWithAssets3.HTML.Audio.FromAssets;
using System.ComponentModel;

namespace UltraApplicationWithAssets3
{

	[Description("UltraApplicationWithAssets3. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "UltraApplicationWithAssets3";


			//var logo2 = new global::UltraLibraryWithAssets.HTML.Images.FromAssets.jsc();

			var a = new HTML.Pages.FromAssets.AboutJSC();

			//a.Logo1 = logo2;

			a.Container.AttachToDocument();


			a.WebService_GetTime.onclick +=
				delegate
				{
					a.WebServiceContainer.removeChildren();
					a.WebServiceContainer.Add(new IHTMLDiv("loading.."));

					new UltraWebService().GetTime("client at " + DateTime.Now,
						result =>
						{
							new IHTMLDiv { innerText = result }.AttachTo(a.WebServiceContainer);

						}
					);

				};

			a.Inline1.onclick +=
				delegate
				{
					try
					{
						// are we running HTML5 browser
						new rooster().play();
					}
					catch
					{
						// no? :)
					}
				};

			a.Inline1.onclick +=
				delegate
				{
					new Timer(
						delegate
						{
							a.Inline1.style.color = "";
						}
					).StartTimeout(1000);
				};


		}


	}


}
