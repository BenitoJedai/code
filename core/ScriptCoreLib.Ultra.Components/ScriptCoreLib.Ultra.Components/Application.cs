using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Components.HTML.Pages;

namespace ScriptCoreLib.Ultra.Components
{

	[Description("ScriptCoreLib.Ultra.Components. Write javascript, flash and java applets within a C# project.")]
	internal sealed partial class Application
	{
		public Application(IHTMLElement e)
		{
			var p = new ApplicationLoader.FromDocument();

			p.Content.Add("hello world");
			
			Native.Document.title = "Hi!";

			{
				var s = new Section().ToSectionConcept();

				s.Header = "Syntax1";

				s.Target.Container.AttachTo(p.Content);
			}

			{
				var s = new Section().ToSectionConcept();

				s.Header = "Methods2";

				s.Target.Container.AttachTo(p.Content);
			}
		}


	}


}
