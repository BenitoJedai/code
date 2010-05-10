using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using ScriptCoreLib.ActionScript.Components;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Ultra.Components.HTML.Images.SpriteSheet.FromAssets;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Studio;

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
			AddSection9(AddSection);
			AddSection10(AddSection);
			AddSection11(AddSection);
			AddSection12(AddSection);

		}

		private static void AddSection11(Action<string, IHTMLDiv> AddSection)
		{
			var ToolbarHeight = "24px";

			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "20em";
				}
			);

			new TwentyTenWorkspace().ToBackground(Content.style, true);

			var ToolbarContainerBackground = new IHTMLDiv().With(
				k =>
				{
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					k.style.left = "0px";
					k.style.right = "0px";
					k.style.top = "0px";
					k.style.height = ToolbarHeight;

					k.style.backgroundColor = Color.White;
					k.style.Opacity = 0.5;
				}
			).AttachTo(Content);

			var ToolbarContainer = new IHTMLDiv().With(
				k =>
				{
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					k.style.left = "0px";
					k.style.right = "0px";
					k.style.top = "0px";
					k.style.height = ToolbarHeight;


				}
			).AttachTo(Content);

			var PreviewContainer = new IHTMLDiv().With(
				k =>
				{
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					k.style.left = "0px";
					k.style.right = "0px";
					k.style.top = ToolbarHeight;
					k.style.bottom = "0px";
				}
			).AttachTo(Content);


			var PreviewFrame = new IHTMLIFrame { src = "about:blank" };

			PreviewFrame.style.width = "100%";
			PreviewFrame.style.height = "100%";
			PreviewFrame.style.border = "0";
			PreviewFrame.style.margin = "0";
			PreviewFrame.style.padding = "0";
			PreviewFrame.frameborder = "0";
			PreviewFrame.border = "0";

			PreviewFrame.AttachTo(PreviewContainer);

			PreviewContainer.Hide();

			var EditorContainer = new IHTMLDiv().With(
				k =>
				{
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					k.style.left = "0px";
					k.style.right = "0px";
					k.style.top = ToolbarHeight;
					k.style.bottom = "0px";
				}
			).AttachTo(Content);

			var EditorFrame = VisualStudioView.CreateEditor().AttachTo(EditorContainer);

			PreviewFrame.allowTransparency = true;
			EditorFrame.allowTransparency = true;

			EditorFrame.WhenContentReady(
				body =>
				{
					body.style.backgroundColor = Color.Transparent;

					new IHTMLDiv
					{
						"Hello world :)"
					}.With(
						div =>
						{
							div.style.backgroundColor = Color.White;
							div.style.borderColor = Color.Gray;
							div.style.borderStyle = "solid";
							div.style.borderWidth = "1px";

							div.style.margin = "2em";
							div.style.padding = "2em";
						}
					).AttachTo(body);

				}
			);

			var ToolbarContent = new IHTMLDiv().AttachTo(ToolbarContainer);

			ToolbarContent.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;

			Action<IHTMLElement> ApplyToolbarButtonStyle =
				k =>
				{
					k.style.verticalAlign = "top";

					k.style.padding = "0";
					k.style.margin = "0";
					k.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
					k.style.SetSize(24, 24);

					VisualStudioView.ApplyMouseHoverStyle(k, Color.Transparent);
				};

			Func<IHTMLImage, IHTMLButton> AddButtonDummy =
				(img) =>
				{
					return new IHTMLButton { img.WithinContainer() }.With(k => ApplyToolbarButtonStyle(k)).AttachTo(ToolbarContent);
				};

			Func<IHTMLImage, Action, IHTMLButton> AddButtonAction =
				(img, command) =>
				{
					return AddButtonDummy(img).With(
						k =>
						{
							k.onclick +=
								delegate
								{
									command();
								};

						}
					);
				};

			Func<IHTMLImage, string, IHTMLButton> AddButton =
				(img, command) =>
				{
					return AddButtonAction(img, () =>
						EditorFrame.contentWindow.document.execCommand(
							command, false, null
						)
					);
				};


			//AddButtonDummy(new RTA_save());

			var SaveContainer = new IHTMLDiv().With(k => ApplyToolbarButtonStyle(k)).AttachTo(ToolbarContent);


			SaveContainer.style.display = ScriptCoreLib.JavaScript.DOM.IStyle.DisplayEnum.inline_block;

			//var Save = new InternalSaveActionSprite();

			//Save.ToTransparentSprite();
			//Save.AttachSpriteTo(SaveContainer);


			var s = new { VisualStudioTemplates.VisualCSharpProject };

			EditorFrame.WhenContentReady(
				body =>
				{
					var t = (IHTMLTextArea)EditorFrame.contentWindow.document.createElement("textarea");

					t.AttachTo(body);

					t.value = s.ToString();

				}
			);

			//Save.WhenReady(
			//    i =>
			//    {
			//        i.FileName = "Project1.zip";


			//        i.Add("Project1.txt", "x");
			//        i.Add("Project1.csproj", s.VisualCSharpProject);
			//    }
			//);

			ToolbarContent.Add(new RTA_separator_horizontal());

			var RTAButtons = new Dictionary<string, IHTMLImage>
			{
				// http://trac.symfony-project.org/browser/plugins/dmCkEditorPlugin/web/js/ckeditor/_source/plugins?rev=27455

				{"Bold", new RTA_bold()},
				{"Underline", new RTA_underline()},
				{"Strikethrough", new RTA_strikethrough()},
				{"Italic", new RTA_italic()},
				{"JustifyLeft", new RTA_justifyleft()},
				{"JustifyCenter", new RTA_justifycenter()},
				{"JustifyRight", new RTA_justifyright()},
				{"JustifyFull", new RTA_justifyfull()},
				{"Indent", new RTA_indent()},
				{"Outdent", new RTA_outdent()},
				{"Superscript", new RTA_superscript()},
				{"Subscript", new RTA_sub()},
				{"Removeformat", new RTA_removeformat()},
				{"InsertOrderedList", new RTA_numberedlist()},
				{"InsertUnorderedList", new RTA_numberedlist()},
				{"undo", new RTA_undo()},
				{"redo", new RTA_redo()},
			}.ToDictionary(
				k => k.Key,
				k => AddButton(k.Value, k.Key)
			);




			var ButtonDesign = default(IHTMLButton);
			var ButtonHTML = default(IHTMLButton);

			ButtonDesign = AddButtonAction(new RTA_mode_design(),
				delegate
				{
					ButtonDesign.Hide();
					ButtonHTML.Show();

					EditorContainer.Show();
					PreviewContainer.Hide();
				}
			);

			ButtonHTML = AddButtonAction(new RTA_mode_html(),
				delegate
				{
					ButtonHTML.Hide();


					PreviewFrame.WithContent(
						body =>
						{
							body.style.backgroundColor = Color.Transparent;
							body.innerHTML = EditorFrame.contentWindow.document.body.innerHTML;

							EditorContainer.Hide();
							PreviewContainer.Show();
							ButtonDesign.Show();
						}
					);
				}
			);

			ButtonDesign.Hide();

			AddSection(
				"Editor with toolbar with background and preview",
				Content
			);
		}



		private static void AddSection10(Action<string, IHTMLDiv> AddSection)
		{

			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "20em";
				}
			);

			new TwentyTenWorkspace().ToBackground(Content.style, true);

			var ToolbarContainerBackground = new IHTMLDiv().With(
				k =>
				{
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					k.style.left = "0px";
					k.style.right = "0px";
					k.style.top = "0px";
					k.style.height = "2em";

					k.style.backgroundColor = Color.White;
					k.style.Opacity = 0.5;
				}
			).AttachTo(Content);

			var ToolbarContainer = new IHTMLDiv().With(
				k =>
				{
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					k.style.left = "0px";
					k.style.right = "0px";
					k.style.top = "0px";
					k.style.height = "2em";


				}
			).AttachTo(Content);

			var EditorContainer = new IHTMLDiv().With(
				k =>
				{
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					k.style.left = "0px";
					k.style.right = "0px";
					k.style.top = "2em";
					k.style.bottom = "0px";
				}
			).AttachTo(Content);

			var Editor = VisualStudioView.CreateEditor().AttachTo(EditorContainer);

			Editor.allowTransparency = true;

			Editor.WhenContentReady(
				body =>
				{
					body.style.backgroundColor = Color.Transparent;

					new IHTMLDiv
					{
						"Hello world :)"
					}.With(
						div =>
						{
							div.style.backgroundColor = Color.White;
							div.style.borderColor = Color.Gray;
							div.style.borderStyle = "solid";
							div.style.borderWidth = "1px";

							div.style.margin = "2em";
							div.style.padding = "2em";
						}
					).AttachTo(body);

				}
			);

			var ToolbarContent = new IHTMLDiv().AttachTo(ToolbarContainer);

			ToolbarContent.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;

			Func<IHTMLImage, IHTMLButton> AddButtonDummy =
				(img) =>
				{
					return new IHTMLButton { img.WithinContainer() }.With(
						k =>
						{
							k.style.padding = "0";
							k.style.margin = "0";
							k.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
							k.style.SetSize(24, 24);

							VisualStudioView.ApplyMouseHoverStyle(k, Color.Transparent);
						}
					).AttachTo(ToolbarContent);
				};

			Func<IHTMLImage, string, IHTMLButton> AddButton =
				(img, command) =>
				{
					return AddButtonDummy(img).With(
						k =>
						{
							k.onclick +=
								delegate
								{
									Editor.contentWindow.document.execCommand(
										command, false, null
									);
								};

						}
					);
				};


			AddButtonDummy(new RTA_save());

			ToolbarContent.Add(new RTA_separator_horizontal());


			AddButton(new RTA_bold(), "Bold");
			AddButton(new RTA_underline(), "Underline");
			AddButton(new RTA_strikethrough(), "Strikethrough");
			AddButton(new RTA_italic(), "Italic");

			ToolbarContent.Add(new RTA_separator_horizontal());

			AddButton(new RTA_justifyleft(), "JustifyLeft");
			AddButton(new RTA_justifycenter(), "JustifyCenter");
			AddButton(new RTA_justifyright(), "JustifyRight");
			AddButton(new RTA_justifyfull(), "JustifyFull");

			AddSection(
				"Editor with toolbar with background",
				Content
			);
		}


		private static void AddSection9(Action<string, IHTMLDiv> AddSection)
		{

			var Content = new IHTMLDiv().With(
				k =>
				{
					k.style.border = "1px solid gray";
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.relative;
					k.style.width = "100%";
					k.style.height = "20em";
				}
			);

			var ToolbarContainer = new IHTMLDiv().With(
				k =>
				{
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					k.style.left = "0px";
					k.style.right = "0px";
					k.style.top = "0px";
					k.style.height = "2em";
				}
			).AttachTo(Content);

			var EditorContainer = new IHTMLDiv().With(
				k =>
				{
					k.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
					k.style.left = "0px";
					k.style.right = "0px";
					k.style.top = "2em";
					k.style.bottom = "0px";
				}
			).AttachTo(Content);

			var Editor = VisualStudioView.CreateEditor().AttachTo(EditorContainer);




			Func<IHTMLImage, IHTMLButton> AddButtonDummy =
				(img) =>
				{
					return new IHTMLButton { img }.With(
						k =>
						{
							k.style.padding = "0";
							k.style.margin = "0";
							k.style.overflow = ScriptCoreLib.JavaScript.DOM.IStyle.OverflowEnum.hidden;
							k.style.SetSize(24, 24);

							VisualStudioView.ApplyMouseHoverStyle(k, Color.FromGray(0xf0));
						}
					).AttachTo(ToolbarContainer);
				};

			Func<IHTMLImage, string, IHTMLButton> AddButton =
				(img, command) =>
				{
					return AddButtonDummy(img).With(
						k =>
						{
							k.onclick +=
								delegate
								{
									Editor.contentWindow.document.execCommand(
										command, false, null
									);
								};

						}
					);
				};


			AddButtonDummy(new RTA_save());

			ToolbarContainer.Add(new RTA_separator_horizontal());


			AddButton(new RTA_bold(), "Bold");
			AddButton(new RTA_underline(), "Underline");
			AddButton(new RTA_strikethrough(), "Strikethrough");
			AddButton(new RTA_italic(), "Italic");

			ToolbarContainer.Add(new RTA_separator_horizontal());

			AddButton(new RTA_justifyleft(), "JustifyLeft");
			AddButton(new RTA_justifycenter(), "JustifyCenter");
			AddButton(new RTA_justifyright(), "JustifyRight");
			AddButton(new RTA_justifyfull(), "JustifyFull");

			AddSection(
				"Editor with toolbar",
				Content
			);
		}

		private static void AddSection12(Action<string, IHTMLDiv> AddSection)
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

			var vsv = new VisualStudioView();

			vsv.Container.AttachTo(Content);

			//var Save = new InternalSaveActionSprite().AddSaveTo(vsv,
			//    i =>
			//    {
			//        i.FileName = "Project1.zip";


			//        new SolutionBuilder().WriteTo(i.Add);

			//    }
			//);

			AddSection(
				"TwentyTen Design With Save",
				Content
			);


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

			var vsv = new VisualStudioView();

			vsv.Container.AttachTo(Content);

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

						VisualStudioView.CreateToolboxTo(s.Content);

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

			EditorTreeSplit.Split.LeftScrollable = VisualStudioView.CreateEditor().WithinContainer();
			EditorTreeSplit.Split.RightContainer = VisualStudioView.DemoTree().Container;

			ToolboxSplit.Split.RightScrollable = EditorTreeSplit.Container;

			ToolboxSplit.Container.AttachTo(Content);

			AddSection(
				"Horizontal split with tree and sub split reversed",
				Content
			);
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
				RightContainer = VisualStudioView.DemoTree().Container,
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
				RightContainer = VisualStudioView.DemoTree().Container,
			};

			hh.Split.LeftScrollable = new IHTMLDiv();

			hh.Split.LeftScrollable.style.position = ScriptCoreLib.JavaScript.DOM.IStyle.PositionEnum.absolute;
			hh.Split.LeftScrollable.style.width = "100%";
			hh.Split.LeftScrollable.style.height = "100%";


			var edit = VisualStudioView.CreateEditor();

			edit.AttachTo(hh.Split.LeftScrollable);

			hh.Container.AttachTo(Content);

			AddSection(
				"Horizontal split with tree",
				Content
			);
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

			s.Content = VisualStudioView.DemoTree().Container;
			s.Content.style.border = "1px solid gray";


			s.IsExpanded = false;
			s.Target.Container.style.backgroundColor = "#efefef";
			s.Target.Container.AttachTo(p.Content);
		}



	}


}
