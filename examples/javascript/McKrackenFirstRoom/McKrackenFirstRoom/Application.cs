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
using McKrackenFirstRoom.HTML.Audio.FromAssets;

namespace McKrackenFirstRoom
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


#if FCHROME
            #region ChromeTCPServer
            dynamic self = Native.self;
            dynamic self_chrome = self.chrome;
            object self_chrome_socket = self_chrome.socket;

            if (self_chrome_socket != null)
            {
                chrome.Notification.DefaultIconUrl = new HTML.Images.FromAssets.Preview().src;
                chrome.Notification.DefaultTitle = "McKrackenFirstRoom";


                ChromeTCPServer.TheServerWithStyledForm.Invoke(
                    AppSource.Text
                );

                return;
            }
            #endregion

#endif
            // why does it activate in float mode?
            // shake should make it go away?
            //global::DiagnosticsConsole.ApplicationContent.BindKeyboardToDiagnosticsConsole();


            var music = new HTML.Audio.FromAssets.zak().AttachToDocument();
            music.loop = true;
            //music.load();

            music.play();

            new Timer(
                delegate
                {
                    #region clocksound
                    var clocksound = default(_548202_SOUNDDOGS__cl);

                    Action loop = null;

                    loop = delegate
                    {
                        var volume = 0.0;
                        if (clocksound != null)
                            volume = clocksound.volume;

                        clocksound = new _548202_SOUNDDOGS__cl { volume = volume }.AttachToDocument();

                        clocksound.onended +=
                            delegate
                            {
                                Console.WriteLine(" music.onended ");
                                clocksound.Orphanize();

                                loop();
                            };

                        clocksound.play();

                    };

                    loop();
                    #endregion

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


            new NatureBoy.js.Class5().Control.style.SetLocation(0, 0);


            @"Mr. McKracken".ToDocumentTitle();
            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
