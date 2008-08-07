using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.Shared;
using ScriptCoreLib.ActionScript.Nonoba.api;

namespace FlashSpaceInvaders.ActionScript.MultiPlayer
{
	partial class Client 
	{
		SharedClass1.RemoteEvents.ServerPlayerHelloArguments MyIdentity;

		public override void InitializeEvents()
		{
			Events.ServerPlayerHello +=
				e =>
				{
					MyIdentity = e;

					//ShowMessage("Howdy, " + e.name);


					// local only
					Map.SendTextMessage.Direct("Howdy, " + e.name);
				};

		}
	}
}
