// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using PromotionApplicationP;
using PromotionApplicationP.HTML.Pages;
using ScriptCoreLib;
using ScriptCoreLib.Avalon;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using TestSolutionBuilderV1.Views;

namespace PromotionApplicationP
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
            @"connecting...".ToDocumentTitle();

            page.Animation.Clear();


            page.Animation.style.marginTop = (JSCSolutionsNETWhiteCarouselCanvas.DefaultHeight / -2) +  "px";

            var c = new JSCSolutionsNETWhiteCarouselCanvas();
            c.CloseOnClick = false;
            c.Container.AttachToContainer(page.Animation);

            var IsStudio = Native.Document.location.hash.StartsWith("#/studio");

            Action  ActivateStudio =
                delegate
                {
                    page.PageContainer.Clear();
                    new StudioView(null).Content.AttachToDocument();
                };

            c.AtLogoClick +=
                delegate
                {
                    Native.Document.location.hash = "#/studio";
                    ActivateStudio();
                    //new Uri("http://www.jsc-solutions.net").NavigateTo();
                };

            new ApplicationWebService().WebMethod2("powered by ",
                e =>
                {
                    e.ToDocumentTitle();
                }
            );

            if (IsStudio)
                ActivateStudio();
        }

    }
}
