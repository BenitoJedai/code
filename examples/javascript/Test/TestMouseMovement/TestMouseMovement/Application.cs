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
using TestMouseMovement;
using TestMouseMovement.Design;
using TestMouseMovement.HTML.Images.FromAssets;
using TestMouseMovement.HTML.Pages;

namespace TestMouseMovement
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
            // "X:\jsc.svn\examples\javascript\android\MultiMouse\MultiMouse.SVGCursor\MultiMouse.SVGCursor.csproj"

            new IStyle(Native.document.head.css | IHTMLElement.HTMLElementEnum.title).display = IStyle.DisplayEnum.block;

            var c = Native.document.documentElement.css.before;
            c.contentImage = new cursor1();

            var c0 = Native.document.documentElement.css.after;
            c0.contentImage = new cursor1();


            //Native.document.documentElement.style.cursorImage = new cursor1();

            new IStyle().cursorImage = new cursor1();



            var PointerLock_false = new XAttribute("PointerLock", Convert.ToString(false));

            PointerLock_false.AttachTo(Native.document.documentElement);

            Native.document.documentElement.css[PointerLock_false].before.style.visibility = IStyle.VisibilityEnum.hidden;
            Native.document.documentElement.css[PointerLock_false].after.style.visibility = IStyle.VisibilityEnum.hidden;

            var x = 0;
            var y = 0;


            (page.Foo.css + page.Foo.async.onclick).style.backgroundColor = "yellow";
            (page.Bar.css + page.Bar.async.onclick).style.backgroundColor = "cyan";

            Native.document.documentElement.style.overflow = IStyle.OverflowEnum.hidden;

            Native.document.documentElement.onclick +=
                e =>
                {
                    var pointerLock = e.Element == Native.document.pointerLockElement;

                    if (pointerLock)
                    {
                        Native.document.elementFromPoint(x, y).With(
                            z => z.click()
                        );

                    }
                };




            Native.document.documentElement.onmousedown +=
                e =>
                {
                    // this pauses pointerLockElement
                    //e.CaptureMouse();

                    if (e.MouseButton == IEvent.MouseButtonEnum.Middle)
                    {
                        Native.document.documentElement.requestFullscreen();
                        Native.document.documentElement.requestPointerLock();
                    }
                    else
                    {
                        e.CaptureMouse();
                    }
                };


            Native.document.documentElement.onmousemove +=
                e =>
                {
                    // https://dvcs.w3.org/hg/pointerlock/raw-file/default/index.html#why-use-.movementx-y-instead-of-.deltax-y

                    var pointerLock = e.Element == Native.document.pointerLockElement;

                    PointerLock_false.Value = Convert.ToString(pointerLock);

                    if (pointerLock)
                    {
                        x = (x + e.movementX + Native.window.Width + cursor1.ImageDefaultWidth) % Native.window.Width - cursor1.ImageDefaultWidth;
                        y = (y + e.movementY + Native.window.Height + cursor1.ImageDefaultHeight) % Native.window.Height - cursor1.ImageDefaultHeight;
                    }
                    else
                    {
                        x = e.CursorX;
                        y = e.CursorY;
                    }


                    c.style.SetLocation(
                        x,
                        y + 1
                    );

                    x = (x + Native.window.Width) % Native.window.Width;
                    y = (y + Native.window.Height) % Native.window.Height;

                    c0.style.SetLocation(
                        x,
                        y + 1
                    );


                    var hot = Native.document.elementFromPoint(x, y);


                    //                    var target = document.elementFromPoint(x, y)
                    //if (target)
                    //  target.dispatchEvent(ee)


                    new { about = "middle click to see wrapping cursor", pointerLock, e.OffsetX, e.OffsetY, e.movementX, e.movementY, e.MouseButton, hot }.ToString().ToDocumentTitle();


                };
        }

    }
}
