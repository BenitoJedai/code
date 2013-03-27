using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using FurbeeByRoman.Design;
using FurbeeByRoman.HTML.Pages;

namespace FurbeeByRoman
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
            // http://www.romancortes.com/blog/furbee-my-js1k-spring-13-entry/

            Native.Window.onresize +=
              delegate
              {
                  var sw = (double)Native.Window.Width / (double)page.c.width;
                  var sh = (double)Native.Window.Height / (double)page.c.height;
                  var scale = sh;

                  if (sw < sh)
                  {
                      scale = sw;
                  }


                  page.cc.style.width = (1000 * scale) + "px";
                  page.cc.style.height = (560 * scale) + "px";

                  page.c.style.transform = "scale(" + scale + ")";

                  page.c.style.transformOrigin = "0% 0%";

              };
        }

    }
}
