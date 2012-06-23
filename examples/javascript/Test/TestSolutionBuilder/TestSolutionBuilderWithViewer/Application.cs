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
using ScriptCoreLib.Extensions;
using TestSolutionBuilderWithViewer.HTML.Pages;
using ScriptCoreLib.Ultra.Components.HTML.Images.FromAssets;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.Ultra.Studio;
using ScriptCoreLib.JavaScript.Concepts;
using ScriptCoreLib.Ultra.Components.HTML.Pages;
using System.Collections.Generic;
using TestSolutionBuilderWithViewer.Interactive;
using System.Text;
//using TestSolutionBuilderWithViewer.Flash;
using ScriptCoreLib.ActionScript.Components;
using TestSolutionBuilderWithViewer.Flash;
using TestSolutionBuilderWithViewer.Views;

namespace TestSolutionBuilderWithViewer
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



			page.Content = new StudioView(AddSaveButton).Content;

		}

		private void AddSaveButton(IHTMLElement C, Action<ISaveAction> y)
		{
			var ss = new SaveActionSprite();

			ss.AttachSpriteTo(C);

			ss.WhenReady(y);
		}

	}
}
