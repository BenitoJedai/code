using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using starling.textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace FlashHeatZeeker.UnitPed.Library
{


    public class StarlingGameSpriteWithPedTextures
    {
        public Func<Texture>
            ped_shadow,
            ped_stand,
            ped_walk1_leftfar,
            ped_walk2_leftmid,
            ped_walk3_leftclose,

            ped_walk1x_rightfar,
            ped_walk2x_rightmid,
            ped_walk3x_rightclose;

        public StarlingGameSpriteWithPedTextures(Texture64Constructor new_tex_crop)
        {
            //DRW 3

            ped_shadow = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_shadow.svg", 0.3);

            // do we need to flip?
            ped_stand = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand.svg");

            // left foot
            ped_walk1_leftfar = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk1.svg");
            ped_walk2_leftmid = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk2.svg");
            ped_walk3_leftclose = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk3.svg");

            ped_walk1x_rightfar = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk1.svg", flipx: true);
            ped_walk2x_rightmid = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk2.svg", flipx: true);
            ped_walk3x_rightclose = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk3.svg", flipx: true);

        }

    }

    public class StarlingGameSpriteWithPed : StarlingGameSpriteBase
    {

        public StarlingGameSpriteWithPed()
        {

            this.autorotate = true;


            var textures = new StarlingGameSpriteWithPedTextures(new_tex_crop);



            this.onbeforefirstframe += delegate
            {

                var walk_ani = new[] {
                    textures.ped_walk3_leftclose(), 
                    textures.ped_walk3x_rightclose(),
                    textures.ped_walk1x_rightfar(),
                    textures.ped_walk2x_rightmid(),
                    textures.ped_walk3x_rightclose(),
                    textures.ped_walk3_leftclose(), 
                    textures.ped_walk1_leftfar(), 
                    textures.ped_walk2_leftmid(), 

                };

                var texframes = new[] {

                    textures.ped_stand(),
                };

                // 781
                // 15 FPS
                // 60 FPS
                // 31  FPS
                var peds = new List<Image>();
                for (int i = 0; i < 32; i++)
                    for (int yi = 0; yi < 32; yi++)
                    {
                        var rr = random.Next() % 1024;

                        // 41
                        //var q = new Sprite().AttachTo(Content);

                        // Error: Error #3691: Resource limit for this resource type exceeded.
                        {
                            var imgstand = new Image(
                                textures.ped_shadow()
                                )
                                {
                                    // fkn expensive!!
                                    //alpha = 0.5
                                }.AttachTo(
                                    Content
                                //q
                                    );

                            //peds.Add(imgstand);

                            {
                                var cm = new Matrix();

                                cm.translate(-32, -32);
                                // how big shall the shadow be?
                                cm.scale(4.0, 4.0);

                                //cm.rotate(random.NextDouble() * Math.PI);
                                cm.translate(i * 128, yi * 128);
                                imgstand.transformationMatrix = cm;
                            }
                        }
                        {
                            var imgstand = new Image(
                                texframes[
                                //0
                                random.Next() % texframes.Length
                                    ]) { }.AttachTo(
                                    Content
                                //q
                                    );

                            peds.Add(imgstand);

                            {
                                var cm = new Matrix();

                                cm.translate(-32, -32);
                                cm.scale(2.0, 2.0);

                                cm.rotate(random.NextDouble() * Math.PI);
                                cm.translate(i * 128, yi * 128);
                                imgstand.transformationMatrix = cm;
                            }
                        }



                        // 54FPS without
                        // 43FPS

                    }

                onframe += delegate
                {

                    // animate

                    var ii = ((frameid + 0) / (8)) % walk_ani.Length;

                    foreach (var imgstand in peds)
                    {
                        // 40fps
                        imgstand.texture = walk_ani[ii];
                    }
                };

                //#region sortChildren
                //// http://forum.starling-framework.org/topic/sortchildren-function-causes-flickering
                //Func<DisplayObject, DisplayObject, int> sorter =
                //    (x, y) =>
                //    {
                //        var ix = x as Image;
                //        var iy = y as Image;

                //        if (ix != null)
                //            if (iy != null)
                //            {
                //                if (ix.texture == textures_ped_stand)
                //                    if (iy.texture != textures_ped_stand)
                //                        return -1;

                //                if (ix.texture != textures_ped_stand)
                //                    if (iy.texture == textures_ped_stand)
                //                        return 1;
                //            }

                //        return 0;
                //    };

                //Content.sortChildren(sorter.ToFunction());
                //#endregion
            };


        }
    }
}
