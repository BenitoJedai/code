using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.text;
using System;
using Abstractatech.ActionScript.Audio;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace ScriptCoreLib.ActionScript.Extensions
{
    public static class MP3PitchLoopExtensions
    {
        public static MP3PitchLoop ToMP3PitchLoop(this Sound mp3)
        {
            return new MP3PitchLoop(mp3);
        }
    }
}

namespace Abstractatech.ActionScript.Audio
{
    public delegate void PlayAtAndAllowToStop(string paddingleft, string paddingright, Action<Action> yield_stop, Action<string, string> yield_position_anddiagnostics);
    public delegate void AnalyzeThenPlay(Action<string, PlayAtAndAllowToStop> base64);

    public sealed class ApplicationSprite : Sprite
    {
        public Action PlayDiesel { get; set; }
        public AnalyzeThenPlay BytesForDiesel { get; set; }

        public Action Playhelicopter1 { get; set; }
        public AnalyzeThenPlay BytesForHelicopter { get; set; }


        public Action PlayJeep { get; set; }
        public AnalyzeThenPlay BytesForJeep { get; set; }


        public Action PlayTone { get; set; }
        public AnalyzeThenPlay BytesForTone { get; set; }

        public Action PlaySandrun { get; set; }
        public AnalyzeThenPlay BytesForSandrun { get; set; }



        public ApplicationSprite()
        {
            var Rate = new TextField
            {
                text = "1.0",
                width = 400
            }.AttachTo(this);

            var loopdiesel2 = new MP3PitchLoop(

                KnownEmbeddedResources.Default[
                "assets/Abstractatech.ActionScript.Audio/diesel4.mp3"
                ].ToSoundAsset()

                );

            var loophelicopter1 = new MP3PitchLoop(

              KnownEmbeddedResources.Default[
                    "assets/Abstractatech.ActionScript.Audio/helicopter1.mp3"
              ].ToSoundAsset()

          );


            var loopjeep = new MP3PitchLoop(

              KnownEmbeddedResources.Default[
                    "assets/Abstractatech.ActionScript.Audio/jeepengine.mp3"
              ].ToSoundAsset()

          );


            var looptone = new MP3PitchLoop(

              KnownEmbeddedResources.Default[
                    "assets/Abstractatech.ActionScript.Audio/tone_sin_100hz.mp3"
              ].ToSoundAsset()

          );


            var loopsand_run = new MP3PitchLoop(

        KnownEmbeddedResources.Default[
              "assets/Abstractatech.ActionScript.Audio/sand_run.mp3"
        ].ToSoundAsset()

    );

            var o = new Sprite
            {

            }.AttachTo(this);


            o.graphics.beginFill(0x0, 0.5);
            o.graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight);

            o.mouseMove +=
                e =>
                {
                    loopdiesel2.RightVolume = e.stageX / (this.stage.stageWidth / 2);
                    loopdiesel2.LeftVolume = (this.stage.stageWidth - e.stageX) / (this.stage.stageWidth / 2);

                    var rate = (e.stageY / this.stage.stageHeight) * 1.5 + 0.5;

                    loopdiesel2.Rate = rate;
                    loopjeep.Rate = rate;
                    loophelicopter1.Rate = rate;
                    looptone.Rate = rate;
                    loopsand_run.Rate = rate;

                    Rate.text = new { rate, loopdiesel2.LeftVolume, loopdiesel2.RightVolume }.ToString();


                };

            looptone.AtDiagnostics +=
                text =>
                {
                    Rate.text = text;

                };


            loopjeep.AtDiagnostics +=
              text =>
              {
                  Rate.text = text;

              };

            loophelicopter1.AtDiagnostics +=
                text =>
                {
                    Rate.text = text;

                };


            PlayDiesel = delegate
            {
                loopdiesel2.Sound.play();


            };


