using System.Threading;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;
using System;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.media;
using System.Math;

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

                var tt = new Thread(async arg0 =>
                {
                    // https://www.packtpub.com/books/content/flash-development-android-audio-input-microphone

                    Console.WriteLine("enter thread");

                    var SourceAudio = KnownEmbeddedResources.Default[
                        "assets/TestWorkerSoundAssetLoop/jeepengine.mp3"
                    ].ToSoundAsset();

                    // sometimes wont reach here, why? timerace issue?
                    Console.WriteLine("SourceAudio " + new { SourceAudio.bytesTotal, SourceAudio.id3 });

                    var SourceAudioBytes = new ByteArray { endian = Endian.LITTLE_ENDIAN };


                    //2647ms SourceAudio { { bytesTotal = 34625, id3 = [object ID3Info] } }
                    //2669ms SourceAudio { { samplesperchannel = 105984 } }


                    var samplesperchannel = (int)SourceAudio.extract(
                        target: SourceAudioBytes,
                        length: 0x100000,
                        startPosition: 0
                    );

                    //var MAGIC_DELAY = 2257u; // LAME 3.98.2 + flash.media.Sound Delay
                    SourceAudioBytes.position = 0;

                    // cyclical binary reader?


                    //                    63ms enter thread
                    //66ms SourceAudio { { bytesTotal = 34625, id3 = [object ID3Info] } }
                    //                    95ms SourceAudio { { samplesperchannel = 105984 } }


                    Console.WriteLine("SourceAudio " + new { samplesperchannel });

                    // can we await for a click here?
                    // what if the parameter is the onclick task? /event

                    // should we prerender our audio loop into the pitch we would need?
                    //var loopjeep = new Abstractatech.ActionScript.Audio.MP3PitchLoop(SourceAudio);

                    //it works and keeps the fps
                    // on android it sounds choppy. why?
                    // nexus seems to be able to do 16sounds with 60fps.
                    //loopjeep.Sound.play();

                    var s = new Sound();

                    while (true)
                    {
                        // X:\jsc.svn\examples\actionscript\Test\TestWorkerSoundAssetLoop\TestWorkerSoundAssetLoop\ApplicationSprite.cs

                        var e = await s.async.sampleData;

                        // ftt


                        //1831ms { { position = 0 } }
                        //1847ms { { position = 65536 } }


                        // wrap
                        if (8192 * 8 + SourceAudioBytes.position > SourceAudioBytes.length)
                        {
                            Console.WriteLine(new { SourceAudioBytes.position });

                            SourceAudioBytes.position = 0;
                        }


                        // can we get the pitch from another device over lan?
                        // can we have framerate as audio?
                        for (var c = 0; c < 8192; c++)
                        {
                            // mipmap?
                            // 4
                            var q0 = SourceAudioBytes.readFloat();
                            // 4
                            var q1 = SourceAudioBytes.readFloat();



                            // i wonder, can we use the orientation
                            // or magnetic north here?
                            // prep for Gear VR?
                            e.data.writeFloat(q0 * 0.7);
                            e.data.writeFloat(q1 * 0.7);
                        }
                    }

                }
                );

                tt.Start(null);
            };

            // can we get heatzeeker to be like a small earth ball?


        }

    }
}
