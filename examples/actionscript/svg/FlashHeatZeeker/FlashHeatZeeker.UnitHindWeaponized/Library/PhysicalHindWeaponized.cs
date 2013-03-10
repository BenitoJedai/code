using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitHind.Library;
using FlashHeatZeeker.UnitHindControl.Library;
using FlashHeatZeeker.UnitJeepControl.Library;
using FlashHeatZeeker.UnitRocket.Library;
using starling.display;
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
            StarlingGameSpriteWithPhysics __Context,


            Image Explosion1 = null
            )
            : base(textures_hind, __Context)
        {
            var RocketsMax = 12;
            var Rockets = new Queue<PhysicalRocket>();

            var Context = __Context;
            var rocket0 = new PhysicalRocket(textures_rocket, Context, Explosion1: Explosion1);
            rocket0.body.SetActive(false);
            rocket0.SetPositionAndAngle(-0.5, 2);


            var rocket1 = new PhysicalRocket(textures_rocket, Context, Explosion1: Explosion1);
            rocket1.body.SetActive(false);
            rocket1.SetPositionAndAngle(-0.5, -2);

            #region z fixup
            rocket0.visual.parent.setChildIndex(
                rocket0.visual,

                this.visual.visualnowings.parent.getChildIndex(
                    this.visual.visualnowings
                )
            );
            #endregion

            #region z fixup
            rocket1.visual.parent.setChildIndex(
                rocket1.visual,

                this.visual.visualnowings.parent.getChildIndex(
                    this.visual.visualnowings
                )
            );
            #endregion

            var hind0 = this;

            #region ShowPositionAndAngleForSlaves
            hind0.ShowPositionAndAngleForSlaves = delegate
            {
                // we are faking 3d here!
                var sc = 1 + hind0.visual.airzoom * hind0.visual.Altitude;


                if (rocket0 != null)
                {
                    rocket0.body.SetActive(false);
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
                    rocket1.body.SetActive(false);
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

            #region FireRocket
            FireRocket = delegate
            {



                if (rocket0 != null)
                {
                    var sc = 1 + hind0.visual.airzoom * hind0.visual.Altitude;

                    rocket0.SetPositionAndAngle(


                              hind0.body.GetPosition().x + Math.Cos(hind0.body.GetAngle() - Math.PI * 0.5 - hind0.CameraRotation) * 3.5 * sc,
                              hind0.body.GetPosition().y + Math.Sin(hind0.body.GetAngle() - Math.PI * 0.5 - hind0.CameraRotation) * 3.5 * sc,

                          hind0.body.GetAngle() - hind0.CameraRotation
                      );
                    rocket0.ShowPositionAndAngle();
                    rocket0.body.SetActive(true);

                    rocket0.CreateSmoke();
                    {
                        var up = new KeySample();
                        up[Keys.Up] = true;
                        rocket0.speed = 60 + this.body.GetLinearVelocity().Length();
                        rocket0.SetVelocityFromInput(up);
                    }
                    Rockets.Enqueue(rocket0);
                    rocket0 = null;


                    if (rocket1 == null)
                    {
                        if (Rockets.Count > RocketsMax)
                        {
                            rocket1 = Rockets.Dequeue();
                            rocket1.SetVelocityFromInput(new KeySample());
                            rocket1.visual.visible = true;
                        }
                        else
                        {
                            rocket1 = new PhysicalRocket(textures_rocket, Context, Explosion1: Explosion1);
                            rocket1.body.SetActive(false);

                            #region z fixup
                            rocket1.visual.parent.setChildIndex(
                                rocket1.visual,

                                this.visual.visualnowings.parent.getChildIndex(
                                    this.visual.visualnowings
                                )
                            );
                            #endregion
                        }
                    }
                }
                else if (rocket1 != null)
                {
                    var sc = 1 + hind0.visual.airzoom * hind0.visual.Altitude;

                    rocket1.SetPositionAndAngle(


                              hind0.body.GetPosition().x + Math.Cos(hind0.body.GetAngle() + Math.PI * 0.5 - hind0.CameraRotation) * 3.5 * sc,
                              hind0.body.GetPosition().y + Math.Sin(hind0.body.GetAngle() + Math.PI * 0.5 - hind0.CameraRotation) * 3.5 * sc,

                          hind0.body.GetAngle() - hind0.CameraRotation
                      );
                    rocket1.ShowPositionAndAngle();
                    rocket1.body.SetActive(true);

                    rocket1.CreateSmoke();
                    {
                        var up = new KeySample();
                        up[Keys.Up] = true;
                        rocket1.speed = 60 + this.body.GetLinearVelocity().Length();
                        rocket1.SetVelocityFromInput(up);
                    }
                    Rockets.Enqueue(rocket1);
                    rocket1 = null;

                    if (rocket0 == null)
                    {
                        if (Rockets.Count > RocketsMax)
                        {
                            rocket0 = Rockets.Dequeue();
                            rocket0.SetVelocityFromInput(new KeySample());
                            rocket0.visual.visible = true;
                        }
                        else
                        {
                            rocket0 = new PhysicalRocket(textures_rocket, Context, Explosion1: Explosion1);
                            rocket0.body.SetActive(false);

                            #region z fixup
                            rocket0.visual.parent.setChildIndex(
                                rocket0.visual,

                                this.visual.visualnowings.parent.getChildIndex(
                                    this.visual.visualnowings
                                )
                            );
                            #endregion



                        }

                    }
                }
            };
            #endregion

        }
    }
}
