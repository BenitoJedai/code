using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace TestUInntCompareToNonUInt32
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            uint x = 32;

            // jsc should autostream console to javascript console in 2014!

            var z = x.CompareTo("other");

        }

    }
}
