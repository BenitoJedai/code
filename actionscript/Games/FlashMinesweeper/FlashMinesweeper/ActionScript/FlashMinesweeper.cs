using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.mx.core;
using System;
using System.Linq;
using System.Collections.Generic;
using ScriptCoreLib.ActionScript.flash.utils;



namespace FlashMinesweeper.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script]
    [ScriptApplicationEntryPoint(
        Width = FlashMinesweeper.DefaultControlWidth,
        Height = FlashMinesweeper.DefaultControlHeight
        )]
    [GoogleGadget(
            author_email = "zproxy@hot.ee",
            author_link = "http://zproxy.wordpress.com",
            author = "Arvo Sulakatko",
            category = "lifestyle",
            category2 = "funandgames",
            screenshot = "http://jsc.sourceforge.net/examples/web/MineSweeper/assets/MineSweeper/Preview.png",
            thumbnail = "http://jsc.sourceforge.net/examples/web/MineSweeper/assets/MineSweeper/Preview.png",
            description = "Classic minesweeper game, compiled from c# source to actionscript",
            width = FlashMinesweeper.DefaultControlWidth,
            height = FlashMinesweeper.DefaultControlHeight,
            title = "FlashMinesweeper",
            title_url = "http://zproxy.wordpress.com/2008/03/16/javascript-minesweeper-remake/"
        )]
    [SWF(backgroundColor = 0xc0c0c0,
        width = FlashMinesweeper.DefaultControlWidth,
        height = FlashMinesweeper.DefaultControlHeight
        )]
    public class FlashMinesweeper : MineField
    {
        public const int DefaultControlWidth = FlashMinesweeper.FieldXCount * FlashMinesweeper.MineButton.Width;
        public const int DefaultControlHeight = FlashMinesweeper.FieldYCount * FlashMinesweeper.MineButton.Height;
        // todo:
		// http://nothings.org/games/minesweeper/
        // http://www.kirupa.com/forum/showthread.php?t=261577
        // http://groups.google.com/group/youtube-api-basics/browse_thread/thread/89ff378fc44985f0/6b63c2e46159640f?lnk=gst&q=flash+loadClip&rnum=1
        // snd:
        // http://www.a1sounddownload.com/freesoundsamples.htm
        // http://simplythebest.net/sounds/index.html
        // http://www.pacdv.com/sounds/interface_sounds.html


        private const int FieldXCount = 10;
        private const int FieldYCount = 10;


        public FlashMinesweeper()
            : base(FieldXCount, FieldYCount, 0.2)
        {

        }
        
    }

}
