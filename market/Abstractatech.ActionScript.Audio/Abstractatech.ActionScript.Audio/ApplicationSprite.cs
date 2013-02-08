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
    public sealed class ApplicationSprite : Sprite
    {
        public Action PlayDiesel { get; set; }
        public Action<Action<string>> BytesForDiesel { get; set; }

        public Action Playhelicopter1 { get; set; }
        public Action<Action<string>> BytesForHelicopter { get; set; }


        public Action PlayJeep { get; set; }
        public Action<Action<string>> BytesForJeep { get; set; }


        public Action PlayTone { get; set; }
        public Action<Action<string>> BytesForTone { get; set; }


        public ApplicationSprite()
        {
            var Rate = new TextField
            {
                text = "1.0",
                width = 400
            }.AttachTo(this);

            var loopdiesel2 = new MP3PitchLoop(

                KnownEmbeddedResources.Default[
                //"assets/Abstractatech.ActionScript.Audio/jeepengine.mp3"
                "assets/Abstractatech.ActionScript.Audio/diesel2.mp3"
                ].ToSoundAsset()

                );

            var loophelicopter1 = new MP3PitchLoop(

              KnownEmbeddedResources.Default[
                //"assets/Abstractatech.ActionScript.Audio/jeepengine.mp3"
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

                    var rate = (e.stageY / this.stage.stageHeight) * 2;

                    loopdiesel2.Rate = rate;
                    loopjeep.Rate = rate;
                    loophelicopter1.Rate = rate;

                    Rate.text = new { loopdiesel2.MAGIC_DELAY, rate, loopdiesel2.LeftVolume, loopdiesel2.RightVolume }.ToString();
                };

            PlayDiesel = delegate
            {
                loopdiesel2.Sound.play();


            };

            Playhelicopter1 = delegate
            {
                loophelicopter1.Sound.play();
            };

            PlayJeep = delegate
            {
                loopjeep.Sound.play();
            };

            BytesForJeep =
                yield =>
                {
                    var m = new ByteArray { endian = Endian.LITTLE_ENDIAN };
                    loopjeep.SourceAudio.extract(m, MP3PitchLoop.BLOCK_SIZE * 40, 0);

                    var bytes = m.ToMemoryStream().ToArray();
                    var base64 = Convert.ToBase64String(bytes);


                    yield(base64);
                };




            PlayTone = delegate
            {
                looptone.Sound.play();
            };

            BytesForTone =
                yield =>
                {
                    var m = new ByteArray { endian = Endian.LITTLE_ENDIAN };

                    // can we have it all?
                    looptone.SourceAudio.extract(m, MP3PitchLoop.BLOCK_SIZE * 40, 0);

                    var bytes = m.ToMemoryStream().ToArray();
                    var base64 = Convert.ToBase64String(bytes);


                    yield(base64);


                };
        }

    }

    public class MP3PitchLoop
    {
        public double MAGIC_DELAY = 2257.0; // LAME 3.98.2 + flash.media.Sound Delay
        //public double MAGIC_DELAY = 0; // LAME 3.98.2 + flash.media.Sound Delay


        public const int BLOCK_SIZE = 4096 / 2;

        public double Rate { get; set; }

        public Sound Sound = new Sound();


        public double LeftVolume = 1.0;
        public double RightVolume = 1.0;


        public Sound SourceAudio;

        public MP3PitchLoop(Sound SourceAudio, bool autoplay = false)
        {
            this.SourceAudio = SourceAudio;

            Rate = 1.0;



            var LoopAudioStream = new ByteArray();

            var _position = MAGIC_DELAY;


            Sound.sampleData +=
                e =>
                {
                    //-- REUSE INSTEAD OF RECREATION
                    LoopAudioStream.position = 0;

                    //-- SHORTCUT
                    var TargetAudioStream = e.data;

                    var scaledBlockSize = BLOCK_SIZE * Rate;
                    var positionInt = Convert.ToInt32(_position);
                    var alpha = _position - positionInt;

                    var positionTargetNum = alpha;
                    var positionTargetInt = -1;

                    //-- COMPUTE NUMBER OF SAMPLES NEED TO PROCESS BLOCK (+2 FOR INTERPOLATION)
                    var need = Convert.ToInt32(Math.Ceiling(scaledBlockSize) + 2);

                    var nextposition = _position + scaledBlockSize;

                    //-- EXTRACT SAMPLES

                    //var need1 = Math.Min(positionInt + need, _mp3.bytesTotal - 0) - positionInt;
                    var read = (int)SourceAudio.extract(LoopAudioStream, need, positionInt);


                    var n = BLOCK_SIZE;

                    if (read != need)
                    {
                        var need2 = n - read;

                        nextposition = MAGIC_DELAY;

                        var read2 = (int)SourceAudio.extract(LoopAudioStream, need2, nextposition);

                        read += read2;
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
                        TargetAudioStream.writeFloat(l0 + alpha * (l1 - l0));
                        TargetAudioStream.writeFloat(r0 + alpha * (r1 - r0));

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
                            TargetAudioStream.writeFloat(0.0);
                            TargetAudioStream.writeFloat(0.0);

                            ++i;
                        }
                    }
                    #endregion


                    //-- INCREASE SOUND POSITION
                    _position = nextposition;
                };


            if (autoplay)
                Sound.play();

        }
    }

}
