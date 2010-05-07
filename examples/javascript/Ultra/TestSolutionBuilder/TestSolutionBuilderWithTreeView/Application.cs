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
using ScriptCoreLib;
using System.Collections.Generic;

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


			var _Properties = _Project.Add("Properties");
			_Properties.IsExpanded = true;
			_Properties.WithIcon(() => new SolutionProjectProperties());

			var _References = _Project.Add("References");
			_References.IsExpanded = false;
			_References.WithIcon(() => new References());

		
			sln.References.WithEach(
				k =>
				{
					var _Reference = _References.Add(k.Attribute("Include").Value.TakeUntilIfAny(","));
					_Reference.IsExpanded = true;
					_Reference.WithIcon(() => new Assembly());
				}
			);

			var FolderLookup = new Dictionary<string, TreeNode>();
			var FileLookup = new Dictionary<SolutionFile, TreeNode>();

			FolderLookup[_Properties.Text] = _Properties;


			h.LeftContainer = v.Container;
			h.RightContainer = _Solution.Container;


			var files = sln.ToFiles();

			files.WithEach(
				f =>
				{
					var ProjectInclude = f.Name.SkipUntilIfAny("/").SkipUntilIfAny("/");

					var Folder = ProjectInclude.TakeUntilLastOrEmpty("/");

					if (!string.IsNullOrEmpty(Folder))
					{
						if (!FolderLookup.ContainsKey(Folder))
							FolderLookup[Folder] = _Project.Add(Folder);
					}
				}
			);

			files.WithEach(
				(SolutionFile f) =>
				{
					var n = default(TreeNode);

					var Extension = "." + f.Name.SkipUntilLastIfAny(".");

					if (Extension == ".sln")
					{
						n = _Solution;
					}
					else if (Extension == sln.Language.ProjectFileExtension)
					{
						n = _Project;

						n.Element.TextArea.style.fontWeight = "bold";
					}
					else
					{
						var ProjectInclude = f.Name.SkipUntilIfAny("/").SkipUntilIfAny("/");

						var Folder = ProjectInclude.TakeUntilLastOrEmpty("/");

						var Parent = _Project;

						if (!string.IsNullOrEmpty(Folder))
						{
							Parent = FolderLookup[Folder];
						}

						if (f.DependentUpon != null)
						{
							Parent = FileLookup[f.DependentUpon];
						}

						n = Parent.Add(ProjectInclude.SkipUntilLastIfAny("/"));

						FileLookup[f] = n;
					}

					if (Extension == ".cs")
						n.WithIcon(() => new VisualCSharpCode());
					if (Extension == ".csproj")
						n.WithIcon(() => new VisualCSharpProject());
					if (Extension == ".htm")
						n.WithIcon(() => new HTMLDocument());

					if (f.DependentUpon != null)
					{
						n.WithIcon(() => new SolutionProjectDependentUpon());
					}

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
