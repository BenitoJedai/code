// For more information visit:
// http://studio.jsc-solutions.net/

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
using TestLinkedAssets.HTML.Pages;
using TestLinkedAssets;

namespace TestLinkedAssets
{
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        public string Property1 { get; set; }

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            // Update document title
            // http://do.jsc-solutions.net/Update-document-title


            ShowFiles(page);
           

            @"Hello world".ToDocumentTitle();
            // Send xml to server
            // http://do.jsc-solutions.net/Send-xml-to-server

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
                delegate(XElement doc)
                {
                    // Show server message as document title
                    // http://do.jsc-solutions.net/Show-server-message-as-document-title

                    doc.Element(@"Data").Value.ToDocumentTitle();
                }
            );
        }

        private static void ShowFiles(IDefaultPage page)
        {
            var p = new Assets.Publish();

            foreach (string key in p.Keys)
            {
                new IHTMLDiv { 
                    new IHTMLOrderedList
                    {
                        new IHTMLListItem { new IHTMLAnchor { href = p[key],  innerText = key } },
                        new IHTMLListItem { new IHTMLAnchor { href = "/publish/" + key,  innerText = key } },
                    }
                }.AttachTo(page.Content);
            }
        }

    }
}
