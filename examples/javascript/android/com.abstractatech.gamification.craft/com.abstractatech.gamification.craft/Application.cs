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
using com.abstractatech.gamification.craft;
using com.abstractatech.gamification.craft.Design;
using com.abstractatech.gamification.craft.HTML.Pages;
using ScriptCoreLib.JavaScript.Controls.LayeredControl;
using ScriptCoreLib.Shared.Drawing;
using ScriptCoreLib.Shared.Lambda;
using com.abstractatech.gamification.craft.HTML.Images.FromAssets;

namespace com.abstractatech.gamification.craft
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        // jsc should do correctly uri escapes
        // \kennedy.greg%40gmail.com\warcraft\dungeon1.png

        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            // http://zproxy.wordpress.com/2012/11/13/dos-warcraft/
            // load the shadow land

            var cursor = "url('" + new severed().src + "')";

            Console.WriteLine(new { cursor });

            // why does this not work??
            (page.Preview.style as dynamic).cursor = cursor;
            (page.Preview.style as dynamic).cursor = "url('" + new guantlet().src + "')";

            // do we have to use MIDI.js?
            //new HTML.Audio.FromAssets.Warcraft1_TitleTheme().play();

            #region OrcSounds
            var orc = new Func<Action>(
                delegate
                {
                    //                    I/ChromiumHTTPDataSourceSupport(  124): Request failed with status 4 and os_error -102
                    //I/chromium(27962): [INFO:CONSOLE(40699)] "Uncaught InvalidStateError: An attempt was made to use an object that is not, or is no longer, usable.", source: http://192.168.43.7:18906/view-source (40699)

                    var nodes = new OrcSounds().AudioElements()

                        // will we run out of memory?
                        //.Take(2)

                        .ToArray();

                    nodes.WithEach(a => a.load());

                    return delegate
                    {
                        var r = new Random().Next();
                        var i = r % nodes.Length;

                        Console.WriteLine(new { i });

                        nodes[i].play();


                        var src = nodes[i].src;

                        nodes[i] = (IHTMLAudio)nodes[i].cloneNode(false);

                        //nodes[i].src = src;
                        //nodes[i].load();

                    };
                }
            )();
            #endregion


            page.Preview.onclick +=
                delegate
                {
                    orc();
                };

            // vb xlinq ..<img>


            #region arena
            var map = new Point(2048 * 4, 2048 * 5);

            var arena = new ArenaControl();

            arena.Layers.Canvas.style.backgroundColor = Color.FromGray(0);
            arena.SetLocation(Rectangle.Of(0, 0, Native.window.Width, Native.window.Height));
            arena.SetCanvasSize(map);

            arena.Control.AttachToDocument();


            page.header.AttachTo(
                arena.Layers.User
            );

            //arena.DrawTextToInfo("Craft", new Point(8, 8), Color.White);

            Native.window.onresize +=
                delegate
                {
                    arena.SetLocation(
                        Rectangle.Of(0, 0, Native.window.Width, Native.window.Height));

                    arena.SetCanvasPosition(
                        arena.CurrentCanvasPosition
                        );
                };
            #endregion

            //arena.CurrentCanvasPosition = 
            arena.SetCanvasViewCenter(
              new Point(
                  1048 + 2048,
                  1024 + 2048
              )
            );


            //{ X = -2645 } 
            //{ X = -2290 } 


            //Console.WriteLine(new { arena.CurrentCanvasPosition.X });
            Console.WriteLine(new { arena.GetCanvasViewCenter().X });

            new ScriptCoreLib.JavaScript.Runtime.Timer(
                delegate
                {
                    // two fingers?
                    if (arena.DragEngaged)
                        return;

                    if (arena.InSelectionMode)
                        return;



                    var pp = arena.GetCanvasViewCenter();

                    //Console.WriteLine(new { pp.X });

                    pp.X += 1;

                    //Console.WriteLine(new { pp.X });

                    arena.SetCanvasViewCenter(pp);
                }
            ).StartInterval(1000 / 5);

            var scalarx = 0;

            new WarcraftImages().ImageElements().Randomize()
                //.Take(4)
                .WithEachIndex(
                (img, index) =>
                {
                    // we need 4x images. think retina displays.
                    Console.WriteLine(new { index });

                    img.InvokeOnComplete(
                        delegate
                        {
                            var context = new CanvasRenderingContext2D(img.width * 2, img.height * 2)
                            {
                                // http://phoboslab.org/log/html5
                                // not supported for IE
                                ImageSmoothingEnabled = false
                            };


                            context.drawImage(img, 0, 0, context.canvas.width, context.canvas.height);


                            var x = scalarx % map.X;
                            var y = (int)Math.Floor((float)scalarx / map.X) * context.canvas.height;

                            //Console.WriteLine(new { index, x, y });

                            context.canvas.AttachTo(arena.Layers.Canvas).style.SetLocation(
                                x, y
                            );

                            scalarx += context.canvas.width;
                        }
                    );

                    //img.AttachToDocument();
                }
            );
        }

    }
}
