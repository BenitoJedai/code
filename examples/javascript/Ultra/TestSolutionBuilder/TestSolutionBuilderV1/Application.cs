// For more information visit:
// http://studio.jsc-solutions.net

// View as Visual Basic project
// http://do.jsc-solutions.net/View-as-Visual-Basic-project

// View as Visual FSharp project
// http://do.jsc-solutions.net/View-as-Visual-FSharp-project

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using TestSolutionBuilderV1.HTML.Pages;
using TestSolutionBuilderV1.Views;

namespace TestSolutionBuilderV1
{
	/// <summary>
	/// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
	/// </summary>
	public sealed class Application
	{
		public string Property1
		{
			get;
			set;
		}
		/// <summary>
		/// This is a javascript application.
		/// </summary>
		/// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
		public Application(IDefaultPage page)
		{
			
			// Update document title
			// http://do.jsc-solutions.net/Update-document-title

			@"Hello world".ToDocumentTitle();
			// Send xml to server
			// http://do.jsc-solutions.net/Send-xml-to-server

			page.Content = new StudioView(null).Content;


			new ApplicationWebService().WebMethod2(
				new XElement(@"Document", 
					new object[] {
						new XElement(@"Data", 
							new object[] {
								@"Hello world"
							}
						),
						new XElement(@"Client", 
							new object[] {
								@"Unchanged text"
							}
						)
					}
				),
				delegate (XElement doc)
				{
					// Show server message as document title
					// http://do.jsc-solutions.net/Show-server-message-as-document-title

					doc.Element(@"Data").Value.ToDocumentTitle();
				}
			);
		}

	}
}
