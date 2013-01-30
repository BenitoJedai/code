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
            FormStyler.AtFormCreated = FormStyler.LikeVisualStudioMetro;
            FormStyler.AtFormCreated = FormStyler.LikeWindows3;
            content.label2.Text = "Open this application from " + Native.Document.location.href;


            content.WhenClickedGoFullscreen +=
              (b, f) =>
              {
                  var c = global::ScriptCoreLib.JavaScript.Windows.Forms.Extensions.GetHTMLTargetContainer(f);

                  b.Click +=
                      delegate
                      {
                          c.requestFullscreen();
                      };
              };

            var once = false;

            content.NewForm +=
                f =>
                {
                    if (once)
                        return;

                    once = true;
                    //f.DisableFormClosingHandler = true;

                    global::CSSMinimizeFormToSidebar.ApplicationExtension.InitializeSidebarBehaviour(
                        f
                    );
                };


            //content.WhenClickedGoFullscreen +=
            //  (b, f) =>
            //  {
            //      var c = global::ScriptCoreLib.JavaScript.Windows.Forms.Extensions.GetHTMLTargetContainer(f);

            //      b.Click +=
            //          delegate
            //          {
            //              c.requestFullscreen();
            //          };
            //  };


            //content.AttachControlTo(Native.Document.body);

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
