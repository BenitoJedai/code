// For more information visit:
// http://studio.jsc-solutions.net/

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
using LANMulticast.HTML.Pages;
using LANMulticast.Components;
using LANMulticast;
using ScriptCoreLib.JavaScript.Runtime;

namespace LANMulticast
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
            // Initialize MySprite1

            var s = new MySprite1();

            s.AtMessage +=
                x => page.Log.value += Environment.NewLine + x;

            s.AttachSpriteTo(page.Content);

            new Timer(
                t =>
                {
                    s.PostMessage("timer: " + t.Counter);
                }
                , 500, 1500
            );

            page.Send.onclick +=
                delegate
                {
                    s.PostMessage("entry:" + page.Entry.value);
                };

            @"Hello world".ToDocumentTitle();
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
                    doc.Element(@"Data").Value.ToDocumentTitle();
                }
            );
        }

    }
}
