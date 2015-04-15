using Box2D.Collision;
using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using Box2D.Dynamics.Joints;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Box2DTopDownCarScaled
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

	static class X
	{

		public static double DegreesToRadians(this double Degrees)
		{
			return (Math.PI * 2) * Degrees / 360;
		}

		public static double DegreesToRadians(this int Degrees)
		{
			return (Math.PI * 2) * Degrees / 360;
		}

		public static int RadiansToDegrees(this double Arc)
		{
			return (int)(360 * Arc / (Math.PI * 2));
		}

	}

	public sealed class ApplicationSprite : Sprite
	{
		// http://www.banditracer.eu/carexample/

		public const int DefaultWidth = 600;   //screen width in pixels
		public const int DefaultHeight = 400; //screen height in pixels


		public const int STEER_NONE = 0;
		public const int STEER_RIGHT = 1;
		public const int STEER_LEFT = 2;



		public const int ACC_NONE = 0;
		public const int ACC_ACCELERATE = 1;
		public const int ACC_BRAKE = 2;

		class Wheel
		{
			public int accelerate = ACC_NONE;

			public Action killSidewaysVelocity;
			public Action<double> setAngle;

			public Action<Car> Initialize;

			public bool revolving;
			public bool powered;

			public b2Body body;

			public Wheel(
				b2World b2world,

				double x,
				double y,
				double width,
				double length,
				bool revolving,
				bool powered
				)
			{
				this.revolving = revolving;
				this.powered = powered;

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
					fixdef.isSensor = true;	//wheel does not participate in collision calculations: resulting complications are unnecessary

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
					this.setAngle =
						(angle) =>
						{
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
			public bool disable_accelerate_all;

			public int accelerate_all = ACC_NONE;

			public Action<double> update;

			public b2Body body;


			//        //state of car controls
			public int steer = STEER_NONE;
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
				def.linearDamping = 0.15;  //gradually reduces velocity, makes the car reduce speed slowly if neither accelerator nor brake is pressed
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
				Func<double> getSpeedKMH = delegate
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

					if (steer == STEER_RIGHT)
					{
						wheel_angle = Math.Min(Math.Max(wheel_angle, 0) + incr, max_steer_angle); //increment angle without going over max steer
					}
					else if (steer == STEER_LEFT)
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

					wheels.WithEach(
						w =>
						{


							var base_vect = new double[2]; //vector pointing in the direction force will be applied to a wheel ; relative to the wheel.

							//if accelerator is pressed down and speed limit has not been reached, go forwards
							var lessthanlimit = (getSpeedKMH() < max_speed);
							var flag1 = (w.accelerate == ACC_ACCELERATE) && lessthanlimit;
							if (flag1)
							{
								base_vect = new double[] { 0, -1 };
							}
							else if (w.accelerate == ACC_BRAKE)
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



							var wp = w.body.GetWorldCenter();
							var wf = w.body.GetWorldVector(new b2Vec2(fvect[0], fvect[1]));

							//Console.WriteLine("getPoweredWheels ApplyForce #" + i);
							w.body.ApplyForce(wf, wp);






						}
					);

					if (!disable_accelerate_all)
					{

						#region 3. APPLY FORCE TO WHEELS
						var base_vect = new double[2]; //vector pointing in the direction force will be applied to a wheel ; relative to the wheel.

						//if accelerator is pressed down and speed limit has not been reached, go forwards
						var lessthanlimit = (getSpeedKMH() < max_speed);
						var flag1 = (accelerate_all == ACC_ACCELERATE) && lessthanlimit;
						if (flag1)
						{
							base_vect = new double[] { 0, -1 };
						}
						else if (accelerate_all == ACC_BRAKE)
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
						var flag2 = veryslow && (accelerate_all == ACC_NONE);
						if (flag2)
						{
							//Console.WriteLine("setSpeed 0");
							setSpeed(0);
						}
						#endregion

					}
				};
				#endregion

			}

		}


		public class BoxProp
		{

			double[] size;

			b2Body body;

			public BoxProp(
				   b2World b2world,

				 double[] size,
				 double[] position

				)
			{
				/*
                static rectangle shaped prop
     
                    pars:
                    size - array [width, height]
                    position - array [x, y], in world meters, of center
                */
				this.size = size;

				//initialize body
				var bdef = new b2BodyDef();
				bdef.position = new b2Vec2(position[0], position[1]);
				bdef.angle = 0;
				bdef.fixedRotation = true;
				this.body = b2world.CreateBody(bdef);

				//initialize shape
				var fixdef = new b2FixtureDef();

				var shape = new b2PolygonShape();
				fixdef.shape = shape;

				shape.SetAsBox(this.size[0] / 2, this.size[1] / 2);
				fixdef.restitution = 0.4; //positively bouncy!
				this.body.CreateFixture(fixdef);
			}
		}

#if AtInitializeConsoleFormWriter
		Action<Action<string>, Action<string>> AtInitializeConsoleFormWriter;


		#region InitializeConsoleFormWriter
		class __OutWriter : TextWriter
		{
			public Action<string> AtWrite;
			public Action<string> AtWriteLine;

			public override void Write(string value)
			{
				AtWrite(value);
			}

			public override void WriteLine(string value)
			{
				AtWriteLine(value);
			}

			public override Encoding Encoding
			{
				get { return Encoding.UTF8; }
			}
		}

		public void InitializeConsoleFormWriter(
			Action<string> Console_Write,
			Action<string> Console_WriteLine
		)
		{
			AtInitializeConsoleFormWriter(Console_Write, Console_WriteLine);
		}
		#endregion
#endif



		public ApplicationSprite()
		{
#if AtInitializeConsoleFormWriter
			#region AtInitializeConsoleFormWriter

			var w = new __OutWriter();
            var o = Console.Out;
            var __reentry = false;

            var __buffer = new StringBuilder();

            w.AtWrite =
                x =>
                {
                    __buffer.Append(x);
                };

            w.AtWriteLine =
                x =>
                {
                    __buffer.AppendLine(x);
                };

            Console.SetOut(w);

            this.AtInitializeConsoleFormWriter = (
                Action<string> Console_Write,
                Action<string> Console_WriteLine
            ) =>
            {

                try
                {


                    w.AtWrite =
                        x =>
                        {
                            o.Write(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_Write(x);
                                __reentry = false;
                            }
                        };

                    w.AtWriteLine =
                        x =>
                        {
                            o.WriteLine(x);

                            if (!__reentry)
                            {
                                __reentry = true;
                                Console_WriteLine(x);
                                __reentry = false;
                            }
                        };

                    Console.WriteLine("flash Console.WriteLine should now appear in JavaScript form!");
                    Console.WriteLine(__buffer.ToString());
                }
                catch
                {

                }
            };
			#endregion

#endif



			var SCALE = 7;		//how many pixels in a meter
			var WIDTH_M = DefaultWidth / SCALE;	//world width in meters. for this example, world is as large as the screen
			var HEIGHT_M = DefaultHeight / SCALE; //world height in meters

			//initialize font to draw text with
			//var font=new gamejs.font.Font('16px Sans-serif');

			//key bindings
			//var BINDINGS={accelerate:gamejs.event.K_UP, 
			//              brake:gamejs.event.K_DOWN,      
			//              steer_left:gamejs.event.K_LEFT, 
			//               steer_right:gamejs.event.K_RIGHT}; 









			//initialize display
			//var display = gamejs.display.setMode([WIDTH_PX, HEIGHT_PX]);

			//SET UP B2WORLD
			var b2world = new b2World(new b2Vec2(0, 0), false);

			//set up box2d debug draw to draw the bodies for us.
			//in a real game, car will propably be drawn as a sprite rotated by the car's angle
			var debugDraw = new b2DebugDraw();
			debugDraw.SetSprite(this);
			debugDraw.SetDrawScale(SCALE);
			debugDraw.SetFillAlpha(0.5);
			debugDraw.SetLineThickness(1.0);
			debugDraw.SetFlags(b2DebugDraw.e_shapeBit);
			b2world.SetDebugDraw(debugDraw);

			var myscale = 2.0;

			Func<double, double, double[]> ff = (a, b) => { return new double[] { a, b }; };

			var wheels = new[] {
				//top left
				new Wheel(b2world: b2world, x :-1.2*myscale,  y :0,  width :1.2*myscale,  length :3.8*myscale,  revolving :true,  powered :true),

				//top right
				new Wheel(b2world: b2world, x :1.2*myscale,  y :0,  width :1.2*myscale,  length :3.8*myscale,  revolving :true,  powered :true),

				////back left
				//new Wheel(b2world: b2world, x :-1*myscale,  y :1.2*myscale,  width :0.4*myscale,  length :0.8*myscale,  revolving :false,  powered :false),

				////back right
				//new Wheel(b2world: b2world, x :1*myscale,  y :1.2*myscale,  width :0.4*myscale,  length :0.8*myscale,  revolving :false,  powered :false),
			};

			////initialize car
			var car = new Car(
				b2world: b2world,
				width: 2 * myscale,
				length: 4 * myscale,
				position: ff(40, 10),
				angle: 180,
				power: 600,
				max_steer_angle: 20,
				max_speed: 60,
				wheels: wheels
			)
			{
				disable_accelerate_all = true
			};


			var ywheels = new[] {
				//top left
				new Wheel(b2world: b2world, x :-1*myscale,  y :-1.2*myscale,  width :0.4*myscale,  length :0.8*myscale,  revolving :true,  powered :true),

				//top right
				new Wheel(b2world: b2world, x :1*myscale,  y :-1.2*myscale,  width :0.4*myscale,  length :0.8*myscale,  revolving :true,  powered :true),

				//back left
				new Wheel(b2world: b2world, x :-1*myscale,  y :1.2*myscale,  width :0.4*myscale,  length :0.8*myscale,  revolving :false,  powered :false),

				//back right
				new Wheel(b2world: b2world, x :1*myscale,  y :1.2*myscale,  width :0.4*myscale,  length :0.8*myscale,  revolving :false,  powered :false),
			};

			////initialize car
			var ycar = new Car(
				b2world: b2world,
				width: 2 * myscale,
				length: 4 * myscale,
				position: ff(10, 10),
				angle: 180,
				power: 60,
				max_steer_angle: 20,
				max_speed: 60,
				wheels: ywheels
			);

			#region xwheels
			var xwheels = new[] {
				//top left
				new Wheel(b2world: b2world, x :-1,  y :-1.2,  width :0.4,  length :0.8,  revolving :true,  powered :true),

				//top right
				new Wheel(b2world: b2world, x :1,  y :-1.2,  width :0.4,  length :0.8,  revolving :true,  powered :true),

				//back left
				new Wheel(b2world: b2world, x :-1,  y :1.2,  width :0.4,  length :0.8,  revolving :false,  powered :false),

				//back right
				new Wheel(b2world: b2world, x :1,  y :1.2,  width :0.4,  length :0.8,  revolving :false,  powered :false),
			};


			var xcar = new Car(
				 b2world: b2world,
				 width: 2,
				 length: 4,
				 position: ff(5, 10),
				 angle: 180,
				 power: 60,
				 max_steer_angle: 20,
				 max_speed: 60,
				 wheels: xwheels
			 );
			#endregion


			//initialize some props to bounce against
			var props = new List<BoxProp>();


			//outer walls
			props.Add(new BoxProp(b2world, size: ff(WIDTH_M, 1), position: ff(WIDTH_M / 2, 0.5)));
			props.Add(new BoxProp(b2world, size: ff(1, HEIGHT_M - 2), position: ff(0.5, HEIGHT_M / 2)));
			props.Add(new BoxProp(b2world, size: ff(WIDTH_M, 1), position: ff(WIDTH_M / 2, HEIGHT_M - 0.5)));
			props.Add(new BoxProp(b2world, size: ff(1, HEIGHT_M - 2), position: ff(WIDTH_M - 0.5, HEIGHT_M / 2)));

			//pen in the center
			var center = new double[] { WIDTH_M / 2, HEIGHT_M / 2 };
			props.Add(new BoxProp(b2world, size: ff(1, 6), position: ff(center[0] - 3, center[1])));
			props.Add(new BoxProp(b2world, size: ff(1, 6), position: ff(center[0] + 3, center[1])));
			props.Add(new BoxProp(b2world, size: ff(5, 1), position: ff(center[0], center[1] + 2.5)));

			var frameid = 0;

			var KEYS_DOWN = new Dictionary<Keys, bool> {

				{ Keys.Left, false },
				{ Keys.Up, false },
				{ Keys.Right, false },
				{ Keys.Down, false },
			}; //keep track of what keys are held down by the player


			this.stage.keyDown +=
				e =>
				{
					Console.WriteLine("keyDown " + new { e.keyCode });
					KEYS_DOWN[(Keys)e.keyCode] = true;
				};

			this.stage.keyUp +=
			   e =>
			   {
				   Console.WriteLine("keyUp " + new { e.keyCode });
				   KEYS_DOWN[(Keys)e.keyCode] = false;
			   };


			#region tick
			Action<double> tick = (msDuration) =>
			{
				frameid++;

				//if (frameid > 1)
				//    return;

				//Console.WriteLine(new { frameid });
				//GAME LOOP

				//handle events. Key status (depressed or no) is tracked in via KEYS_DOWN associative array
				//gamejs.event.get().forEach(function(event){
				//    //key press
				//    if (event.type === gamejs.event.KEY_DOWN) KEYS_DOWN[event.key] = true;
				//    //key release
				//    else if (event.type === gamejs.event.KEY_UP) KEYS_DOWN[event.key] = false;           
				//});

				//set car controls according to player input
				if (KEYS_DOWN[Keys.Up])
				{
					ycar.accelerate_all = ACC_ACCELERATE;

					car.wheels[0].accelerate = ACC_ACCELERATE;
					car.wheels[1].accelerate = ACC_ACCELERATE;
				}
				else if (KEYS_DOWN[Keys.Down])
				{
					ycar.accelerate_all = ACC_BRAKE;

					car.wheels[0].accelerate = ACC_BRAKE;
					car.wheels[1].accelerate = ACC_BRAKE;
				}
				else
				{
					ycar.accelerate_all = ACC_NONE;

					car.wheels[0].accelerate = ACC_NONE;
					car.wheels[1].accelerate = ACC_NONE;
				}

				if (KEYS_DOWN[Keys.Right])
				{

					car.wheels[0].accelerate = ACC_ACCELERATE;
					car.wheels[1].accelerate = ACC_BRAKE;

					ycar.steer = STEER_RIGHT;
				}
				else if (KEYS_DOWN[Keys.Left])
				{

					car.wheels[0].accelerate = ACC_BRAKE;
					car.wheels[1].accelerate = ACC_ACCELERATE;

					ycar.steer = STEER_LEFT;
				}
				else
				{
					ycar.steer = STEER_NONE;
				}

				////update car
				car.update(msDuration);
				ycar.update(msDuration);

				//update physics world
				b2world.Step(msDuration / 1000.0, 10, 8);

				//clear applied forces, so they don't stack from each update
				b2world.ClearForces();

				//fill background
				//gamejs.draw.rect(display, '#FFFFFF', new gamejs.Rect([0, 0], [WIDTH_PX, HEIGHT_PX]),0)

				//let box2d draw it's bodies
				b2world.DrawDebugData();

				//fps and car speed display
				//display.blit(font.render('FPS: '+parseInt((1000)/msDuration)), [25, 25]);
				//display.blit(font.render('SPEED: '+parseInt(Math.ceil(car.getSpeedKMH()))+' km/h'), [25, 55]);
				//Console.WriteLine(new { frameid } + " done!");
			};

			////gamejs.time.fpsCallback(tick, this, 60);
			var sw = new Stopwatch();
			sw.Start();

			this.enterFrame +=
				delegate
				{
					tick(sw.ElapsedMilliseconds);
					sw.Restart();
				};
			#endregion

		}
	}
}

//TypeError: Error #1009: Cannot access a property or method of a null object reference.
//	at Box2DTopDownCarScaled::ApplicationSprite_Car___c__DisplayClass6_1/__ctor_b__5_1694574d_06000017()
//    at Function/http://adobe.com/AS3/2006/builtin::apply()
//	at ScriptCoreLib.Shared.BCLImplementation.System::__Action_1/Invoke_4ebbe596_06002605()
//    at Box2DTopDownCarScaled::ApplicationSprite___c__DisplayClass11_0/__ctor_b__3_1694574d_0600000d()
//    at Function/http://adobe.com/AS3/2006/builtin::apply()
//	at ScriptCoreLib.Shared.BCLImplementation.System::__Action_1/Invoke_4ebbe596_06002605()
//    at Box2DTopDownCarScaled::ApplicationSprite___c__DisplayClass11_0/__ctor_b__4_1694574d_0600000e()
