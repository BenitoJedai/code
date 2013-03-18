using HerokuFacebookLoginApp;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.external;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;

namespace HerokuFacebookLogin.ActionScript
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            // https://code.google.com/p/chromium/issues/detail?id=42796
            // https://code.google.com/p/chromium/issues/detail?id=58909
            // https://code.google.com/p/chromium/issues/detail?id=42796
            // https://groups.google.com/a/chromium.org/forum/?fromgroups=#!msg/chromium-apps/BcC1N2nZuew/BqnyRlNSa0MJ

            // error: { errorID = 0, error = SecurityError: Error #2060, e = [UncaughtErrorEvent type="uncaughtError" bubbles=true cancelable=true eventPhase=2] }
            Security.allowDomain("*");
            Security.allowInsecureDomain("*");

            this.loaderInfo.uncaughtErrorEvents.uncaughtError +=
               e =>
               {
                   //Console.WriteLine("error: " + new { e.errorID, e.error, e } + "\n run in flash debugger for more details!");

                   // http://www.adobetutorialz.com/articles/1785/1/Local-Sandboxes
                   content.t.Text = ("error: " + new { e.errorID, e.error, e });
               };

            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);

                    HerokuFacebookLoginAppLoginExperienceForFlash.Diagnostics =
                        x => content.t.Text = x;

                    content.t.Text = new { Security.sandboxType }.ToString();


                    Func<string, string> yield_poll =
                           (string x) =>
                           {
                               content.t.Text = x;

                               return "foo";
                           };

                    ExternalExtensions.TryAddCallback("yield_poll", yield_poll.ToFunction());

                    content.MouseLeftButtonUp +=
                        delegate
                        {
                            HerokuFacebookLoginAppLoginExperienceForFlash.Login(
                                (string id, string name, string third_party_id) =>
                                {
                                    content.t.Text = new { id, name, third_party_id }.ToString();

                                }
                            );
                        };



                }
            );
        }

        // this is for the default experience
        // this can not be used if mixing differenc compilations
        public void set_Login(Action<HerokuFacebookLoginAppLoginExperienceAction> value)
        {
            HerokuFacebookLoginAppLoginExperienceForFlash.__Login = value;

        }

    }


    public static class HerokuFacebookLoginAppLoginExperienceForFlash
    {
        public static Action<string> Diagnostics = delegate { };


        public static bool InitializeDone;

        public static void Initialize(Action init_done)
        {
            if (InitializeDone)
            {
                init_done();
                return;
            }

            InitializeDone = true;

            Diagnostics("Initialize...");


            // does __Login exist?

            // add a callback

            ExternalInterface.call("setTimeout", "window.__check__Login = function (e) { return '__Login' in window; };", 0);

            Diagnostics("prepared __check__Login...");

            100.AtDelay(
             delegate
             {
                 Diagnostics("call __check__Login...");

                 var value = (bool)ExternalInterface.call("__check__Login");

                 if (value)
                 {
                     // js app is ready for us!

                     var y = default(HerokuFacebookLoginAppLoginExperienceAction);

                     HerokuFacebookLoginAppLoginExperienceAction yield_to_sprite =
                            (string id, string name, string third_party_id) =>
                            {
                                if (y == null)
                                    return;

                                y(id, name, third_party_id);
                                y = null;
                            };

                     ExternalExtensions.TryAddCallback("__Login_yield", yield_to_sprite.ToFunction());

                     __Login = yield =>
                         {
                             y = yield;

                             Diagnostics("context switch to js");

                             var value_Login = ExternalInterface.call("__Login");
                         };


                     init_done();
                 }
                 else
                 {
                     Diagnostics("js app needs init");
                 }
             }
            );

        }

        public static Action<HerokuFacebookLoginAppLoginExperienceAction> __Login;

        public static void Login(HerokuFacebookLoginAppLoginExperienceAction yield)
        {
            Initialize(
                delegate
                {
                    // initialized?
                    // cached?

                    if (__Login == null)
                    {
                        //yield("id", "name", "1");
                        return;
                    }

                    __Login(yield);
                }
            );
        }
    }
}
