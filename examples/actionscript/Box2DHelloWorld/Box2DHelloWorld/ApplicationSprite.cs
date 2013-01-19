using Box2D.Collision;
using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.Extensions;
using System;

namespace Box2DHelloWorld
{
    [SWF(width = 1280, height = 720)]
    public sealed class ApplicationSprite : Sprite
    {
        private const int PIXELS_TO_METRE = 30;
        private const int SWF_HALF_WIDTH = 400;
        private const int SWF_HEIGHT = 600;
        private b2World _world;

        // http://blog.allanbishop.com/box2d-2-1a-tutorial-part-1/

        public ApplicationSprite()
        {

            _world = new b2World(new b2Vec2(0, 10), true);

            var groundBodyDef = new b2BodyDef();
            groundBodyDef.position.Set(SWF_HALF_WIDTH / PIXELS_TO_METRE,
                          SWF_HEIGHT / PIXELS_TO_METRE - 20 / PIXELS_TO_METRE);

            var groundBody = _world.CreateBody(groundBodyDef);

            var groundBox = new b2PolygonShape();
            groundBox.SetAsBox(SWF_HALF_WIDTH / PIXELS_TO_METRE,
                           20 / PIXELS_TO_METRE);

            var groundFixtureDef = new b2FixtureDef();
            groundFixtureDef.shape = groundBox;
            groundFixtureDef.density = 1;
            groundFixtureDef.friction = 1;
            groundBody.CreateFixture(groundFixtureDef);

            var bodyDef = new b2BodyDef();
            bodyDef.type = b2Body.b2_dynamicBody;
            bodyDef.position.Set(SWF_HALF_WIDTH / PIXELS_TO_METRE, 4);
            var body = _world.CreateBody(bodyDef);

            var dynamicBox = new b2PolygonShape();
            dynamicBox.SetAsBox(1, 1);

            var fixtureDef = new b2FixtureDef();
            fixtureDef.shape = dynamicBox;
            fixtureDef.density = 1;
            fixtureDef.friction = 0.3;

            body.CreateFixture(fixtureDef);

            var debugSprite = new Sprite();
            addChild(debugSprite);
            var debugDraw = new b2DebugDraw();
            debugDraw.SetSprite(debugSprite);
            debugDraw.SetDrawScale(PIXELS_TO_METRE);
            debugDraw.SetLineThickness(1.0);
            debugDraw.SetAlpha(1);
            debugDraw.SetFillAlpha(0.4);
            debugDraw.SetFlags(b2DebugDraw.e_shapeBit);
            _world.SetDebugDraw(debugDraw);


            // Add event for main loop

            this.stage.enterFrame +=
                delegate
                {
                    var timeStep = 1 / 30.0;
                    var velocityIterations = 6;
                    var positionIterations = 2;

                    _world.Step(timeStep, velocityIterations, positionIterations);
                    _world.ClearForces();
                    _world.DrawDebugData();

                };


        }


    }
}
