using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitCannon.Library
{
    public class VisualCannon : IVisualUnit
    {
        Image mount;
        Image shadow;
        Image guntower;

        public VisualCannon(StarlingGameSpriteWithCannonTextures textures, StarlingGameSpriteBase Context)
        {
            mount = new Image(
                textures.tracergun_mount()
                )
                {
                }.AttachTo(
                Context.Content_layer0_tracks
            );

            shadow = new Image(
                 textures.tracergun_guntower_shadow()
                 )
                    {
                    }.AttachTo(
                 Context.Content_layer2_shadows
             );

            guntower = new Image(
                textures.tracergun_guntower()
                )
                    {
                    }.AttachTo(
                Context.Content_layer3_buildings
            );
        }

        public void SetPositionAndAngle(double x, double y, double angle)
        {
            {
                var cm = new Matrix();
                cm.translate(-64, -64);
                cm.translate(
                    x,
                    y
                );

                mount.transformationMatrix = cm;
            }
            {
                var cm = new Matrix();
                cm.translate(-64, -64);
                cm.rotate(angle);
                cm.translate(
                    x,
                    y
                );


                guntower.transformationMatrix = cm;
            }
            {
                var cm = new Matrix();
                cm.translate(-64, -64);
                cm.rotate(angle);
                cm.translate(
                    x,
                    y
                );
                cm.translate(8, 8);

                shadow.transformationMatrix = cm;
            }
        }
    }
}
