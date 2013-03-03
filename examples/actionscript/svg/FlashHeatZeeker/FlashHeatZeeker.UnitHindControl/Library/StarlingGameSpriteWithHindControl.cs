using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitHind.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using starling.display;
using starling.filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitHindControl.Library
{

    public class StarlingGameSpriteWithHindControl : StarlingGameSpriteWithPhysics
    {
        public static object[] __keyDown = new object[0xffffff];


        public StarlingGameSpriteWithHindControl()
        {
            // how much bigger are units in flight altidude?

            var textures_hind = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);


            this.onbeforefirstframe += (stage, s) =>
            {


                var physical0 = new PhysicalHind(textures_hind, this);


                for (int ix = 0; ix < 32; ix++)
                {
                    var physical1 = new PhysicalHind(textures_hind, this);
                    var physical2 = new PhysicalHind(textures_hind, this);
                    var physical3 = new PhysicalHind(textures_hind, this);

                    physical1.SetPositionAndAngle(10 + 20 * ix, 20);

                    physical2.visual.Altitude = 1.0;

                    physical2.SetPositionAndAngle(20 + 20 * ix, 40);

                    physical3.visual.Altitude = 0.5;

                    physical3.SetPositionAndAngle(30 + 20 * ix, 60);
                }





                bool mode_changepending = false;
                //bool visual0_flightmode = false;


                #region __keyDown
                var __keyDown = new KeySample();

                stage.keyDown +=
                   e =>
                   {
                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[System.Windows.Forms.Keys.Alt] = true;

                       __keyDown[(System.Windows.Forms.Keys)e.keyCode] = true;
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[System.Windows.Forms.Keys.Alt] = false;

                     __keyDown[(System.Windows.Forms.Keys)e.keyCode] = false;
                 };

                #endregion

                this.current = physical0;

                // http://doc.starling-framework.org/core/starling/filters/ColorMatrixFilter.html
                // create an inverted filter with 50% saturation and 180° hue rotation
                var filter = new ColorMatrixFilter();
                filter.adjustSaturation(-1.0);
                filter.invert();
                filter.adjustContrast(0.5);

                this.filter = filter;
                this.stage.color = 0x808080;

                onsyncframe +=
                    delegate
                    {
                        #region mode
                        if (!__keyDown[System.Windows.Forms.Keys.Space])
                        {
                            // space is not down.
                            mode_changepending = true;
                        }
                        else
                        {
                            if (mode_changepending)
                            {
                                (current as PhysicalHind).With(
                                    hind1 =>
                                    {
                                        if (hind1.visual.Altitude == 0)
                                            hind1.VerticalVelocity = 1.0;
                                        else
                                            hind1.VerticalVelocity = -0.4;

                                    }
                                );






                                mode_changepending = false;



                            }
                        }
                        #endregion


                        // for camera

                        current.SetVelocityFromInput(__keyDown);




                        #region simulate a weapone!
                        if (__keyDown[System.Windows.Forms.Keys.ControlKey])
                            if (frameid % 20 == 0)
                            {
                                var bodyDef = new b2BodyDef();

                                bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                                // stop moving if legs stop walking!
                                bodyDef.linearDamping = 0;
                                bodyDef.angularDamping = 0;
                                //bodyDef.angle = 1.57079633;
                                bodyDef.fixedRotation = true;

                                var body = physical0.body.GetWorld().CreateBody(bodyDef);
                                body.SetPosition(
                                    new b2Vec2(
                                        current.body.GetPosition().x + 2,
                                        current.body.GetPosition().y + 2
                                    )
                                );

                                body.SetLinearVelocity(
                                       new b2Vec2(
                                         100,
                                        100
                                    )
                                );

                                var fixDef = new Box2D.Dynamics.b2FixtureDef();
                                fixDef.density = 0.1;
                                fixDef.friction = 0.01;
                                fixDef.restitution = 0;


                                fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(1.0);


                                var fix = body.CreateFixture(fixDef);

                                //body.SetPosition(
                                //    new b2Vec2(0, -100 * 16)
                                //);
                            }
                        #endregion
                    };
            };
        }

    }
}
