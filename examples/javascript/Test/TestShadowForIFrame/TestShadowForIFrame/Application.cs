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
using TestShadowForIFrame;
using TestShadowForIFrame.Design;
using TestShadowForIFrame.HTML.Pages;

namespace TestShadowForIFrame
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

            // X:\jsc.svn\examples\javascript\test\TestShadowForIFrame\TestShadowForIFrame\Application.cs
            var yt1 = Native.document.querySelectorAll(" [class='youtube-player']");

            Console.WriteLine("yt found " + new { yt1.Length });
            yt1.WithEach(
                  e =>
                  {
                      var xiframe = (IHTMLIFrame)e;

                      // can we interact, swap the videos?

                      var size = new { xiframe.clientWidth, xiframe.clientHeight };

                      var swap = new IHTMLDiv {
								 new IHTMLPre { xiframe.src },

                                 //new IHTMLContent {  }
							 };

                      new IStyle(swap)
                      {
                          backgroundColor = "yellow",

                          width = size.clientWidth + "px",
                          height = size.clientHeight + "px",
                          overflow = IStyle.OverflowEnum.hidden
                          
                      };

                      // https://code.google.com/p/dart/issues/detail?id=19561
                      // 27ms HierarchyRequestError: Failed to execute 'createShadowRoot' on 'Element': Author-created shadow roots are disabled for this element.
                      //swap.AttachTo(xiframe.shadow);

                      xiframe.ReplaceWith(swap);

                      xiframe.AttachTo(swap);

                      

                  }
             );
        }

    }
}
