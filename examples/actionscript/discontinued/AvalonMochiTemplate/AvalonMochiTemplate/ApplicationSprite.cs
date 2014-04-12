using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace AvalonMochiTemplate
{

    //Error: MochiAd requires a clip that is an instance of a dynamic class.  If your class extends Sprite or MovieClip, you must make it dynamic.

    [ScriptCoreLib.ActionScript.DynamicType]
    public sealed class ApplicationSprite : Init
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        public ApplicationSprite()
        {
            this.InvokeWhenStageIsReady(
                () =>
                {
                    content.AttachToContainer(this);
                    content.AutoSizeTo(this.stage);


                }
            );
        }

    }
}
