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
using TestDragStart;
using TestDragStart.Design;
using TestDragStart.HTML.Pages;

namespace TestDragStart
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
            //http://en.wikipedia.org/wiki/Magnet_URI_scheme

            page.Header.ondragstart +=
                e =>
                {

                    // what other mime types are available?

                    e.dataTransfer.setData("text/uri-list", "http://my.jsc-solutions.net");

                };

            page.text.ondragstart +=
              e =>
              {
                  // https://bugzilla.mozilla.org/show_bug.cgi?id=567343
                  // this will not work!
                  // X:\jsc.svn\examples\javascript\appengine\AppEngineImplicitDataRow\AppEngineImplicitDataRow\Application.cs
                  e.dataTransfer.setData("text/uri-list", "http://my.jsc-solutions.net");

              };



        }

    }
}
