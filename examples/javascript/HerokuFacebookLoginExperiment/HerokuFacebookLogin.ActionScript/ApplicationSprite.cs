using HerokuFacebookLoginApp;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;

namespace HerokuFacebookLogin.ActionScript
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);


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


        public void set_Login(Action<HerokuFacebookLoginAppLoginExperienceAction> value)
        {
            HerokuFacebookLoginAppLoginExperienceForFlash.__Login = value;

        }

    }


    public static class HerokuFacebookLoginAppLoginExperienceForFlash
    {
        public static void Initialize()
        {

        }

        public static Action<HerokuFacebookLoginAppLoginExperienceAction> __Login;

        public static void Login(HerokuFacebookLoginAppLoginExperienceAction yield)
        {
            // initialized?
            // cached?

            if (__Login == null)
            {
                yield("id", "name", "1");
                return;
            }

            __Login(yield);
        }
    }
}
