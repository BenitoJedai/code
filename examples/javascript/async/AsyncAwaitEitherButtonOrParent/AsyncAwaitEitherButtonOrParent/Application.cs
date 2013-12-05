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
using AsyncAwaitEitherButtonOrParent;
using AsyncAwaitEitherButtonOrParent.Design;
using AsyncAwaitEitherButtonOrParent.HTML.Pages;

namespace AsyncAwaitEitherButtonOrParent
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

            new IHTMLButton { "click me" }.AttachToDocument().With(
                async button =>
                {


                    var first = await Task.WhenAny(
                        Native.window.async.onmessage,
                        button.async.onclick
                    );



                    button.disabled = true;

                    button.innerText = "at event";
                }
            );



            new IHTMLButton { "click to create new iframe" }.AttachToDocument().WhenClicked(
                async button =>
                {
                    var i = new IHTMLIFrame { src = "/" }.AttachToDocument();

                    await i.async.onload;


                    button.innerText = "click to send iframe a message";

                    //await button;
                    await button.async.onclick;

                    i.contentWindow.postMessage("hi");

                    //await Task.Delay(TimeSpan.

                    button.Orphanize();
                }
            );


        }

    }
}
