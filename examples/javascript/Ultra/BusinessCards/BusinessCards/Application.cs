using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using BusinessCards.HTML.Audio.FromAssets;
using System.ComponentModel;
using BusinessCards.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Extensions;

namespace BusinessCards
{

	[Description("BusinessCards. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAbout a)
		{
			Native.Document.title = "BusinessCards";

			8.Times(
				delegate()
				{
					new Card().With(k => k.Content.style.margin = "1em").Content.AttachTo(a.Foo);
				}
			);
		}

	}


}
