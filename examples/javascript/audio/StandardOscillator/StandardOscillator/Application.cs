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
                    var start = new IHTMLButton { "start" };
                    var stop = new IHTMLButton { "stop" };

                    var a = new AudioContext();

                    var o = a.createOscillator();

                    retry:

                    var ee = await start.AttachToDocument().async.onclick;
                    start.Orphanize();

                    o.start(0);

                    o.frequency.value = 440;

                    o.type = OscillatorType.sawtooth;

                    o.connect(a.destination);

                    var e = await stop.AttachToDocument().async.onclick;
                    stop.Orphanize();

               
                    goto retry;
                }
            );


        }

    }
}
