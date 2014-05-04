using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Extensions;

namespace xstarling
{
    public sealed class ApplicationSprite : ScriptCoreLib.ActionScript.flash.display.Sprite
    {
        public ApplicationSprite()
        {
            // X:\jsc.svn\examples\actionscript\synergy\StarlingTemplate\StarlingTemplate\ApplicationSprite.cs
            // wtf, where is the ref?

            var s = new starling.core.Starling(
                    typeof(Game).ToClassToken(),
                    this.stage
                );

            s.start();
        }

    }

    public class Game : starling.display.Sprite
    {
        public Game()
        {
            var textField = new starling.text.TextField(400, 300, "Welcome to Starling!");

            addChild(textField);
        }
    }
}
