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
            hud_look,
            hud_look_building,
            hud_look_goggles,

            ped_shadow,

            ped_footprints,
            ped_down,

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

            hud_look = new_tex_crop("assets/FlashHeatZeeker.UnitPed/hud_look.svg", innersize: 128);
            hud_look_building = new_tex_crop("assets/FlashHeatZeeker.UnitPed/hud_look_building.svg", innersize: 128);
            hud_look_goggles = new_tex_crop("assets/FlashHeatZeeker.UnitPed/hud_look_goggles.svg", innersize: 128);

            ped_shadow = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_shadow.svg", 0.3, innersize: 96);

            ped_footprints = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_footprints.svg", 0.07);

            ped_down = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_down.svg", innersize: 96);
            ped_stand = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand.svg", innersize: 96);

            // left foot
            ped_walk1_leftfar = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk1.svg", innersize: 96);
            ped_walk2_leftmid = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk2.svg", innersize: 96);
            ped_walk3_leftclose = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk3.svg", innersize: 96);

            ped_walk1x_rightfar = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk1.svg", flipx: true, innersize: 96);
            ped_walk2x_rightmid = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk2.svg", flipx: true, innersize: 96);
            ped_walk3x_rightclose = new_tex_crop("assets/FlashHeatZeeker.UnitPed/ped_stand_walk3.svg", flipx: true, innersize: 96);

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
                var hud = new Image(textures.hud_look_goggles()).AttachTo(this);


                // 781
                // 15 FPS
                // 60 FPS
                // 31  FPS
                var peds = new List<VisualPed>();
                for (int i = 0; i < 32; i++)
                    for (int yi = 0; yi < 32; yi++)
                    {
                        var rr = random.Next() % 1024;

                        var visual0 = new VisualPed(textures, this);
                        visual0.SetPositionAndAngle(
                            i * 128, yi * 128, random.NextDouble() * Math.PI
                            );
                        peds.Add(visual0);



                    }

                onresize(
                    (w, h) =>
                    {

                    }
                );

                onframe += delegate
                {

                    // animate

                    foreach (var visual0 in peds)
                    {
                        visual0.Animate(1, 1);
                    }


                    {
                        var cm = new Matrix();

                        cm.scale(0.5, 0.5);
                        cm.translate(16, stage.stageHeight - 64 - 24);

                        hud.transformationMatrix = cm;
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
