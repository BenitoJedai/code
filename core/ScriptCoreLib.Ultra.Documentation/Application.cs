using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.Ultra.Documentation.HTML.Pages;
using System.Linq;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.JavaScript.Controls;

namespace ScriptCoreLib.Ultra.Documentation
{

	[Description("ScriptCoreLib.Ultra.Documentation. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IApplicationLoader a)
		{
			Native.Document.title = "ScriptCoreLib.Documentation";

			a.LoadingAnimation.FadeOut();

			new DocumentationCompilationViewer();
		}

	}


}
