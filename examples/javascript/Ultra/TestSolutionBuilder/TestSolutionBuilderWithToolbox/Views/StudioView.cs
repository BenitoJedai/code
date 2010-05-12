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

			var EditorTreeSplit = new HorizontalSplit
			{
				Minimum = 0,
				Maximum = 1,
				Value = 0.3,
			};

			EditorTreeSplit.With(ApplyStyle);

			EditorTreeSplit.Split.Splitter.style.backgroundColor = Color.None;

			EditorTreeSplit.Container.AttachTo(Workspace);



			var SolutionToolbox = new SolutionDockWindowPage();

			SolutionToolbox.HeaderText.innerText = "Toolbox";
			SolutionToolbox.Content.style.backgroundColor = Color.White;
			SolutionToolbox.Content.style.padding = "2px";

			//SolutionToolbox.Content.ReplaceContentWith();


			EditorTreeSplit.Split.LeftContainer = SolutionToolbox.Container;



			var Viewer = new SolutionDocumentViewer();
			SolutionDocumentViewerTab AboutTab = "About";
			Viewer.Add(AboutTab);

			var AboutDesigner = new SolutionFileDesigner();

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
			DesignContent.AttachTo(AboutDesigner.Content);

			#endregion


			#region source
			var Source = new SolutionFileDesignerTab
			{
				Text = "Source",
				Image = new RTA_mode_html()
			};


			var v = new SolutionFileView();

			var f = new SolutionFile();

			f.WriteHTMLElement(StockPageDefault.Element);

			v.File = f;



			v.Container.style.position = IStyle.PositionEnum.absolute;
			v.Container.style.left = "0px";
			v.Container.style.right = "0px";
			v.Container.style.top = "0px";
			v.Container.style.bottom = "0px";

			v.Container.style.display = IStyle.DisplayEnum.none;
			v.Container.AttachTo(AboutDesigner.Content);


			#endregion



			Design.Activated +=
				delegate
				{
					v.Container.style.display = IStyle.DisplayEnum.none;
					DesignContent.style.display = IStyle.DisplayEnum.empty;
				};

			Source.Activated +=
				delegate
				{
					f.Clear();

					DesignContent.WhenContentReady(
						body =>
						{
							f.WriteHTMLElement(body.AsXElement());

							// update
							v.File = f;

							v.Container.style.display = IStyle.DisplayEnum.empty;
						}
					);


					DesignContent.style.display = IStyle.DisplayEnum.none;
				};

			AboutDesigner.Add(Design);
			AboutDesigner.Add(Source);

			AboutDesigner.First().RaiseActivated();

			AboutTab.Activated +=
				delegate
				{
					Viewer.Content.ReplaceContentWith(AboutDesigner.Container);
				};

			Viewer.First().Activate();

			EditorTreeSplit.Split.RightContainer = Viewer.Container;
		}
	}
}
