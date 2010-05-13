using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using ScriptCoreLib.Ultra.Studio.StockPages;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Studio.StockTypes;
using ScriptCoreLib.Ultra.Studio.Languages;
using TestSolutionBuilderWithToolbox.HTML.Pages;

namespace TestSolutionBuilderWithToolbox.Views
{
	public class StudioView
	{
		public readonly IHTMLDiv Content = new IHTMLDiv();

		public StudioView()
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

			var Split = new HorizontalSplit
			{
				Minimum = 0,
				Maximum = 1,
				Value = 0.2,
			};

			Split.With(ApplyStyle);

			Split.Split.Splitter.style.backgroundColor = Color.None;

			Split.Container.AttachTo(Workspace);



			var SolutionToolbox = new SolutionDockWindowPage();

			SolutionToolbox.HeaderText.innerText = "Toolbox";
			SolutionToolbox.Content.style.backgroundColor = Color.White;
			SolutionToolbox.Content.style.padding = "2px";
			SolutionToolbox.Content.style.overflow = IStyle.OverflowEnum.auto;
			SolutionToolbox.Content.Clear();


			var vv = new SolutionToolboxListView();

			vv.Container.AttachTo(SolutionToolbox.Content);
			vv.Add(
				new SolutionToolboxListViewTab
				{
					Icon = new StockToolboxImageForHTMLDocument(),
					Name = "Page1",
					Title = "HTML Document",
					Text = "HTML Document"
				}
			);

			vv.Add(
				new SolutionToolboxListViewTab
				{
					Icon = new StockToolboxImageForFormsControl(),
					Name = "UserControl1",
					Title  = "Windows Forms UserControl",
					Text = "UserControl"
				}
			);

			vv.Add(
				new SolutionToolboxListViewTab
				{
					Icon = new StockToolboxImageForAvalonCanvas(),
					Name = "Canvas1",
					Title = "Windows Presentation Foundation Canvas",
					Text = "Canvas"
				}
			);

			vv.Add(
				new SolutionToolboxListViewTab
				{
					Icon = new StockToolboxImageForJavaAppletFormsControl(),
					Name = "AppletUserControl1",
					Title = "Java Applet with Windows Forms UserControl",
					Text = "Applet UserControl"
				}
			);

			vv.Add(
				new SolutionToolboxListViewTab
				{
					Icon = new StockToolboxImageForJavaApplet(),
					Name = "Applet1",
					Title = "Java Applet",
					Text = "Applet"
				}
			);

			vv.Add(
				new SolutionToolboxListViewTab
				{
					Icon = new StockToolboxImageForFlashSpriteAvalonCanvas(),
					Name = "SpriteCanvas1",
					Title = "Flash Sprite with Windows Presentation Foundation Canvas",
					Text = "Flash Canvas"
				}
			);


			vv.Add(
				new SolutionToolboxListViewTab
				{
					Icon = new StockToolboxImageForFlashSprite(),
					Name = "Sprite1",
					Title = "Flash Sprite",
					Text = "Flash"
				}
			);



			var Viewer = new SolutionDocumentViewer();
			SolutionDocumentViewerTab AboutTab = "About";
			Viewer.Add(AboutTab);

			var CurrentDesigner = new SolutionFileDesigner();




			var HTMLDesigner = new SolutionFileDesignerHTMLElementTabs();

			CurrentDesigner.Add(HTMLDesigner);

			#region CodeSource
			var CodeSourceTab =
				new SolutionFileDesignerTab
				{
					Image = new ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets.ClassViewer(),
					Text = "XDefaultPage"
				};

			var CodeSourceView = new SolutionFileView();


			CodeSourceView.Container.style.position = IStyle.PositionEnum.absolute;
			CodeSourceView.Container.style.left = "0px";
			CodeSourceView.Container.style.right = "0px";
			CodeSourceView.Container.style.top = "0px";
			CodeSourceView.Container.style.bottom = "0px";

			CodeSourceView.Container.style.display = IStyle.DisplayEnum.none;
			CodeSourceView.Container.AttachTo(CurrentDesigner.Content);

			CodeSourceTab.Deactivated +=
				delegate
				{
					CodeSourceView.Container.style.display = IStyle.DisplayEnum.none;
				};

			CodeSourceTab.Activated +=
				delegate
				{
					HTMLDesigner.HTMLDesignerContent.WhenContentReady(
						body =>
						{
							var CodeSourceFile = new SolutionFile();

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

							CodeSourceView.File = CodeSourceFile;

							CodeSourceView.Container.style.display = IStyle.DisplayEnum.empty;
						}
					);
				};


			#endregion


			CurrentDesigner.Add(CodeSourceTab);




			CurrentDesigner.First().RaiseActivated();

			AboutTab.Activated +=
				delegate
				{
					Viewer.Content.ReplaceContentWith(CurrentDesigner.Container);
				};

			Viewer.First().Activate();

			Split.Split.LeftScrollable = SolutionToolbox.Container;
			Split.Split.RightScrollable = Viewer.Container;
		}
	}
}
