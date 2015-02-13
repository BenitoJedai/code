using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.JavaScript.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TiltDatabaseExperiment.Design;
using TiltDatabaseExperiment.HTML.Pages;

namespace TiltDatabaseExperiment
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

            var dx = 0;
            var dy = 0;

            #region onmousemove
            Native.Document.body.tabIndex = 101;
            Native.Document.body.onmousedown +=
                e =>
                {
                    e.preventDefault();
                    Native.Document.body.focus();
                    Native.Document.body.requestPointerLock();
                    Native.Document.body.style.backgroundColor = JSColor.Yellow;
                };

            Native.Document.body.onmousemove +=
                e =>
                {
                    if (Native.Document.pointerLockElement == Native.Document.body)
                    {
                        dy += e.movementY;
                        dx += e.movementX;


                    }
                    else
                    {

                    }
                };


            Native.Document.body.onmouseup +=
                 e =>
                 {
                     Native.Document.body.style.backgroundColor = JSColor.None;
                     if (Native.Document.pointerLockElement == Native.Document.body)
                     {
                         Native.Document.exitPointerLock();
                     }
                 };
            #endregion

            #region ontouchmove
            var touchx = 0;
            var touchy = 0;


            Native.Document.body.ontouchend +=
                e =>
                {
                    Native.Document.body.style.backgroundColor = JSColor.None;
                };

            Native.Document.body.ontouchstart +=
             e =>
             {
                 Native.Document.body.style.backgroundColor = JSColor.Yellow;
                 touchx = e.touches[0].pageX;
                 touchy = e.touches[0].pageY;

                 e.preventDefault();
             };

            Native.Document.body.ontouchmove +=
              e =>
              {
                  e.preventDefault();

                  var ztouchx = e.touches[0].pageX;
                  var ztouchy = e.touches[0].pageY;

                  dy += (ztouchy - touchy);
                  dx += (ztouchx - touchx);

                  touchx = ztouchx;
                  touchy = ztouchy;
              };
            #endregion


            var id = new Random().Next();
            var frame = 0;

            var zdx = 0;
            var zdy = 0;

            Action loop = null;

            loop = delegate
            {
                frame++;

                page.idelement.innerText = "" + id;
                page.dx.innerText = "" + dx;
                page.dy.innerText = "" + dy;

                // heavy stress on network.

                if (frame > 1)
                    if (dx == zdx)
                        if (dy == zdy)
                        {
                            // check again
                            Native.window.requestAnimationFrame += loop;

                            return;
                        }

                zdx = dx;
                zdy = dy;

                service.AtFrame("" + id, "" + frame, "" + dx, "" + dy,
                    yield:
                        delegate
                        {
                            Native.window.requestAnimationFrame += loop;
                        }
                );
            };

            Native.window.requestAnimationFrame += loop;

            @"Hello world".ToDocumentTitle();

        }

    }

    static class X
    {
        public static void AtAnimationFrame(this Action e)
        {
            Action x = null;

            x = delegate
            {
                e();
                Native.window.requestAnimationFrame += x;

            };

            Native.window.requestAnimationFrame += x;
        }
    }
}
