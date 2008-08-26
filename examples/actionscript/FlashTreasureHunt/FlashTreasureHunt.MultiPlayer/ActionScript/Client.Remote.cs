using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using FlashTreasureHunt.Shared;
using FlashTreasureHunt.ActionScript.Properties;
using System.IO;
using ScriptCoreLib.ActionScript.RayCaster;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		readonly Property<SharedClass1.RemoteEvents.ServerPlayerHelloArguments> MyIdentity = new Property<SharedClass1.RemoteEvents.ServerPlayerHelloArguments>();



		public void InitializeEventsOnce()
		{
			Events.ServerPlayerHello +=
				e =>
				{
					new Handshake().VerifyArray(e.handshake);

					// Map.WriteLine("handshake ok");

					// at this point we know it is our server
					// at the other end
					// atleast it seems to knows our protocol


					Map.WriteLine("Howdy, " + e.name);




					//// now we know our player id.
					//this.MapSharedState.RemoteObjects[e.user] = this.MapSharedState.LocalObjects;

					
					// how long should we wait for the map?
					FirstMapLoader.Wait(5000);

					if (e.user_with_map == -1)
						FirstMapLoader.Signal(
							delegate
							{
								Map.WriteLine("using generated map");
							}
						);

					// this causes other events to be attached
					MyIdentity.Value = e;
				};

			MyIdentity.ValueChanged += InitializeOtherEvents;
		}

		public void InitializeOtherEvents()
		{
			Events.ServerPlayerJoined +=
				e =>
				{
					Map.WriteLine("joined: " + e.name);

					Messages.PlayerAdvertise(MyIdentity.Value.name);
				};

			Events.UserPlayerAdvertise +=
				e =>
				{
					Map.WriteLine("present: " + e.name);

				};

			Events.ServerPlayerLeft +=
				e =>
				{
					Map.WriteLine("left: " + e.name);

				};

			Events.ServerSendMap +=
				e =>
				{
					// we have been chosen to tell the new guy about current map

					WriteSync();
				};

			Events.UserSendMap +=
				e =>
				{
					var bytestream = e.bytestream;

					FirstMapLoader.Signal(
						delegate
						{
							ReadSync(bytestream);

						}
					);
				};

		}


	}
}