            Func<MP3PitchLoop, AnalyzeThenPlay> f =
                x =>
                    yield =>
                    {
                        var m = new ByteArray { endian = Endian.LITTLE_ENDIAN };
                        x.SourceAudio.extract(m, MP3PitchLoop.BLOCK_SIZE * 160, 0);

                        var bytes = m.ToMemoryStream().ToArray();
                        var base64 = Convert.ToBase64String(bytes);


                        yield(base64,
                            (paddingleft,
                                paddingright, stop, yield_position) =>
                            {
                                x.SourceAudioInitialPosition = Convert.ToDouble(paddingleft);
                                x.SourceAudioPosition = Convert.ToDouble(paddingleft);

                                x.SourceAudioPaddingRight = Convert.ToDouble(paddingright);

                                var s = x.Sound.play();

                                var once = false;

                                x.SourceAudioPositionChanged +=
                                    delegate
                                    {
                                        if (s == null)
                                            return;

                                        if (x.glitchmode)
                                        {
                                            if (once)
                                            {
                                            }
                                            else
                                            {
                                                once = true;

                                                //s.stop();
                                                //s = null;

                                                var diagnostics = new MemoryStream();
                                                var w = new BinaryWriter(diagnostics);

                                                foreach (var item in x.glitch)
                                                {
                                                    w.Write((float)item);
                                                }
                                                diagnostics.Position = 0;

                                                var diagnostics_base64 = Convert.ToBase64String(diagnostics.ToArray());

                                                yield_position("" + x.SourceAudioPosition, diagnostics_base64);

                                            }

                                            return;
                                        }

                                        yield_position("" + x.SourceAudioPosition, "");
                                    };

                                stop(
                                    delegate
                                    {
                                        s.stop();
                                        s = null;
                                    }
                                );



                            }
                        );
                    };

            BytesForDiesel = f(loopdiesel2);




            Playhelicopter1 = delegate
            {
                loophelicopter1.Sound.play();
            };

            BytesForHelicopter = f(loophelicopter1);



            PlayJeep = delegate
            {
                loopjeep.Sound.play();
            };

            BytesForJeep = f(loopjeep);





            PlayTone = delegate
            {
                looptone.Sound.play();
            };

            BytesForTone = f(looptone);


            PlaySandrun = delegate
            {
                loopsand_run.Sound.play();
            };

