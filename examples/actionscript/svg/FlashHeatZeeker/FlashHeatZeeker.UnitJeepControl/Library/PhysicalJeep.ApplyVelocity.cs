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
    public partial class PhysicalJeep : IPhysicalUnit
    {
        long xgt;
        public void ApplyVelocity()
        {
            unit4_physics.update(Context.gametime.ElapsedMilliseconds - xgt);


            #region RemoteGameReference
            if (RemoteGameReference != null)
            {
                //this.visual0.currentvisual.alpha = 0.5;

                // not moving anymore in network mode
                // far enough to be out of sync?

                if (karmaunit4_physics.body.GetLinearVelocity().Length() < 0.5)
                    if (this.KarmaInput0.All(k => k.value == 0))
                    {
                        var gap = new __vec2(
                            (float)this.karmaunit4_physics.body.GetPosition().x - (float)this.body.GetPosition().x,
                            (float)this.karmaunit4_physics.body.GetPosition().y - (float)this.body.GetPosition().y
                        );

                        this.body.SetAngle(
                               this.body.GetAngle() + (this.karmaunit4_physics.body.GetAngle() - this.body.GetAngle()) * 0.2
                        );

                        // tolerate lesser distance?
                        //if (gap.GetLength() > 3)
                        {

                            // too much out of sync!
                            var TooMuchOutOfSyncOrOutOfView = gap.GetLength() > 10;
                            if (TooMuchOutOfSyncOrOutOfView)
                            {
                                this.body.SetPositionAndAngle(
                                    new b2Vec2(
                                        this.karmaunit4_physics.body.GetPosition().x,
                                        this.karmaunit4_physics.body.GetPosition().y
                                    ),
                                    this.karmaunit4_physics.body.GetAngle()
                                );
                            }
                            else
                            {
                                this.body.SetPosition(
                                    new b2Vec2(
                                        this.body.GetPosition().x + gap.x * 0.2,
                                        this.body.GetPosition().y + gap.y * 0.2
                                    )
                                );
                            }


                            // look at where we should be instead


                        }
                    }

            }
            #endregion



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



    }
}
