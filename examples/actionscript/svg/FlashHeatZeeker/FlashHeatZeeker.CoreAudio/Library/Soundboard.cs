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
        public MP3PitchLoop loopheartbeat3 = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/heartbeat3.mp3"].ToSoundAsset().ToMP3PitchLoop();



        public Sound snd_whatsthatsound = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_whatsthatsound.mp3"].ToSoundAsset();
        public Sound snd_jeepengine_start = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_jeepengine_start.mp3"].ToSoundAsset();

        public Sound snd_hardmetalsmash = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_hardmetalsmash.mp3"].ToSoundAsset();
        public Sound snd_metalsmash = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_metalsmash.mp3"].ToSoundAsset();
        public Sound snd_woodsmash = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_woodsmash.mp3"].ToSoundAsset();

        public Sound snd_needweapon = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_needweapon.mp3"].ToSoundAsset();
        public Sound snd_didyouhearthat = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_didyouhearthat.mp3"].ToSoundAsset();
        public Sound snd_touchdown = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_touchdown.mp3"].ToSoundAsset();
        public Sound snd_ped_hit = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_ped_hit.mp3"].ToSoundAsset();
        public Sound snd_letsgo = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_letsgo.mp3"].ToSoundAsset();
        public Sound snd_SelectWeapon = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_SelectWeapon.mp3"].ToSoundAsset();
        public Sound snd_dooropen = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_dooropen.mp3"].ToSoundAsset();
        public Sound snd_itsempty = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_itsempty.mp3"].ToSoundAsset();
        public Sound snd_nothinghere = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_nothinghere.mp3"].ToSoundAsset();

        public Sound snd_lookingforlongrangecomms = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_lookingforlongrangecomms.mp3"].ToSoundAsset();

        public Sound snd_missleLaunch = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_missleLaunch.mp3"].ToSoundAsset();
        public Sound snd_click = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_click.mp3"].ToSoundAsset();
        public Sound snd_explosion = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_explosion.mp3"].ToSoundAsset();
        public Sound snd_Argh = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_Argh.mp3"].ToSoundAsset();

        //  Error: unsupported sampling rate (24000Hz)
        public Sound snd_nightvision = KnownEmbeddedResources.Default["assets/FlashHeatZeeker.CoreAudio/snd_nightvision.mp3"].ToSoundAsset();
    }
}
