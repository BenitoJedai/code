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
using CSSnthChildByWebService;
using CSSnthChildByWebService.Design;
using CSSnthChildByWebService.HTML.Pages;
using System.Diagnostics;

namespace CSSnthChildByWebService
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
            Native.document.body.css[IHTMLElement.HTMLElementEnum.button][() => index].style.color = "red";


            new Stopwatch().With(
                async w =>
                {
                    w.Start();

                    while (w.IsRunning)
                    {
                        await this.onframe;


                        await Task.Delay(1000);
                    }
                }
            );
        }

    }
}
