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
using TestAwaitMutation;
using TestAwaitMutation.Design;
using TestAwaitMutation.HTML.Pages;

namespace TestAwaitMutation
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
                    // what about IL mutation, and code patching?
                    while (await Native.body.async.onmutation)
                    {
                        //Native.body.css.style.transition = "\{Native.body.css.style.backgroundColor} 0ms linear";
                        Native.body.css.style.transition = "background-color 0ms linear";
                        Native.body.css.style.backgroundColor = "yellow";
                        await Task.Delay(1);
                        Native.body.css.style.transition = "background-color 800ms linear";
                        Native.body.css.style.backgroundColor = "cyan";
                    }
                }
            );

            Native.document.onclick +=
                e =>
                {
                    Native.body.innerText = new
                    {
                        e.CursorX,
                        e.CursorY
                    }.ToString();
                };
        }

    }
}
