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
using HeatZeeker.Design;
using HeatZeeker.HTML.Pages;
using ScriptCoreLib.Shared.Avalon.Tween;

namespace HeatZeeker
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
            // why cant we creat svg objects yet?

            var h = new Hind();

            h.Container.AttachTo(page.worldzoom);

            h.Container.style.SetLocation(800, 1200);
            page.world.style.transformOrigin = "800px 1400px";

            var r = 0.0;
            var t = new IDate().getTime();

            Action loop = null;

            var frames = 0;
            var ddt = 0l;

            loop = delegate
            {
                var tt = new IDate().getTime();
                var dt = tt - t;
                t = tt;

                frames++;
                ddt += dt;

                if (ddt > 1000)
                {

                    var fps = frames;

                    Native.Document.title = new { fps }.ToString();
                    page.fps.innerText = "" + fps;

                    frames = 0;
                    ddt = 0;
                }

                r += double.Parse(page.range.value) * 0.01 * dt;

                //page.wings.style.transform = " rotate(" + r + "deg)";
                h.wings.style.transform = " rotate(" + r + "deg)";

                Native.window.requestAnimationFrame += loop;
            };

            Native.window.requestAnimationFrame += loop;


            var tween_worldrotation = NumericEmitter.OfDouble(
                 (worldrotation, y) =>
                 {

                     page.world.style.transform = "translate(0px, -200px) rotate(" + worldrotation + "deg)";
                 }
             );


            page.worldrotation.onchange +=
                delegate
                {
                    var worldrotation = int.Parse(page.worldrotation.value);

                    worldrotation += 180;
                    //worldrotation = worldrotation % 360;

                    tween_worldrotation(worldrotation, 0);
                };

            @"Hello world".ToDocumentTitle();
            // Send data from JavaScript to the server tier
            service.WebMethod2(
                @"A string from JavaScript.",
                value => value.ToDocumentTitle()
            );
        }

    }
}
