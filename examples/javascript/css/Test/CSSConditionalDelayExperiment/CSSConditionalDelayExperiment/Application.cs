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
using CSSConditionalDelayExperiment;
using CSSConditionalDelayExperiment.Design;
using CSSConditionalDelayExperiment.HTML.Pages;

namespace CSSConditionalDelayExperiment
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

            Native.document.body.css.before.contentText = "click me!";

            (Native.document.body.css > 900).style.color = "red";

            (Native.document.body.css + Native.document.documentElement.async.onclick).before.contentText = "thanks for clicking !!!";

            (Native.document.body.css + Native.document.documentElement.async.onclick + 300).before.contentText = "thanks for clicking";

            //((Native.document.body.css > 900) + Native.document.documentElement.async.onclick > 300).before.contentText = "thanks for clicking after 900";

            //(Native.document.body.css + Native.document.documentElement.async.onclick > 300 > 300).before.contentText = "thanks for clicking 600";

        }

    }
}
