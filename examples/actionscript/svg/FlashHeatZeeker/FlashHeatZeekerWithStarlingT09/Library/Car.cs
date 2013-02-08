using Box2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Extensions;
using Box2D.Common.Math;
using Box2D.Collision.Shapes;
using Box2D.Dynamics.Joints;


namespace FlashHeatZeekerWithStarlingT09.Library
{
    static class math
    {
        public static double normaliseRadians(this double a)
        {
            a %= 2 * Math.PI;

            if (a < 0)
                a += 2 * Math.PI;

            return a;
        }
    }

    class vectors
    {
        //"gamejs/utils/vectors": function (f, a) {
        //       var g = f("./math"); 


        //    a.distance = function (a, b) { return c(d(a, b)) }; 
        //    var d = a.substract = function (a, b) { return [a[0] - b[0], a[1] - b[1]] }; 

        //    a.add = function (a, b) { return [a[0] + b[0], a[1] + b[1]] }; 
        //    a.multiply = function (a, b) { if (typeof b === "number") return [a[0] * b, a[1] * b]; return [a[0] * b[0], a[1] * b[1]] }; 
        //    a.divide = function (a, b) { if (typeof b === "number") return [a[0] / b, a[1] / b]; throw Error("only divide by scalar supported"); }; 
        public static double len(double[] a)
        {
            return Math.Sqrt(a[0] * a[0] + a[1] * a[1]);
        }

        public static double[] unit(double[] a)
        {
            var b = len(a);

            if (b != 0)
                return new double[] { a[0] / b, a[1] / b };

            //doubleArray3 = new Array(2);
            //return doubleArray3;

            //var zero = new double[] { 0, 0 };
            var zero = new double[2];
            // jsc where are my numbers?
            // jsc does not do that yet. needs to be fixed!
            // X:\jsc.svn\examples\actionscript\Test\TestEmptyDoubleArray\TestEmptyDoubleArray\Class1.cs
            zero[0] = 0;
            zero[1] = 0;

            return zero;
        }

        public static double[] rotate(double[] a, double b)
        {
            b = math.normaliseRadians(b);

            return new double[] { 
                 a[0] * Math.Cos(b) - a[1] * Math.Sin(b), 
                 a[0] * Math.Sin(b) + a[1] * Math.Cos(b)
             };
        }

        public static double dot(double[] a, double[] b)
        {
            return a[0] * b[0] + a[1] * b[1];
        }
        //a.angle = function (a, b) { var d = c(a), f = c(b); return d && f ? Math.acos((a[0] * b[0] + a[1] * b[1]) / (d * f)) : 0 }
    }


    class Wheel
    {
        public Action killSidewaysVelocity;

        public double rotation;
        public Action<double> setAngle;

        public Action<Car> Initialize;

        public bool revolving;
        public bool powered;

        public b2Body body;

        public b2World b2world;

        public double x;
        public double y;
        public double width;
        public double length;


        public Wheel()
        {


            this.Initialize = car =>
            {

                /*
                wheel object 

                pars:

                car - car this wheel belongs to
                x - horizontal position in meters relative to car's center
                y - vertical position in meters relative to car's center
                width - width in meters
                length - length in meters
                revolving - does this wheel revolve when steering?
                powered - is this wheel powered?
                */

                var position = new double[] { x, y };
                //this.car=pars.car;
                //this.revolving=pars.revolving;
                //this.powered=pars.powered;

                //initialize body
                var def = new b2BodyDef();
                def.type = b2Body.b2_dynamicBody;
                def.position = car.body.GetWorldPoint(new b2Vec2(position[0], position[1]));
                def.angle = car.body.GetAngle();
                this.body = b2world.CreateBody(def);

                //initialize shape
                var fixdef = new b2FixtureDef();
                fixdef.density = 1;
                fixdef.isSensor = true; //wheel does not participate in collision calculations: resulting complications are unnecessary

                var fixdef_shape = new b2PolygonShape();

                fixdef.shape = fixdef_shape;
                fixdef_shape.SetAsBox(width / 2, length / 2);
                body.CreateFixture(fixdef);

                var jointdef = new b2RevoluteJointDef();

                //create joint to connect wheel to body
                if (revolving)
                {
                    jointdef.Initialize(car.body, body, body.GetWorldCenter());
                    jointdef.enableMotor = false; //we'll be controlling the wheel's angle manually
                }
                else
                {
                    jointdef.Initialize(car.body, body, body.GetWorldCenter()
                        //, new b2Vec2(1, 0)
                        );
                    jointdef.enableLimit = true;


                    //jointdef.lowerTranslation = 0;
                    //jointdef.upperTranslation = 0;
                }
                b2world.CreateJoint(jointdef);

                #region setAngle
                this.setAngle +=
                    (angle) =>
                    {
                        this.rotation = angle.DegreesToRadians();

                        /*
                        angle - wheel angle relative to car, in degrees
                        */
                        body.SetAngle(car.body.GetAngle() + angle.DegreesToRadians());
                    };
                #endregion


                #region getLocalVelocity
                Func<double[]> getLocalVelocity = delegate
                {
                    /*returns get velocity vector relative to car
                    */
                    var res = car.body.GetLocalVector(car.body.GetLinearVelocityFromLocalPoint(new b2Vec2(position[0], position[1])));
                    return new double[] { res.x, res.y };
                };
                #endregion



                #region getDirectionVector
                Func<double[]> getDirectionVector = delegate
                {
                    /*
                    returns a world unit vector pointing in the direction this wheel is moving
                    */

                    if (getLocalVelocity()[1] > 0)
                        return vectors.rotate(new double[] { 0, 1 }, body.GetAngle());
                    else
                        return vectors.rotate(new double[] { 0, -1 }, body.GetAngle());
                };
                #endregion


                #region getKillVelocityVector
                Func<double[]> getKillVelocityVector = delegate
                {
                    /*
                    substracts sideways velocity from this wheel's velocity vector and returns the remaining front-facing velocity vector
                    */
                    var velocity = body.GetLinearVelocity();
                    var sideways_axis = getDirectionVector();
                    var dotprod = vectors.dot(new[] { velocity.x, velocity.y }, sideways_axis);
                    return new double[] { sideways_axis[0] * dotprod, sideways_axis[1] * dotprod };
                };
                #endregion

                #region killSidewaysVelocity
                this.killSidewaysVelocity = delegate
                {
                    /*
                    removes all sideways velocity from this wheels velocity
                    */
                    var kv = getKillVelocityVector();

                    body.SetLinearVelocity(new b2Vec2(kv[0], kv[1]));

                };
                #endregion
            };

        }





    }

