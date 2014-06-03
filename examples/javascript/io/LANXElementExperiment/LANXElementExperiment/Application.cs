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
using LANXElementExperiment;
using LANXElementExperiment.Design;
using LANXElementExperiment.HTML.Pages;

namespace LANXElementExperiment
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
            Key = new Random().Next();

            Native.document.documentElement.onmousemove +=
                e =>
                {
                    this.x = e.CursorX;
                    this.y = e.CursorY;
                };

            new Action(async delegate
            {
                while (true)
                {
                    await Task.Delay(500);

                    await yield();

                    page.PageContainer.Clear();

                    foreach (var item in others)
                    {
                        new IHTMLPre {

                            new {
                            item.Key, item.x, item.y }
                        }.AttachTo(page.PageContainer);
                    }
                }
            }
            )();
        }

    }
}
