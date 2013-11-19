using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CoreMap.ActionScript.Images;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitTank.Library;
using FlashHeatZeeker.UnitTankControl.Library;
using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.CoreMap.Library
{
    public class StarlingGameSpriteWithMapTextures
    {
        public Func<Texture>
            tree0,
            tree0_shadow,

            hill1,
            hole1,
            grass1,

            road0,
            touchdown;




        public StarlingGameSpriteWithMapTextures(Texture64Constructor new_tex_crop)
        {
            tree0 = new_tex_crop("assets/FlashHeatZeeker.CoreMap/tree0.svg", innersize: 256);
            tree0_shadow = new_tex_crop("assets/FlashHeatZeeker.CoreMap/tree0_shadow.svg", innersize: 256, alpha: 0.3);

            hill1 = new_tex_crop("assets/FlashHeatZeeker.CoreMap/hill1.svg", innersize: 256);
            hole1 = new_tex_crop("assets/FlashHeatZeeker.CoreMap/hole1.svg", innersize: 256);
            grass1 = new_tex_crop("assets/FlashHeatZeeker.CoreMap/grass1.svg", innersize: 256);

            road0 = new_tex_crop("assets/FlashHeatZeeker.CoreMap/road0.svg", innersize: 256);
            touchdown = new_tex_crop("assets/FlashHeatZeeker.CoreMap/touchdown.svg", innersize: 256);

         

        }
    }

    public class StarlingGameSpriteWithMapExplosionsTextures
    {
     

        public Func<Texture>[] explosions;

        public StarlingGameSpriteWithMapExplosionsTextures(TextureFromImage new_tex96)
        {
          
            explosions = new[] {
                new_tex96(new ani6_01(), innersize: 64),
                new_tex96(new ani6_02(), innersize: 64),
                new_tex96(new ani6_03(), innersize: 64),
                new_tex96(new ani6_04(), innersize: 64),
                new_tex96(new ani6_05(), innersize: 64),
                new_tex96(new ani6_06(), innersize: 64),
                new_tex96(new ani6_07(), innersize: 64),
                new_tex96(new ani6_08(), innersize: 64),
                new_tex96(new ani6_09(), innersize: 64),
                new_tex96(new ani6_10(), innersize: 64),
                new_tex96(new ani6_11(), innersize: 64),
                new_tex96(new ani6_12(), innersize: 64),
                new_tex96(new ani6_13(), innersize: 64),
                new_tex96(new ani6_14(), innersize: 64),
                new_tex96(new ani6_15(), innersize: 64),
                new_tex96(new ani6_16(), innersize: 64)
            };

        }
    }

    class StarlingGameSpriteWithMap : StarlingGameSpriteWithPhysics
    {
        public StarlingGameSpriteWithMap()
        {
            var textures = new StarlingGameSpriteWithTankTextures(new_texsprite_crop);
            var textures_map = new StarlingGameSpriteWithMapTextures(new_tex_crop);
            var textures_explosions = new StarlingGameSpriteWithMapExplosionsTextures(new_tex96);

      

            this.onbeforefirstframe += (stage, s) =>
            {
                new Image(textures_map.hill1()).AttachTo(Content).y = -256;
                new Image(textures_map.hole1()).AttachTo(Content).y = -512;
                new Image(textures_map.grass1()).AttachTo(Content).y = -512 - 256;

                new Image(textures_map.road0()).AttachTo(Content).x = -256;
                new Image(textures_map.road0()).AttachTo(Content).x = 0;
                new Image(textures_map.road0()).AttachTo(Content).x = 256;

                new Image(textures_map.touchdown()).AttachTo(Content).y = 256;
                new Image(textures_map.tree0_shadow()).AttachTo(Content).y = 128 + 16;
                new Image(textures_map.tree0()).AttachTo(Content).y = 128;

                var exp = new Image(textures_explosions.explosions[0]()).AttachTo(Content);

                #region __keyDown
                var __keyDown = new KeySample();

                stage.keyDown +=
                   e =>
                   {
                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[Keys.Alt] = true;

                       __keyDown[(Keys)e.keyCode] = true;
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[Keys.Alt] = false;

                     __keyDown[(Keys)e.keyCode] = false;
                 };

                #endregion



                var tank1 = new PhysicalTank(textures, this);
                current = tank1;

                onsyncframe += delegate
                {
                    exp.texture = textures_explosions.explosions[this.syncframeid % textures_explosions.explosions.Length]();


                    tank1.SetVelocityFromInput(__keyDown);



                    this.Text = new { this.syncframeid, this.syncframetime }.ToString();
                };
            };
        }
    }
}
