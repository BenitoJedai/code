using Box2D.Collision.Shapes;
using Box2D.Common.Math;
using Box2D.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeekerWithStarlingT04.Library
{
    public class BoxProp
    {

        double[] size;

        public b2Body body;

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

    public class CircleProp
    {

        double[] size;

        public b2Body body;

        public CircleProp(
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

            var shape = new b2CircleShape();
            fixdef.shape = shape;

            shape.SetRadius(this.size[0] / 2);

            fixdef.restitution = 0.4; //positively bouncy!
            this.body.CreateFixture(fixdef);
        }
    }
}
