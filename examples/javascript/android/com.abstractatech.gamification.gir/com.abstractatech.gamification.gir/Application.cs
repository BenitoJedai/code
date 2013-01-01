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
using com.abstractatech.gamification.gir.Design;
using com.abstractatech.gamification.gir.HTML.Pages;
using com.abstractatech.gamification.gir.HTML.Audio.FromAssets;

namespace com.abstractatech.gamification.gir
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
            page.talk.style.With(
               (dynamic s) => s.webkitTransition = "all 0.1s linear"
           );


            guidancechip snd = null;


            Action prepare = delegate
            {
                snd = new guidancechip();
                snd.load();

                page.talk.style.Opacity = 0;
            };

            snd = new guidancechip();
            snd.load();


            Action play = delegate
            {

                var xsnd = new guidancechip();
                xsnd.load();

                snd.onended +=
                    delegate
                    {
                        snd = xsnd;

                        page.talk.style.Opacity = 0;
                    };

                snd.play();

                page.talk.style.Opacity = 1;
            };

            snd.addEventListener(
             "canplaythrough",
                 new Action(
                     delegate
                     {
                         page.talk.style.Opacity = 0;

                         Native.Document.onclick +=
                            delegate
                            {
                                play();
                            };
                     }
                 )
             );





            @"GIR".ToDocumentTitle();

            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
