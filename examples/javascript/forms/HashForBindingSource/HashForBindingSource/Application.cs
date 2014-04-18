using HashForBindingSource;
using HashForBindingSource.Design;
using HashForBindingSource.HTML.Pages;
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
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace HashForBindingSource
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application : ApplicationWebService
    {
        public readonly ApplicationControl content = new ApplicationControl();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201404/20140418

            content.AttachControlToDocument();

            Console.WriteLine(
                new { Native.document.location.hash }
                );


            // notice chrome will not show hash or url in dev version anymore!
            // 4:71ms { hash =  }
            content.ParentForm.Text =
                Native.document.location.hash;

            Native.window.onhashchange +=
                e =>
                {
                    content.ParentForm.Text =
                        Native.document.location.hash;

                };
        }

    }
}
