using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using FlashMinesweeper.ActionScript;
using ScriptCoreLib;

namespace FlashMinesweeper.ActionScript.Client
{
    [Script]
    [SWF(backgroundColor = 0xc0c0c0,
        width = DefaultControlWidth,
        height = DefaultControlHeight
        )]
    public class TeamPlay : Sprite
    {
        /* Let's make another game for Nonoba Multplayer API. We already
         * have a working minesweeper base. Lets extend it as less as we
         * can for the moment to get the multiplayer version online as soon
         * as possible. It will be a non-stop deathmatch game :). The
         * achievement and score API should also be used.
         * 
         * We will need to extract the message interface from flashtowerdefense.
         * 
         * TemPlay means you get one field to play at the same time.
        */

        private const int FieldXCount = 22;
        private const int FieldYCount = 22;

        public const int DefaultControlWidth = FieldXCount * FlashMinesweeper.MineButton.Width;
        public const int DefaultControlHeight = FieldYCount * FlashMinesweeper.MineButton.Height;


        public TeamPlay()
        {
            stage.scaleMode = StageScaleMode.NO_SCALE;

            var field = new MineField(FieldXCount, FieldYCount, 0.15);

            field.AttachTo(this);
        }
    }
}
