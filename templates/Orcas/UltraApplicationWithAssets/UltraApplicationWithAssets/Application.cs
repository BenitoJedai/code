using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using UltraApplicationWithAssets.HTML.Audio.FromAssets;
using System.ComponentModel;
using UltraApplicationWithAssets.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;

namespace UltraApplicationWithAssets
{

	[Description("UltraApplicationWithAssets. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAbout a)
		{
			Data.MyDocument.CreateAsElement(
				Document =>
				{
					var q = Enumerable.ToArray(
						from x in Document.Elements("Data")
						let Color = x.Element("Color").Value
						let Text = x.Element("Text").Value
						let div = new IHTMLDiv { innerText = Text }.Apply(k => k.style.color = Color)
						select div.AttachToDocument()
					);
				}
			);

			Audio.XMLSource.CreateAsElement(
				xaudio =>
				{
					new IHTMLPre { innerText = xaudio.ToString() }.AttachToDocument();
				}
			);

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

	}


}
