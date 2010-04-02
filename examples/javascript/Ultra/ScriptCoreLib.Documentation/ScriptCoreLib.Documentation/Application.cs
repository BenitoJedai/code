using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.JavaScript;

namespace ScriptCoreLib.Documentation
{
	public sealed class Application
	{
		public Application(IApplicationLoader a)
		{
			Native.Document.title = "ScriptCoreLib.Documentation";

			a.LoadingAnimation.FadeOut();

			new CompilationViewer();
		}
	}
}
