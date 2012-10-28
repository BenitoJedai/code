using TestAsXElement.Design;
using TestAsXElement.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace TestAsXElement
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        public readonly ApplicationSprite sprite = new ApplicationSprite();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {


            // Initialize ApplicationSprite
            sprite.AttachSpriteToDocument();

            page.PageContainer.AsXElement().With(
                 PageContainerBefore =>
                 {
                     sprite.Foo(PageContainerBefore,
                         PageContainerBeforeAfter =>
                         {
                             page.ViewXMLSource.innerText = PageContainerBeforeAfter.ToString();
                             page.Placeholder.innerHTML = PageContainerBeforeAfter.ToString();
                         }
                     );

                 }
             );

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
