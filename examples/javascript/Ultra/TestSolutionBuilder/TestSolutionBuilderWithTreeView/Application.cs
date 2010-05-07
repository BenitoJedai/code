using System;
using System.ComponentModel;
using System.Linq;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Library.Extensions;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Extensions;

namespace TestSolutionBuilderWithTreeView
{

	[Description("TestSolutionBuilderWithTreeView. Write javascript, flash and java applets within a C# project.")]
	public sealed partial class Application
	{
		public Application(IApplicationLoader a)
		{
			a.LoadingAnimation.Orphanize();

			var h = new HorizontalSplit
			{

			};

			h.Container.AttachTo(a.Content);
			var sln = new SolutionBuilder();
			var v = new SolutionFileView();

			v.Container.style.height = "100%";

			// phase 1 has only one project at once.

			var _Solution = new TreeNode(VistaTreeNodePage.Create);
			_Solution.Text = "Solution '" + sln.Name + "' (1 project)";
			_Solution.IsExpanded = true;

			var _Project = _Solution.Add(sln.Name);
			_Project.IsExpanded = true;

			h.LeftContainer = v.Container;
			h.RightContainer = _Solution.Container;

			var files = sln.ToFiles();

			files.WithEach(
				(SolutionFile f) =>
				{
					var n = _Project.Add(f.Name.SkipUntilIfAny("/").SkipUntilIfAny("/"));

					var Extension = "." + f.Name.SkipUntilLastIfAny(".");

					if (Extension == ".cs")
						n.Element.OpenImage = new VisualCSharpCode();
					if (Extension == ".csproj")
						n.Element.OpenImage = new VisualCSharpProject();
					if (Extension == ".htm")
						n.Element.OpenImage = new HTMLDocument();


					n.IsExpanded = true;

					n.Click +=
						delegate
						{
							v.File = f;
						};
				}
			);
		}
	}


}
