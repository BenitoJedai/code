using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using CreatingXElements.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;

namespace CreatingXElements
{

	[Description("CreatingXElements. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAboutJSC a)
		{
			Native.Document.title = "CreatingXElements";

			a.XMLSource.value = DocumentBuilder.Create().ToString();

		}

	}


}
