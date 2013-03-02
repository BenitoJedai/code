using FlashHeatZeeker.Core.Library;
using FlashHeatZeeker.CorePhysics.Library;
using FlashHeatZeeker.UnitTank.Library;
using FlashHeatZeeker.UnitTankControl.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlashHeatZeeker.UnitTankSync.Library
{
    class StarlingGameSpriteWithTankSync : StarlingGameSpriteWithPhysics
    {


        public static Action<string> __raise_sync = delegate { };
        public static Action<string> __at_sync = delegate { };



        public static SetVelocityFromInputAction __raise_SetVelocityFromInput = delegate { };
        public static SetVelocityFromInputAction __at_SetVelocityFromInput = delegate { };



        public List<RemoteGame> others = new List<RemoteGame>();

        public StarlingGameSpriteWithTankSync()
        {
            var textures_tank = new StarlingGameSpriteWithTankTextures(new_tex_crop);


            this.onbeforefirstframe += (stage, s) =>
            {

                #region __keyDown
                var __keyDown = new KeySample();

                stage.keyDown +=
                   e =>
                   {
                       // http://circlecube.com/2008/08/actionscript-key-listener-tutorial/
                       if (e.altKey)
                           __keyDown[System.Windows.Forms.Keys.Alt] = true;

                       __keyDown[(System.Windows.Forms.Keys)e.keyCode] = true;
                   };

                stage.keyUp +=
                 e =>
                 {
                     if (!e.altKey)
                         __keyDown[System.Windows.Forms.Keys.Alt] = false;

                     __keyDown[(System.Windows.Forms.Keys)e.keyCode] = false;
                 };

                #endregion

                #region ego
                var ego = new PhysicalTank(textures_tank, this)
                {
                    Identity = sessionid + ":ego"
                };

                ego.SetPositionAndAngle(
                   random.NextDouble() * -20 - 4,
                   random.NextDouble() * -20 - 4,
                   random.NextDouble() * Math.PI
               );

                current = ego;
                #endregion







                #region other
                Func<string, RemoteGame> other = __egoid =>
                {
                    // that other game has sent us a sync frame!

                    var already_known_other = others.FirstOrDefault(k => k.__egoid == __egoid);

                    if (already_known_other == null)
                    {
                        already_known_other = new RemoteGame
                        {
                            __egoid = __egoid,

                            // this
                            __syncframeid = this.syncframeid
                        };

                        others.Add(already_known_other);
                    }


                    return already_known_other;
                };
                #endregion


                #region __at_sync
                __at_sync += __egoid =>
                {
                    // that other game has sent us a sync frame!

                    var o = other(__egoid);

                    o.__syncframeid++;
                    // move on!
                };
                #endregion

                #region __at_SetVelocityFromInput
                __at_SetVelocityFromInput +=
                    (
                        string __egoid,
                        string __identity,
                        string __KeySample,
                        string __fixup_x,
                        string __fixup_y,
                        string __fixup_angle

                        ) =>
                    {
                        var o = other(__egoid);



                        if (o.ego == null)
                        {
                            o.ego = new PhysicalTank(textures_tank, this)
                            {
                                Identity = __identity,
                                RemoteGameReference = o
                            };

                            o.ego.SetPositionAndAngle(
                                double.Parse(__fixup_x),
                                double.Parse(__fixup_y),
                                double.Parse(__fixup_angle)
                            );
                        }


                        // set the input!


                        o.ego.SetVelocityFromInput(
                            new KeySample
                            {
                                value = int.Parse(__KeySample),

                                fixup = true,

                                x = double.Parse(__fixup_x),
                                y = double.Parse(__fixup_y),
                                angle = double.Parse(__fixup_angle)

                            }
                        );

                    };
                #endregion



                onsyncframe += delegate
                {

                    current.SetVelocityFromInput(__keyDown);




                    __raise_SetVelocityFromInput(
                         "" + sessionid,
                         current.Identity,
                         "" + current.CurrentInput.value,
                         "" + current.body.GetPosition().x,
                         "" + current.body.GetPosition().y,
                         "" + current.body.GetAngle()

                     );


                    // tell others this sync frame ended for us
                    __raise_sync("" + sessionid);
                };
            };
        }
    }
}
