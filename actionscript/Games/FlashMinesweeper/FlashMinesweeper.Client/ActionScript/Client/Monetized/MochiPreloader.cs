using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.MochiLibrary;

namespace FlashMinesweeper.ActionScript.Client.Monetized
{
    [Script, 
    ScriptApplicationEntryPoint(
        Width = TeamPlay.DefaultControlWidth + TeamPlay.NonobaChatWidth, Height = TeamPlay.DefaultControlHeight)]
    [SWF(width = TeamPlay.DefaultControlWidth + TeamPlay.NonobaChatWidth, height = TeamPlay.DefaultControlHeight)]
    [GoogleGadget(
           author_email = "zproxy@hot.ee",
           author_link = "http://zproxy.wordpress.com",
           author = "Arvo Sulakatko",
           category = "lifestyle",
           category2 = "funandgames",
           screenshot = "http://jsc.sourceforge.net/examples/web/MineSweeper/assets/MineSweeper/Preview.png",
           thumbnail = "http://jsc.sourceforge.net/examples/web/MineSweeper/assets/MineSweeper/Preview.png",
           description = "Classic minesweeper game, compiled from c# source to actionscript",
           width = FlashMinesweeper.DefaultControlWidth + TeamPlay.NonobaChatWidth,
           height = FlashMinesweeper.DefaultControlHeight,
           title = "FlashMinesweeper",
           title_url = "http://nonoba.com/zproxy/flashminesweepermp"

       )]
    public class MochiPreloader : MochiAdPreloaderBase
    {
  
        public MochiPreloader()
        {
            
            _mochiads_game_id = "5a5be1df755e6cdc";

            showPreGameAd(
                delegate
                {
                    new TeamPlay().AttachTo(stage);
                }
            );
        }
    }
}
