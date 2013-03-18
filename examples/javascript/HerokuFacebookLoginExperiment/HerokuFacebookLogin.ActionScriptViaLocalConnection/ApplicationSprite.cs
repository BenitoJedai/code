using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System;
using System.Windows.Media;
using System.Xml.Linq;

namespace HerokuFacebookLogin.ActionScriptViaLocalConnection
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        // X:\jsc.svn\examples\actionscript\LocalConnectionExperiment\LocalConnectionExperiment\ApplicationSprite.cs


        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);

                    HerokuFacebookLoginAppLoginExperienceViaLocalConnection.Diagnostics =
                        x => content.t.Text = x;

                    content.MouseLeftButtonUp +=
                        delegate
                        {
                            content.r.Fill = Brushes.Yellow;

                            HerokuFacebookLoginAppLoginExperienceViaLocalConnection.Invoke(
                                (string id, string name, string third_party_id, string accessToken) =>
                                {


                                    content.t.Text = new { id, name, third_party_id, accessToken }.ToString();

                                    content.r.Fill = Brushes.Green;


                                }
                            );
                        };
                }
            );
        }

    }
}
