using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using FlashTowerDefense.ActionScript;
using ScriptCoreLib.ActionScript;
using System;

namespace FlashTowerDefense.ActionScript.SplitScreen
{
    /// <summary>
    /// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = 0x00101010)]
    public class FlashTowerDefenseSplitScreen : Sprite
    {
        public const int Padding = 2;
        public const int DefaultWidth = FlashTowerDefense.DefaultWidth * 2 + 3 * Padding;
        public const int DefaultHeight = FlashTowerDefense.DefaultHeight + 2 * Padding;

        /// <summary>
        /// Default constructor
        /// </summary>
        public FlashTowerDefenseSplitScreen()
        {
            graphics.beginFill(0);
            graphics.drawRect(0, 0, DefaultWidth, DefaultHeight);
            graphics.endFill();


            #region CreateView
            Func<FlashTowerDefense> CreateView =
                delegate
                {

                    var v = new FlashTowerDefense();

                    var v_mask = new Shape();
                    v_mask.graphics.beginFill(0xffffff);
                    v_mask.graphics.drawRect(0, 0, FlashTowerDefense.DefaultWidth, FlashTowerDefense.DefaultHeight);
                    v_mask.graphics.endFill();

                    v.mask = v_mask;
                    v_mask.AttachTo(v);

                    return v;
                };
            #endregion

            var left = CreateView();
            left.MovementArrows.Enabled = false;

            var right = CreateView();
            right.MovementWASD.Enabled = false;

            right.ToggleMusic();

            left.MoveTo(Padding, Padding).AttachTo(this);
            right.MoveTo(Padding * 2 + FlashTowerDefense.DefaultWidth, Padding).AttachTo(this);

            //left_container.MoveTo(Padding, Padding).AttachTo(this);
        }
    }
}