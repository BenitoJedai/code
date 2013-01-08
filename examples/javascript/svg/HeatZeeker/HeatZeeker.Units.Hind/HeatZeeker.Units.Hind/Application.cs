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
using HeatZeeker.Units.Hind.Design;
using HeatZeeker.Units.Hind.HTML.Pages;
using HeatZeeker.Units.Hind.HTML.Audio.FromAssets;

namespace HeatZeeker.Units.Hind
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
            {

                Func<IHTMLAudio, Action<double>> loop = snd =>
                {

                    Action iloop = null;

                    var r = new IHTMLInput();
                    var label = new IHTMLLabel(snd.src, r).AttachToDocument();

                    dynamic rr = r;

                    rr.type = "range";

                    r.AttachToDocument();

                    Action newvolume = delegate
                    {

                        snd.volume = int.Parse(r.value) / 100;
                    };
                    r.onchange +=
                        delegate
                        {
                            newvolume();
                        };

                    iloop =
                        delegate
                        {
                            Console.WriteLine("at canplaythrough " + new { snd.duration });


                            newvolume();
                            snd.play();


                            snd.onended +=
                                delegate
                                {
                                    Console.WriteLine("at onended");

                                };

                            //var xsnd = snd;

                            var xsnd = new IHTMLAudio { src = snd.src };
                            xsnd.autobuffer = true;

                            new ScriptCoreLib.JavaScript.Runtime.Timer(
                                delegate
                                {
                                    Console.WriteLine("at timer " + new { snd.currentTime, snd.duration });
                                    snd = xsnd;
                                    iloop();
                                }
                            ).StartTimeout((int)(1000 * snd.duration - 0.4));


                            xsnd.load();
                        };

                    snd.autobuffer = true;

                    // http://areweplayingyet.org/event-canplaythrough
                    Console.WriteLine("waiting for canplaythrough");

                    snd.addEventListener(
                          "canplaythrough",
                          new Action(
                              delegate
                              {

                                  iloop();
                              }
                          )
                    );

                    new IHTMLBreak().AttachToDocument();

                    return
                        nextvolume =>
                        {


                            r.value = "" + (int)(nextvolume * 100);

                            newvolume();
                        };
                };


                //loop(new helicopter1wav());
                var r0 = loop(new helicopter1());

                r0(1);

            }
            {
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
                        //page.fps.innerText = "" + fps;

                        frames = 0;
                        ddt = 0;
                    }

                    r += 50 * 0.01 * dt;

                    //page.wings.style.transform = " rotate(" + r + "deg)";
                    page.wings.style.transform = " rotate(" + r + "deg)";

                    Native.Window.requestAnimationFrame += loop;
                };

                Native.Window.requestAnimationFrame += loop;
            }
        }

    }
}
