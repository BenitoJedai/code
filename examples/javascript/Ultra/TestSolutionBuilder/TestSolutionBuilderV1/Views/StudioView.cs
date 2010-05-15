using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.ActionScript.Components;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.JavaScript.Studio.StockToolboxTabs;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Studio.Languages;
using ScriptCoreLib.JavaScript.Concepts;
using TestSolutionBuilderV1.HTML.Pages;
using System.Xml.Linq;
using ScriptCoreLib.Ultra.Studio.StockPages;
using ScriptCoreLib.Ultra.Studio.StockExpressions;
using ScriptCoreLib.Ultra.Studio.StockTypes;

namespace TestSolutionBuilderV1.Views
{
	public class StudioView
	{
		public readonly IHTMLDiv Content = new IHTMLDiv();

		public StudioView(Action<IHTMLElement, Action<ISaveAction>> AddSaveButton)
		{
			Content.style.position = IStyle.PositionEnum.absolute;
			Content.style.left = "0px";
			Content.style.right = "0px";
			Content.style.top = "0px";
			Content.style.bottom = "0px";

			new TwentyTenWorkspace().ToBackground(Content.style, true);

			var WorkspaceHeader = default(IHTMLSpan);

			@"jsc-solutions.net studio".ToDocumentTitle().With(
				title =>
				{
					WorkspaceHeader = new IHTMLSpan { innerText = title };

					WorkspaceHeader.AttachTo(Content);
					WorkspaceHeader.style.SetLocation(16, 8);
					WorkspaceHeader.style.color = Color.White;

					// http://www.quirksmode.org/css/textshadow.html
					WorkspaceHeader.style.textShadow = "#808080 4px 2px 2px";

				}
			);


			if (AddSaveButton != null)
				AddSaveButton(WorkspaceHeader, i => Save = i);

			// em + px :)
			var Workspace0 = new IHTMLDiv().With(
				div =>
				{
					div.style.position = IStyle.PositionEnum.absolute;
					div.style.left = "0px";
					div.style.right = "0px";
					div.style.bottom = "0px";
					div.style.top = "3em";
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
				Minimum = 0,
				Maximum = 1,
				Value = 0.7,
			};

			EditorTreeSplit.With(ApplyStyle);

			EditorTreeSplit.Split.Splitter.style.backgroundColor = Color.None;


			EditorTreeSplit.Container.AttachTo(Workspace);



			var Split = new HorizontalSplit
			{
				Minimum = 0,
				Maximum = 1,
				Value = 0.3,
			};

			Split.With(ApplyStyle);

			Split.Split.Splitter.style.backgroundColor = Color.None;

			EditorTreeSplit.LeftContainer = Split.Container;



			var SolutionToolbox = new SolutionDockWindowPage();

			SolutionToolbox.HeaderText.innerText = "Toolbox";
			SolutionToolbox.Content.style.backgroundColor = Color.White;
			SolutionToolbox.Content.style.padding = "2px";
			SolutionToolbox.Content.style.overflow = IStyle.OverflowEnum.auto;
			SolutionToolbox.Content.Clear();


			var vv = new SolutionToolboxListView();

			vv.Container.AttachTo(SolutionToolbox.Content);

			var items = new StockToolboxTabsForHTMLDocument();

			items.WithEach(vv.Add);


			var Viewer = new SolutionDocumentViewer();
			SolutionDocumentViewerTab File7Tab = "Design/Default.htm";
			Viewer.Add(File7Tab);

			#region OutputFile
			var OutputFile = new SolutionFile();
			var OutputFileViewer = new SolutionFileView();

			// fullscreen! :)
			OutputFileViewer.Container.style.height = "100%";

			OutputFile.IndentStack.Push(
				delegate
				{
					OutputFile.Write(SolutionFileTextFragment.Comment, "" + DateTime.Now);
					OutputFile.WriteSpace();
				}
			);

			Action<string> OutputWriteLine =
				n =>
				{
					// Would we want to rewire System.Out? Console.WriteLine?
					OutputFile.WriteIndent();
					OutputFile.WriteLine(n);

					// we could have a resume feature? now we just go and clear...
					OutputFileViewer.File = OutputFile;
				};


			OutputWriteLine("studio.jsc-solutions.net ready!");
			#endregion

			SolutionDocumentViewerTab OutputTab = "Output";
			Viewer.Add(OutputTab);
			OutputTab.TabElement.style.Float = IStyle.FloatEnum.right;


			SolutionDocumentViewerTab AboutTab = "About";
			Viewer.Add(AboutTab);
			AboutTab.TabElement.style.Float = IStyle.FloatEnum.right;


			var CurrentDesigner = new SolutionFileDesigner();




			var HTMLDesigner = new SolutionFileDesignerHTMLElementTabs();

			CurrentDesigner.Add(HTMLDesigner);






			#region CodeSourceA
			var CodeSourceATab =
				new SolutionFileDesignerTab
				{
					Image = new RTA_mode_html(),
					Text = "Source [Generated]"
				};

			var CodeSourceAView = new SolutionFileView();

			CodeSourceAView.Container.style.position = IStyle.PositionEnum.absolute;
			CodeSourceAView.Container.style.left = "0px";
			CodeSourceAView.Container.style.right = "0px";
			CodeSourceAView.Container.style.top = "0px";
			CodeSourceAView.Container.style.bottom = "0px";

			CodeSourceAView.Container.style.display = IStyle.DisplayEnum.none;
			CodeSourceAView.Container.AttachTo(CurrentDesigner.Content);

			CodeSourceATab.Deactivated +=
				delegate
				{
					CodeSourceAView.Container.style.display = IStyle.DisplayEnum.none;
				};

			CodeSourceATab.Activated +=
				delegate
				{
					HTMLDesigner.HTMLDesignerContent.WhenContentReady(
						body =>
						{
							var CodeSourceFile = new SolutionFile
							{
								Name = "Default.htm"
							};

							var Type = new SolutionProjectLanguageType
							{
								Namespace = "HTML.Pages",
								Name = "IDefaultPage",
								IsInterface = true,
							};

							(from n in body.AsXElement().DescendantsAndSelf()
							 let id = n.Attribute("id")
							 where id != null
							 select new { n, id }
							).WithEach(
								k =>
								{
									Type.Properties.Add(
										new SolutionProjectLanguageProperty
										{
											Name = k.id.Value,
											GetMethod = new SolutionProjectLanguageMethod(),
											SetMethod = new SolutionProjectLanguageMethod(),
											PropertyType = new SolutionProjectLanguageType
											{
												Namespace = "ScriptCoreLib.JavaScript.DOM.HTML",
												Name = "IHTMLElement"
											}
										}
									);
								}
							);

							KnownLanguages.VisualCSharp.WriteType(CodeSourceFile, Type, null);

							CodeSourceAView.File = CodeSourceFile;

							CodeSourceAView.Container.style.display = IStyle.DisplayEnum.empty;
						}
					);
				};


			#endregion


			#region CodeSourceB
			var CodeSourceBTab =
				new SolutionFileDesignerTab
				{
					Image = new RTA_mode_html(),
					Text = "Source"
				};

			var CodeSourceBView = new SolutionFileView();

			CodeSourceBView.Container.style.position = IStyle.PositionEnum.absolute;
			CodeSourceBView.Container.style.left = "0px";
			CodeSourceBView.Container.style.right = "0px";
			CodeSourceBView.Container.style.top = "0px";
			CodeSourceBView.Container.style.bottom = "0px";

			CodeSourceBView.Container.style.display = IStyle.DisplayEnum.none;
			CodeSourceBView.Container.AttachTo(CurrentDesigner.Content);

			CodeSourceBTab.Deactivated +=
				delegate
				{
					CodeSourceBView.Container.style.display = IStyle.DisplayEnum.none;
				};

			CodeSourceBTab.Activated +=
				delegate
				{

					CodeSourceBView.Container.style.display = IStyle.DisplayEnum.empty;
				};


			#endregion


			CurrentDesigner.Add(CodeSourceATab);
			CurrentDesigner.Add(CodeSourceBTab);







			Split.Split.LeftScrollable = SolutionToolbox.Container;
			Split.Split.RightScrollable = Viewer.Container;

			// ...



			var sln = new SolutionBuilder();


			#region dynamic content
			Func<IEnumerable<XElement>> GetPages = delegate
			{
				return from n in sln.ApplicationPage.DescendantsAndSelf()
					   let type = n.Attribute(SolutionBuilderInteractive.DataTypeAttribute)
					   where type != null
					   let id = n.Attribute("id")
					   where id != null
					   select n;
			};

			sln.Interactive.GenerateApplicationExpressions +=
				Add =>
				{


					// page.PageContainer.ReplaceWith(
					GetPages().WithEach(
						k =>
						{
							var id = k.Attribute("id").Value;

							if (id == "Page1")
							{
								Add(
									new StockReplaceWithNewPageExpression(id)
								);
							}

							if (id == "UserControl1")
							{
								Add(
									new StockReplaceWithNewPageExpression(id)
								);
							}
						}
					);
				};

			sln.Interactive.GenerateHTMLFiles +=
				Add =>
				{

					GetPages().WithEach(
						k =>
						{
							var id = k.Attribute("id").Value;

							if (id == "Page1")
							{
								var __Content = new XElement(StockPageDefault.Page);


								__Content.Element("head").Element("title").Value = id;

								Add(
									new SolutionProjectHTMLFile
									{
										Name = "Design/" + id + ".htm",
										Content = __Content
									}
								);
							}
						}
					 );
				};

			sln.Interactive.GenerateTypes +=
				Add =>
				{
					GetPages().WithEach(
						k =>
						{
							var id = k.Attribute("id").Value;

							if (id == "UserControl1")
							{
								Add(
									new StockUserControlType(sln.Name + ".Components", id)
								);
							}
						}
					 );
				};
			#endregion


			var _Solution = new TreeNode(VistaTreeNodePage.Create);


			var _Project = _Solution.Add();

			var About = new AboutPage();

			Action UpdateFile1Text =
				delegate
				{

					if (CodeSourceBView.File != null)
					{
						File7Tab.Text = CodeSourceBView.File.Name.SkipUntilIfAny("/");
					}
					else
					{
						File7Tab.Text = sln.Name;
					}


				};




			Action Update =
				delegate
				{
					sln.Name = About.ProjectName.value;

					UpdateFile1Text();

					_Project.Clear();
					UpdateTree(sln, CodeSourceBView, _Solution, _Project);
				};

			var PreviousVersion = default(string);

			HTMLDesigner.HTMLDesignerContent.WhenContentReady(
				body =>
				{
					if (PreviousVersion == null)
					{
						var x = new XElement(body.AsXElement());
						var y = x.ToString();
						PreviousVersion = y;
					}

					HTMLDesigner.HTMLDesignerContent.contentWindow.onfocus +=
						delegate
						{
							OutputWriteLine("Designer activated.");
							//"focus".ToDocumentTitle();
						};

					HTMLDesigner.HTMLDesignerContent.contentWindow.onblur +=
						delegate
						{
							//"blur".ToDocumentTitle();

							var x = new XElement(body.AsXElement());
							var y = x.ToString();

							if (PreviousVersion != y)
							{
								PreviousVersion = y;


								sln.ApplicationPage = x;


								// allow any blur causing action to complete first
								// we get reselected for some odd reason, why?
								new Timer(
									delegate
									{
										OutputWriteLine("Designer has caused an update.");
										Update();

									}
								).StartTimeout(700);
							}
						};
				}
			);


			CodeSourceBView.FileChanged +=
				delegate
				{
					UpdateFile1Text();


					OutputWriteLine("Select: " + CodeSourceBView.File.Name);

					// hack :)
					if (CodeSourceBView.File.Name.EndsWith("/Default.htm"))
					{
						// currently we only have one element :)

						HTMLDesigner.HTMLDesignerTab.RaiseActivated();

						HTMLDesigner.HTMLDesignerTab.TabElement.style.display = IStyle.DisplayEnum.inline_block;
						HTMLDesigner.HTMLSourceTab.TabElement.style.display = IStyle.DisplayEnum.none;
						CodeSourceATab.TabElement.style.display = IStyle.DisplayEnum.inline_block;
						CodeSourceBTab.TabElement.style.display = IStyle.DisplayEnum.inline_block;

						HTMLDesigner.HTMLDesignerContent.WhenContentReady(
							body =>
							{
								HTMLDesigner.HTMLDesignerContent.contentWindow.focus();
							}
						);

						// show the design/source buttons
					}
					else if (CodeSourceBView.File.Name.EndsWith(sln.Language.CodeFileExtension))
					{
						// show type outline / member
						CodeSourceBTab.RaiseActivated();

						HTMLDesigner.HTMLDesignerTab.TabElement.style.display = IStyle.DisplayEnum.none;
						HTMLDesigner.HTMLSourceTab.TabElement.style.display = IStyle.DisplayEnum.none;
						CodeSourceATab.TabElement.style.display = IStyle.DisplayEnum.none;
						CodeSourceBTab.TabElement.style.display = IStyle.DisplayEnum.inline_block;
					}


				};

			//AddSaveButton(WorkspaceHeader, i => Save = i);

			About.ProjectName.value = sln.Name;
			About.ProjectName.onchange +=
				delegate
				{
					OutputWriteLine("Project name has changed.");
					Update();
				};



			Viewer.Content.Clear();
			Viewer.Content.Add(About.Container);
			Viewer.Content.Add(CurrentDesigner.Container);
			Viewer.Content.Add(OutputFileViewer.Container);

			AboutTab.WhenActivated(About.Container);
			File7Tab.WhenActivated(CurrentDesigner.Container);
			OutputTab.WhenActivated(OutputFileViewer.Container);



			Viewer.First().Activate();

			var SolutionExplorer = new SolutionDockWindowPage();

			SolutionExplorer.HeaderText.innerText = "Solution Explorer";
			SolutionExplorer.Content.style.backgroundColor = Color.White;
			SolutionExplorer.Content.style.padding = "2px";
			SolutionExplorer.Content.ReplaceContentWith(_Solution.Container);

			_Solution.Container.style.overflow = IStyle.OverflowEnum.auto;
			_Solution.Container.style.height = "100%";
			_Solution.Container.style.backgroundColor = Color.White;

			EditorTreeSplit.Split.RightContainer = SolutionExplorer.Container;

			EditorTreeSplit.Container.AttachTo(Workspace);

			//CurrentDesigner.First().RaiseActivated();

			Update();

			new Rules(CodeSourceBView, sln, Update);


		}


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
						{
							var _Folder = _Project.Add(Folder);
							FolderLookup[Folder] = _Folder;
							_Folder.IsExpanded = true;
						}
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

					if (f.ContextType != null)
					{
						if (f.ContextType.BaseType != null)
						{
							if (f.ContextType.BaseType.Name == "UserControl")
								n.WithIcon(() => new SolutionProjectFormsControl());
						}
					}

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
				this.Save.FileName = sln.Name + ".sln.zip";
				files.WithEach(f => this.Save.Add(f.Name, f.Content));
			}
		}

		ISaveAction Save;


	}
}
