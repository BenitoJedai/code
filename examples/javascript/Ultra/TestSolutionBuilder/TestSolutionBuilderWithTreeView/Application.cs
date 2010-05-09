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

			var h = new HorizontalSplit
			{

			};

			h.Container.AttachTo(a.Content);
			var sln = new SolutionBuilder
			{
				Name = "VisualBasicProject1",
				Language = new VisualBasicLanguage()
			};

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


			v.LinkCommentClick +=
				uri =>
				{
					if (uri == sln.Interactive.ToVisualCSharpLanguage.Link)
					{
						sln.Language = new VisualCSharpLanguage();

						_Project.Clear();
						UpdateTree(sln, v, _Solution, _Project);
						return;
					}

					if (uri == sln.Interactive.ToVisualBasicLanguage.Link)
					{
						sln.Language = new VisualBasicLanguage();

						_Project.Clear();
						UpdateTree(sln, v, _Solution, _Project);
						return;
					}

					if (uri == sln.Interactive.ApplicationToDocumentTitle.Comment.Link)
					{

						var Now = DateTime.Now;

						sln.Interactive.ApplicationToDocumentTitle.Title.Value =
							"Dynamic at " + Now.ToString();

						_Project.Clear();
						UpdateTree(sln, v, _Solution, _Project);
						return;
					}


					Native.Document.location.hash = uri.Fragment;
				};


			UpdateTree(sln, v, _Solution, _Project);
		}

		private static void UpdateTree(SolutionBuilder sln, SolutionFileView v, TreeNode _Solution, TreeNode _Project)
		{
			// Or my project?
			var _Properties = _Project.Add("Properties");
			_Properties.IsExpanded = true;
			_Properties.WithIcon(() => new SolutionProjectProperties());

			var _References = _Project.Add("References");
			_References.IsExpanded = false;
			_References.WithIcon(() => new References());


			foreach (var item in sln.References.ToArray())
			{
				var _Reference = _References.Add(item.Attribute("Include").Value.TakeUntilIfAny(","));
				_Reference.IsExpanded = true;
				_Reference.WithIcon(() => new Assembly());
			}


			var FolderLookup = new Dictionary<string, TreeNode>();
			var FileLookup = new Dictionary<SolutionFile, TreeNode>();

			FolderLookup[_Properties.Text] = _Properties;




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
							Parent.IsExpanded = false;
						}

						n = Parent.Add(ProjectInclude.SkipUntilLastIfAny("/"));

						FileLookup[f] = n;
					}

					if (Extension == ".cs")
						n.WithIcon(() => new VisualCSharpCode());
					if (Extension == ".csproj")
						n.WithIcon(() => new VisualCSharpProject());
					if (Extension == ".vb")
						n.WithIcon(() => new VisualBasicCode());
					if (Extension == ".vbproj")
						n.WithIcon(() => new VisualBasicProject());
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

					// somebody refreshed the solution.
					if (v.File == null)
					{
						if (f.Name.TakeUntilLastIfAny(".") == "Application")
							v.File = f;
					}
					else
					{
						// we may not care about the file extensions, will we see a glitch? :)
						if (v.File.Name.TakeUntilLastIfAny(".") == f.Name.TakeUntilLastIfAny("."))
							v.File = f;
					}
				}
			);
		}
	}


}
