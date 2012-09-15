using MichaelVincentProgramManager.Design;
using MichaelVincentProgramManager.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Windows.Forms;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MichaelVincentProgramManager
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            content.Augment +=
                f =>
                {
                    var c = global::ScriptCoreLib.JavaScript.Windows.Forms.Extensions.GetHTMLTargetContainer(f);

                    var i = new IHTMLIFrame
                    {
                        src = "http://www.michaelv.org/"
                    }.AttachTo(c);
                    i.style.SetSize(c);


                    content.WhenClickedGoFullscreen +=
                      b =>
                      {
                          b.Click +=
                              delegate
                              {
                                  i.requestFullscreen();
                              };
                      };
                    //new IHTMLButton { innerText = "hi" }.AttachTo(c);
                };

      

            content.AttachControlTo(page.Content);
            content.AutoSizeControlTo(page.ContentSize);
            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
