using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using com.abstractatech.gamification.gir.Design;
using com.abstractatech.gamification.gir.HTML.Pages;
using com.abstractatech.gamification.gir.HTML.Audio.FromAssets;
using ScriptCoreLib.JavaScript.Runtime;

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


            IHTMLAudio snd = null;



            snd = new guidancechip();
            snd.load();


            #region play
            Action play = delegate
            {
                // invader.zim.[5x01].gir.goes.crazy.and.stuff

                var xsnd =
                    new IHTMLAudio[] {


                    new blend_in(),
                    new cant_see(),
                    new data_canister_not_yet_full(),
                    new did_you_know_that(),
                    new do_di(),
                    new excellent(),
                    new free_will(),
                    new guidancechip(),
                    new information_center(),
                    //new it_is_broken(),
                    new knowledge_fills_me(),
                    new not_acceptable(),
                    new observing(),
                    new open_the_door(),
                    new praise_me(),
                    new right_away_sir(),
                    new require_access(),
                    new television_is_stupid(),
                    new the_master(),
                    new stupidity_is_enemey(),
                    new where_is_my_mouth(),
                    //new who_is_it(),
                    new yes_sir(),
                    new yes_sir2(),
                    new yes_sir3(),
                    new yes_i_didnt_like_it(),
                    new you_are_an_intruder(),
                    new you_must_be_terminated(),



                }.Random();

                xsnd.load();

                snd.onended +=
                    delegate
                    {
                        snd = xsnd;

                        page.idle.style.Opacity = 1;
                        page.talk.style.Opacity = 0;
                    };


                page.idle.style.Opacity = 0;
                page.talk.style.Opacity = 1;

                Native.Window.requestAnimationFrame +=
                    delegate
                    {
                        snd.play();
                    };

            };
            #endregion

            page.talk.style.Opacity = 1;
            page.loading.style.Opacity = 0;

            snd.addEventListener(
             "canplaythrough",
                 new Action(
                     delegate
                     {
                         page.idle.style.Opacity = 1;
                         page.talk.style.Opacity = 0;

                         // wont play on android?
                         //play();

                         Native.Document.body.ontouchstart +=
                             e =>
                             {
                                 e.preventDefault();
                                 play();

                             };

                         Native.Document.onclick +=
                            delegate
                            {
                                play();
                            };

                         Native.Window.onorientationchange +=
                         delegate
                         {
                             play();
                         };
                     }
                 )
             );

            Native.Document.oncontextmenu +=
                e =>
                {
                    e.preventDefault();
                };


            Console.WriteLine("GIR operational!");
            @"GIR".ToDocumentTitle();

            //// Send data from JavaScript to the server tier
            //service.WebMethod2(
            //    @"A string from JavaScript.",
            //    value => value.ToDocumentTitle()
            //);
        }

    }
}
