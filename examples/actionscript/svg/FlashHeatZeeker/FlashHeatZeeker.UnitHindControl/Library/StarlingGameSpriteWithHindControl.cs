using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.StarlingSetup.Library;
using FlashHeatZeeker.UnitHind.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Extensions;
using starling.display;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitHindControl.Library
{
    public class StarlingGameSpriteWithHindControl : StarlingGameSpriteWithPhysics
    {
        public static object[] __keyDown = new object[0xffffff];


        public StarlingGameSpriteWithHindControl()
        {
            // how much bigger are units in flight altidude?
            var airzoom = 1.5;

            var textures = new StarlingGameSpriteWithHindTextures(this.new_tex_crop);


            this.onbeforefirstframe += (stage, s) =>
            {
                // refactor physics, visual

                b2Body ground_current = null;
                b2Body air_current = null;

                


                #region ground_b2world
               

                {
                    var ground_bodyDef = new b2BodyDef();

                    ground_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                    // stop moving if legs stop walking!
                    ground_bodyDef.linearDamping = 10.0;
                    ground_bodyDef.angularDamping = 20.0;
                    //bodyDef.angle = 1.57079633;
                    ground_bodyDef.fixedRotation = true;

                    var ground_body = ground_b2world.CreateBody(ground_bodyDef);
                    ground_current = ground_body;


                    var ground_fixDef = new Box2D.Dynamics.b2FixtureDef();
                    ground_fixDef.density = 0.1;
                    ground_fixDef.friction = 0.01;
                    ground_fixDef.restitution = 0;

                    var ground_fixdef_shape = new b2PolygonShape();

                    ground_fixDef.shape = ground_fixdef_shape;

                    // physics unit is looking to right
                    ground_fixdef_shape.SetAsBox(4, 1);



                    var ground_fix = ground_body.CreateFixture(ground_fixDef);
                }



                #endregion


                #region air_b2world
               



                {
                    var air_bodyDef = new b2BodyDef();

                    air_bodyDef.type = Box2D.Dynamics.b2Body.b2_dynamicBody;

                    // stop moving if legs stop walking!
                    air_bodyDef.linearDamping = 10.0;
                    air_bodyDef.angularDamping = 20.0;
                    //bodyDef.angle = 1.57079633;
                    air_bodyDef.fixedRotation = true;

                    var air_body = air_b2world.CreateBody(air_bodyDef);
                    air_current = air_body;


                    var air_fixDef = new Box2D.Dynamics.b2FixtureDef();
                    air_fixDef.density = 0.1;
                    air_fixDef.friction = 0.01;
                    air_fixDef.restitution = 0;

                    var air_fixdef_shape = new b2PolygonShape();

                    air_fixDef.shape = air_fixdef_shape;

                    // physics unit is looking to right
                    air_fixdef_shape.SetAsBox(4, 1);



                    var air_fix = air_body.CreateFixture(air_fixDef);
                }


                #endregion

        





                {

                    #region currentvisual
                    var currentshadow = new Image(
                      textures.hind0_shadow()
                      )
                    {
                    }.AttachTo(
                       Content
                   );

                    var currentvisual = new Sprite().AttachTo(Content);

                    var nowings = new Image(
                      textures.hind0_nowings()
                      )
                    {
                    }.AttachTo(currentvisual);

                    var wings = new Sprite().AttachTo(currentvisual);

                    Enumerable.Range(0, 5).Select(
                        wingindex =>
                            new Image(textures.hind0_wing1()).AttachTo(wings).With(
                              img =>
                              {
                                  var cm = new Matrix();

                                  cm.translate(-160, -160);
                                  cm.rotate(Math.PI * 2 * wingindex / 5);


                                  img.transformationMatrix = cm;

                              }
                            )
                    ).ToArray();





                    {
                        var cm = new Matrix();

                        cm.translate(-160, -160);


                        nowings.transformationMatrix = cm;
                    }
                    #endregion



            

                    bool flightmode_changepending = false;
                    bool flightmode = false;

                    #region __keyDown

                    stage.keyDown +=
                       e =>
                       {
                           if (__keyDown[e.keyCode] != null)
                               return;

                           // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                           if (e.altKey)
                               __keyDown[(int)Keys.Alt] = new object();

                           __keyDown[e.keyCode] = new object();
                       };

                    stage.keyUp +=
                     e =>
                     {
                         if (!e.altKey)
                             __keyDown[(int)Keys.Alt] = null;

                         __keyDown[e.keyCode] = null;
                     };

                    #endregion


                    var current_speed = 40.0;

             
                    onframe +=
                        delegate
                        {
                            #region flightmode
                            if (__keyDown[(int)Keys.Space] == null)
                            {
                                // space is not down.
                                flightmode_changepending = true;
                            }
                            else
                            {
                                if (flightmode_changepending)
                                {
                                    flightmode = !flightmode;
                                    flightmode_changepending = false;
                                }
                            }
                            #endregion

                            #region SetLinearVelocity

                            var rot = 0;
                            var dx = 0.0;
                            var dy = 0.0;

                            if (__keyDown[(int)Keys.Up] != null)
                            {
                                // we have reasone to keep walking

                                dy = 1;
                            }

                            if (__keyDown[(int)Keys.Down] != null)
                            {
                                // we have reasone to keep walking
                                // go slow backwards
                                dy = -0.5;

                            }

                            if (__keyDown[(int)Keys.Alt] == null)
                            {
                                if (__keyDown[(int)Keys.Left] != null)
                                {
                                    // we have reasone to keep walking

                                    rot = -1;

                                }

                                if (__keyDown[(int)Keys.Right] != null)
                                {
                                    // we have reasone to keep walking

                                    rot = 1;

                                }
                            }
                            else
                            {
                                if (__keyDown[(int)Keys.Left] != null)
                                {
                                    // we have reasone to keep walking

                                    dx = -1;

                                }

                                if (__keyDown[(int)Keys.Right] != null)
                                {
                                    // we have reasone to keep walking

                                    dx = 1;

                                }
                            }

                             current = ground_current;
                             current_slave1 = air_current;

                            if (flightmode)
                            {
                                current = air_current;
                                current_slave1 = ground_current;


                                air_dd.alpha = 0.6;
                                ground_dd.alpha = 0.1;
                            }
                            else
                            {
                                air_dd.alpha = 0.1;
                                ground_dd.alpha = 0.6;
                            }


                   


                            {
                                var v = rot * 10;
                                if (v != 0)
                                    current.SetAngularVelocity(v);
                            }

                            {
                                var vx = Math.Cos(current.GetAngle()) * dy * current_speed
                                        + Math.Cos(current.GetAngle() + Math.PI / 2) * dx * current_speed;
                                var vy = Math.Sin(current.GetAngle()) * dy * current_speed
                                        + Math.Sin(current.GetAngle() + Math.PI / 2) * dx * current_speed;

                                if (vx == 0 && vy == 0)
                                {

                                }
                                else
                                {
                                    current.SetLinearVelocity(
                                        new b2Vec2(
                                         vx, vy


                                        )
                                    );
                                }
                            }

                            #endregion

                            #region animate wings
                            {
                                var cm = new Matrix();

                                if (flightmode)
                                    cm.rotate(this.gametime.ElapsedMilliseconds * 0.001 * 5);
                                else
                                    cm.rotate(this.gametime.ElapsedMilliseconds * 0.001);


                                wings.transformationMatrix = cm;

                            }
                            #endregion


                        

                            #region transformationMatrix, phisics updated, now update visual

                    


                            {
                                var cm = new Matrix();


                                cm.translate(-160, -160);


                                // shadow with tracks!
                                cm.scale(1.2, 1.0);
                                //cm.rotate(rot);
                                //cm.translate(i * 128, yi * 128);



                                cm.rotate(current.GetAngle() + Math.PI / 2);
                                cm.translate(
                                    current.GetPosition().x * 16,
                                    current.GetPosition().y * 16
                                );


                                cm.translate(8, 8);

                                if (flightmode)
                                    cm.translate(96 * airzoom, 96 * airzoom);

                                currentshadow.transformationMatrix = cm;
                            }

                            {
                                var cm = new Matrix();

                                cm.rotate(current.GetAngle() + Math.PI / 2);

                                if (flightmode)
                                    cm.scale(airzoom, airzoom);

                                cm.translate(
                                    current.GetPosition().x * 16,
                                    current.GetPosition().y * 16
                                );

                                currentvisual.transformationMatrix = cm;
                            }
                            #endregion


                         
                        };
                }
            };
        }

    }
}
