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

            // events after init
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
        }



    }
}
