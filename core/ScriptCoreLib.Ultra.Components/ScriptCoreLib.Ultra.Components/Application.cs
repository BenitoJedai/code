using System;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System.ComponentModel;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Components.HTML.Images.SpriteSheet.FromAssets;
using ScriptCoreLib.JavaScript.Components;
using System.Xml.Linq;
using ScriptCoreLib.Shared.Drawing;
using System.Collections.Generic;

namespace ScriptCoreLib.Ultra.Components
{

	[Description("ScriptCoreLib.Ultra.Components. Write javascript, flash and java applets within a C# project.")]
	internal sealed partial class Application
	{
		public Application(IApplicationLoader p)
		{
			p.LoadingAnimation.FadeOut();

			p.Content.Add("ScriptCoreLib.Ultra.Components");

			Native.Document.title = "Hi!";

			p.Content.style.margin = "2em";

			AddSection1(p);
			AddSection2(p);
			AddSection3(p);
			AddSection4(p);

			var Sections = new List<SectionConcept<Section>>();

			Action<string, IHTMLDiv> AddSection =
				(Header, Content) =>
				{
					var s = new Section().ToSectionConcept();

					Sections.Add(s);

					if (Sections.Count % 2 == 0)
						s.Content.style.border = "1px solid gray";

					s.Header = Header;

					s.Content = Content;

					s.IsExpanded = false;
					s.Target.Container.AttachTo(p.Content);
				};

			AddSection5(AddSection);
			AddSection6(AddSection);
			AddSection7(AddSection);
			AddSection8(AddSection);

		}

		private static void AddSection8(Action<string, IHTMLDiv> AddSection)
		{

			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "25em";
				}
			);

			new TwentyTenWorkspace().ToBackground(Content.style, true);

			var Workspace = new IHTMLDiv().With(
				div =>
				{
					div.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					div.style.left = "6px";
					div.style.right = "6px";
					div.style.bottom = "6px";
					div.style.top = "6px";
				}
			).AttachTo(Content);

