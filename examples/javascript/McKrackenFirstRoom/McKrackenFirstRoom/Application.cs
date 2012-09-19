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
using McKrackenFirstRoom.Design;
using McKrackenFirstRoom.HTML.Pages;
using ScriptCoreLib.JavaScript.Runtime;

namespace McKrackenFirstRoom
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
        public Application(IDefaultPage page)
        {
            var music = new HTML.Audio.FromAssets.zak();
            music.loop = true;
            music.load();

            music.play();

            new Timer(
                delegate
                {
                    var clocksound = new HTML.Audio.FromAssets._548202_SOUNDDOGS__cl();
                    clocksound.loop = true;

                    clocksound.volume = 0;
                    clocksound.onended +=
                        delegate
                        {
                            clocksound.pause();
                            clocksound.currentTime = 0;
                            clocksound.play();
                        };

                    clocksound.play();

                    new Timer(
                        t =>
                        {
                            if (t.Counter > 800)
                                return;

                            music.volume = 1 - (t.Counter / 1000);
                            if (t.Counter > 200)
                            {
                                clocksound.volume = (t.Counter - 200) / (1000 - 200);
                            }

                            if (t.Counter == 800)
                                t.Stop();
                        }
                    ).StartInterval(20);
                }
            ).StartTimeout(2000);

            new NatureBoy.js.Class5().Initialize();

            @"Mr. McKracken".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
