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
using TestInteractiveStyleRule;
using TestInteractiveStyleRule.Design;
using TestInteractiveStyleRule.HTML.Pages;

namespace TestInteractiveStyleRule
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {

            page.Button.stylerule.before.style.content = "'" + page.Button.innerText + "'";
            page.Button.stylerule.before.print.style.content = "'" + page.Button.innerText + " for print'";

            page.Button.innerText = "";

            ((Action)(
                async delegate
                {
                    while (true)
                    {
                        page.output.stylerule.before.style.content = "'red'";
                        page.output.stylerule.before.print.style.content = "'red for print'";

                        page.Button.stylerule.hover.style.color = "red";
                        await page.Button;
                        page.output.stylerule.before.style.content = "'blue'";
                        page.output.stylerule.before.print.style.content = "'blue for print'";
                        page.Button.stylerule.hover.style.color = "blue";
                        await page.Button;
                    }
                }
            ))();
        }

    }
}
