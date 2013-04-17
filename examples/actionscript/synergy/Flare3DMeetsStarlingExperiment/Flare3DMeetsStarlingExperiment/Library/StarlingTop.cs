using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flare3DMeetsStarlingExperiment.Library
{
    class StarlingTop : Sprite
    {
        public StarlingTop()
        {
            var button0 = new Button(Texture.fromBitmap(new ActionScript.Images.tree()), "?");
            button0.useHandCursor = true;

            addChild(button0);
        }
    }
}
