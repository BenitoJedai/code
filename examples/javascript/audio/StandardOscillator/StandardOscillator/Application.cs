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
using StandardOscillator;
using StandardOscillator.Design;
using StandardOscillator.HTML.Pages;
using ScriptCoreLib.JavaScript.WebAudio;

namespace StandardOscillator
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
            // http://www.sitepoint.com/using-fourier-transforms-web-audio-api/

            new { }.With(
                async delegate
                {
                    var start = new IHTMLButton { "connect" };
                    var stop = new IHTMLButton { "disconnect" };

                    var a = new AudioContext();
                    var o = a.createOscillator();

                    o.start(0);

                    o.frequency.value = 440;


                    o.type = OscillatorType.sawtooth;

                    o.frequency.valueInput = new IHTMLInput { type = ScriptCoreLib.Shared.HTMLInputTypeEnum.range, min = 1, max = 2000 }.AttachToDocument();


                    new IHTMLLabel {
                        () =>
                            $"frequency: { o.frequency.value }Hz"
                            + $" type: { o.type }"
                    }.AttachToDocument();




                    //.onchange +=
                    //    eee =>
                    //    {
                    //        var i = ((IHTMLInput)eee.Element);

                    //        o.frequency.value = i.valueAsNumber;
                    //    };

                    new IHTMLHorizontalRule { }.AttachToDocument();

                    new IHTMLButton { nameof(OscillatorType.sawtooth) }.AttachToDocument().onclick += delegate { o.type = OscillatorType.sawtooth; };
                    new IHTMLButton { nameof(OscillatorType.sine) }.AttachToDocument().onclick += delegate { o.type = OscillatorType.sine; };
                    new IHTMLButton { nameof(OscillatorType.square) }.AttachToDocument().onclick += delegate { o.type = OscillatorType.square; };
                    new IHTMLButton { nameof(OscillatorType.triangle) }.AttachToDocument().onclick += delegate { o.type = OscillatorType.triangle; };

                    new IHTMLHorizontalRule { }.AttachToDocument();

                    //s.Add()

                    new IHTMLButton { "Beep()" }.AttachToDocument().onclick +=
                        async delegate
                        {
                            //Console.Beep(frequency: 400, duration: 300);

                            o.frequency.value = 400;

                            o.type = OscillatorType.square;


                            o.connect(o.context.destination);

                            await Task.Delay(300);

                            o.disconnect();
                        };

                    new IHTMLButton { "Console.Beep()" }.AttachToDocument().onclick +=
                       delegate
                      {
                          Console.Beep();

                          //Console.Beep(frequency: 400, duration: 300);


                      };

                    new IHTMLButton { () => $"Console.Beep({ o.frequency.value }Hz, 300)" }.AttachToDocument().onclick +=
                      delegate
                      {
                          Console.Beep(frequency: (int)o.frequency.value, duration: 300);
                      };

                    retry:


                    var ee = await start.AttachToDocument().async.onclick;
                    start.Orphanize();

                    o.connect(a.destination);

                    var e = await stop.AttachToDocument().async.onclick;
                    stop.Orphanize();

                    o.disconnect();


                    goto retry;
                }
            );


        }

    }
}
