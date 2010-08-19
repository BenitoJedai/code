// For more information visit:
// http://studio.jsc-solutions.net/

using System;
using System.Text;
using System.Linq;
using System.Xml.Linq;
using ScriptCoreLib;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Extensions;
using MultitouchPong.HTML.Pages;
using MultitouchPong;

namespace MultitouchPong
{
    /// <summary>
    /// This type can be used from javascript. The method calls will seamlessly be proxied to the server.
    /// </summary>
    public sealed class Application
    {
        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IDefaultPage page)
        {
            // wallE

            page.Content.onmousedown +=
                e =>
                {
                    e.PreventDefault();
                    page.Log.value =
         "onmousedown " +
         new
         {
             x = e.GetOffsetX(page.Content),
             y = e.GetOffsetY(page.Content),
         }.ToString() + "\n" + page.Log.value;
                };

            page.Content.ontouchstart +=
        e =>
        {
            e.PreventDefault();

            page.Log.value =
                "ontouchstart " +
                new
                {
                    x = e.GetOffsetX(page.Content),
                    y = e.GetOffsetY(page.Content),
                    e.streamId
                }.ToString() + "\n" + page.Log.value;
        };

            page.Content.ontouchend +=
e =>
{
    e.PreventDefault();

    page.Log.value =
        "ontouchend " +
        new
        {
            x = e.GetOffsetX(page.Content),
            y = e.GetOffsetY(page.Content),
            e.streamId
        }.ToString() + "\n" + page.Log.value;
};

            page.Content.ontouchmove +=
                e =>
                {
                    e.PreventDefault();

                    page.Log.value =
                        "ontouchmove " +
                        new
                        {
                            x = e.GetOffsetX(page.Content),
                            y = e.GetOffsetY(page.Content),
                            e.streamId
                        }.ToString() + "\n" + page.Log.value;
                };
        }

    }
}
