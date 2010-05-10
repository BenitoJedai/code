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

			page.Content.Add("hello world");
		}


	}
}
