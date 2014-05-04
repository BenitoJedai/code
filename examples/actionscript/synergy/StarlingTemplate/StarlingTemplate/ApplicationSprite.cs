using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;
using starling.core;
using starling.display;
using starling.text;

namespace StarlingTemplate
{
    public sealed class ApplicationSprite : ScriptCoreLib.ActionScript.flash.display.Sprite
    {
        public ApplicationSprite()
        {
            //starling.
            // http://gamua.com/starling/first-steps/

            var s = new Starling(
                typeof(Game).ToClassToken(),
                this.stage
            );

            s.start();
        }

    }

    public class Game : Sprite
    {
        public Game()
        {
            var textField = new TextField(400, 300, "Welcome to Starling!");

            addChild(textField);
        }
    }
}
