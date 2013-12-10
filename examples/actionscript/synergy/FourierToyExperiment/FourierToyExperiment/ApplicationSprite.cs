using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;

namespace FourierToyExperiment
{
    public sealed class ApplicationSprite : Sprite
    {
        public ApplicationSprite()
        {
            // http://toxicdump.org/stuff/FourierToy.swf

            var x = FourierToyExperiment.Design.FourierToy.Source.ToSprite();

            x.AttachTo(this);

        }

    }
}
