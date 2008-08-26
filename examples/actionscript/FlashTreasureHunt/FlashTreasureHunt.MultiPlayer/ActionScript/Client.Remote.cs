using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FlashTreasureHunt.ActionScript.Properties;
using FlashTreasureHunt.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.Shared.Lambda;

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
			// todo: events need to automatically 
			// queue based on message id and wait timer
			// currently this is done manually.

			Events.ServerPlayerJoined +=
				e =>
				{
					MapInitialized.ContinueWhenDone(
						delegate
						{
							Map.WriteLine("joined: " + e.name);

							PlayerAdvertise();
						}
					);
				};

			Events.UserPlayerAdvertise +=
				e =>
				{
					MapInitialized.ContinueWhenDone(
						delegate
						{
							PlayerAdvertise(e);
						}
					);
				};


			Events.ServerPlayerLeft +=
				e =>
				{
					Map.WriteLine("left: " + e.name);

					// kill the player guard, and remove the coplayer entity
					this.CoPlayers.Where(k => k.Identity.user == e.user).ToArray().ForEach(k => k.RemoveFrom(CoPlayers).Guard.TakeDamage(k.Guard.Health));

				};

			Events.ServerSendMap +=
				e =>
				{
					// we have been chosen to tell the new guy about current map
					MapInitialized.ContinueWhenDone(
						delegate
						{
							WriteSync();
						}
					);
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

			// note: you should not listen to non user, non server events

			Events.UserTakeAmmo +=
				e =>
				{
					this.Map.AmmoSprites.Where(k => k.ConstructorIndexForSync == e.index).ToArray().ForEach(
						i => i.RemoveFrom(this.Map.AmmoSprites).RemoveFrom(this.Map.EgoView.Sprites)
					);
				};

			Events.UserTakeGold +=
				e =>
				{
					this.Map.GoldSprites.Where(k => k.ConstructorIndexForSync == e.index).ToArray().ForEach(
						i => i.RemoveFrom(this.Map.GoldSprites).RemoveFrom(this.Map.EgoView.Sprites)
					);
				};
		}

	


	}
}
