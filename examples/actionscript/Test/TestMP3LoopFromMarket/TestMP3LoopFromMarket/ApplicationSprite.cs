using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace TestMP3LoopFromMarket
{
    public sealed class ApplicationSprite : Sprite
    {
        public readonly ApplicationCanvas content = new ApplicationCanvas();

        MP3LoopExperiment.ApplicationSprite loop = new MP3LoopExperiment.ApplicationSprite();


        public ApplicationSprite()
        {
            loop.enabled = true;

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
