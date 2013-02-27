using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.Extensions;
using Abstractatech.ActionScript.Audio;

namespace FlashHeatZeeker.CoreAudio.Library
{
    public class Soundboard
    {
        public MP3PitchLoop loopmachinegun = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/FNCL.mp3"].ToSoundAsset().ToMP3PitchLoop();

        public MP3PitchLoop loophelicopter1 = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/helicopter1.mp3"].ToSoundAsset().ToMP3PitchLoop();

        public MP3PitchLoop loopdiesel2 = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/diesel4.mp3"].ToSoundAsset().ToMP3PitchLoop();
        public MP3PitchLoop loopsand_run = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/sand_run.mp3"].ToSoundAsset().ToMP3PitchLoop();
        public MP3PitchLoop loopjeepengine = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/jeepengine.mp3"].ToSoundAsset().ToMP3PitchLoop();
        public MP3PitchLoop loopcrickets = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/crickets.mp3"].ToSoundAsset().ToMP3PitchLoop();

    }
}
