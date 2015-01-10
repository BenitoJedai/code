using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitTank.ActionScript.Images;
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

        public StarlingGameSpriteWithTankTextures(SpriteToTexture64Constructor new_tex_crop)
        {
            tracks0 = new_tex_crop(new tracks0(), innersize: 128, alpha: 0.1);

            tanktrackpattern = new_tex_crop(new tanktrackpattern(), innersize: 128);
            greentank_guntower = new_tex_crop( new greentank_guntower(), innersize: 128);
            greentank_shadow = new_tex_crop(new greentank_shadow(), innersize: 128, alpha: 0.3);

            greentank = new_tex_crop(new greentank(), innersize: 128);
        }
    }

    public sealed class StarlingGameSpriteWithTank : StarlingGameSpriteBase
    {
        public StarlingGameSpriteWithTank()
        {
            var textures = new StarlingGameSpriteWithTankTextures(new_texsprite_crop);

            this.autorotate = true;

            this.onbeforefirstframe += delegate
             {
                 // this will be called once.




                 
                 // 12 FPS
                 //var c = 64;

                 // 60 FPS
                 // ios 17FPS ?
                 //var c = 20;

                 // ios8 60FPS!
                 var c = 10;

                 for (int i = 0; i < c; i++)
                     for (int yi = 0; yi < c; yi++)
                     {
                         var rot = random.NextDouble() * Math.PI;

                         var tank1 = new VisualTank(textures, this);


                         // setting up some randomization
                         tank1.SetPositionAndAngle(i * 128, yi * 128, rot);

                         // this will be called a lot.
                         onframe += delegate
                         {
                             tank1.Animate(1);


                         };
                     }

             };
        }
    }
}
