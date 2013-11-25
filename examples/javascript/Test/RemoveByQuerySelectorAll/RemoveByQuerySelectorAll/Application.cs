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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using RemoveByQuerySelectorAll;
using RemoveByQuerySelectorAll.Design;
using RemoveByQuerySelectorAll.HTML.Pages;

namespace RemoveByQuerySelectorAll
{
    using html = IHTMLElement.HTMLElementEnum;
    using css = IStyleSheet;


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
            // X:\jsc.internal.svn\compiler\jsc.meta\jsc.meta\Commands\Rewrite\RewriteToJavaScriptDocument.InjectJavaScriptBootstrap.cs
            //Native.document.body.querySelectorAll("script[src='view-source']").Orphanize();



            new IHTMLButton { "select and then remove" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    css.all[html.p].style.color = "red";

                    Native.document.title = "before 2nd click";

                    await button;


                    (
                        from x in html.p
                        where x.innerText != "keep it"
                        select x
                    ).Orphanize();


                    Native.document.title = "before 3nd click";
                    await button;

                    //html.button[
                    html.button.Orphanize();
                }
            );


        }

    }
}
