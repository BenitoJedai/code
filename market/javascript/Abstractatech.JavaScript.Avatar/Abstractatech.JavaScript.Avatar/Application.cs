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
using Abstractatech.JavaScript.Avatar;
using Abstractatech.JavaScript.Avatar.Design;
using Abstractatech.JavaScript.Avatar.HTML.Pages;
using ScriptCoreLib.Lambda;
using System.Diagnostics;


namespace Abstractatech.JavaScript.Avatar
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
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2013/201312/20131229-avatar


            new IHTMLDiv { }.AttachToDocument().With(
                async c =>
                {

                    //c.css.children
                    c.css[">*"].style.SetLocation(0, 0);

                    c.style.position = IStyle.PositionEnum.relative;
                    c.style.backgroundColor = "black";

                    c.style.width = (640 + 96) + "px";
                    c.style.height = 480 + "px";

                    var css = c.css.empty.before;

                    css.content = "either drag a picture here -or- click here to use your webcam";
                    css.style.textAlign = IStyle.TextAlignEnum.center;
                    css.style.display = IStyle.DisplayEnum.block;
                    css.style.width = (640 + 96) + "px";
                    css.style.color = "white";
                    css.style.paddingTop = 200 + "px";


                    c.css.hover.empty.before.style.color = "yellow";
                    c.css.hover.empty.style.cursor = IStyle.CursorEnum.pointer;

                    await c.async.onclick;

                    css.content = "awaiting for video";

                    var v = await Native.window.navigator.async.onvideo;

                    v.AttachTo(c);
                    v.play();

                    var size = 320;

                    var mask_css = c.css[IHTMLElement.HTMLElementEnum.canvas];

                    #region grid
                    new IHTMLDiv
                    {

                    }.AttachTo(c).With(
                        async grid =>
                        {
                            grid.style.SetLocation(
                                (640 - size) / 2,
                                (480 - size) / 2,

                                size,
                                size
                            );

                            var s = Stopwatch.StartNew();

                            while (true)
                            {
                                await Native.window.requestAnimationFrameAsync;

                                var a = (Math.Cos(s.ElapsedMilliseconds * 0.001) + 1) / 2.0;

                                grid.style.border = "1px dotted rgba(255,255,255, "
                                    + (1.0 - a)
                                    + ")";

                                //mask_css.style.Opacity = a;

                            }
                        }
                    );
                    #endregion

                    var z = new CanvasRenderingContext2D(96, 96);


                    z.canvas.AttachTo(c);
                    z.canvas.style.backgroundColor = "gray";
                    z.canvas.style.SetLocation(640, 480, 96, 96);



                    #region mask
                    var mask = new CanvasRenderingContext2D(640, 480);

                    //mask.drawImage(
                    //    v, 0, 0,

                    //    mask.canvas.width,
                    //    mask.canvas.height
                    //);

                    mask.fillStyle = "rgba(0,0,0, 0.5)";
                    mask.fillRect(
                           0, 0,

                           640,
                           480
                       );


                    mask.clearRect(
                          (640 - size) / 2,
                            (480 - size) / 2,

                            size,
                            size
                    );



                    //var bytes = i.bytes;

                    mask.canvas.AttachTo(c);
                    #endregion


                    var frames = new List<IHTMLImage>();

                    c.css[IHTMLElement.HTMLElementEnum.img][0].style.SetLocation(640, 96 * 0);
                    c.css[IHTMLElement.HTMLElementEnum.img][1].style.SetLocation(640, 96 * 1);
                    c.css[IHTMLElement.HTMLElementEnum.img][2].style.SetLocation(640, 96 * 2);
                    c.css[IHTMLElement.HTMLElementEnum.img][3].style.SetLocation(640, 96 * 3);
                    c.css[IHTMLElement.HTMLElementEnum.img][4].style.SetLocation(640, 96 * 4);

                    //await 1000;
                    while (true)
                    {
                        //c.css.style


                        z.drawImage(
                            image: v,
                            sx: (640 - size) / 2,
                            sy: (480 - size) / 2,

                            sw: size,
                            sh: size,
                            dx: 0,
                            dy: 0,
                            dw: 96,
                            dh: 96
                        );

                        var newframe = new IHTMLImage { src = z.canvas.toDataURL() };


                        newframe.AttachTo(c);
                        frames.Add(newframe);

                        if (frames.Count > 5)
                            frames.Remove(frames[0].Orphanize());


                        await (1000 / 15);
                    }

                }
            );


        }

    }
}
