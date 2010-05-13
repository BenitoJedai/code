using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.Ultra.Library;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Ultra.Studio.StockPages;

namespace ScriptCoreLib.JavaScript.Components
{
	public class SolutionFileDesignerHTMLElementTabs : Disposable
	{
		public readonly SolutionFileDesignerTab HTMLDesignerTab = new SolutionFileDesignerTab
		{
			Text = "Design",
			Image = new RTA_mode_design()
		};

		public readonly SolutionFileDesignerTab HTMLSourceTab = new SolutionFileDesignerTab
		{
			Text = "Source",
			Image = new RTA_mode_html()
		};

		public readonly IHTMLIFrame HTMLDesignerContent = new IHTMLIFrame { src = "about:blank" };

		public readonly SolutionFileView HTMLSourceView = new SolutionFileView();
		public readonly SolutionFile HTMLSourceFile = new SolutionFile();


		public SolutionFileDesignerHTMLElementTabs()
		{

			HTMLDesignerContent.style.position = IStyle.PositionEnum.absolute;
			HTMLDesignerContent.style.left = "0px";
			HTMLDesignerContent.style.width = "100%";
			HTMLDesignerContent.style.top = "0px";
			HTMLDesignerContent.style.height = "100%";

			HTMLDesignerContent.style.border = "0";
			HTMLDesignerContent.style.margin = "0";
			HTMLDesignerContent.style.padding = "0";

			HTMLDesignerContent.frameborder = "0";
			HTMLDesignerContent.border = "0";

			HTMLDesignerContent.WhenDocumentReady(
				document =>
				{
					document.WithContent(StockPageDefault.Element);
					document.DesignMode = true;
				}
			);

			HTMLDesignerContent.style.display = IStyle.DisplayEnum.none;


		



			HTMLSourceView.Container.style.position = IStyle.PositionEnum.absolute;
			HTMLSourceView.Container.style.left = "0px";
			HTMLSourceView.Container.style.right = "0px";
			HTMLSourceView.Container.style.top = "0px";
			HTMLSourceView.Container.style.bottom = "0px";

			HTMLSourceView.Container.style.display = IStyle.DisplayEnum.none;





			HTMLDesignerTab.Deactivated +=
				delegate
				{
					HTMLDesignerContent.style.display = IStyle.DisplayEnum.none;
				};

			HTMLDesignerTab.Activated +=
				delegate
				{
					HTMLDesignerContent.style.display = IStyle.DisplayEnum.empty;
				};

			HTMLSourceTab.Deactivated +=
				delegate
				{
					HTMLSourceView.Container.style.display = IStyle.DisplayEnum.none;
				};

			HTMLSourceTab.Activated +=
				delegate
				{
					HTMLSourceFile.Clear();

					HTMLDesignerContent.WhenContentReady(
						body =>
						{
							HTMLSourceFile.WriteHTMLElement(body.AsXElement());

							// update
							HTMLSourceView.File = HTMLSourceFile;

							HTMLSourceView.Container.style.display = IStyle.DisplayEnum.empty;
						}
					);


				};

		}
	}
}
