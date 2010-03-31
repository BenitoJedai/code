using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Components.HTML.Pages.FromAssets;
using ScriptCoreLib.JavaScript.Concepts;

namespace ScriptCoreLib.Ultra.Components
{

	[Description("ScriptCoreLib.Ultra.Components. Write javascript, flash and java applets within a C# project.")]
	internal sealed partial class Application
	{

		public Application(IHTMLElement e)
		{
			Native.Document.title = "ScriptCoreLib.Ultra.Components";

			{
				var s = new Section().ToSectionConcept();

				s.Header = "Syntax";

				s.Target.Container.AttachToDocument();
			}

			{
				var s = new Section().ToSectionConcept();

				s.Header = "Methods";

				s.Target.Container.AttachToDocument();
			}
		}


	}


}
