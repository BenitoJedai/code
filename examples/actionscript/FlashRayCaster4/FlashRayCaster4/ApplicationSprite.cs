using FlashRayCaster4.Library;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FlashRayCaster4
{
    public sealed class ApplicationSprite : RayCaster4base
    {
        public const int DefaultWidth = 640;
        public const int DefaultHeight = 480;

        public ApplicationSprite()
        {
            // http://www.digital-ist-besser.de/
            // http://www.fredheintz.com/sitefred/main.html


            this.scaleX = 2;
            this.scaleY = 2;
        }

    }
}
