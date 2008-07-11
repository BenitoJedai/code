using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.flash.display;
using LightsOut.ActionScript.Shared;
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
        }

        private void SyncMap(int[] p)
        {
            var data = p.Select(i => new BitField { Value = i }).ToArray();

            for (int x = 0; x < Map.Values.XLength; x++)
                for (int y = 0; y < Map.Values.YLength; y++)
                {
                    Map.Values[x, y].Value = data[x][y];
                }
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

            Messages.SendMap(data.Select(i => i.Value).ToArray());
        }



    }
}
