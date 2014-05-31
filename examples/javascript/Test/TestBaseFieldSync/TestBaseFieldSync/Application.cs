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
using TestBaseFieldSync;
using TestBaseFieldSync.Design;
using TestBaseFieldSync.HTML.Pages;

namespace TestBaseFieldSync
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
            Native.css.style.backgroundColor = "cyan";

            Native.window.onbeforeunload +=
                e =>
                {
                    e.Text = "Shall we close the window?";

                };

            base.close =
                async delegate
            {
                Native.css.style.backgroundColor = "yellow";

                await Task.Delay(1300);

                // will this call IDispose? yes

                Native.window.close();
            };

            //base.close = Native.window.close;

            new IHTMLPre { this.Tag }.AttachToDocument();


            // implicit databind?
            // Error	2	Cannot implicitly convert type 'System.Xml.Linq.XElement' to 'ScriptCoreLib.JavaScript.DOM.HTML.IHTMLDiv'. An explicit conversion exists (are you missing a cast?)	X:\jsc.svn\examples\javascript\Test\TestBaseFieldSync\TestBaseFieldSync\Application.cs	38	28	TestBaseFieldSync
            // x:\jsc.svn\examples\javascript\linq\test\vb\testxmlselect\testxmlselect\application.vb
            // it works in vb, why?
            page.content = (IHTMLDiv)base.content.AsHTMLElement();

            Native.document.documentElement.onclick +=
                async delegate
            {
                page.content.Add(new IHTMLPre { new { status = "will call invoke" } });

                Tag += " onclick";

                await Invoke();

                page.content.Add(new IHTMLPre { new { status = "will call invoke complete" } });
            };
        }

    }
}
