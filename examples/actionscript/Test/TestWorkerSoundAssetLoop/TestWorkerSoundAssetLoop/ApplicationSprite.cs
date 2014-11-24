using System.Threading;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using ScriptCoreLib.ActionScript.flash.utils;

namespace TestWorkerSoundAssetLoop
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // well does the assets library do something with mp3 for us?
            //jeepengine

            // not for AIR.
            // lets move into embedded resources

            var t = new TextField
            {
                multiline = true,

                autoSize = TextFieldAutoSize.LEFT,

                text = "click to start",

                x = 100

                // X:\jsc.svn\examples\actionscript\Test\TestWorkerConsole\TestWorkerConsole\ApplicationSprite.cs
            }.AttachToSprite().AsConsole();

            new net.hires.debug.Stats().AttachToSprite();


            // X:\jsc.svn\examples\actionscript\air\AIRThreadedSoundAsyncLoop\AIRThreadedSoundAsyncLoop\ApplicationSprite.cs

            var threadId = 0;

            t.click += delegate
            {
                threadId++;

                Console.WriteLine(new { threadId });

                var tt = new Thread(arg0 =>
                {
                    // https://www.packtpub.com/books/content/flash-development-android-audio-input-microphone

                    Console.WriteLine("enter thread");

                    var SourceAudio = KnownEmbeddedResources.Default[
                        "assets/TestWorkerSoundAssetLoop/jeepengine.mp3"
                    ].ToSoundAsset();

                    // sometimes wont reach here, why? timerace issue?
                    Console.WriteLine("SourceAudio " + new { SourceAudio.bytesTotal, SourceAudio.id3 });

                    var SourceAudioBytes = new ByteArray { endian = Endian.LITTLE_ENDIAN };


                    var samplesperchannel = (int)SourceAudio.extract(
                        target: SourceAudioBytes,
                        length: 0x100000,
                        startPosition: 0
                    );

                    //                    63ms enter thread
                    //66ms SourceAudio { { bytesTotal = 34625, id3 = [object ID3Info] } }
                    //                    95ms SourceAudio { { samplesperchannel = 105984 } }


                    Console.WriteLine("SourceAudio " + new { samplesperchannel });

                    // can we await for a click here?
                    // what if the parameter is the onclick task? /event

                    var loopjeep = new Abstractatech.ActionScript.Audio.MP3PitchLoop(SourceAudio);

                    //it works and keeps the fps
                    // on android it sounds choppy. why?
                    // nexus seems to be able to do 16sounds with 60fps.
                    loopjeep.Sound.play();


                }
                );

                tt.Start(null);
            };



        }

    }
}
