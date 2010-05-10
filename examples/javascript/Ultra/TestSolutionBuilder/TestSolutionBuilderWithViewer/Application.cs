// For more information visit:
// http://studio.jsc-solutions.net

// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project

// View as Visual FSharp project
// http://do.jsc-solutions.net/View-as-Visual-FSharp-project

using System;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using TestSolutionBuilderWithViewer.HTML.Pages;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using System.Collections.Generic;
using TestSolutionBuilderWithViewer.Interactive;
using System.Text;
//using TestSolutionBuilderWithViewer.Flash;
using ScriptCoreLib.ActionScript.Components;
using TestSolutionBuilderWithViewer.Flash;

namespace TestSolutionBuilderWithViewer
{
	/// <summary>
	/// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public sealed class Application
	{
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IDefaultPage page)
		{
			// Update document title
			// http://do.jsc-solutions.net/Update-document-title

			@"Hello world".ToDocumentTitle();


			var Content = new IHTMLDiv();

			page.Content = Content;

			Content.style.position = IStyle.PositionEnum.absolute;
			Content.style.left = "0px";
			Content.style.right = "0px";
			Content.style.top = "0px";
			Content.style.bottom = "0px";

			new TwentyTenWorkspace().ToBackground(Content.style, true);

			// em + px :)
			var Workspace0 = new IHTMLDiv().With(
				div =>
				{
					div.style.position = IStyle.PositionEnum.absolute;
					div.style.left = "0px";
					div.style.right = "0px";
					div.style.bottom = "0px";
					div.style.top = "2em";
				}
			).AttachTo(Content);

			// workspace contains the split views
			var Workspace = new IHTMLDiv().With(
				div =>
				{
					div.style.position = IStyle.PositionEnum.absolute;
					div.style.left = "6px";
					div.style.right = "6px";
					div.style.bottom = "6px";
					div.style.top = "6px";
				}
			).AttachTo(Workspace0);

			// in this project we wont be having toolbox or toolbar yet

			Action<HorizontalSplit> ApplyStyle =
				t =>
				{
					t.Split.Splitter.style.backgroundColor = Color.None;
					t.SplitImageContainer.Orphanize();
					t.SplitArea.Target.style.borderLeft = "0";
					t.SplitArea.Target.style.borderRight = "0";
					t.SplitArea.Target.style.width = "6px";
					t.SplitArea.Target.style.Opacity = 0.7;

					// should we obselete JSColor already?
					t.SelectionColor = JSColor.Black;
				};

			var EditorTreeSplit = new HorizontalSplit
			{
				Minimum = 0.3,
				Maximum = 0.95,
				Value = 0.7,
			};

			EditorTreeSplit.With(ApplyStyle);

			EditorTreeSplit.Split.Splitter.style.backgroundColor = Color.None;

			var Viewer = new SolutionDocumentViewer();
			SolutionDocumentViewerTab AboutTab = "About";
			Viewer.Add(AboutTab);
			AboutTab.TabElement.style.Float = IStyle.FloatEnum.right;

			SolutionDocumentViewerTab File1 = "File1";
			Viewer.Add(File1);

			var v = new SolutionFileView();

			v.Container.style.height = "100%";


			var sln = new SolutionBuilder();

			var _Solution = new TreeNode(VistaTreeNodePage.Create);

			_Solution.Container.style.backgroundColor = Color.White;
			var _Project = _Solution.Add();

			var About = new AboutPage();

			Action UpdateFile1Text =
				delegate
				{
					var w = new StringBuilder();

					w.Append(sln.Name);

					if (v.File != null)
					{
						w.Append(" - ");
						w.Append(v.File.Name);
					}

					File1.Text = w.ToString();

				};
			Action Update =
				delegate
				{
					sln.Name = About.ProjectName.value;

					UpdateFile1Text();

					_Project.Clear();
					UpdateTree(sln, v, _Solution, _Project);
				};

			v.FileChanged += UpdateFile1Text;

			AddSaveButton(About);

			About.ProjectName.value = sln.Name;
			About.ProjectName.onchange +=
				delegate
				{
					Update();
				};


			AboutTab.Activated +=
				delegate
				{
					// our about page has dynamic size..
					Viewer.Content.ReplaceContentWith(About.Container);
				};

			File1.Activated +=
				delegate
				{
					// our about page has dynamic size..
					Viewer.Content.ReplaceContentWith(v.Container);
				};

			AboutTab.Activate();

			EditorTreeSplit.Split.LeftContainer = Viewer.Container;
			EditorTreeSplit.Split.RightContainer = _Solution.Container;
			//EditorTreeSplit.Split.RightContainer = DemoTree().Container.WithinContainer().With(div => div.style.backgroundColor = Color.White);

			EditorTreeSplit.Container.AttachTo(Workspace);


			Update();

			new Rules(v, sln, Update);
		}

		private void AddSaveButton(AboutPage About)
		{
			var ss = new SaveActionSprite();

			ss.AttachSpriteTo(About.SaveContainer);

			ss.WhenReady(
				i =>
				{
					Save = i;
				}
			);
		}

		ISaveAction Save;

		void UpdateTree(
			SolutionBuilder sln, SolutionFileView v, TreeNode _Solution, TreeNode _Project)
		{
			_Solution.Text = "Solution '" + sln.Name + "' (1 project)";
			_Solution.IsExpanded = true;

			_Solution.WithIcon(() => new SolutionTwentyTen());

			_Project.Text = sln.Name;
			_Project.IsExpanded = true;

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
					else if (Extension == ".csproj")
						n.WithIcon(() => new VisualCSharpProject());
					else if (Extension == ".vb")
						n.WithIcon(() => new VisualBasicCode());
					else if (Extension == ".vbproj")
						n.WithIcon(() => new VisualBasicProject());
					else if (Extension == ".fs")
						n.WithIcon(() => new VisualFSharpCode());
					else if (Extension == ".fsproj")
						n.WithIcon(() => new VisualFSharpProject());
					else if (Extension == ".htm")
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
						if (f.Name.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".") == "Application")
							v.File = f;
					}
					else
					{
						// we may not care about the file extensions, will we see a glitch? :)
						if (v.File.Name.SkipUntilLastIfAny("/").TakeUntilLastIfAny(".") == f.Name.SkipUntilLastIfAny("/").TakeUntilLastIfAny("."))
							v.File = f;
					}
				}
			);

			if (this.Save != null)
			{
				this.Save.Clear();

				files.WithEach(f => this.Save.Add(f.Name, f.Content));
			}
		}

	}
}
