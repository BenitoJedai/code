using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.Extensions;

namespace TestFlashBC
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {

            // scriptcorelib does not like roslyn that much?

            new TextField { text = "piiksuland!!!" }.AttachTo(this);

            // what if no changes are made beyond comments?
            // what if only constants were changed, can we update the live versions already running?
            // what happens if we want to launch into debugger? will it need to connect to a pre running zombie?
        }

    }
}
