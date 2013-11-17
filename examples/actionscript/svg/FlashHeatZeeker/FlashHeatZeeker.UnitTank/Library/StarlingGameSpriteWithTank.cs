using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitTank.Library
{
    public class StarlingGameSpriteWithTankTextures
    {
        public Func<Texture>
             tracks0,
             tanktrackpattern,
             greentank_guntower,
             greentank_shadow,
             greentank;

        public StarlingGameSpriteWithTankTextures(Texture64Constructor new_tex_crop)
        {
            tracks0 = new_tex_crop("assets/FlashHeatZeeker.UnitTank/tracks0.svg", innersize: 128, alpha: 0.1);

            tanktrackpattern = new_tex_crop("assets/FlashHeatZeeker.UnitTank/tanktrackpattern.svg", innersize: 128);
            greentank_guntower = new_tex_crop("assets/FlashHeatZeeker.UnitTank/greentank_guntower.svg", innersize: 128);
            greentank_shadow = new_tex_crop("assets/FlashHeatZeeker.UnitTank/greentank_shadow.svg", innersize: 128, alpha: 0.3);

            greentank = new_tex_crop("assets/FlashHeatZeeker.UnitTank/greentank.svg", innersize: 128);
        }
    }

    public sealed class StarlingGameSpriteWithTank : StarlingGameSpriteBase
    {
        public StarlingGameSpriteWithTank()
        {
            var textures = new StarlingGameSpriteWithTankTextures(new_tex_crop);

            this.autorotate = true;

            this.onbeforefirstframe += delegate
             {

                 for (int i = 0; i < 12; i++)
                     for (int yi = 0; yi < 12; yi++)
                     {
                         var rot = random.NextDouble() * Math.PI;

                         var tank1 = new VisualTank(textures, this);

                         tank1.SetPositionAndAngle(i * 128, yi * 128, rot);

                         onframe += delegate
                         {
                             tank1.Animate(1);


                         };
                     }

             };
        }
    }
}
