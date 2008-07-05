using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;

//[assembly: ScriptResources(FlashMinesweeper.ActionScript.Assets.Path)]

namespace FlashMinesweeper.ActionScript
{
    [Script]
    static public class Assets
    {
        public const string Path = "/assets/FlashMinesweeper";

        [Embed(source = Path + "/flag.png")]
        static public readonly Class flag;

        [Embed(source = Path + "/button.png")]
        static public readonly Class button;

        [Embed(source = Path + "/1.png")]
        static public readonly Class _1;

        [Embed(source = Path + "/2.png")]
        static public readonly Class _2;

        [Embed(source = Path + "/3.png")]
        static public readonly Class _3;

        [Embed(source = Path + "/4.png")]
        static public readonly Class _4;

        [Embed(source = Path + "/5.png")]
        static public readonly Class _5;

        [Embed(source = Path + "/6.png")]
        static public readonly Class _6;

        [Embed(source = Path + "/7.png")]
        static public readonly Class _7;

        [Embed(source = Path + "/8.png")]
        static public readonly Class _8;

        [Embed(source = Path + "/empty.png")]
        static public readonly Class empty;

        [Embed(source = Path + "/mine.png")]
        static public readonly Class mine;

        [Embed(source = Path + "/notmine.png")]
        static public readonly Class notmine;


        [Embed(source = Path + "/mine_found.png")]
        static public readonly Class mine_found;


        [Embed(source = Path + "/click.mp3")]
        static public readonly Class click;

        [Embed(source = Path + "/explosion.mp3")]
        static public readonly Class explosion;

        [Embed(source = Path + "/flag.mp3")]
        static public readonly Class snd_flag;


        [Embed(source = Path + "/reveal.mp3")]
        static public readonly Class snd_reveal;

        [Embed(source = Path + "/tick.mp3")]
        static public readonly Class snd_tick;

        //[Embed(source = Path + "/applause.mp3")]
        //static public readonly Class snd_applause;

        [Embed(source = Path + "/buzzer.mp3")]
        static public readonly Class snd_buzzer;
    }
}
