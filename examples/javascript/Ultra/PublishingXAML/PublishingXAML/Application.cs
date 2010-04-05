using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using PublishingXAML.HTML.Audio.FromAssets;
using System.ComponentModel;
using PublishingXAML.HTML.Pages;
using PublishingXAML.Data;

namespace PublishingXAML
{

	[Description("PublishingXAML. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IAboutJSC a)
		{
			new IHTMLIFrame { src = "/" + new DrawingSource().Name }.AttachToDocument();

		
		}

	}


}
