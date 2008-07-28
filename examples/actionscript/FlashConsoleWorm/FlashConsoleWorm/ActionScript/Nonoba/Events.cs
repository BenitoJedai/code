using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib;
using FlashConsoleWorm.Shared;
using FlashConsoleWorm.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.display;

namespace FlashConsoleWorm.ActionScript.Nonoba
{
	partial class Client
	{
		readonly Dictionary<int, string> CoPlayerNames = new Dictionary<int, string>();
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



			
		}
	}
}
