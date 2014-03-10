using Abstractatech.ActionScript.Audio;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions;

namespace AIRAudioWorker
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140310

            if (Worker.current.isPrimordial)
            {
                var w = WorkerDomain.current.createWorker(
                    this.loaderInfo.bytes
                );

                w.start();

                return;
            }

            var loopdiesel2 = new MP3PitchLoop(

                  KnownEmbeddedResources.Default[
                  "assets/Abstractatech.ActionScript.Audio/diesel4.mp3"
                  ].ToSoundAsset()

                  );

            loopdiesel2.Sound.play();

        }

    }
}