    class Car
    {
        public const int STEER_NONE = 0;
        public const int STEER_RIGHT = 1;
        public const int STEER_LEFT = 2;



        public const int ACC_NONE = 0;
        public const int ACC_ACCELERATE = 1;
        public const int ACC_BRAKE = 2;

        public Action<double> update;

        public b2Body body;

        public int accelerate = ACC_NONE;

        //        //state of car controls
        public int steer_left = STEER_NONE;
        public int steer_right = STEER_NONE;

        public Func<double> getSpeedKMH;

        public Wheel[] wheels;


        public Car(
            b2World b2world,

            double width,
            double length,
            double[] position,
            double angle,
            double power,
            double max_steer_angle,
            double max_speed,
            Wheel[] wheels
            )
        {
            this.wheels = wheels;

            //        /*
            //        pars is an object with possible attributes:

            //        width - width of the car in meters
            //        length - length of the car in meters
            //        position - starting position of the car, array [x, y] in meters
            //        angle - starting angle of the car, degrees
            //        max_steer_angle - maximum angle the wheels turn when steering, degrees
            //        max_speed       - maximum speed of the car, km/h
            //        power - engine force, in newtons, that is applied to EACH powered wheel
            //        wheels - wheel definitions: [{x, y, rotatable, powered}}, ...] where
            //                 x is wheel position in meters relative to car body center
            //                 y is wheel position in meters relative to car body center
            //                 revolving - boolean, does this turn rotate when steering?
            //                 powered - is force applied to this wheel when accelerating/braking?
            //        */



            //        this.max_steer_angle=pars.max_steer_angle;
            //        this.max_speed=pars.max_speed;
            //        this.power=pars.power;
            var wheel_angle = 0.0;//keep track of current wheel angle relative to car.
            //                           //when steering left/right, angle will be decreased/increased gradually over 200ms to prevent jerkyness.

            //initialize body
            var def = new b2BodyDef();
            def.type = b2Body.b2_dynamicBody;
            def.position = new b2Vec2(position[0], position[1]);
            def.angle = angle.DegreesToRadians();
            def.linearDamping = 0.55;  //gradually reduces velocity, makes the car reduce speed slowly if neither accelerator nor brake is pressed
            def.bullet = true; //dedicates more time to collision detection - car travelling at high speeds at low framerates otherwise might teleport through obstacles.
            def.angularDamping = 0.3;

            this.body = b2world.CreateBody(def);

            //initialize shape
            var fixdef = new b2FixtureDef();
            fixdef.density = 1.0;
            fixdef.friction = 0.3; //friction when rubbing agaisnt other shapes
            fixdef.restitution = 0.4;  //amount of force feedback when hitting something. >0 makes the car bounce off, it's fun!

            var fixdef_shape = new b2PolygonShape();

            fixdef.shape = fixdef_shape;
            fixdef_shape.SetAsBox(width / 2, length / 2);
            body.CreateFixture(fixdef);

            //initialize wheels
            foreach (var item in wheels)
            {
                item.Initialize(this);
            }

            //return array of wheels that turn when steering
            IEnumerable<Wheel> getRevolvingWheels = from w in wheels where w.revolving select w;
            //        //return array of powered wheels
            IEnumerable<Wheel> getPoweredWheels = from w in wheels where w.powered select w;

            #region setSpeed
            Action<double> setSpeed = (speed) =>
            {
                /*
                speed - speed in kilometers per hour
                */
                var velocity0 = this.body.GetLinearVelocity();

                //Console.WriteLine("car setSpeed velocity0 " + new { velocity0.x, velocity0.y });

                var velocity2 = vectors.unit(new[] { velocity0.x, velocity0.y });

                //Console.WriteLine("car setSpeed velocity2 " + new { x = velocity2[0], y = velocity2[1] });
                var velocity = new b2Vec2(
                    velocity2[0] * ((speed * 1000.0) / 3600.0),
                    velocity2[1] * ((speed * 1000.0) / 3600.0)
                );

                //Console.WriteLine("car setSpeed SetLinearVelocity " + new { velocity.x, velocity.y });
                this.body.SetLinearVelocity(velocity);

            };
            #endregion


            #region getSpeedKMH
            this.getSpeedKMH = delegate
            {
                var velocity = this.body.GetLinearVelocity();
                var len = vectors.len(new double[] { velocity.x, velocity.y });
                return (len / 1000.0) * 3600.0;
            };
            #endregion

            #region getLocalVelocity
            Func<double[]> getLocalVelocity = delegate
            {
                /*
                returns car's velocity vector relative to the car
                */
                var retv = this.body.GetLocalVector(this.body.GetLinearVelocityFromLocalPoint(new b2Vec2(0, 0)));
                return new double[] { retv.x, retv.y };
            };
            #endregion



            #region update
            this.update = (msDuration) =>
            {


                #region 1. KILL SIDEWAYS VELOCITY

                //kill sideways velocity for all wheels
                for (var i = 0; i < wheels.Length; i++)
                {
                    wheels[i].killSidewaysVelocity();
                }
                #endregion



                #region 2. SET WHEEL ANGLE

                //calculate the change in wheel's angle for this update, assuming the wheel will reach is maximum angle from zero in 200 ms
                var incr = (max_steer_angle / 200.0) * msDuration;

                if (steer_right == STEER_RIGHT)
                {
                    wheel_angle = Math.Min(Math.Max(wheel_angle, 0) + incr, max_steer_angle); //increment angle without going over max steer
                }
                else if (steer_left == STEER_LEFT)
                {
                    wheel_angle = Math.Max(Math.Min(wheel_angle, 0) - incr, -max_steer_angle); //decrement angle without going over max steer
                }
                else
                {
                    wheel_angle = 0;
                }

                //update revolving wheels
                getRevolvingWheels.WithEach(
                    w => w.setAngle(wheel_angle)
                );

                #endregion


                #region 3. APPLY FORCE TO WHEELS
                var base_vect = new double[2]; //vector pointing in the direction force will be applied to a wheel ; relative to the wheel.

                //if accelerator is pressed down and speed limit has not been reached, go forwards
                var lessthanlimit = (getSpeedKMH() < max_speed);
                var flag1 = (accelerate == ACC_ACCELERATE) && lessthanlimit;
                if (flag1)
                {
                    base_vect = new double[] { 0, -1 };
                }
                else if (accelerate == ACC_BRAKE)
                {
                    //braking, but still moving forwards - increased force
                    if (getLocalVelocity()[1] < 0)
                    {
                        base_vect = new double[] { 0, 1.3 };
                    }
                    //going in reverse - less force
                    else
                    {
                        base_vect = new double[] { 0, 0.7 };
                    }
                }
                else
                {
                    base_vect[0] = 0;
                    base_vect[1] = 0;
                }

                //multiply by engine power, which gives us a force vector relative to the wheel
                var fvect = new double[] { 
                        power * base_vect[0], 
                        power * base_vect[1] 
                    };

                //apply force to each wheel



                getPoweredWheels.WithEachIndex(
                    (w, i) =>
                    {
                        var wp = w.body.GetWorldCenter();
                        var wf = w.body.GetWorldVector(new b2Vec2(fvect[0], fvect[1]));

                        //Console.WriteLine("getPoweredWheels ApplyForce #" + i);
                        w.body.ApplyForce(wf, wp);
                    }
                );




                //if going very slow, stop - to prevent endless sliding
                var veryslow = (getSpeedKMH() < 4);
                var flag2 = veryslow && (accelerate == ACC_NONE);
                if (flag2)
                {
                    //Console.WriteLine("setSpeed 0");
                    setSpeed(0);
                }
                #endregion


            };
            #endregion

        }

    }

}
