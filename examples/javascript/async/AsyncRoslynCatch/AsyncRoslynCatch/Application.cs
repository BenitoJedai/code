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
using AsyncRoslynCatch;
using AsyncRoslynCatch.Design;
using AsyncRoslynCatch.HTML.Pages;

using ScriptCoreLib.JavaScript.Native;

namespace AsyncRoslynCatch
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


            css.style.backgroundColor = "cyan";

            new IHTMLButton
            {
                "click me"

                //onclick += delegate
                //{

                //}
            }.AttachToDocument().onclick +=
                async delegate
            {

                try
                {
                    css.style.backgroundColor = "yellow";
                    await WebMethod2();
                    css.style.backgroundColor = "cyan";
                }
                catch (Exception ex)
                {
                    // if the server would throw us, would we be able to catch it?
                    // jsc would need to do analysis on what could be thrown tho
                    // are we allowing Stacktrace, Exceptions to be serialized yet?

                    css.style.backgroundColor = "red";

                    // retry? now that roslyn allows this. will jsc?
                    await WebMethod2();
                }
            };

        }

    }
}
