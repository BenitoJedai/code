using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitHind.Library;
using FlashHeatZeeker.UnitHindControl.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitRocket.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashHeatZeeker.UnitHindWeaponized.Library
{
    public class PhysicalHindWeaponized : PhysicalHind
    {
        public Action FireRocket;

        public PhysicalHindWeaponized(
            StarlingGameSpriteWithHindTextures textures_hind,
            StarlingGameSpriteWithRocketTextures textures_rocket,
            StarlingGameSpriteWithPhysics __Context
            )
            : base(textures_hind, __Context)
        {
            var Context = __Context;
            var rocket0 = new PhysicalRocket(textures_rocket, Context);
            rocket0.SetPositionAndAngle(-0.5, 2);

            var rocket1 = new PhysicalRocket(textures_rocket, Context);
            rocket1.SetPositionAndAngle(-0.5, -2);

            var hind0 = this;

            #region ShowPositionAndAngleForSlaves
            hind0.ShowPositionAndAngleForSlaves = delegate
            {
                // we are faking 3d here!
                var sc = 1 + hind0.visual.airzoom * hind0.visual.Altitude;


                if (rocket0 != null)
                {
                    rocket0.Altitude = hind0.visual.Altitude;
                    rocket0.SetPositionAndAngle(


                            hind0.body.GetPosition().x + Math.Cos(hind0.body.GetAngle() - Math.PI * 0.5 - hind0.CameraRotation) * 2.2 * sc,
                            hind0.body.GetPosition().y + Math.Sin(hind0.body.GetAngle() - Math.PI * 0.5 - hind0.CameraRotation) * 2.2 * sc,

                        hind0.body.GetAngle() - hind0.CameraRotation
                    );
                    rocket0.ShowPositionAndAngle();
                }

                if (rocket1 != null)
                {
                    rocket1.Altitude = hind0.visual.Altitude;
                    rocket1.SetPositionAndAngle(


                            hind0.body.GetPosition().x + Math.Cos(hind0.body.GetAngle() + Math.PI * 0.5 - hind0.CameraRotation) * 2.2 * sc,
                            hind0.body.GetPosition().y + Math.Sin(hind0.body.GetAngle() + Math.PI * 0.5 - hind0.CameraRotation) * 2.2 * sc,

                        hind0.body.GetAngle() - hind0.CameraRotation
                    );
                    rocket1.ShowPositionAndAngle();
                }
            };
            #endregion

            FireRocket = delegate
            {
                #region create_smoke
                Action<PhysicalRocket> create_smoke = rocket =>
                {
                    var smoke = new PhysicalRocket(textures_rocket, Context)
                    {
                        issmoke = true,
                        smokerandom = Context.random.NextDouble() * Math.PI * 2,
                        smoketime = Context.gametime.ElapsedMilliseconds
                    };

                    {
                        var up = new KeySample();
                        up[Keys.Up] = true;
                        smoke.speed = 5;
                        smoke.SetVelocityFromInput(up);
                    }

                    var a = rocket.body.GetAngle() + (175 + Context.random.Next(10)).DegreesToRadians();

                    smoke.SetPositionAndAngle(
                        rocket.body.GetPosition().x + Math.Cos(a) * 2,
                        rocket.body.GetPosition().y + Math.Sin(a) * 2,
                        a
                        );
                    smoke.ShowPositionAndAngle();
                };
                #endregion


                if (rocket0 != null)
                {
                    {
                        var up = new KeySample();
                        up[Keys.Up] = true;
                        rocket0.speed = 40 + this.body.GetLinearVelocity().Length();
                        rocket0.SetVelocityFromInput(up);
                    }
                    create_smoke(rocket0);
                    rocket0 = null;


                    if (rocket1 == null)
                    {

                        rocket1 = new PhysicalRocket(textures_rocket, Context);
                        rocket1.SetPositionAndAngle(-0.5, -2);


                    }
                }
                else if (rocket1 != null)
                {
                    var up = new KeySample();
                    up[Keys.Up] = true;
                    rocket1.speed = 40 + this.body.GetLinearVelocity().Length();
                    rocket1.SetVelocityFromInput(up);
                    create_smoke(rocket1);
                    rocket1 = null;

                    if (rocket0 == null)
                    {
                        rocket0 = new PhysicalRocket(textures_rocket, Context);
                        rocket0.SetPositionAndAngle(-0.5, 2);
                    }
                }
            };
        }
    }
}
