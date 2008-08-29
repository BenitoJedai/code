using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using FlashTreasureHunt.Shared;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		public FlashTreasureHunt Map;



		public readonly TimeoutAction FirstMapLoader = new TimeoutAction();

		public readonly OrderedAction MapInitialized = new OrderedAction();
		public readonly OrderedAction MapInitializedAndLoaded = new OrderedAction();

	

		public FlashTreasureHunt.ScoreTag[] GetScoreValues()
		{
			var a = CoPlayers.Select(k => new FlashTreasureHunt.ScoreTag
							{
								Kills = k.Kills,
								Name = k.Identity.name,
								Score = k.Score
							}).ToArray();

			Map.WriteLine("scoretable length: " + a.Length);

			return a;
		}

		public void InitializeMapOnce()
		{
			// this should be a ctor instead?

			this.Map = new FlashTreasureHunt();
			this.Map.ReadyWithLoadingCurrentLevel = this.ReadyWithLoadingCurrentLevel;
			this.Map.ReadyForNextLevel = this.ReadyForNextLevel;
			this.Map.VirtualGetScoreValues = GetScoreValues;



			#region FirstMapLoader
			this.FirstMapLoader.SignalMissed +=
				delegate
				{
					this.Map.WriteLine("FirstMapLoader.SignalMissed - Noone sent us a map");
				};

			this.FirstMapLoader.SignalNotExpected +=
				delegate
				{
					this.Map.WriteLine("FirstMapLoader.SignalNotExpected - Already got it or it is too late");
				};

			this.FirstMapLoader.SignalWaisted +=
				delegate
				{
					this.Map.WriteLine("FirstMapLoader.SignalWaisted - We were really slow loading that map...");
				};

			this.FirstMapLoader.SignalWasExpected +=
				delegate
				{
					this.Map.WriteLine("FirstMapLoader.SignalWasExpected - we got map in time");
				};
			#endregion




			// pass sync events from singleplayer to server

			this.Map.Sync_TakeGold +=
				index =>
				{
					this.LocalCoPlayer.Score += FlashTreasureHunt.ScoreForGold;
					this.Messages.TakeGold(index);
				};

			this.Map.Sync_GuardKilled +=
				DamageOwner =>
				{
					this.CoPlayers.Where(k => k.WeaponIdentity == DamageOwner).ForEach(k => k.Kills++);

				};

			this.Map.Sync_TakeAmmo += this.Messages.TakeAmmo;
			this.Map.Sync_FireWeapon += this.Messages.FireWeapon;

			this.Map.Sync_GuardLookAt += this.Messages.GuardLookAt;
			this.Map.Sync_GuardWalkTo += GuardWalkTo;
			this.Map.Sync_GuardAddDamage +=
				(index, damage, DamageOwner) =>
				{
					if (DamageOwner != Map.EgoWeaponIdentity)
						return;

					if (DisableGuardAddDamage)
						return;

					//this.Map.WriteLine("sent GuardAddDamage " + new { index, damage });

					this.Messages.GuardAddDamage(index, damage);
				};


			this.Map.Sync_PortalUsed +=
				delegate
				{
					this.LocalCoPlayer.Teleports++;
				};

			var LevelFPS = 0;

			this.MapInitializedAndLoaded.ContinueWhenDone(
				delegate
				{
					this.Map.EgoView.FramesPerSecondChanged +=
						delegate
						{
							LevelFPS = (LevelFPS + this.Map.EgoView.FramesPerSecond) / 2;
						};

					this.Map.AddAmmoToEgoAndSwitchWeapon();
				}
			);
		
			this.Map.Sync_EnterEndLevelMode +=
				delegate
				{
					this.CoPlayers.ForEach(k => this.Map.EgoView.Sprites.Remove(k.Guard));

					FirstMapLoader.Wait(TimeoutAction.LongOperation);

					var LScoreForLevelEnding = 0;

					if (!DisableEnterEndLevelMode)
					{
						LScoreForLevelEnding = 1;

						this.LocalCoPlayer.Score += FlashTreasureHunt.ScoreForEndLevel;

						this.Map.WriteLine("send EnterEndLevelMode");

						this.Messages.EnterEndLevelMode();

						UseOurMapForNextLevel();

					}


					this.Messages.ReportScore(LScoreForLevelEnding,
						this.LocalCoPlayer.Score ,
						this.LocalCoPlayer.Kills ,
						this.LocalCoPlayer.Teleports,
						
						LevelFPS
					);


				};

			this.Map.Sync_ExitEndLevelMode +=
				delegate
				{
					LevelFPS = 0;

					this.CoPlayers.ForEach(
						k =>
						{
							k.Score = 0;
							k.Kills = 0;
							k.Teleports = 0;
						});
				};
			this.Map.AttachTo(Element);


		}

		private void UseOurMapForNextLevel()
		{
			FirstMapLoader.Signal(
							delegate
							{
								this.Map.WriteLine("sync copy that level to others");

								WriteSync();

								RestoreCoPlayers();
							}
						);
		}




	}
}
