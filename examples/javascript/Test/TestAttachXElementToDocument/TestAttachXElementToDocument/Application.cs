using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TestAttachXElementToDocument;
using TestAttachXElementToDocument.Design;
using TestAttachXElementToDocument.HTML.Pages;

namespace TestAttachXElementToDocument
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            page.body.AsXElement().Add(
                new XElement("button", "hello world")
            );

            page.body.AsXElement().Add(
                 new XElement("button", new XElement("b", "hello world"))
             );


            this.Content().ContinueWithResult(
                xml =>
                {
                    page.body.AsXElement().Add(
                           xml
                      );





                }
            );

            this.Content().ContinueWithResult(
                  xxml =>
                  {


                      page.body.AsXElement().Add(
                          new XElement("button", xxml)
                      );

                  }
              );

            this.Content().ContinueWithResult(
                xxml =>
                {

                    IStyleSheet.Default[".special"].style.color = "red";
                    IStyleSheet.Default[".special b"].style.color = "blue";

                    page.output.AsXElement().ReplaceWith(
                        new XElement("button",
                            new XAttribute("class", "special"),
                            xxml)
                    );

                }
            );
        }

    }
}
