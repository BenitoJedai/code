using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

[assembly: ScriptResources(FlashTowerDefense.ActionScript.Assets.Sounds.Path)]

namespace FlashTowerDefense.ActionScript.Assets
{
    [Script]
    public static class Sounds
    {
        public const string Path = "/assets/FlashTowerDefense.Sounds";

        [Embed(source = Path + "/SelectWeapon.mp3")]
        public static Class SelectWeapon;


        [Embed(source = Path + "/colt45.mp3")]
        public static Class colt45;


        [Embed(source = Path + "/sound35.mp3")]
        public static Class OutOfAmmo;

        [Embed(source = Path + "/sound20.mp3")]
        public static Class sound20;


        
        [Embed(source = Path + "/shotgun2.mp3")]
        public static Class shotgun2;

        [Embed(source = Path + "/door-open.mp3")]
        public static Class door_open;

        [Embed(source = Path + "/FNCL.mp3")]
        public static Class gunfire;

        [Embed(source = Path + "/world.mp3")]
        public static Class snd_world;

        [Embed(source = Path + "/birds.mp3")]
        public static Class snd_birds;

        [Embed(source = Path + "/bird2.mp3")]
        public static Class snd_bird2;

        [Embed(source = Path + "/click.mp3")]
        public static Class snd_click;

        [Embed(source = Path + "/scroll12.mp3")]
        public static Class snd_message;

        [Embed(source = Path + "/sheep.mp3")]
        public static Class snd_sheep;

        [Embed(source = Path + "/screams1a.mp3")]
        public static Class snd_man2;

        [Embed(Path + "/ghoullaugh.mp3")]
        public static Class snd_ghoullaugh;

    }

}
