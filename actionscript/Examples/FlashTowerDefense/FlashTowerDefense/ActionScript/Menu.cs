using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.ui;

namespace FlashTowerDefense.ActionScript
{
    [Script, ScriptApplicationEntryPoint(Width = Width, Height = Height)]
    [SWF(width = Width, height = Height, backgroundColor = ColorWhite)]
    class Menu : FlashTowerDefense
    {
        public Menu()
        {
            CanFire = false;

            var menumusic = Assets.snd_birds.ToSoundAsset().play(0, 999);

            Mouse.show();

            Aim.visible = false;

            GetWarzone().filters = new[] { new BlurFilter() };

            var InfoMenu = new Sprite
            {
                x = 120,
                y = 160
            }.AttachTo(this);

            var y = 0d;

            this.contextMenu = new ContextMenu();
            this.contextMenu.hideBuiltInItems();

            foreach (var v in KnownActors)
            {
                AddInfo(InfoMenu, y, v);

                y += 72;
            }

            #region Play
            var PlayButton = new TextField
            {
                defaultTextFormat = new TextFormat
                {
                    size = 36
                },
                autoSize = TextFieldAutoSize.LEFT,
                htmlText = "Play!",
                selectable = false,
                textColor = ColorBlue,
                filters = new [] { new GlowFilter(ColorBlueLight) }

            }.AttachTo(this);

            PlayButton.x = Width - PlayButton.width - 32;
            PlayButton.y = Height - PlayButton.height - 32;


            PlayButton.click +=
                delegate
                {
                    menumusic.stop();
                    InfoMenu.Orphanize();

                    GetWarzone().filters = null;

                    Assets.snd_click.ToSoundAsset().play();

                    CanFire = true;
                    PlayButton.Orphanize();
                    Aim.visible = true;
                    Mouse.hide();
                };
            #endregion

        }

        //[Script(IsDebugCode = true)]
        private static void AddInfo(Sprite InfoMenu, double y, Actor v)
        {
            v.CanMakeFootsteps = false;

            
            var h = new Sprite();

            Action<uint> Draw =
                color =>
                {
                    h.graphics.clear();
                    h.graphics.beginFill(color, 0.5);
                    h.graphics.drawRect(-32, -32, 64, 64);
                    h.graphics.endFill();
                    //h.graphics.lineStyle(1, 0x808080, 0.8);
                    //h.graphics.drawRect(-32, -32, 64, 64);

                    h.graphics.beginFill(color, 0.5);
                    h.graphics.drawRect(48, -32, 180, 64);
                    h.graphics.endFill();
                    //h.graphics.lineStyle(1, 0x808080, 0.8);
                    //h.graphics.drawRect(48, -32, 180, 64);
                };

            Draw(ColorWhite);

            h.click +=
                delegate
                {
                    if (0.5.ByChance())
                        v.PlayHelloSound();
                    else
                        v.PlayDeathSound();
                };

            h.mouseChildren = false;

            //h.filters = new[] { new GlowFilter(0x808080) };
            h.mouseOver += e => Draw(ColorBlueLight);
            h.mouseOut += e => Draw(ColorWhite);

            var t = new TextField
            {
                x = 64,
                text = v.ActorName + "\n" + v.ScoreValue + " points\n" + v.Description,
                autoSize = TextFieldAutoSize.LEFT,
                
            }.AttachTo(h);

            t.y -= t.height / 2;

            v.AttachTo(h);

            h.AttachTo(InfoMenu).MoveTo(0, y);
        }
    }
}
