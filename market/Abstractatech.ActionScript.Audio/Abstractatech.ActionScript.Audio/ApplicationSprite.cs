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
        public ApplicationSprite()
        {
            var Rate = new TextField
            {
                text = "1.0",
                width = 400
            }.AttachTo(this);

            var p = new MP3PitchLoop(

                KnownEmbeddedResources.Default["assets/Abstractatech.ActionScript.Audio/diesel3.mp3"].ToSoundAsset()

                );

            var o = new Sprite
            {

            }.AttachTo(this);

            o.click +=
                delegate
                {
                    p.Sound.close();
                };

            o.graphics.beginFill(0x0, 0.5);
            o.graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight);

            o.mouseMove +=
                e =>
                {
                    p.RightVolume = e.stageX / (this.stage.stageWidth / 2);
                    p.LeftVolume = (this.stage.stageWidth - e.stageX) / (this.stage.stageWidth / 2);

                    var rate = (e.stageY / this.stage.stageHeight) * 2;
                    p.Rate = rate;

                    Rate.text = new { rate, p.LeftVolume, p.RightVolume }.ToString();
                };

        }

    }

    public class MP3PitchLoop
    {
        private const double MAGIC_DELAY = 2257.0; // LAME 3.98.2 + flash.media.Sound Delay


        private const int BLOCK_SIZE = 4096 / 2;

        public double Rate { get; set; }

        public Sound Sound = new Sound();


        public double LeftVolume = 1.0;
        public double RightVolume = 1.0;

        public MP3PitchLoop(Sound _mp3)
        {
            Rate = 1.0;



            var _target = new ByteArray();

            var _position = MAGIC_DELAY;


            Sound.sampleData +=
                e =>
                {
                    //-- REUSE INSTEAD OF RECREATION
                    _target.position = 0;

                    //-- SHORTCUT
                    var data = e.data;

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
                    var read = (int)_mp3.extract(_target, need, positionInt);


                    var n = BLOCK_SIZE;

                    if (read != need)
                    {
                        var need2 = n - read;

                        nextposition = MAGIC_DELAY ;

                        var read2 = (int)_mp3.extract(_target, need2, nextposition);

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
                            _target.position = (uint)(positionTargetInt << 3);

                            //-- READ TWO STEREO SAMPLES FOR LINEAR INTERPOLATION
                            l0 = _target.readFloat() * LeftVolume;
                            r0 = _target.readFloat() * RightVolume;

                            l1 = _target.readFloat() * LeftVolume;
                            r1 = _target.readFloat() * RightVolume;
                        }

                        //-- WRITE INTERPOLATED AMPLITUDES INTO STREAM
                        data.writeFloat(l0 + alpha * (l1 - l0));
                        data.writeFloat(r0 + alpha * (r1 - r0));

                        //-- INCREASE TARGET POSITION
                        positionTargetNum += Rate;

                        //-- INCREASE FRACTION AND CLAMP BETWEEN 0 AND 1
                        alpha += Rate;
                        while (alpha >= 1.0) --alpha;
                    }

                    //-- FILL REST OF STREAM WITH ZEROs
                    if (i < BLOCK_SIZE)
                    {
                        while (i < BLOCK_SIZE)
                        {
                            data.writeFloat(0.0);
                            data.writeFloat(0.0);

                            ++i;
                        }
                    }

                    //-- INCREASE SOUND POSITION
                    _position = nextposition;
                };


            Sound.play();

        }
    }

}
