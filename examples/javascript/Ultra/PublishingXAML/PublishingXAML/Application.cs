using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using PublishingXAML.HTML.Pages;
using PublishingXAML.Data;

namespace PublishingXAML
{

	[Description("PublishingXAML. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IDrawingPage a)
		{
			{
				var f = new IHTMLIFrame { src = "/" + new DrawingSource().Name };


				f.style.width = "100%";
				f.style.height = "50%";


				a.Frame1 = f;
			}


			{
				var f = new IHTMLIFrame { src = "/" + new Drawing2Source().Name };


				f.style.width = "100%";
				f.style.height = "50%";


				a.Frame2 = f;
			}
		}

	}


}
