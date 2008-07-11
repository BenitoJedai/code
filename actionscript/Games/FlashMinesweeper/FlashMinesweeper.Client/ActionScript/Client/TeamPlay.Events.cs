using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using FlashMinesweeper.ActionScript;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.flash.filters;
using FlashMinesweeper.ActionScript.Client.Assets;
using ScriptCoreLib.ActionScript.Nonoba.api;
using FlashMinesweeper.ActionScript.Shared;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashMinesweeper.ActionScript.Client
{

    partial class TeamPlay
    {

        private void InitializeEvents()
        {

            var MyIdentity = default(SharedClass1.RemoteEvents.ServerPlayerHelloArguments);

            // events after init
            Events.ServerPlayerHello +=
                e =>
                {
                    MyIdentity = e;

                    ShowMessage("Howdy, " + e.name);
                };

            var CoPlayerNames = new Dictionary<int, string>();

            Events.ServerPlayerJoined +=
              e =>
              {
                  CoPlayerNames[e.user] = e.name;

                  ShowMessage("Player joined - " + e.name);


                  Messages.PlayerAdvertise(MyIdentity.name);
              };

            var Cursors = new Dictionary<int, ShapeWithMovement>();


            Events.ServerPlayerLeft +=
              e =>
              {
                  if (CoPlayerNames.ContainsKey(e.user))
                  {
                      CoPlayerNames.Remove(e.user);
                  }

                  if (Cursors.ContainsKey(e.user))
                  {
                      Cursors[e.user].Orphanize();
                      Cursors.Remove(e.user);
                  }

                  ShowMessage("Player left - " + e.name);
              };

            Events.UserPlayerAdvertise +=
                e =>
                {
                    if (CoPlayerNames.ContainsKey(e.user))
                        return;

                    CoPlayerNames[e.user] = e.name;

                    ShowMessage("Player already here - " + e.name);
                };



            Events.UserMouseMove +=
                e =>
                {
                    var s = default(ShapeWithMovement);

                    if (Cursors.ContainsKey(e.user))
                        s = Cursors[e.user];
                    else
                    {
                        s = new ShapeWithMovement
                            {
                                filters = new[] { new DropShadowFilter() },
                                alpha = 0.5
                            };


                        var g = s.graphics;

                        g.beginFill((uint)e.color);
                        g.moveTo(0, 0);
                        g.lineTo(14, 14);
                        g.lineTo(0, 20);
                        g.lineTo(0, 0);
                        g.endFill();

                        Cursors[e.user] = s;
                    };

                    s.AttachTo(this).MoveTo(e.x, e.y);
                };

            Events.UserMouseOut +=
                e =>
                {
                    if (Cursors.ContainsKey(e.color))
                    {
                        Cursors[e.color].Orphanize();
                    }
                };

            Events.ServerSendMap +=
                e =>
                {
                    SendMap();
                };

            Events.UserSendMap +=
                e =>
                {
                    StopCrudeMapReset();

                    for (int i = 0; i < Field.Buttons.Length; i++)
                    {
                        var v = Field.Buttons[i];

                        var x = new BitField { Value = e.buttons[i] };


                        v.IsMined = x[1];
                        v.IsFlag = x[2];
                        v.Enabled = x[3];

                    }

                    foreach (var v in Field.Buttons)
                    {
                        if (v.Enabled)
                            v.Update();
                        else
                            v.RevealLocal();
                    }
                };

            Events.UserSetFlag +=
                e =>
                {
                    StopCrudeMapReset();

                    Field.Buttons[e.button].IsFlag = e.value == 1;
                    Field.Buttons[e.button].Update();
                    Field.Buttons[e.button].snd_flag.play();
                    Field.Buttons[e.button].CheckComplete();
                };

            Events.UserReveal +=
                e =>
                {
                    StopCrudeMapReset();

                    Field.Buttons[e.button].RevealOrExplode(false,
                        delegate
                        {
                            ShowMessage(CoPlayerNames[e.user] + " found a mine");
                        }
                    );

                    Field.Buttons[e.button].CheckComplete();
                };
        }

        private void StopCrudeMapReset()
        {
            // we got the map
            if (CrudeMapReset != null)
            {
                CrudeMapReset.stop();
                CrudeMapReset = null;
            }
        }


    }

}
