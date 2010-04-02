using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using UltraApplicationWithAssets.HTML.Audio.FromAssets;
using System.ComponentModel;
using UltraApplicationWithAssets.HTML.Pages;

namespace UltraApplicationWithAssets
{

	[Description("UltraApplicationWithAssets. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public void Application_ctor(IAboutJSC a)
		{
			Native.Document.title = "UltraApplicationWithAssets";

			a.WebService_GetTime.onclick +=
				delegate
				{

					new UltraWebService().GetTime("time: ",
						result =>
						{
							new IHTMLPre { innerText = result }.AttachTo(a.WebServiceContainer);

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

		public Application(IHTMLElement e)
		{
			var a = new HTML.Pages.FromAssets.AboutJSC();

			a.Container.AttachToDocument();
			Application_ctor(a);
		}


	}


}
