using Abstractatech.ActionScript.Audio;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.system;
using ScriptCoreLib.Extensions;
using System;

namespace AIRAudioWorker
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201403/20140310
            // http://16bit.signt.com/post/31487077697/extendable-as3-worker-class
            // http://performancebydesign.blogspot.com/2011/11/measuring-thread-execution-state-using.html
            // http://16bit.signt.com/post/31601682385/utilizing-multiple-worker-in-as3
            // http://www.blixtsystems.com/2010/11/audio-mixing-on-air-for-android/
            // http://coenraets.org/blog/2010/07/voicenotes-for-android-sample-app-using-flex-air-and-the-microphone-api/

            if (Worker.current.isPrimordial)
            {
                var w = WorkerDomain.current.createWorker(
                    this.loaderInfo.bytes
                );

                w.start();

                new net.hires.debug.Stats().AttachTo(this);


                return;
            }

            // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/media/Sound.html
            var mySound = new Sound();

            mySound.sampleData += e =>
                {
                    for (var c = 0; c < 8192; c++)
                    {
                        e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.25);
                        e.data.writeFloat(Math.Sin(((c + e.position) / Math.PI / 2)) * 0.25);
                    }
                };
            mySound.play();


            //var loopdiesel2 = new MP3PitchLoop(

            //      KnownEmbeddedResources.Default[
            //      "assets/Abstractatech.ActionScript.Audio/diesel4.mp3"
            //      ].ToSoundAsset()

            //      );

            //// on android this feels choppy. why?
            //loopdiesel2.Sound.play();

        }

    }
}
