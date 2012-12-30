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
using HeatZeeker.Units.Tank.Design;
using HeatZeeker.Units.Tank.HTML.Pages;
using HeatZeeker.Units.Tank.HTML.Audio.FromAssets;
using ScriptCoreLib.Shared.Avalon.Tween;

namespace HeatZeeker.Units.Tank
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
                                snd.volume = snd.volume * 0.5;

                                Console.WriteLine("at timer " + new { snd.currentTime, snd.duration });
                                snd = xsnd;
                                iloop();
                            }
                        ).StartTimeout((int)(1000 * snd.duration - 0.8));


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
            var r0 = loop(new dieselloop());

            var r1 = loop(new dieselloopm5());

            var r2 = loop(new dieselloopm5m5());

            // we need SIN



            var zero = 0;

            var lookup = new[] {
                new double [] {zero, 0, 0},             
                new double [] {0, 0, 1},             
                new double [] {0, 1, 0},             
                new double [] {1, 0, 0},             
            };

            var tween_r0 = NumericEmitter.OfDouble(
                (x, y) =>
                {
                    r0(x);
                }
            );


            var tween_r1 = NumericEmitter.OfDouble(
                (x, y) =>
                {
                    r1(x);
                }
            );

            var tween_r2 = NumericEmitter.OfDouble(
                  (x, y) =>
                  {
                      r2(x);
                  }
              );


            Action masterswitchchanged = delegate
            {
                var masterswitch = int.Parse(page.masterswitch.value);

                Console.WriteLine(
                    new { masterswitch }
                );

                lookup[masterswitch].With(
                    x =>
                    {
                        tween_r0(x[0], 0);
                        tween_r1(x[1], 0);
                        tween_r2(x[2], 0);
                    }
                );
            };
            page.masterswitch.onchange +=
                delegate
                {
                    masterswitchchanged();
                };

            page.masterswitch.value = "" + (lookup.Length - 1);

            masterswitchchanged();
        }

    }
}
