using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flare3DMeetsStarlingExperiment.Library
{
    class StarlingBack : Sprite
    {
        public StarlingBack()
        {
            var image = new Image(Texture.fromBitmap(new ActionScript.Images.reflections()));

            addChild(image);
        }
    }
}
