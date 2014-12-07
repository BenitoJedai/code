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

            Native.document.body.Clear();
            Native.document.body.style.backgroundColor = "black";
            Native.document.body.style.margin = "0px";
            Native.document.body.style.overflow = IStyle.OverflowEnum.hidden;

            new IHTMLDiv { }.AttachToDocument().With(x => ApplicationImplementation.MakeCamGrabber(x, sizeToWindow: true));


        }
    }

    public static class ApplicationImplementation
    {
        // 20140526 roslyn friendly!
        // and broken again

        //        script: error JSC1000:
        //error:
        //  statement cannot be a load instruction(or is it a bug?)
        //  [0x000a]
        //        ldarg.0    +1 -0

        // assembly: V:\Abstractatech.JavaScript.Avatar.Application.exe
        // type: Abstractatech.JavaScript.Avatar.ApplicationImplementation+<MakeCamGrabber>d__1+<MoveNext>0600002d, Abstractatech.JavaScript.Avatar.Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
        // offset: 0x000a
        //  method:Int32<06ad> call.try(<MoveNext>0600002d, <MakeCamGrabber>d__1 ByRef, System.Runtime.CompilerServices.TaskAwaiter`1[ScriptCoreLib.JavaScript.DOM.HTML.IHTMLImage] ByRef, System.Runtime.CompilerServic

        public static async void MakeCamGrabber(
            IHTMLDiv c,
            bool sizeToWindow = false,
            Action<WebCamAvatarsSheet1Row> yield = null
            )
        {
            if (sizeToWindow)
            {
                #region onresize
                Native.window.With(
                    async window =>
                    {
                        while (true)
                        {
                            c.style.transformOrigin = "0% 0%";

                            var scale =

                                Native.window.Height / (double)(480 + 96);

                            if (Native.window.Height > Native.window.Width)
                                scale = Native.window.Width / (double)(640);


                            c.style.transform = "scale("
                                + scale
                                + ")";

                            var w = (int)(scale * (640));
                            var h = (int)(scale * (480 + 96));


                            c.style.width = w + "px";
                            c.style.height = h + "px";

                            c.style.SetLocation(
                                (Native.window.Width - w) / 2,
                                (Native.window.Height - h) / 2
                            );

                            await window.async.onresize;
                        }
                    }
                );
                #endregion

            }

            c.style.backgroundColor = "black";

            #region localStorageKeys

            // or webSQL?
            var localStorageKeys = new
            {

                img640x480 = new { img = "avatar", w = 640, h = 480 },
                img96gif = new { img = "avatar", w = 96, h = 96 },

                frames = new[] {
                            new { index= 0, img = "avatar", w = 96, h = 96 },
                            new { index= 1, img = "avatar", w = 96, h = 96 },
                            new { index= 2, img = "avatar", w = 96, h = 96 },
                            new { index= 3, img = "avatar", w = 96, h = 96 },
                        }
            };
            #endregion



            //c.css.children
            c.css.children.style.SetLocation(0, 0);

            c.style.position = IStyle.PositionEnum.relative;
            c.style.width = (640) + "px";
            c.style.height = (480 + 96) + "px";






            c.css.hover.style.cursor = IStyle.CursorEnum.pointer;

            #region empty
            var css = c.css.empty.before;

            css.style.textAlign = IStyle.TextAlignEnum.center;
            css.style.display = IStyle.DisplayEnum.block;
            css.style.width = (640) + "px";
            css.style.color = "white";
            css.style.paddingTop = 300 + "px";


            c.css.hover.empty.before.style.color = "yellow";
            #endregion




            var retry = 0;
        retry:
            retry++;

            Console.WriteLine(new { retry });


            css.contentText = "either drag a picture here -or- click here to use your webcam";

            var snapshot = new CanvasRenderingContext2D(640, 480);

            var frames = new List<IHTMLImage>();

            c.css[IHTMLElement.HTMLElementEnum.img][0].style.SetLocation(96 * 0, 480);
            c.css[IHTMLElement.HTMLElementEnum.img][1].style.SetLocation(96 * 1, 480);
            c.css[IHTMLElement.HTMLElementEnum.img][2].style.SetLocation(96 * 2, 480);
            c.css[IHTMLElement.HTMLElementEnum.img][3].style.SetLocation(96 * 3, 480);
            c.css[IHTMLElement.HTMLElementEnum.img][4].style.SetLocation(96 * 4, 480);
            c.css[IHTMLElement.HTMLElementEnum.img][5].style.SetLocation(96 * 5, 480);

            var size = 400;

            #region newmask
            Action newmask = delegate
            {
                #region grid
                new IHTMLDiv
                {

                }.AttachTo(c).With(
                    async grid =>
                    {
                        grid.style.SetLocation(
                            (640 - size) / 2,
                            (480 - size) / 2,

                            size - 2,
                            size - 2
                        );

                        var s = Stopwatch.StartNew();

                        // X:\jsc.svn\examples\javascript\LINQ\LINQWebCamAvatars\LINQWebCamAvatars\Application.cs
                        // until orphanized
                        while (c.parentNode != null)
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

                #region mask
                var mask = new CanvasRenderingContext2D(640, 480 + 96);

                mask.canvas.style.zIndex = 100;

                //mask.drawImage(
                //    v, 0, 0,

                //    mask.canvas.width,
                //    mask.canvas.height
                //);

                mask.fillStyle = "rgba(0,0,0, 0.8)";
                mask.fillRect(
                       0, 0,

                       640,
                       480 + 96
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


            };
            #endregion


            #region localStorage
            var base64 = Native.window.localStorage[localStorageKeys.img640x480];
            if (base64 != null)
            {
                var base64image = new IHTMLImage { src = base64 };

                await base64image;

                snapshot.drawImage(base64image, 0, 0, 640, 480);
                snapshot.canvas.AttachTo(c);


                for (int i = 0; i < 5; i++)
                {
                    var base64f = Native.window.localStorage[localStorageKeys.frames[
                        localStorageKeys.frames.Length - i - 1]];

                    if (base64f != null)
                    {
                        var newframe = new IHTMLImage { src = base64f };
                        newframe.AttachTo(c);
                        frames.Add(newframe);
                    }
                }



                newmask();

                var base64gif = Native.window.localStorage[localStorageKeys.img96gif];

                #region atgif
                Action<string> atgif =
                    gif =>
                    {
                        //Native.document.title = new { gif.Length }.ToString();

                        var newframe = new IHTMLImage { src = gif };

                        newframe.style.zIndex = 300;

                        newframe.AttachTo(c);
                        frames.Add(newframe);

                        //if (frames.Count > 5)
                        //    frames.Remove(frames[0].Orphanize());
                    };
                #endregion


                if (base64gif != null)
                    atgif(base64gif);
                else
                {
                    var bytes = frames.Select(x => x.bytes.Result).ToArray().AsEnumerable();

                    //bytes = bytes.Concat(bytes.Skip(1).Reverse().Skip(1)).ToArray().AsEnumerable();
                    
                        // build it
                    new GIFEncoderWorker(
                         96,
                         96,
                             delay: 1000 / 10,
                         frames: bytes,
                         AtFrame:
                          async index =>
                          {
                              //Native.document.title = new { index }.ToString();
                          }


                     ).Task.ContinueWithResult(
                        gif =>
                        {
                            Native.window.localStorage[localStorageKeys.img96gif] = gif;


                            // report sizes. smaller is better if db
                            Console.WriteLine(
                                // { Avatar640x480 = 54843, Avatar96gif = 54734 } 
                                new
                            {
                                Avatar640x480 = base64.Length,
                                Avatar96gif = gif.Length
                            }
                            );


                            if (yield != null)
                                yield(
                                    new WebCamAvatarsSheet1Row
                                {
                                    Avatar640x480 = base64,
                                    Avatar96frame1 = Native.window.localStorage[localStorageKeys.frames[0]],
                                    // do we want to report frames?
                                    Avatar96gif = gif
                                }
                                );


                            atgif(gif);
                        }
                        );
                }

            }
            #endregion

            Console.WriteLine("await c.async.onclick");
            await c.async.onclick;
            Console.WriteLine("await c.async.onclick done");

            c.Clear();

            css.content = "awaiting for video";




            var v = await Native.window.navigator.async.onvideo;


            v.AttachTo(c);
            v.play();


            var mask_css = c.css[IHTMLElement.HTMLElementEnum.canvas];



            newmask();

            var z96 = new CanvasRenderingContext2D(96, 96);

            z96.canvas.AttachTo(c);
            //z96.canvas.style.backgroundColor = "gray";
            z96.canvas.style.SetLocation(96 * 5, 480);

            z96.canvas.style.zIndex = 300;


            var ok = c.async.onclick;

            #region frames



            while (!ok.IsCompleted)
            {

                z96.drawImage(
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

                var newframe = new IHTMLImage { src = z96.canvas.toDataURL() };
                newframe.AttachTo(c);
                frames.Add(newframe);

                if (frames.Count > 5)
                    frames.Remove(frames[0].Orphanize());


                await (1000 / 15);
            }
            #endregion

            snapshot.drawImage(v, 0, 0, 640, 480);

            #region localStorage

            // https://developer.mozilla.org/en/docs/Web/API/HTMLCanvasElement
            //Native.window.localStorage[localStorageKeys.img640x480] = 

            var firstTry = snapshot.canvas.toDataURL(

            // shall we use enum
            type: "image/jpeg"
            );
            if (firstTry.Length >= (1024 * 64))
            {
                Console.WriteLine("Reducing quality");
                firstTry = snapshot.canvas.toDataURL(

            // shall we use enum
            type: "image/jpeg",
            quality: 0.5
            );
            }

            // can we use SQL instead now?
            Native.window.localStorage[localStorageKeys.img640x480] = firstTry;



            frames.WithEachIndex(
                (k, index) =>
                {
                    Native.window.localStorage[localStorageKeys.frames[index]] = k.src;
                }
            );

            Native.window.localStorage.removeItem(localStorageKeys.img96gif);
            #endregion


            v.src = "";
            c.Clear();



            goto retry;
        }

    }
}
