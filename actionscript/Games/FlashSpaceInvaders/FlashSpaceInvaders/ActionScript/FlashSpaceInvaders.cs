using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using System;
using ScriptCoreLib.ActionScript.mx.core;


namespace FlashSpaceInvaders.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    [SWF(backgroundColor = Colors.Black, width = DefaultWidth, height = DefaultHeight)]
    public class FlashSpaceInvaders : Sprite
    {
        public const int DefaultWidth = 480;
        public const int DefaultHeight = 480;

        [Script]
        public static class Colors
        {
            public const uint Green = 0xff00;
            public const uint White = 0xffffff;
            public const uint Black = 0x0;
        }


        public const int KeyLeft = 37;
        public const int KeyRight = 39;

        public FlashSpaceInvaders()
        {

            var menu = new Sprite().AttachTo(this);

            #region Spawn_A
            Func<int, int, Sprite> Spawn_A =
                (x, y) =>
                    new Sprite { x = x, y = y }.AttachTo(menu).AnimateAt(
                        new BitmapAsset[]
                        {
                            Assets.aenemy_1.ToBitmapAsset(),
                            Assets.aenemy_2.ToBitmapAsset()
                        }    
                    , 500);
            #endregion

            #region Spawn_B
            Func<int, int, Sprite> Spawn_B =
                (x, y) =>
                    new Sprite { x = x, y = y }.AttachTo(menu).AnimateAt(
                        new BitmapAsset[]
                        {
                            Assets.benemy_1.ToBitmapAsset(),
                            Assets.benemy_2.ToBitmapAsset()
                        }
                    , 500);
            #endregion

            #region Spawn_C
            Func<int, int, Sprite> Spawn_C =
                (x, y) =>
                    new Sprite { x = x, y = y }.AttachTo(menu).AnimateAt(
                        new BitmapAsset[]
                        {
                            Assets.cenemy_1.ToBitmapAsset(),
                            Assets.cenemy_2.ToBitmapAsset()
                        }
                    , 500);
            #endregion

            #region Spawn_BigGun
            Func<int, int, Sprite> Spawn_BigGun =
                (x, y) =>
                    new Sprite { x = x, y = y }.AttachTo(menu).AnimateAt(
                        new BitmapAsset[]
                        {
                            Assets.biggun_1.ToBitmapAsset(),
                        }
                    , 500);
            #endregion


            Spawn_A(60, 180);
            Spawn_A(DefaultWidth - 60, 180);
            
            Spawn_B(100, 120);
            Spawn_B(DefaultWidth - 100, 120);

            Spawn_A(120, 200);
            Spawn_B(120, 240);
            Spawn_C(120, 280);

            var Player = Spawn_BigGun(300, 360);

            stage.doubleClickEnabled = true;
            stage.doubleClick +=
                ev =>
                {
                    stage.SetFullscreen(true);
                };

            stage.keyDown +=
                ev =>
                {
                    if (ev.keyCode == KeyLeft)
                        Player.x -= 4;

                    if (ev.keyCode == KeyRight)
                        Player.x += 4;
                };

            new TextField
            {

                y = 100,
                width = DefaultWidth,
                autoSize = TextFieldAutoSize.CENTER,
                textColor = Colors.White,
                defaultTextFormat = new TextFormat
                {
                    font = "Courier New",
                    size = 48,
                },
                selectable = false,
                text = "SPACE",
            }.AttachTo(menu);

            new TextField
            {

                y = 160,
                width = DefaultWidth,
                autoSize = TextFieldAutoSize.CENTER,
                textColor = Colors.Green,
                defaultTextFormat = new TextFormat
                {
                    font = "Courier New",
                    size = 48,
                },
                selectable = false,
                text = "INVADERS",
            }.AttachTo(menu);

        }
    }

}
