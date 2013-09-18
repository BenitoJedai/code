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
            #region TheServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultTitle = "Craft";
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                ChromeTCPServer.TheServer.Invoke(
                    AppSource.Text
                );


                return;
            }
            #endregion



            // Process com.abstractatech.gamification.craft (pid 1456) has died.
            // http://stackoverflow.com/questions/7316680/process-has-died

            // http://zproxy.wordpress.com/2012/11/13/dos-warcraft/
            // load the shadow land

            var cursor = "url('" + new severed().src + "'), auto";

            Console.WriteLine(new { cursor });

            // why does this not work??
            (page.body.style as dynamic).cursor = cursor;
            (page.Preview.style as dynamic).cursor = "url('" + new guantlet().src + "'), auto";
            (page.Avatar.style as dynamic).cursor = "url('" + new guantlet().src + "'), auto";

            // http://www.effectgames.com/effect/article.psp.html/joe/Old_School_Color_Cycling_with_HTML5
            // http://www.effectgames.com/demos/canvascycle/
            // http://productforums.google.com/forum/#!topic/chrome/hgxRgmT0INY
            // http://www.boutell.com/newfaq/creating/midi.html
            //new com.abstractatech.gamification.craft.HTML.Audio.FromAssets.intro().AttachToHead().play();

            // http://www.javascripter.net/faq/sound/play.htm

            new HTML.Audio.FromAssets.IntroWarII { loop = true, volume = 0.5 }.play();

            //new IHTMLObject
            //{
            //    data =
            //        new com.abstractatech.gamification.craft.HTML.Audio.FromAssets.intro().src
            //}.AttachToHead();

            //            <object data="mysong.mid">
            //<param name="loop" value="10"/>
            //If you're seeing this, you don't have a MIDI player
            //on your computer.
            //</object>


            // IStyleSheet.Default.AddRule("body", "cursor: url('" + new cursor().src + "'), auto;", 0);
            // 

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
            // could run out of memory fast!
            var map = new Point(2048 * 3, 2048 * 2);

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
                .Take(6)
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
