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
using TestQuerySelectorFromServer;
using TestQuerySelectorFromServer.Design;
using TestQuerySelectorFromServer.HTML.Pages;

namespace TestQuerySelectorFromServer
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
            //this.__document_location_reload = Native.document.location.reload;
            this.__document_location_reload = () => Native.document.location.reload();

            this.query_setInternalElement =
                (selector, InternalElement) =>
                {
                    Console.WriteLine(
                        "query_setInternalElement " + new { selector, InternalElement }
                        );

                    Native.document.body.querySelectorAll(selector).WithEach(
                        x =>
                        {
                            var e = (IHTMLElement)x;


                            //e.AsXElement().ReplaceAttributes(InternalElement.Attributes());
                            //e.AsXElement().ReplaceNodes(InternalElement.Nodes());

                            e.AsXElement().ReplaceAll(InternalElement);
                        }
                    );
                };

            this.query_onclick =
                (selector, qmethod) =>
                {
                    Native.document.body.querySelectorAll(selector).WithEach(
                         x =>
                         {
                             var e = (IHTMLElement)x;

                             e.onclick +=
                                 delegate
                                 {
                                     this.__QMethod_Invoke(
                                        qmethod,
                                        selector,
                                        e
                                     );

                                 };
                         }
                     );
                };

            this.query_setStyle =
               (selector, styleName, styleValue) =>
               {
                   IStyleSheet.Default[selector].style.setProperty(styleName, styleValue, "");


               };

            this.query_setInnerText =
                (selector, innerText) =>
                {
                    Native.document.body.querySelectorAll(selector).WithEach(
                        x => ((IHTMLElement)x).innerText = innerText
                    );

                };

            this.Fill();

        }

    }
}
