using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.net;
using ScriptCoreLib.ActionScript.flash.media;
using ScriptCoreLib.ActionScript.flash.filters;


namespace MySoundDemo.ActionScript
{
    /// <summary>
    /// testing...
    /// </summary>
    [Script, ScriptApplicationEntryPoint]
    public class MySoundDemo : Sprite
    {
        void DrawHelloWorld(int step)
        {
            
            for (int i = 0; i < 4; i++)
            {
                addChild(
                   new TextField
                   {
                       text = Text,
                       x = step * i,
                       y = 20,
                       textColor = Color,
                       sharpness = 400,
                       filters = new [] { Filter }
                   }
               );
            }
        }

        public string Text;
        public uint Color { get; set; }
        public BitmapFilter Filter { get; set; }

        public MySoundDemo()
        {
            Text = "Howdy!";
            Color = 0x00ffff00;
            Filter = new DropShadowFilter();

            "assets/MySoundDemo/world.mp3".PlaySoundLoop();



            for (var j = 0.0; j < 1; j += 0.1)
            {
                this.graphics.beginFill(0xff0000, j);
                this.graphics.drawCircle(40, 40, 40 * (1.0 - j));
                this.graphics.endFill();
            }


            DrawHelloWorld(50);



            addChild(
                    new TextField
                    {
                        text = "powered by jsc",
                        x = 20,
                        y = 40,
                        selectable = false,
                        sharpness = -400,
                        textColor = 0xffffff,
                        filters = new [] { new BlurFilter() }
                    }
                );

            addChild(
                new TextField
                {
                    text = "powered by jsc",
                    x = 20,
                    y = 40,
                    selectable = false,
                    textColor = 0xffffff,
                }
            );
        }
    }

}
