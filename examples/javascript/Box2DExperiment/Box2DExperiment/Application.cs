using ScriptCoreLib;
using ScriptCoreLib.Delegates;
using ScriptCoreLib.Extensions;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Components;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Extensions;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Box2DExperiment.Design;
using Box2DExperiment.HTML.Pages;
using box2dweb.opensource.googlecode.box2dweb;
using box2dweb.Design;

namespace Box2DExperiment
{
    /// <summary>
    /// Your client side code running inside a web browser as JavaScript.
    /// </summary>
    public sealed class Application
    {
        public readonly ApplicationWebService service = new ApplicationWebService();

        /// <summary>
        /// This is a javascript application.
        /// </summary>
        /// <param name="page">HTML document rendered by the web server which can now be enhanced.</param>
        public Application(IApp page)
        {
            new box2dweb.opensource.googlecode.box2dweb.Box2D().Content.With(
                source =>
                {
                    source.onload +=
                        delegate
                        {
                            //new IFunction("alert(Box2D);").apply(null);

                            InitializeContent(page);
                        };

                    source.AttachToDocument();
                }
            );
        }

        private void InitializeContent(IApp page)
        {
            Console.WriteLine("InitializeContent");

            //, b2Fixture = Box2D.Dynamics.b2Fixture

            //, b2MassData = Box2D.Collision.Shapes.b2MassData



            ;
            var world = new Box2D.Dynamics.b2World(
               new Box2D.Common.Math.b2Vec2(0, 10)    //gravity
            , true                 //allow sleep
         );
            var fixDef = new Box2D.Dynamics.b2FixtureDef();
            fixDef.density = 1.0;
            fixDef.friction = 0.5;
            fixDef.restitution = 0.2;

            var bodyDef = new Box2D.Dynamics.b2BodyDef();

            //create ground
            const int b2Body_b2_staticBody = 0;
            bodyDef.type = b2Body_b2_staticBody;

            bodyDef.position.x = 9;
            bodyDef.position.y = 13;
            fixDef.shape = new Box2D.Collision.Shapes.b2PolygonShape();
            ((Box2D.Collision.Shapes.b2PolygonShape)fixDef.shape).SetAsBox(10, 0.5);
            world.CreateBody(bodyDef).CreateFixture(fixDef);
            //var x = world.CreateBody(bodyDef);
            //new IFunction("x", "alert(typeof x);").apply(null, x);
            var __random = new IFunction("return Math.random();");
            Func<double> Math_random = () => (double)__random.apply(null);
            //create some objects

            const int b2Body_b2_dynamicBody = 2;
            bodyDef.type = b2Body_b2_dynamicBody;
            for (var i = 0; i < 10; i++)
            {
                if (Math_random() > 0.5)
                {
                    fixDef.shape = new Box2D.Collision.Shapes.b2PolygonShape();
                    ((Box2D.Collision.Shapes.b2PolygonShape)fixDef.shape).SetAsBox(
                          Math_random() + 0.1 //half width
                       , Math_random() + 0.1 //half height
                    );
                }
                else
                {
                    fixDef.shape = new Box2D.Collision.Shapes.b2CircleShape(
                       Math_random() + 0.1 //radius
                    );
                }
                bodyDef.position.x = Math_random() * 10;
                bodyDef.position.y = Math_random() * 10;
                world.CreateBody(bodyDef).CreateFixture(fixDef);
            }
            //setup debug draw
            var debugDraw = new Box2D.Dynamics.b2DebugDraw();
            var context = page.canvas1.getContext("2d");
            debugDraw.SetSprite(context);
            debugDraw.SetDrawScale(30.0);
            debugDraw.SetFillAlpha(0.3);
            debugDraw.SetLineThickness(1.0);
            const int b2DebugDraw_e_shapeBit = 0x1;
            const int b2DebugDraw_e_jointBit = 0x2;
            debugDraw.SetFlags(b2DebugDraw_e_shapeBit | b2DebugDraw_e_jointBit);
            world.SetDebugDraw(debugDraw);

            var tick = default(Action);
            var c = 0;
            tick = delegate
            {
                c++;
                Native.Document.title = "" + c;
                world.Step(
                       1.0 / 60   //frame-rate
                    , 10       //velocity iterations
                    , 10       //position iterations
                        );
                world.DrawDebugData();
                world.ClearForces();



                //requestAnimFrame.apply(null, IFunction.OfDelegate(tick));
                Native.Window.requestAnimationFrame += tick;
            };
            tick();

            Native.Window.requestAnimationFrame += tick;

            Console.WriteLine("InitializeContent done");

            //new IFunction("alert(Box2D);").apply(null);
            //Native.Window.alert("bodyDef=" + bodyDef);
        }
    }
}
