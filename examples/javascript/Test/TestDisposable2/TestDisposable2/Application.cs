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
using TestDisposable2;
using TestDisposable2.Design;
using TestDisposable2.HTML.Pages;

namespace WebApplication1
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
            page.feature4 = Feature2.Class1.Foo();

            this.yield = text =>
            {
                new IHTMLPre { new { text } }.AttachToDocument();
            };


            var x = (IHTMLButton)this.button1.AttachToDocument();

            x.WhenClicked(
                async button =>
                {
                    await this.onclick();

                }
           );
        }

    }
}
