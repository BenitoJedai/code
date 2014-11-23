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
            }.AttachToSprite().MoveTo(128, 8);


            new net.hires.debug.Stats().AttachToSprite();

            try
            {
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

                        loopjeep.RightVolume = e.stageX / (this.stage.stageWidth / 2);
                        loopjeep.LeftVolume = (this.stage.stageWidth - e.stageX) / (this.stage.stageWidth / 2);


                        var rate = (e.stageY / this.stage.stageHeight) * 1.5 + 0.5;

                        loopdiesel2.Rate = rate;
                        loopjeep.Rate = rate;
                        loophelicopter1.Rate = rate;
                        looptone.Rate = rate;
                        loopsand_run.Rate = rate;

                        var MasterVolume = (e.stageY / this.stage.stageHeight);

                        loopdiesel2.MasterVolume = MasterVolume;
                        loopjeep.MasterVolume = MasterVolume;
                        loophelicopter1.MasterVolume = MasterVolume;
                        looptone.MasterVolume = MasterVolume;
                        loopsand_run.MasterVolume = MasterVolume;

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


                #region f
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

                                    var diagnostics = new MemoryStream();
                                    var w = new BinaryWriter(diagnostics);

                                    var once = 0;

                                    x.SourceAudioPositionChanged +=
                                        delegate
                                        {
                                            if (s == null)
                                                return;

                                            if (once == 1)
                                            {
                                                once++;

                                                foreach (var item in x.glitch)
                                                {
                                                    w.Write((float)item);
                                                }
                                                //diagnostics.Position = 0;

                                                var diagnostics_base64 = Convert.ToBase64String(diagnostics.ToArray());

                                                yield_position("" + x.SourceAudioPosition, diagnostics_base64);

                                            }

                                            if (x.glitchmode)
                                            {
                                                if (once == 0)
                                                {
                                                    once++;

                                                    foreach (var item in x.glitch)
                                                    {
                                                        w.Write((float)item);
                                                    }

                                                }
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
                #endregion


                BytesForDiesel = f(loopdiesel2);




                Playhelicopter1 = delegate
                {
                    loophelicopter1.Sound.play();
                };

                BytesForHelicopter = f(loophelicopter1);


                o.click +=
                    delegate
                    {

                        Playhelicopter1();
                    };


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
            catch (Exception ex)
            {
                var StackTrace = ((ScriptCoreLib.ActionScript.Error)(object)ex).getStackTrace();

                Rate.text = ("error: " + new { ex.Message, StackTrace });
            }

        }

    }


}