			var ToolboxSplit = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.5,
				Value = 0.15,
			};

			Action<HorizontalSplit> ApplyStyle =
				t =>
				{
					t.Split.Splitter.style.backgroundColor = Color.None;
					t.SplitImageContainer.Orphanize();
					t.SplitArea.Target.style.borderLeft = "0";
					t.SplitArea.Target.style.borderRight = "0";
					t.SplitArea.Target.style.width = "6px";
					t.SplitArea.Target.style.Opacity = 0.7;
					t.SelectionColor = JSColor.Black;
				};

			ToolboxSplit.With(ApplyStyle);

			var ToolboxContainer = ToolboxSplit.LeftContainer;

			ToolboxSplit.Split.Left.style.backgroundColor = Color.White;

			Action<string, string> AddGroup =
				(Header, Title) =>
				{
					#region group
					{
						var s = new Section().ToSectionConcept();

						s.Target.Header.style.marginLeft = "1em";
						s.Header = Header;
						s.Header.title = Title;

						s.Content.Clear();

						CreateToolboxTo(s.Content);

						s.IsExpanded = false;
						s.Target.Container.AttachTo(ToolboxContainer);
					}
					#endregion
				};

			AddGroup("HTML", "For example a section or a split view");
			AddGroup("Flash Components", "For example an empty sprite or a visualization");
			AddGroup("Java Applets", "For example a calculator form or a visualization");
			AddGroup("General", "Snippets");

			var EditorTreeSplit = new HorizontalSplit
			{
				Minimum = 0.5,
				Maximum = 0.95,
				Value = 0.6,
			};

			EditorTreeSplit.With(ApplyStyle);

			EditorTreeSplit.Split.Splitter.style.backgroundColor = Color.None;

			var EditorContainer = new IHTMLDiv { CreateEditor() };

			EditorContainer.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			EditorContainer.style.top = "1.3em";
			EditorContainer.style.left = "0px";
			EditorContainer.style.right = "0px";
			EditorContainer.style.bottom = "0.3em";
			EditorContainer.style.backgroundColor = Color.White;

			var DocumentsContainer = EditorContainer.WithinContainer();

			new IHTMLDiv().With(
				div =>
				{
					div.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					div.style.top = "0px";
					div.style.height = "1em";
					div.style.left = "0px";
					div.style.right = "0px";

					div.style.color = Color.White;

					div.Add("About.htm");
				}
			).AttachTo(DocumentsContainer);

			new IHTMLDiv().With(
				div =>
				{
					div.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					div.style.top = "1em";
					div.style.height = "0.3em";
					div.style.left = "0px";
					div.style.right = "0px";

					div.style.backgroundColor = Color.FromRGB(255, 232, 166);

				}
			).AttachTo(DocumentsContainer);

			new IHTMLDiv().With(
				div =>
				{
					div.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					div.style.bottom = "0px";
					div.style.height = "0.3em";
					div.style.left = "0px";
					div.style.right = "0px";

					div.style.backgroundColor = Color.FromRGB(255, 232, 166);

				}
			).AttachTo(DocumentsContainer);


			EditorTreeSplit.Split.LeftScrollable = DocumentsContainer;
			EditorTreeSplit.Split.RightContainer = DemoTree().Container.WithinContainer().With(div => div.style.backgroundColor = Color.White);

			ToolboxSplit.Split.RightScrollable = EditorTreeSplit.Container;

			ToolboxSplit.Container.AttachTo(Workspace);

			AddSection(
				"TwentyTen Design",
				Content
			);
		}

		private static void AddSection7(Action<string, IHTMLDiv> AddSection)
		{

			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "15em";
				}
			);



			var ToolboxSplit = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.5,
				Value = 0.15,
			};

			var ToolboxContainer = ToolboxSplit.LeftContainer;

			Action<string, string> AddGroup =
				(Header, Title) =>
				{
					#region group
					{
						var s = new Section().ToSectionConcept();

						s.Target.Header.style.marginLeft = "1em";
						s.Header = Header;
						s.Header.title = Title;

						s.Content.Clear();

						CreateToolboxTo(s.Content);

						s.IsExpanded = false;
						s.Target.Container.AttachTo(ToolboxContainer);
					}
					#endregion
				};

			AddGroup("HTML Components", "For example a section or a split view");
			AddGroup("Flash Components", "For example an empty sprite or a visualization");
			AddGroup("Java Applets", "For example a calculator form or a visualization");

			var EditorTreeSplit = new HorizontalSplit
			{
				Minimum = 0.5,
				Maximum = 0.95,
				Value = 0.6,
			};

			EditorTreeSplit.Split.LeftScrollable = CreateEditor().WithinContainer();
			EditorTreeSplit.Split.RightContainer = DemoTree().Container;

			ToolboxSplit.Split.RightScrollable = EditorTreeSplit.Container;

			ToolboxSplit.Container.AttachTo(Content);

			AddSection(
				"Horizontal split with tree and sub split reversed",
				Content
			);
		}

		private static void CreateToolboxTo(IHTMLDiv ToolboxContainer)
		{
			var items = new
			{
				img = default(IHTMLImage)
			}.ToEmptyList();

			Action<IHTMLImage> Add =
				img =>
				{
					img.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.move;

					new IHTMLDiv().With(
						div =>
						{
							if (items.Count % 2 == 0)
								div.style.backgroundColor = Color.FromGray(0xf7);
							else
								div.style.backgroundColor = Color.FromGray(0xf0);

							div.style.height = "90px";
							div.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
							div.style.textAlign = ScriptCoreLib.JavaScript.DOM.IStyle.TextAlignEnum.center;

							div.Add(
								img
							);

							img.title = "Drag this component into the designer!";

							items.Add(
								new
								{
									img = img
								}
							);

							ToolboxContainer.Add(div);
						}
					);
				};

			Add(new JSCSolutionsNETImage());
			Add(new JSCSolutionsNETImage());
			Add(new JSCSolutionsNETImage());

		}

		private static void AddSection6(Action<string, IHTMLDiv> AddSection)
		{

			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "6em";
				}
			);



			var hh = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.95,
				Value = 0.7,
				RightContainer = DemoTree().Container,
			};

			var hh2 = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.5,
				Value = 0.7,
			};

			hh.Split.LeftScrollable = hh2.Container;

			hh.Container.AttachTo(Content);

			AddSection(
				"Horizontal split with tree and sub split",
				Content
			);
		}


		private static void AddSection5(Action<string, IHTMLDiv> AddSection)
		{

			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "6em";
				}
			);



			var hh = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.95,
				Value = 0.7,
				RightContainer = DemoTree().Container,
			};

			hh.Split.LeftScrollable = new IHTMLDiv();

			hh.Split.LeftScrollable.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			hh.Split.LeftScrollable.style.width = "100%";
			hh.Split.LeftScrollable.style.height = "100%";


			var edit = CreateEditor();

			edit.AttachTo(hh.Split.LeftScrollable);

			hh.Container.AttachTo(Content);

			AddSection(
				"Horizontal split with tree",
				Content
			);
		}

		private static IHTMLIFrame CreateEditor()
		{
			var edit = new IHTMLIFrame { src = "about:blank" };

			edit.style.width = "100%";
			edit.style.height = "100%";
			edit.style.border = "0";
			edit.style.margin = "0";
			edit.style.padding = "0";
			edit.frameborder = "0";
			edit.border = "0";

			edit.WhenReady(
				document =>
				{

					document.WithContent(
						new XElement("style", "span { color: red; }"),
						new XElement("div",
							new XAttribute("style", "margin: 2em;"),
							new XElement("h1", "powered  by jsc"),
							new XElement("span", "hello world")
						)
					);

					document.DesignMode = true;
				}
			);
			return edit;
		}

		private static void AddSection4(IApplicationLoader pp)
		{
			var s = new Section().ToSectionConcept();

			s.Header = "Horizontal split";
			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "4em";
				}
			);

			var hh = new HorizontalSplit
			{
				Minimum = 0.05,
				Maximum = 0.95,
				Value = 0.7
			};

			hh.Container.AttachTo(Content);

			s.Content = Content;

			s.IsExpanded = false;
			s.Target.Container.AttachTo(pp.Content);
		}

		private static void AddSection3(IApplicationLoader p)
		{
			var s = new Section().ToSectionConcept();

			s.Header = "Countdown Days";

			s.Content = new CountDownGadgetConcept(CountDownGadget.Create)
			{
				ShowOnlyDays = true,
				Event = new DateTime(2010, 5, 24),

			};

			//s.Content = x.Container;

			s.IsExpanded = false;
			s.Target.Container.style.backgroundColor = "#efefef";
			s.Target.Container.AttachTo(p.Content);
		}

		private static void AddSection2(IApplicationLoader p)
		{
			var s = new Section().ToSectionConcept();

			s.Header = "Countdown";

			var Now = DateTime.Now;
			var Event = new DateTime(2010, 5, 24, Now.Hour, Now.Minute, Now.Second);

			s.Content = new CountDownGadgetConcept(CountDownGadget.Create)
			{
				Event = Event,
				AutoUpdate = true
			};

			//s.Content = x.Container;

			s.IsExpanded = false;
			s.Target.Container.AttachTo(p.Content);
		}

		private static void AddSection1(IApplicationLoader p)
		{
			var s = new Section().ToSectionConcept();

			s.Header = "Syntax1";

			s.Content = DemoTree().Container;
			s.Content.style.border = "1px solid gray";


			s.IsExpanded = false;
			s.Target.Container.style.backgroundColor = "#efefef";
			s.Target.Container.AttachTo(p.Content);
		}

		private static TreeNode DemoTree()
		{
			var sln = new TreeNode(VistaTreeNodePage.Create);


			sln.Text = "Solution";
			sln.IsExpanded = true;

			Action<TreeNode> AddReferences =
				p =>
				{
					var r = p.Add("References", new References());

					r.Add("System", new Assembly());
					r.Add("System.Core", new Assembly());
					r.Add("ScriptCoreLib", new Assembly());
					r.Add("ScriptCoreLib.Ultra", new Assembly());
					r.Add("ScriptCoreLib.Ultra.Library", new Assembly());
					r.Add("ScriptCoreLib.Ultra.Controls", new Assembly());
					r.Add("ScriptCoreLibJava", new Assembly());
					r.Add("jsc.meta", new Assembly());
				};

			Action<TreeNode> AddUltraSource =
				p =>
				{
					var my = p.Add("My.UltraSource");
					my.Add("Control1.htm", new HTMLDocument());
					my.Add("Default.htm", new HTMLDocument());
					my.Add("jsc.png", new ImageFile());

				};

			{
				var p = sln.Add("UltraApplication1.Concepts", new VisualCSharpProject());

				AddReferences(p);

				p.Add("Control1Concept.cs", new VisualCSharpCode());
			}


			{
				var p = sln.Add("UltraApplication1", new VisualCSharpProject());


				AddReferences(p);
				AddUltraSource(p);



				p.Add("Application.cs", new VisualCSharpCode());
				p.Add("WebService.cs", new VisualCSharpCode());
				p.Add("Program.cs", new VisualCSharpCode());
			}

			{
				var p = sln.Add("UltraApplication1", new VisualBasicProject());

				AddReferences(p);
				AddUltraSource(p);

				p.Add("Application.vb", new VisualBasicCode());
				p.Add("WebService.vb", new VisualBasicCode());
				p.Add("Program.vb", new VisualBasicCode());
			}


			{
				var p = sln.Add("UltraApplication1", new VisualFSharpProject());

				AddReferences(p);
				AddUltraSource(p);


				p.Add("Application.fs", new VisualFSharpCode());
				p.Add("WebService.fs", new VisualFSharpCode());
				p.Add("Program.fs", new VisualFSharpCode());
			}

			return sln;
		}


	}


}
