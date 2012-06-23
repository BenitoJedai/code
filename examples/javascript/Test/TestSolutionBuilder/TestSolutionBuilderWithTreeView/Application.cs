using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ScriptCoreLib;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Studio.Languages;

namespace TestSolutionBuilderWithTreeView
{

	[Description("TestSolutionBuilderWithTreeView. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IApplicationLoader a)
		{
			a.LoadingAnimation.Orphanize();

			var h = CodeView.CreateView();

			h.Container.AttachTo(a.Content);
		}

	}


}
