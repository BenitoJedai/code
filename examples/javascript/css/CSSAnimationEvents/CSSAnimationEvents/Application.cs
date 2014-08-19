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
using CSSAnimationEvents;
using CSSAnimationEvents.Design;
using CSSAnimationEvents.HTML.Pages;

namespace CSSAnimationEvents
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
            // https://developer.mozilla.org/en-US/docs/Web/Guide/CSS/Using_CSS_animations


            IStyleSheet.all[".foo"].style.transition = "color 3000ms linear";
            IStyleSheet.all[".foo"].style.color = "red";


            var x = new IHTMLButton { "go" }.AttachToDocument();


            // transition not creating animationstart events?
            x.onanimationstart +=
                delegate
            {
                new IHTMLPre {
                    "onanimationstart"
                }.AttachToDocument();
            };

            x.onanimationiteration +=
                delegate
            {
                new IHTMLPre {
                    "onanimationiteration"
                }.AttachToDocument();
            };

            x.onanimationend +=
                delegate
            {
                new IHTMLPre {
                    "onanimationend"
                }.AttachToDocument();
            };
            x.onclick +=
                e =>
            {

                e.Element.className = "foo";
            };

        }

    }
}
