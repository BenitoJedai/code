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

namespace ScriptCoreLib.JavaScript.Components
{
	/// <summary>
	/// Embed a visual studio like view in your application!
	/// </summary>
	public class VisualStudioView
	{
		public IHTMLDiv Container { get; private set; }

		public VisualStudioView()
		{
			var Content = new IHTMLDiv();

			this.Container = Content;
			this.Container.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			this.Container.style.width = "100%";
			this.Container.style.height = "100%";

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

			var EditorContainer = new IHTMLDiv { VisualStudioView.CreateEditor() };

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
		}

		internal static void CreateToolboxTo(IHTMLDiv ToolboxContainer)
		{
			var items = new
			{
				img = default(IHTMLElement)
			}.ToEmptyList();

			Action<IHTMLElement> Add =
				img =>
				{
					img.style.cursor = ScriptCoreLib.JavaScript.DOM.IStyle.CursorEnum.move;

					new IHTMLDiv().With(
						div =>
						{
							var backgroundColor = default(Color);

							if (items.Count % 2 == 0)
								backgroundColor = Color.FromGray(0xf7);
							else
								backgroundColor = Color.FromGray(0xf0);

							div.style.backgroundColor = backgroundColor;
							div.style.borderWidth = "1px";
							div.style.borderStyle = "solid";
							div.style.borderColor = backgroundColor;

							//div.style.height = "90px";
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

							div.onmouseover +=
								delegate
								{
									div.style.backgroundColor = Color.FromRGB(0xFF, 0xEF, 0xBB);
									div.style.borderColor = Color.FromRGB(0xE5, 0xC3, 0x65);
								};

							div.onmouseout +=
								delegate
								{
									div.style.backgroundColor = backgroundColor;
									div.style.borderColor = backgroundColor;
								};
						}
					);
				};

			Add(new JSCSolutionsNETImage());
			Add(new ToolboxIconsIHTMLButton());
			Add(new ToolboxIconsIHTMLInputCheckbox());
			Add(new ToolboxIconsIHTMLInputText());
			Add(new ToolboxIconsIHTMLTextarea());

		}

		internal static TreeNode DemoTree()
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

		internal static IHTMLIFrame CreateEditor()
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

	}
}
