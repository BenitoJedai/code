
// wtf
// X:\jsc.svn\examples\actionscript\svg\FlashHeatZeeker\FlashHeatZeeker.CorePhysics\Library\XContactListener.cs
// box2dflash dependency set?
// yes
// 168KB
// "X:\jsc.svn\market\synergy\box2dflash\box2dflash\bin\staging.AssetsLibrary\box2dflash.AssetsLibrary.dll"
// again, jsc why did you build us assetslibrary without swc?
// 220 KB
using Box2D.Collision;
using Box2D.Dynamics;
using Box2D.Dynamics.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.CorePhysics.Library
{

    public class XContactListener : IContactListener
    {
        // http://www.box2dflash.org/docs/2.1a/reference/Box2D/Dynamics/b2ContactListener.html
        public void BeginContact(b2Contact contact)
        {
        }
        public void EndContact(b2Contact contact) { }


        public bool DiscardSmallImpulse = true;

        public void PostSolve(b2Contact contact, b2ContactImpulse impulse)
        {
            // http://stackoverflow.com/questions/11149091/box2d-damage-physics

            //M:\web\FlashHeatZeekerWithStarlingB2\XContactListener.as(34): col: 83 Error: Implicit coercion of a value of type __AS3__.vec:Vector.<Number> to an unrelated type __AS3__.vec:Vector.<*>.

            //double0 = __Enumerable.Sum_100669321(X.AsEnumerable_100664676(impulse.normalImpulses));

            // http://blog.allanbishop.com/box-2d-2-1a-tutorial-part-6-collision-strength/
            var forceA = impulse.normalImpulses[0];
            // { impulse = { length = 2, forceA = 2.9642496469208197, forceB = 0 } }

            var forceB = impulse.normalImpulses[1];

            if (DiscardSmallImpulse)
                if (forceA < 0.5)
                    if (forceB < 0.5)
                    {
                        // do we care about friction?
                        return;
                    }

            //var min = impulse.normalImpulses.AsEnumerable().Min();
            //var max = impulse.normalImpulses.AsEnumerable().Max();

            //            System.Linq.Enumerable for Double Min(System.Collections.Generic.IEnumerable`1[System.Double]) used at
            //FlashHeatZeekerWithStarlingB2.XContactListener.PostSolve at offset 001d.

            var done = false;

            var fixA = contact.GetFixtureA();
            if (fixA != null)
            {
                var hitA = fixA.GetUserData() as Action<double>;
                if (hitA != null)
                {
                    //Console.WriteLine(new
                    //{
                    //    hitA = new
                    //    {
                    //        forceA,
                    //    }
                    //});

                    hitA(forceA);
                    done = true;
                }
            }

            var fixB = contact.GetFixtureB();
            if (fixB != null)
            {
                var hitB = fixB.GetUserData() as Action<double>;
                if (hitB != null)
                {
                    //Console.WriteLine(new
                    //{
                    //    hitB = new
                    //    {
                    //        forceA,
                    //    }
                    //});

                    hitB(forceA);
                    done = true;
                }
            }

            if (done) return;

            Console.WriteLine(new
            {
                impulse = new
                {
                    impulse.normalImpulses.length,
                    forceA,
                    fixA,
                    forceB,
                    fixB
                    //, min, max 
                }
            });
        }

        public void PreSolve(b2Contact contact, b2Manifold oldManifold) { }
    }

}
