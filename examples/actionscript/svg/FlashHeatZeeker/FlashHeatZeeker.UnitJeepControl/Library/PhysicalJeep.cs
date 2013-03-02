using Box2D.Common.Math;
using Box2D.Dynamics;
using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitJeep.Library;
using FlashHeatZeekerWithStarlingB2.Library;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.Shared.BCLImplementation.GLSL;
using starling.display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitJeepControl.Library
{
    public class PhysicalJeep : IPhysicalUnit
    {
        public string Identity { get; set; }

        public double CameraRotation { get; set; }

        public DriverSeat driverseat { get; set; }

        StarlingGameSpriteWithJeepTextures textures;
        StarlingGameSpriteWithPhysics Context;
        VisualJeep visual0;

        Wheel[] xwheels;

        public Car unit4_physics;
        public Car karmaunit4_physics;

        public void SetPositionAndAngle(double x, double y, double a)
        {
            this.unit4_physics.body.SetPositionAndAngle(
                new b2Vec2(x, y), a
            );

            this.karmaunit4_physics.body.SetPositionAndAngle(
              new b2Vec2(x, y), a
            );

        }

        public Queue<KeySample> KarmaInput0 = new Queue<KeySample>();

        public PhysicalJeep(StarlingGameSpriteWithJeepTextures textures, StarlingGameSpriteWithPhysics Context)
        {
            this.CameraRotation = Math.PI / 2;

            this.textures = textures;
            this.driverseat = new DriverSeat();
            this.Context = Context;

            visual0 = new VisualJeep(textures, Context);

            for (int i = 0; i < 7; i++)
            {
                this.KarmaInput0.Enqueue(
                    new KeySample()
                );
            }

            Func<double, double, double[]> ff = (a, b) => { return new double[] { a, b }; };


            {
                xwheels = new[] { 
                        //top left
                        new Wheel { b2world = Context.groundkarma_b2world, x = -1.1, y = -1.2, width = 0.4, length = 0.8, revolving = true, powered = true },

                        //top right
                        new Wheel{b2world= Context.groundkarma_b2world, x =1.1,  y =-1.2,  width =0.4,  length =0.8,  revolving =true,  powered =true},


                        //back left
                        new Wheel{b2world= Context.groundkarma_b2world, x =-1.1,  y =1.2,  width =0.4,  length =0.8,  revolving =false,  powered =false},

                        //back right
                        new Wheel{b2world= Context.groundkarma_b2world, x =1.1,  y =1.2,  width =0.4,  length =0.8,  revolving =false,  powered =false},
                    };



                karmaunit4_physics = new Car(
                   b2world: Context.groundkarma_b2world,
                   width: 2,
                   length: 4,
                   position: ff(0, 0),
                   angle: 180,

                   // how fast can the jeep go?
                   power: 120,
                   max_speed: 120,

                   max_steer_angle: 33,
                    //max_steer_angle: 40,

                   wheels: xwheels
               );

            }

            {


                xwheels = new[] { 
                        //top left
                        new Wheel { b2world = Context.ground_b2world, x = -1.1, y = -1.2, width = 0.4, length = 0.8, revolving = true, powered = true },

                        //top right
                        new Wheel{b2world= Context.ground_b2world, x =1.1,  y =-1.2,  width =0.4,  length =0.8,  revolving =true,  powered =true},


                        //back left
                        new Wheel{b2world= Context.ground_b2world, x =-1.1,  y =1.2,  width =0.4,  length =0.8,  revolving =false,  powered =false},

                        //back right
                        new Wheel{b2world= Context.ground_b2world, x =1.1,  y =1.2,  width =0.4,  length =0.8,  revolving =false,  powered =false},
                    };



                unit4_physics = new Car(
                    b2world: Context.ground_b2world,
                    width: 2,
                    length: 4,
                    position: ff(0, 0),
                    angle: 180,

                    // how fast can the jeep go?
                    power: 120,
                    max_speed: 120,

                    max_steer_angle: 33,
                    //max_steer_angle: 40,

                    wheels: xwheels
                );


                xwheels[0].setAngle += a =>
                {
                    var cm = new Matrix();
                    cm.translate(-2, -2);
                    cm.scale(2, 4);
                    cm.rotate(a.DegreesToRadians());

                    cm.translate(-18, -20);

                    visual0.tire0.transformationMatrix = cm;
                };

                xwheels[1].setAngle += a =>
                {

                    var cm = new Matrix();
                    cm.translate(-2, -2);
                    cm.scale(2, 4);
                    cm.rotate(a.DegreesToRadians());

                    cm.translate(18, -20);

                    visual0.tire1.transformationMatrix = cm;
                };
            }
            Context.internalunits.Add(this);

        }

        public b2Body body
        {
            get { return unit4_physics.body; }
        }




        bool prev = false;
        double prevx = 0.0;
        double prevy = 0.0;


        public void ShowPositionAndAngle()
        {
            this.visual0.SetPositionAndAngle(
              this.unit4_physics.body.GetPosition().x * 16,
              this.unit4_physics.body.GetPosition().y * 16,
              this.unit4_physics.body.GetAngle()
          );

            #region Content_layer0_tracks
            if (!prev)
            {
                prev = true;
                prevx = this.body.GetPosition().x;
                prevy = this.body.GetPosition().y;
            }
            else
            {
                var p = this.body.GetPosition();


                var distance = X.GetLength(
                    new __vec2(
                    (float)(p.x - prevx),
                    (float)(p.y - prevy)
                    )
                );

                if (distance > 0.5)
                {

                    foreach (var item in xwheels)
                    {
                        var tex = textures.jeep_trackpattern_semi();

                        var a = item.body.GetAngle() - this.body.GetAngle();

                        if (item.powered)
                            if (Math.Abs(a) > 2.DegreesToRadians())
                            {
                                tex = textures.jeep_trackpattern();
                            }

                        var tracks0 = new Image(tex).AttachTo(Context.Content_layer0_tracks);

                        var cm = new Matrix();

                        cm.translate(-32, -32);
                        cm.rotate(a);

                        cm.translate(
                            item.x * 16,
                            item.y * 16
                         );

                        cm.rotate(this.body.GetAngle());
                        cm.translate(
                            p.x * 16.0,
                            p.y * 16.0
                        );



                        tracks0.transformationMatrix = cm;




                    }

                    prevx = p.x;
                    prevy = p.y;
                }


            }
            #endregion
        }


        public void FeedKarma()
        {
            if (this.KarmaInput0.Count > 0)
            {
                this.KarmaInput0.Enqueue(new KeySample
                {
                    value = CurrentInput.value,

                    fixup = true,
                    angle = this.body.GetAngle(),
                    x = this.body.GetPosition().x,
                    y = this.body.GetPosition().y,
                });
                this.KarmaInput0.Dequeue();
            }
        }


        long xgt;
        public void ApplyVelocity()
        {
            unit4_physics.update(Context.gametime.ElapsedMilliseconds - xgt);


            // what about our karma body?
            if (this.KarmaInput0.Count > 0)
            {
                var _karma__keyDown = this.KarmaInput0.Peek();
                ExtractVelocityFromInput(_karma__keyDown, karmaunit4_physics);


                karmaunit4_physics.update(Context.gametime.ElapsedMilliseconds - xgt);



                if (_karma__keyDown.fixup)
                {
                    var fixupmultiplier = 0.95;

                    // like a magnet
                    karmaunit4_physics.body.SetPositionAndAngle(
                        new b2Vec2(
                            _karma__keyDown.x + (karmaunit4_physics.body.GetPosition().x - _karma__keyDown.x) * fixupmultiplier,
                            _karma__keyDown.y + (karmaunit4_physics.body.GetPosition().y - _karma__keyDown.y) * fixupmultiplier
                        ),
                        // meab me in scotty,
                            _karma__keyDown.angle + (karmaunit4_physics.body.GetAngle() - _karma__keyDown.angle) * fixupmultiplier

                    );
                }


            }

            xgt = Context.gametime.ElapsedMilliseconds;
        }


        public void ExtractVelocityFromInput(KeySample __keyDown, Car unit4_physics)
        {
            //var rot = 0;
            //var dx = 0.0;
            //var dy = 0.0;

            unit4_physics.accelerate = Car.ACC_NONE;
            unit4_physics.steer_left = Car.STEER_NONE;
            unit4_physics.steer_right = Car.STEER_NONE;

            if (__keyDown == null)
                return;

            if (__keyDown[Keys.Up])
            {
                // we have reasone to keep walking

                unit4_physics.accelerate = Car.ACC_ACCELERATE;
                //dy = 1;
            }

            if (__keyDown[Keys.Down])
            {
                // we have reasone to keep walking
                // go slow backwards
                //dy = -0.5;
                unit4_physics.accelerate = Car.ACC_BRAKE;

            }


            if (__keyDown[Keys.Left])
            {
                // we have reasone to keep walking

                unit4_physics.steer_left = Car.STEER_LEFT;

            }

            if (__keyDown[Keys.Right])
            {
                // we have reasone to keep walking

                unit4_physics.steer_right = Car.STEER_RIGHT;

            }
        }

        // nop
        KeySample CurrentInput = new KeySample();
        public void SetVelocityFromInput(KeySample __keyDown)
        {
            CurrentInput = __keyDown;
            ExtractVelocityFromInput(__keyDown, unit4_physics);
        }

     }
}
