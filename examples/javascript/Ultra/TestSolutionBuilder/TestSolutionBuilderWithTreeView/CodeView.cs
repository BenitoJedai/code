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
	public static class CodeView
	{
		public static HorizontalSplit CreateView()
		{
			var h = new HorizontalSplit
			{

			};


			var sln = new SolutionBuilder
			{
				Name = "VisualBasicProject1",
				Language = new VisualBasicLanguage()
			};

			var v = new SolutionFileView();



			v.Container.style.height = "100%";

			// phase 1 has only one project at once.

			var _Solution = new TreeNode(VistaTreeNodePage.Create);
			var _Project = _Solution.Add();





			h.LeftContainer = v.Container;
			h.RightContainer = _Solution.Container;

			h.Value = 0.7;

			#region AtLink
			Action<Uri, Action<int>> AtLink =
				(Link, Handler) =>
				{
					var Counter = 0;

					v.LinkCommentClick +=
						uri =>
						{
							if (uri == Link)
							{
								Counter++;
								Handler(Counter);
							}
						};
				};
			#endregion

			Action Update =
				delegate
				{
					_Project.Clear();
					UpdateTree(sln, v, _Solution, _Project);
				};

			AtLink(sln.Interactive.ToVisualCSharpLanguage,
				delegate
				{
					sln.Language = new VisualCSharpLanguage();
					sln.Name = "VisualCSharpProject1";
					Update();
				}
			);

			AtLink(sln.Interactive.ToVisualBasicLanguage,
				delegate
				{
					sln.Language = new VisualBasicLanguage();
					sln.Name = "VisualBasicProject1";
					Update();
				}
			);


			AtLink(sln.Interactive.ToVisualFSharpLanguage,
				delegate
				{
					sln.Language = new VisualFSharpLanguage();
					sln.Name = "VisualFSharpProject1";
					Update();
				}
			);



			AtLink(sln.Interactive.ApplicationToDocumentTitle.Comment,
				ApplicationToDocumentTitleVariation =>
				{
					var Now = DateTime.Now;


					if (ApplicationToDocumentTitleVariation % 2 == 0)
						sln.Interactive.ApplicationToDocumentTitle.Title.Value =
							"Time: " + Now.ToString();
					else
						sln.Interactive.ApplicationToDocumentTitle.Title.Value =
							sln.Name;

					Update();
				}
			);

			var WebMethod2_From = new[]
			{
				"IL",
				"C#",
				"Visual Basic",
				"F#",
			};

			var WebMethod2_To = new[]
			{
				"JavaScript",
				"ActionScript",
				"Java",
				"PHP",
			};

			AtLink(sln.Interactive.WebMethod2,
				Variation =>
				{

					sln.Interactive.WebMethod2.Title.Value =
						"jsc can convert " + WebMethod2_From.Random() + " to " + WebMethod2_To.Random();

					Update();
				}
			);


			v.LinkCommentClick +=
				uri =>
				{
					Native.Document.location.hash = uri.Fragment;
				};


			UpdateTree(sln, v, _Solution, _Project);
			return h;
		}

		private static void UpdateTree(SolutionBuilder sln, SolutionFileView v, TreeNode _Solution, TreeNode _Project)
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
		}

	}
}
