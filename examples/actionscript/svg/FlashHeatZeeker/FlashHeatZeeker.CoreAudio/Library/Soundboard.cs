using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.Extensions;
using Abstractatech.ActionScript.Audio;
using ScriptCoreLib.ActionScript.flash.media;

namespace FlashHeatZeeker.CoreAudio.Library
{
    public partial class Soundboard
    {
        public MP3PitchLoop loopmachinegun = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/FNCL.mp3"].ToSoundAsset().ToMP3PitchLoop();

        public MP3PitchLoop loophelicopter1 = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/helicopter1.mp3"].ToSoundAsset().ToMP3PitchLoop();

        public MP3PitchLoop loopdiesel2 = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/diesel4.mp3"].ToSoundAsset().ToMP3PitchLoop();
        public MP3PitchLoop loopsand_run = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/sand_run.mp3"].ToSoundAsset().ToMP3PitchLoop();
        public MP3PitchLoop loopjeepengine = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/jeepengine.mp3"].ToSoundAsset().ToMP3PitchLoop();
        public MP3PitchLoop loopcrickets = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/crickets.mp3"].ToSoundAsset().ToMP3PitchLoop();

        public MP3PitchLoop loopstrange1 = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/loopstrange1.mp3"].ToSoundAsset().ToMP3PitchLoop();



        public Sound snd_whatsthatsound = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_whatsthatsound.mp3"].ToSoundAsset();
        public Sound snd_jeepengine_start = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_jeepengine_start.mp3"].ToSoundAsset();
        public Sound snd_metalsmash = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_metalsmash.mp3"].ToSoundAsset();
        public Sound snd_needweapon = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_needweapon.mp3"].ToSoundAsset();
        public Sound snd_didyouhearthat = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_didyouhearthat.mp3"].ToSoundAsset();
        public Sound snd_touchdown = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_touchdown.mp3"].ToSoundAsset();
    }
}
