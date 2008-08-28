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


					//Map.WriteLine("Howdy, " + e.name);




					//// now we know our player id.
					//this.MapSharedState.RemoteObjects[e.user] = this.MapSharedState.LocalObjects;


					// how long should we wait for the map?
					FirstMapLoader.Wait(TimeoutAction.LongOperation);

					if (e.user_with_map == -1)
					{
						Map.WriteLine("got ServerPlayerHello");
						FirstMapLoader.Signal(
							delegate
							{
								Map.WriteLine("using generated map");
							}
						);
					}


					// this causes other events to be attached
					MyIdentity.Value = e;
				};

			MyIdentity.ValueChanged +=
				() => MapInitialized.ContinueWhenDone(CreateLocalCoPlayer);

			MyIdentity.ValueChanged +=
				() => MapInitialized.ContinueWhenDone(InitializeWalkTo);


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
							//Map.WriteLine("joined: " + e.name);

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
					//Map.WriteLine("left: " + e.name);

					// kill the player guard, and remove the coplayer entity
					this.CoPlayers.Where(k => k.Identity.user == e.user).Where(k => k.Guard != null).ToArray().ForEach(k => k.RemoveFrom(CoPlayers).Guard.TakeDamage(k.Guard.Health));

				};

			Events.ServerSendMap +=
				e =>
				{
				

					// we have been chosen to tell the new guy about current map
					MapInitializedAndLoaded.ContinueWhenDone(
						delegate
						{
							

							if (FirstMapLoader.Ready)
								WriteSync();
							else
							{
								this.Map.WriteLine("we are not ready to send out a map - between levels");

							}

							
						}
					);
				};

			Events.UserSendMap +=
				e =>
				{
					// stop showing score and keep map in sync instead by loading a new map
					if (this.Map.EnterEndLevelMode_ReadyToContinue != null)
					{
						this.Map.EnterEndLevelMode_ReadyToContinue();
						this.Map.WriteLine("did we speed up the end game menu?");
					}
					else
					{
						this.Map.WriteLine("looks like we cannot speed up the end game menu?");
					}

					var bytestream = e.bytestream;

					this.Map.WriteLine("got UserSendMap");

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

			Events.UserAddDamageToCoPlayer +=
				e =>
				{
					this.DisableAddDamageToCoPlayer = true;
					this.CoPlayers.Where(k => k.Identity.user == e.target).Where(k => k.Guard != null).ForEach(k => k.Guard.TakeDamage(e.damage));
					this.DisableAddDamageToCoPlayer = false;
				};

			Events.UserFireWeapon +=
				e =>
				{
					Assets.Default.Sounds.gunshot.play();

					this.CoPlayers.Where(k => k.Identity.user == e.user).ForEach(k => k.Guard.PlayShootingAnimation());
				};

			Events.UserWalkTo += UserWalkTo;
			Events.UserLookAt += UserLookAt;

			Events.UserGuardWalkTo += UserGuardWalkTo;
			Events.UserGuardLookAt += UserGuardLookAt;
			Events.UserGuardAddDamage +=
				e =>
				{
					this.Map.WriteLine("got UserGuardAddDamage " + e);

					this.DisableGuardAddDamage = true;
					this.Map.GuardSprites.Where(k => k.ConstructorIndexForSync == e.index).ForEach(k => k.TakeDamage(e.damage));
					this.DisableGuardAddDamage = false;
				};

			Events.UserEnterEndLevelMode +=
				e =>
				{
					var sender = this.CoPlayers.SingleOrDefault(k => k.Identity.user == e.user);

					if (sender == null)
					{
						Map.WriteLine("must wait until next level is generated");

						if (!MapInitializedAndLoaded.Ready)
							if (!FirstMapLoader.Ready)
								FirstMapLoader.Wait(TimeoutAction.LongOperation);

						return;
					}

					Map.WriteLine("got UserEnterEndLevelMode from " + sender.Identity.name);

					if (this.AbortGhostMode != null)
						this.AbortGhostMode();

					DisableEnterEndLevelMode = true;
					this.Map.EnterEndLevelMode();
					DisableEnterEndLevelMode = false;
				};
		}

	



		bool DisableEnterEndLevelMode;





	}
}
