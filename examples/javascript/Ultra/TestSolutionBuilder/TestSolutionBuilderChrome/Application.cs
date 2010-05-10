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
using TestSolutionBuilderChrome.HTML.Pages;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.Ultra.Components.HTML.Pages;

namespace TestSolutionBuilderChrome
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

			#region phase 2
			var bg = new IHTMLDiv().AttachTo(page.Header);

			bg.style.SetSize(800, 600);
			bg.style.position = IStyle.PositionEnum.relative;

			var c = new IHTMLDiv().AttachTo(bg);

			c.style.left = "0px";
			c.style.right = "0px";
			c.style.bottom = "0px";
			c.style.top = "0px";
			c.style.position = IStyle.PositionEnum.absolute;

			new TwentyTenWorkspace().ToBackground(
				bg.style
			);

			var dv = new SolutionDocumentViewerPage();

			dv.TabContainer.Clear();
			dv.Container.AttachTo(c);


			Action<string> Add =
				Text =>
				{
					var Current = new IHTMLDiv().AttachTo(dv.TabContainer);

					Current.style.display = IStyle.DisplayEnum.inline_block;

					var Prototype = new SolutionDocumentViewerPage();

					var ActiveTab = Prototype.ActiveTab;
					var CandidateTab = Prototype.ActiveTab;
					var InactiveTab = Prototype.ActiveTab.AttachTo(Current);

					Current.onmouseover +=
						delegate
						{
							InactiveTab.ReplaceWith(CandidateTab);
						};

					Current.onmouseout +=
						delegate
						{
							InactiveTab.ReplaceWith(CandidateTab);
						};
				};


			Add("Default.htm");
			Add("Application.cs");

			#endregion

			new TwentyTenWorkspace().ToBackground(
				page.Background.style
			);


		
		}


	}
}
