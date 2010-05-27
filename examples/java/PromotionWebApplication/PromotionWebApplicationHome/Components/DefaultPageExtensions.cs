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
using PromotionWebApplicationHome.HTML.Pages;
using PromotionWebApplicationHome;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Avalon;

namespace PromotionWebApplicationHome.Components
{
    public static class DefaultPageExtensions
    {
        public static void AnimateHomePage(this IDefaultPage page)
        {
            var w = new JSCSolutionsNETWhiteCarouselCanvas();

            w.CloseOnClick = false;

            page.LogoContainer.Clear();

            var we = w.Container.ToHTMLElement();

            we.style.SetLocation((400 - 96) / -2, (400 - 96) / -2);

            page.LogoContainer.ReplaceContentWith(we);

            Action Deactivate = delegate { };
            Action Activate = null;

            Action<IHTMLAnchor, IHTMLElement> Bind =
                (Link, Content) =>
                {
                    Link.onmouseover +=
                       delegate
                       {
                           Deactivate();

                           if (Content == null)
                               Link.style.color = Color.Green;
                           else
                               Link.style.color = Color.Red;

                           Link.style.borderBottom = "2px solid red";
                       };

                    Link.onmouseout +=
                       delegate
                       {
                           Link.style.color = Color.Black;
                           Link.style.borderBottom = "";
                           Activate();
                       };

                    Action LinkDeactivate =
                         delegate
                         {
                             Link.style.borderBottom = "0px solid red";
                         };

                    Action LinkActivate =
                        delegate
                        {

                            Link.style.borderBottom = "2px solid red";
                        };

                    if (Activate == null)
                    {
                        Deactivate = LinkDeactivate;
                        Activate = LinkActivate;
                    }

                    if (Content != null)
                    {
                        Link.onclick +=
                          e =>
                          {
                              Deactivate();

                              Deactivate = LinkDeactivate;
                              Activate = LinkActivate;

                              Activate();

                              e.PreventDefault();

                              page.CurrentContent.ReplaceContentWith(Content);
                          };
                    }
                };

            Bind(page.HomeLink, page.HomeContent);
            Bind(page.AboutLink, page.AboutContent);
            Bind(page.VisionLink, page.VisionContent);
            Bind(page.ContactLink, page.ContactContent);
            Bind(page.DownloadLink, null);
        }

    }
}
