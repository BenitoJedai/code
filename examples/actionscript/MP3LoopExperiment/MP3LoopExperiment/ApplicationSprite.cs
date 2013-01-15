using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.Extensions;

namespace MP3LoopExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        // http://www.flickr.com/photos/schill/4499319436/
        // http://blog.andre-michelle.com/upload/mp3loop/MP3Loop.as
        // http://stackoverflow.com/questions/1227442/prefered-method-for-looping-sound-flash-as3


        private const double MAGIC_DELAY = 2257.0; // LAME 3.98.2 + flash.media.Sound Delay

        private const int bufferSize = 4096; // Stable playback

        private const int samplesTotal = 124417; // original amount of sample before encoding (change it to your loop)

        private Sound mp3 = new Sound(); // Use for decoding
        private Sound _out = new Sound(); // Use for output stream

        private TextField textField = new TextField();

        private int samplesPosition = 0;

        private bool enabled = false;

        public ApplicationSprite()
        {
            initUI();

            loadMp3();
        }

        private void initUI()
        {
            stage.align = StageAlign.TOP_LEFT;
            stage.scaleMode = StageScaleMode.NO_SCALE;

            textField.autoSize = TextFieldAutoSize.LEFT;
            textField.selectable = false;
            //textField.defaultTextFormat = new TextFormat("Verdana", 10, 0xFFFFFF);
            textField.text = "loading...";
            addChild(textField);
        }


        private void updateText()
        {
            if (enabled)
                textField.text = "click to pause...";
            else
                textField.text = "click to play...";
        }

        private void loadMp3()
        {
            mp3.complete +=
                delegate
                {
                    stage.click +=
                        delegate
                        {
                            enabled = !enabled;

                            updateText();
                        };

                    updateText();

                    startPlayback();
                };

            mp3.load(new URLRequest("http://blog.andre-michelle.com/upload/mp3loop/loop.mp3"));
        }


        private void startPlayback()
        {
            _out.sampleData +=
                e =>
                {
                    if (enabled)
                    {
                        extract(e.data, bufferSize);
                    }
                    else
                    {
                        silent(e.data, bufferSize);
                    }
                };

            _out.play();
        }



        /**
         * This methods extracts audio data from the mp3 and wraps it automatically with respect to encoder delay
         *
         * @param target The ByteArray where to write the audio data
         * @param length The amount of samples to be read
         */
        private void extract(ByteArray target, int length)
        {
            while (0 < length)
            {
                if (samplesPosition + length > samplesTotal)
                {
                    var read = samplesTotal - samplesPosition;

                    mp3.extract(target, read, samplesPosition + MAGIC_DELAY);

                    samplesPosition += read;

                    length -= read;
                }
                else
                {
                    mp3.extract(target, length, samplesPosition + MAGIC_DELAY);

                    samplesPosition += length;

                    length = 0;
                }

                if (samplesPosition == samplesTotal) // END OF LOOP > WRAP
                {
                    samplesPosition = 0;
                }
            }
        }

        private void silent(ByteArray target, int length)
        {
            target.position = 0;

            while (length > 0)
            {
                length--;

                target.writeFloat(0.0);
                target.writeFloat(0.0);
            }
        }


    }
}
