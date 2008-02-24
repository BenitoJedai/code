using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.events;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.media;
using System;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.mx.core;


namespace FlashTowerDefense.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashTowerDefense : Sprite
    {
        public FlashTowerDefense()
        {

            var bg = new Sprite { x = 0, y = 0 };

            bg.graphics.beginFill(0xffffff);
            bg.graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight);

            bg.AttachTo(this);

            var t = new TextField
            {
                x = 4,
                y = 4,
                width = 200,
                height = 20,
                mouseEnabled = false
            };

            t.AttachTo(this);

            Action<double, Action> Times =
                (m, h) => (stage.stageWidth * stage.stageHeight * m).Times(h);

            Action<double, Func<BitmapAsset>> AddDoodads =
                (m, GetImage) => Times(m, () => GetImage().AttachTo(bg).SetCenteredPosition(stage.stageWidth.Random(), stage.stageWidth.Random()));

            AddDoodads(0.0001, () => Assets.grass1.ToBitmapAsset());
            AddDoodads(0.00005, () => Assets.bump2.ToBitmapAsset());

            var music = Assets.world.ToSoundAsset().play(0, 999);
            
            
            var turret = Assets.turret1_default.ToBitmapAsset();

            turret.x = (stage.stageWidth - turret.width) / 2;
            turret.y = (stage.stageHeight - turret.height) / 2;

            turret.AttachTo(this);

            var channel1 = default(SoundChannel);

            var f = new[] { new GlowFilter() };

            this.mouseDown +=
                e =>
                {
                    channel1 = Assets.gunfire.ToSoundAsset().play(0, 999);
                    turret.filters = f;
                };

            this.mouseUp +=
                e =>
                {
                    channel1.stop();
                    turret.filters = null;
                };

            this.mouseMove +=
                e =>
                {
                    t.text = "{ x = " + e.stageX.Round() + ", y = " + e.stageY.Round() + " }";

                };
        }
    }

}
