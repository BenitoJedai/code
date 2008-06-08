using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.ui;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.filters;


namespace FlashKeyboard.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class FlashKeyboard1 : Sprite
    {
        public const string Path = "/assets/FlashKeyboard";

        [Embed(source = Path + "/sheep.mp3")]
        static Class sound1;

        [Embed(source = Path + "/explosion.mp3")]
        static Class sound2;

        [Embed(source = Path + "/thunder.mp3")]
        static Class sound3;

        [Embed(source = Path + "/sheep-walk_1.png")]
        static Class sheep1;

        [Embed(source = Path + "/sheep-walk_2.png")]
        static Class sheep2;

        [Embed(source = Path + "/sheep-walk_3.png")]
        static Class sheep3;

        [Embed(source = Path + "/sheep-walk_4.png")]
        static Class sheep4;

        [Embed(source = Path + "/Preview.png")]
        static Class Preview;

        public FlashKeyboard1()
        {
            // sfx: http://www.therecordist.com/pages/downloads.html
            // sfx: http://freesound.iua.upf.edu/
            // sfx: http://www.users.qwest.net/~jhorwath/
            // sfx: http://www.a1freesoundeffects.com/weapons.html
            // sfx: http://simplythebest.net/sounds/MP3/sound_effects_MP3/index.html


            stage.stageWidth = 600;
            stage.stageHeight = 400;

            graphics.beginFill(0xffffff);
            graphics.drawRect(0, 0, stage.stageWidth, stage.stageHeight);

            new TextField
            {
                htmlText = "<a href='http://jsc.sf.net'>powered by jsc</a> - use arrows to move all the sheep",
                x = 20,
                y = 4,
                selectable = false,
                
                autoSize = TextFieldAutoSize.LEFT,
                filters = new[] { new BevelFilter() }

            }.AttachTo(this);

            var info =
                    new TextField
                    {
                        text = "...",
                        x = 20,
                        y = 40,
                        selectable = false,
                        filters = new[] { new BevelFilter() }
                    }.AttachTo(this);

            this.activate += sound3.ToSoundAsset().ToEvent();
            this.deactivate += sound2.ToSoundAsset().ToEvent();


            SpawnSheep(5, 200);
            SpawnSheep(150, 300);
            SpawnSheep(90, 100);

            stage.focus = this;

            this.keyDown +=
                ev =>
                {
                    info.text = "key: " + ev.keyCode;

                    if (ev.keyCode == 38)
                        g_offsety -= 2;

                    if (ev.keyCode == 40)
                        g_offsety += 2;

                    if (ev.keyCode == 37)
                        g_offsetx -= 2;

                    if (ev.keyCode == 39)
                        g_offsetx += 2;
                };

            this.focusOut +=
                delegate
                {
                    stage.focus = this;
                };

            var p = Preview.ToBitmapAsset();

            p.x = stage.stageWidth - 130;
            p.y = stage.stageHeight - 100;
            p.alpha = 0.6;
            p.filters = new[] { new DropShadowFilter() };
            
            p.AttachTo(this);
        }

        public int g_offsety;
        public int g_offsetx;

        private void SpawnSheep(int offsetx, int offsety)
        {
            var sheep = new[]
                {
                    sheep1.ToBitmapAsset(),
                    sheep2.ToBitmapAsset(),
                    sheep3.ToBitmapAsset(),
                    sheep4.ToBitmapAsset()
                };


            var f = new[] { new GlowFilter() };
            var circle2 = new Sprite();



            circle2.click += sound1.ToSoundAsset().ToMouseEvent();

            circle2.mouseOver +=
                delegate
                {
                    for (int i = 0; i < sheep.Length; i++)
                    {
                        var v = sheep[i];
                        sheep[i].filters = f;
                    }
                };

            circle2.mouseOut +=
              delegate
              {
                  for (int i = 0; i < sheep.Length; i++)
                  {
                      var v = sheep[i];
                      sheep[i].filters = null;
                  }
              };



            circle2.graphics.beginFill(0xFFCC00, 0.0);
            circle2.graphics.drawRect(0, 0, 64, 64);

            circle2.AttachTo(this);

            (1000 / 15).AtInterval(
                t =>
                {
                    circle2.x = ((t.currentCount + offsetx + g_offsetx) % stage.stageWidth) - 32;
                    circle2.y = ((offsety + g_offsety) % stage.stageHeight) - 32;

                    for (int i = 0; i < sheep.Length; i++)
                    {
                        var v = sheep[i];



                        if (t.currentCount % sheep.Length == i)
                            v.AttachTo(circle2);
                        else
                            v.Dipsose();
                    }
                }
            );
        }
    }

}
