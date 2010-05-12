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
				Value = 0.3,
			};

			Split.With(ApplyStyle);

			Split.Split.Splitter.style.backgroundColor = Color.None;

			Split.Container.AttachTo(Workspace);



			var SolutionToolbox = new SolutionDockWindowPage();

			SolutionToolbox.HeaderText.innerText = "Toolbox";
			SolutionToolbox.Content.style.backgroundColor = Color.White;

			SolutionToolbox.Content.ReplaceContentWith(
				new ToolboxItemsPage().Container
			);


			Split.Split.LeftContainer = SolutionToolbox.Container;



			var Viewer = new SolutionDocumentViewer();
			SolutionDocumentViewerTab AboutTab = "About";
			Viewer.Add(AboutTab);

			var CurrentDesigner = new SolutionFileDesigner();

			#region Design
			var Design = new SolutionFileDesignerTab
			{
				Text = "Design",
				Image = new RTA_mode_design()
			};

			var DesignContent = new IHTMLIFrame { src = "about:blank" };

			DesignContent.style.position = IStyle.PositionEnum.absolute;
			DesignContent.style.left = "0px";
			DesignContent.style.width = "100%";
			DesignContent.style.top = "0px";
			DesignContent.style.height = "100%";

			DesignContent.style.border = "0";
			DesignContent.style.margin = "0";
			DesignContent.style.padding = "0";

			DesignContent.frameborder = "0";
			DesignContent.border = "0";

			DesignContent.WhenDocumentReady(
				document =>
				{
					document.WithContent(StockPageDefault.Element);
					document.DesignMode = true;
				}
			);

			DesignContent.style.display = IStyle.DisplayEnum.none;
			DesignContent.AttachTo(CurrentDesigner.Content);

			#endregion


			#region HTMLSource
			var HTMLSource = new SolutionFileDesignerTab
			{
				Text = "Source",
				Image = new RTA_mode_html()
			};


			var HTMLSourceView = new SolutionFileView();
			var HTMLSourceFile = new SolutionFile();

		


			HTMLSourceView.Container.style.position = IStyle.PositionEnum.absolute;
			HTMLSourceView.Container.style.left = "0px";
			HTMLSourceView.Container.style.right = "0px";
			HTMLSourceView.Container.style.top = "0px";
			HTMLSourceView.Container.style.bottom = "0px";

			HTMLSourceView.Container.style.display = IStyle.DisplayEnum.none;
			HTMLSourceView.Container.AttachTo(CurrentDesigner.Content);


			#endregion


			Design.Deactivated +=
				delegate
				{
					DesignContent.style.display = IStyle.DisplayEnum.none;
				};

			Design.Activated +=
				delegate
				{
					DesignContent.style.display = IStyle.DisplayEnum.empty;
				};

			HTMLSource.Deactivated +=
				delegate
				{
					HTMLSourceView.Container.style.display = IStyle.DisplayEnum.none;
				};

			HTMLSource.Activated +=
				delegate
				{
					HTMLSourceFile.Clear();

					DesignContent.WhenContentReady(
						body =>
						{
							HTMLSourceFile.WriteHTMLElement(body.AsXElement());

							// update
							HTMLSourceView.File = HTMLSourceFile;

							HTMLSourceView.Container.style.display = IStyle.DisplayEnum.empty;
						}
					);


				};

			HTMLSourceFile.WriteHTMLElement(StockPageDefault.Element);
			HTMLSourceView.File = HTMLSourceFile;


			#region CodeSource
			var CodeSourceTab =
				new SolutionFileDesignerTab
				{
					Image = new ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets.ClassViewer(),
					Text = "DefaultPage"
				};

			var CodeSourceView = new SolutionFileView();
			var CodeSourceFile = new SolutionFile();

			KnownLanguages.VisualCSharp.WriteType(CodeSourceFile, new StockXElementType(), null);

			CodeSourceView.File = CodeSourceFile;

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
					CodeSourceView.Container.style.display = IStyle.DisplayEnum.empty;
				};


			#endregion


			CurrentDesigner.Add(Design);
			CurrentDesigner.Add(HTMLSource);
			CurrentDesigner.Add(CodeSourceTab);

			CurrentDesigner.First().RaiseActivated();

			AboutTab.Activated +=
				delegate
				{
					Viewer.Content.ReplaceContentWith(CurrentDesigner.Container);
				};

			Viewer.First().Activate();

			Split.Split.RightContainer = Viewer.Container;
		}
	}
}
