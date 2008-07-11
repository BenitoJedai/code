using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LightsOut.ActionScript.Client.Assets;
using LightsOut.ActionScript.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.Nonoba.api;

namespace LightsOut.ActionScript.Client
{
    partial class TeamPlay
    {

        private void InitializeEvents()
        {
            var MyIdentity = default(SharedClass1.RemoteEvents.ServerPlayerHelloArguments);

            Events.ServerPlayerHello +=
                e =>
                {
                    MyIdentity = e;

                    ShowMessage("Howdy, " + e.name);
                };

            Events.ServerPlayerJoined +=
              e =>
              {
                  ShowMessage("Player joined - " + e.name);


                  Messages.PlayerAdvertise(e.name);
              };

            Events.ServerPlayerLeft +=
              e =>
              {
                  ShowMessage("Player left - " + e.name);
              };

            Events.UserPlayerAdvertise +=
                e =>
                {
                    ShowMessage("Player already here - " + e.name);
                };

            Events.ServerSendMap +=
                e =>
                {
                    SendMap();
                };

            Events.UserSendMap +=
                e =>
                {
                    SyncMap(e.data);
                };

            Events.UserClick +=
                e =>
                {
                    DecreaseClickCountdown(false);

                    Map.UserClicksWithSound[e.x, e.y]();
                    Map.CheckForCompleteness(false);
                };

            var Cursors = new Dictionary<int, ShapeWithMovement>();


            Events.UserMouseMove +=
                e =>
                {
                    var s = default(ShapeWithMovement);

                    if (Cursors.ContainsKey(e.color))
                        s = Cursors[e.color];
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

                        Cursors[e.color] = s;
                    };

                    s.AttachTo(this).MoveTo(e.x, e.y);
                };

        }

        private void SyncMap(int[] p)
        {
            var data = p.Select(i => new BitField { Value = i }).ToArray();

            for (int x = 0; x < Map.Values.XLength; x++)
                for (int y = 0; y < Map.Values.YLength; y++)
                {
                    Map.Values[x, y].Value = data[x + 1][y];
                }

            Map.Level = data[0].Value;
            Map.mouseChildren = true;

            ResetClickCountdown(Map.Level);
        }

        public int ClickCountdown = 0;

        public void DecreaseClickCountdown(bool IsLocalPlayer)
        {
            ClickCountdown--;

            if (ClickCountdown <= 0)
            {
                AddScore(-2);
                Map.TakeNextMap(IsLocalPlayer);
            }
            else
            {
                if (ClickCountdown == 1)
                    ShowMessage(ClickCountdown + " click left");
                if (ClickCountdown % 3 == 0)
                    ShowMessage(ClickCountdown + " clicks left");
            }
        }

        public void ResetClickCountdown(int level)
        {
            ClickCountdown = level * 2;

            ShowMessage("You can make a total of " + ClickCountdown + " clicks");
        }

        private void SendMap()
        {
            var m = Map.Values;

            var data = new BitField[m.XLength];

            for (int x = 0; x < m.XLength; x++)
            {
                var f = new BitField();
                data[x] = f;

                for (int y = 0; y < m.YLength; y++)
                {
                    var v = m[x, y];

                    if (v == null)
                        throw new Exception("map item missing: " + new { x, y, m.Length, m.XLength, m.YLength });

                    f[y] = v.Value;
                }
            }

            var PreData = new[] { Map.Level };

            Messages.SendMap(PreData.Concat(data.Select(i => i.Value)).ToArray());
        }



    }
}