            BytesForSandrun = f(loopsand_run);


        }

    }

    public class MP3PitchLoop
    {
        public const double MAGIC_DELAY = 2257.0; // LAME 3.98.2 + flash.media.Sound Delay
        //public double MAGIC_DELAY = 0; // LAME 3.98.2 + flash.media.Sound Delay


        public const int BLOCK_SIZE = 4096 / 2;

        public double Rate { get; set; }

        public Sound Sound = new Sound();


        public double LeftVolume = 1.0;
        public double RightVolume = 1.0;


        public Sound SourceAudio;

        public double SourceAudioInitialPosition = MAGIC_DELAY;
        public double SourceAudioPosition = MAGIC_DELAY;

        public event Action SourceAudioPositionChanged;

        public double SourceAudioPaddingRight;


        public Action<string> AtDiagnostics;


        public bool glitchmode = false;
        public List<double> glitch = new List<double>();

        ByteArray LoopAudioStream = new ByteArray();

        public MP3PitchLoop(Sound SourceAudio, bool autoplay = false)
        {
            this.SourceAudio = SourceAudio;
            this.Rate = 1.0;


            var SourceAudioBytes = new ByteArray();

            var samplesperchannel = (int)SourceAudio.extract(SourceAudioBytes, 0x100000, 0);






            Sound.sampleData +=
                e =>
                {
                    var TargetAudioStream = e.data;

                    try
                    {

                        if (glitchmode)
                        {
                            glitchmode = false;
                            glitch.Clear();

                        }

                        //-- REUSE INSTEAD OF RECREATION
                        LoopAudioStream.position = 0;


                        var scaledBlockSize = BLOCK_SIZE * Rate;
                        var positionInt = Convert.ToInt32(SourceAudioPosition);
                        var alpha = SourceAudioPosition - positionInt;

                        var positionTargetNum = alpha;
                        var positionTargetInt = -1;

                        //-- COMPUTE NUMBER OF SAMPLES NEED TO PROCESS BLOCK (+2 FOR INTERPOLATION)
                        var need = Convert.ToInt32(Math.Ceiling(scaledBlockSize) + 2);

                        var nextposition = SourceAudioPosition + scaledBlockSize;

                        //-- EXTRACT SAMPLES


                        //var need1 = Math.Min(
                        //    positionInt + need,
                        //    SourceAudio.bytesTotal //- SourceAudioPaddingRight
                        //) - positionInt;

                        var need1offset = positionInt + need;


                        // http://help.adobe.com/en_US/FlashPlatform/reference/actionscript/3/flash/media/Sound.html#extract()
                        var need1 = Math.Min(positionInt + need,
                            // in bytes or in samples??
                            samplesperchannel - this.SourceAudioPaddingRight) - positionInt;


                        if (AtDiagnostics != null)
                            AtDiagnostics(new { positionInt, samplesperchannel, SourceAudioBytes.length, need1, need }.ToString());


                        var read = (int)SourceAudio.extract(LoopAudioStream, need1, positionInt);


                        var n = BLOCK_SIZE;

                        if (read != need)
                        {
                            var p0 = LoopAudioStream.position;

                            var need2 = n - read;

                            nextposition = SourceAudioInitialPosition;


                            var read2 = (int)SourceAudio.extract(LoopAudioStream, need2, nextposition);

                            // we need to ease the edges now to make the click effect to o away.


                            //var samples = read + read2;

                            //var trace0 = new { samples, read, read2, LoopAudioStream.length };

                            //if (AtDiagnostics != null)
                            //    AtDiagnostics("fixup_LoopAudioStream " + trace0);

                            //var fixup_LoopAudioStream = new ByteArray();

                            var p1 = LoopAudioStream.position;

                            //for (int fixup_i = 0; fixup_i < (read + read2); fixup_i++)
                            {
                                LoopAudioStream.position = p0;

                                var fixup_l0 = LoopAudioStream.readFloat();
                                var fixup_r0 = LoopAudioStream.readFloat();


                                // mark it, can we see it?
                                fixup_l0 = 1.0;
                                fixup_l0 = 1.0;

                                LoopAudioStream.position = p0;

                                LoopAudioStream.writeFloat(fixup_l0);
                                LoopAudioStream.writeFloat(fixup_r0);
                            }

                            LoopAudioStream.position = p1;

                            //if (AtDiagnostics != null)
                            //    AtDiagnostics("fixup_LoopAudioStream done! " + new { fixup_LoopAudioStream.length, trace0 });

                            //LoopAudioStream = fixup_LoopAudioStream;


                            read += read2;

                            glitchmode = true;
                        }

                        if (read != need)
                        {
                            n = Convert.ToInt32(read / Rate);
                        }

                        var l0 = .0;
                        var r0 = .0;
                        var l1 = .0;
                        var r1 = .0;

                        var i = 0;
                        for (; i < n; i++)
                        {
                            //-- AVOID READING EQUAL SAMPLES, IF RATE < 1.0
                            if (Convert.ToInt32(positionTargetNum) != positionTargetInt)
                            {
                                positionTargetInt = Convert.ToInt32(positionTargetNum);

                                //-- SET TARGET READ POSITION
                                LoopAudioStream.position = (uint)(positionTargetInt << 3);

                                //-- READ TWO STEREO SAMPLES FOR LINEAR INTERPOLATION
                                l0 = LoopAudioStream.readFloat() * LeftVolume;
                                r0 = LoopAudioStream.readFloat() * RightVolume;

                                l1 = LoopAudioStream.readFloat() * LeftVolume;
                                r1 = LoopAudioStream.readFloat() * RightVolume;
                            }

                            //-- WRITE INTERPOLATED AMPLITUDES INTO STREAM
                            var tl0 = l0 + alpha * (l1 - l0);

                            if (glitchmode)
                                glitch.Add(tl0);

                            TargetAudioStream.writeFloat(tl0);

                            var tr0 = r0 + alpha * (r1 - r0);

                            if (glitchmode)
                                glitch.Add(tr0);

                            TargetAudioStream.writeFloat(tr0);

                            //-- INCREASE TARGET POSITION
                            positionTargetNum += Rate;

                            //-- INCREASE FRACTION AND CLAMP BETWEEN 0 AND 1
                            alpha += Rate;
                            while (alpha >= 1.0) --alpha;
                        }

                        #region -- FILL REST OF STREAM WITH ZEROs
                        if (i < BLOCK_SIZE)
                        {
                            while (i < BLOCK_SIZE)
                            {
                                if (glitchmode)
                                {
                                    glitch.Add(0);
                                    glitch.Add(0);
                                }

                                TargetAudioStream.writeFloat(0.0);
                                TargetAudioStream.writeFloat(0.0);

                                ++i;
                            }
                        }
                        #endregion


                        //-- INCREASE SOUND POSITION
                        SourceAudioPosition = nextposition;

                        if (SourceAudioPositionChanged != null)
                            SourceAudioPositionChanged();
                    }
                    catch (Exception ex)
                    {
                        var StackTrace = ((ScriptCoreLib.ActionScript.Error)(object)ex).getStackTrace();

                        if (AtDiagnostics != null)
                            AtDiagnostics("error: " + new { ex.Message, StackTrace });
                    }


                };


            if (autoplay)
                Sound.play();

        }
    }

}
