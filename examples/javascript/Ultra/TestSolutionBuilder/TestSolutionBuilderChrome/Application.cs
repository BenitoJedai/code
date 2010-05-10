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
using ScriptCoreLib.JavaScript.Components;

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

			c.style.left = "4px";
			c.style.right = "4px";
			c.style.bottom = "4px";
			c.style.top = "4px";
			c.style.position = IStyle.PositionEnum.absolute;

			new TwentyTenWorkspace().ToBackground(
				bg.style
			);


			var dv = new SolutionDocumentViewer
			{
				"Default.htm",
				"Application.cs",
				"ApplicationWebService.cs",
				"XX XXX XX XXX XX XXX XXX XXx XXX XXX",
				
			};

			dv.Add(
				new SolutionDocumentViewerTab
				{
					Text = "Program.cs"
				}.With(
					k =>
					{
						k.Activated +=
							delegate
							{
								dv.Content.innerHTML = "hello world";
							};

					}
				)
			);

			dv.Container.AttachTo(c);
			dv.Last().Activate();


			#endregion

			new TwentyTenWorkspace().ToBackground(
				page.Background.style
			);



		}


	}
}
