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
            Console.WriteLine("loading GIR");

            page.talk.style.With(
               (dynamic s) => s.webkitTransition = "opacity 0.1s linear"
           );


            page.idle.style.Opacity = 1;
            page.talk.style.Opacity = 0;

            Native.Window.requestAnimationFrame +=
                delegate
                {
                    IHTMLAudio snd = null;


                    Console.WriteLine("loading first sound");

                    snd = new guidancechip();
                    //Console.WriteLine("calling audio load");
                    //snd.load();

                    Console.WriteLine("preparing play");

                    var play_disabled = false;


                    Func<IHTMLAudio> random_snd =
                        delegate
                        {
                            var a = new Func<IHTMLAudio>[] {


                                ()=>new blend_in(),
                                ()=>new cant_see(),
                                ()=>new data_canister_not_yet_full(),
                                ()=>new did_you_know_that(),
                                ()=>new do_di(),
                                ()=>new excellent(),
                                ()=>new free_will(),
                                ()=>new guidancechip(),
                                ()=>new information_center(),
                                //()=>//new it_is_broken(),
                                ()=>new knowledge_fills_me(),
                                ()=>new not_acceptable(),
                                ()=>new observing(),
                                ()=>new open_the_door(),
                                ()=>new praise_me(),
                                ()=>new right_away_sir(),
                                ()=>new require_access(),
                                ()=>new television_is_stupid(),
                                ()=>new the_master(),
                                ()=>new stupidity_is_enemey(),
                                ()=>new where_is_my_mouth(),
                                //()=>//new who_is_it(),
                                ()=>new yes_sir(),
                                ()=>new yes_sir2(),
                                ()=>new yes_sir3(),
                                ()=>new yes_i_didnt_like_it(),
                                ()=>new you_are_an_intruder(),
                                ()=>new you_must_be_terminated(),

                            };

                            return a.Random()();
                        };

                    #region play
                    Action play = delegate
                    {
                        if (play_disabled)
                        {
                            // what if connection breaks and audio never finishes?


                            //                     Caused by: libcore.io.ErrnoException: sendto failed: EPIPE (Broken pipe)
                            //at libcore.io.Posix.sendtoBytes(Native Method)
                            //at libcore.io.Posix.sendto(Posix.java:151)
                            //at libcore.io.BlockGuardOs.sendto(BlockGuardOs.java:177)
                            //at libcore.io.IoBridge.sendto(IoBridge.java:473)
                            //... 29 more


                            Console.WriteLine("play disabled.");

                            return;
                        }
                        // invader.zim.[5x01].gir.goes.crazy.and.stuff
                        play_disabled = true;

                        Console.WriteLine("play!");

                        var xsnd = random_snd();

                        //xsnd.load();

                        snd.onended +=
                            delegate
                            {
                                snd = xsnd;
                                page.idle.style.Opacity = 1;
                                page.talk.style.Opacity = 0;
                                play_disabled = false;
                            };


                        page.idle.style.Opacity = 0;
                        page.talk.style.Opacity = 1;


                        snd.play();

                    };
                    #endregion

                    //page.talk.style.Opacity = 1;
                    //page.loading.style.Opacity = 0;

                    //Console.WriteLine("await canplaythrough");

                    // what if android webview does not
                    // fire this event?
                    //snd.addEventListener(
                    // "canplaythrough",
                    //     new Action(
                    //         delegate
                    //         {
                    //             Console.WriteLine("canplaythrough");


                    //page.loading.style.Opacity = 0;

                    // wont play on android?
                    //play();

                    Native.Document.body.ontouchstart +=
                        e =>
                        {
                            Console.WriteLine("ontouchstart");
                            e.preventDefault();
                            play();

                        };

                    page.idleimg.ontouchstart +=
                        e =>
                        {
                            Console.WriteLine("idleimg ontouchstart");
                            e.preventDefault();
                            play();

                        };


                    Native.Document.onclick +=
                       delegate
                       {
                           play();
                       };


                    Native.Window.onblur +=
                       delegate
                       {
                           play();
                       };
                    // does not work correctly on nexus7???
                    //Native.Window.onorientationchange +=
                    //    delegate
                    //    {
                    //        Console.WriteLine("onorientationchange");
                    //        play();
                    //    };

                    Native.Document.oncontextmenu +=
                        e =>
                        {
                            e.preventDefault();
                        };


                    Console.WriteLine("GIR operational!");
                    @"evil GIR".ToDocumentTitle();

                    //// Send data from JavaScript to the server tier
                    //service.WebMethod2(
                    //    @"A string from JavaScript.",
                    //    value => value.ToDocumentTitle()
                    //);
                };

        }

    }
}
