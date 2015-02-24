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
using ElementsChangedByWebService;
using ElementsChangedByWebService.Design;
using ElementsChangedByWebService.HTML.Pages;

namespace ElementsChangedByWebService
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


            new { }.With(
                async delegate
                {

                    var frame = new IHTMLIFrame();

                    frame.AttachToDocument();

                    frame.src = "about:blank";

                    await frame.async.onload;

                    frame.contentWindow.document.DesignMode = true;

                    // Additional information: The 'link' start tag on line 3 position 6 does not match the end tag of 'body'. Line 12, position 38.
                    this.content = frame.contentWindow.document.body;

                    while (await page.Yield.async.onclick)
                    {

                        Console.WriteLine(
                            new { this.content }
                            );

                        await this.yield();
                    }
                }
            );



        }

    }
}
