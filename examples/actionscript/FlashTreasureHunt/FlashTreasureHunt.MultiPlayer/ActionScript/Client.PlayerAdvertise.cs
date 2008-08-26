using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FlashTreasureHunt.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		private void PlayerAdvertise()
		{
			// we are in ghost mode...
			if (this.LocalCoPlayer.Guard == null)
				return;

			// we are starting on a new position, advertise that
		

			IVector EgoVector = this.Map.EgoView;

			var ms = new MemoryStream();
			var mw = new BinaryWriter(ms);

			mw.Write(this.LocalCoPlayer.Guard.Health);

			mw.Write(EgoVector.Direction);
			mw.Write(EgoVector.Position.x);
			mw.Write(EgoVector.Position.y);

			// we will waste 3 bytes - 0xffffff00 cuz memorystream isn't supported
			var MemoryStream_Int32 = ms.ToArray().Select(i => (int)i).ToArray();

			Map.WriteLine("sent PlayerAdvertise " + MemoryStream_Int32.Length);

			this.Messages.PlayerAdvertise(MyIdentity.Value.name, MemoryStream_Int32);
		}

		public readonly List<CoPlayer> CoPlayers = new List<CoPlayer>();

		public bool DisableAddDamageToCoPlayer;

		private void PlayerAdvertise(SharedClass1.RemoteEvents.UserPlayerAdvertiseArguments e)
		{
			CoPlayer c = CoPlayers.SingleOrDefault(k => k.Identity.user == e.user);

			#region does that coplayer already exist?
			if (c == null)
			{
				// the player has just joined

				c = new CoPlayer { Identity = e }.AddTo(CoPlayers);



				//c.WalkToTeleported +=
				//    delegate
				//    {
				//        this.Map.WriteLine(c.Identity.name + " has teleported");
				//    };

				//c.WalkToDone +=
				//    delegate
				//    {
				//        this.Map.WriteLine(c.Identity.name + " has stopped");
				//    };

				//c.WalkToStart +=
				//    delegate
				//    {
				//        this.Map.WriteLine(c.Identity.name + " started walking");
				//    };
			}
			#endregion

			if (c.Guard == null)
			{
				Map.WriteLine("coplayer " + e.name + " spawned as a new guard");


				c.Guard = this.Map.CreateGuard().AddTo(this.Map.EgoView.BlockingSprites);
				c.Guard.MinimapZIndex = SpriteInfoExtended.MinimapZIndex_OnTop;
				c.Guard.MinimapColor = 0xff0000ff;

				c.Guard.TakeDamage +=
					damage =>
					{
						// we should not mirror remoted damage
						if (DisableAddDamageToCoPlayer)
							return;

						// we have been shot
						Messages.AddDamageToCoPlayer(c.Identity.user, damage);
					};

				c.Guard.TakeDamageDone +=
					damage =>
					{
						if (c.Guard.Health <= 0)
						{
							c.Guard.MinimapColor = 0xff00007f;
							c.Guard = null;

							Map.WriteLine("coplayer " + e.name + " died and left a corpse");

						}
					};

				this.Map.EgoView.UpdatePOV(true);
			}

			var MemoryStream_UInt8 = e.vector.Select(i => (byte)i).ToArray();
			var ms = new MemoryStream(MemoryStream_UInt8);
			var mr = new BinaryReader(ms);

			c.Guard.Health = mr.ReadDouble();
			c.Guard.Direction = mr.ReadDouble();
			c.Guard.Position = new Point(mr.ReadDouble(), mr.ReadDouble());

			this.Map.WriteLine("present: " + e.name);

		}

		#region LocalCoPlayer
		public CoPlayer LocalCoPlayer;

		private void CreateLocalCoPlayer()
		{
			var c = new CoPlayer
			{
				Identity =
					new SharedClass1.RemoteEvents.UserPlayerAdvertiseArguments
					{
						name = MyIdentity.Value.name,
						user = MyIdentity.Value.user
					}
			}.AddTo(CoPlayers);


			LocalCoPlayer = c;

			CreateLocalCoPlayerGuard();

			this.Map.Sync_Suicide +=
				delegate
				{
					if (this.LocalCoPlayer.Guard != null)
						this.LocalCoPlayer.Guard.TakeDamage(this.LocalCoPlayer.Guard.Health);
				};
		}

		private void CreateLocalCoPlayerGuard()
		{
			LocalCoPlayer.Guard = this.Map.CreateGuard();
			LocalCoPlayer.Guard.RemoveFrom(Map.EgoView.Sprites);
			LocalCoPlayer.Guard.RemoveFrom(Map.EgoView.BlockingSprites);

			LocalCoPlayer.Guard.TakeDamage +=
				damage =>
				{
					if (this.DisableAddDamageToCoPlayer)
						return;

					Map.WriteLine("ego has been damaged... was it a suicide?");

					this.Messages.AddDamageToCoPlayer(this.LocalCoPlayer.Identity.user, damage);
				};

			LocalCoPlayer.Guard.TakeDamageDone +=
				damage =>
				{
					this.Map.FlashColors(0xffff0000);

					if (this.LocalCoPlayer.Guard.Health <= 0)
					{
						EnterGhostMode();

					}
				};
		}
		#endregion

		private void EnterGhostMode()
		{
			// we die!
			if (this.Map.music != null)
				this.Map.music.stop();

			var music = Assets.Default.Music.funkyou.play(0, 9999);

			if (music == null)
				Map.WriteLine("music not playing...");

			this.Map.filters = new[] { Filters.RedChannelFilter };

			this.Map.EnableItemPickup = false;
			this.Map.EgoView.EnableBlockingSprites = false;
			this.Map.WeaponAmmo = 0;
			this.Map.HudContainer.FadeOut(
				delegate
				{
					this.Map.SwitchToHand();

				}
			);

			LocalCoPlayer.Guard.Position = this.Map.EgoView.ViewPosition;
			LocalCoPlayer.Guard.AddTo(this.Map.EgoView.Sprites);
			LocalCoPlayer.Guard = null;

			Map.WriteLine("ego died and left a corpse");

			var GhostWeapon = default(Action);

			GhostWeapon =
				delegate
				{
					if (music != null)
						music.stop();


					this.Map.FireWeapon -= GhostWeapon;

					GhostWeapon = null;

					this.Map.FlashColors(0xffffffff);
					this.Map.WriteLine("respawn now!");

					this.Map.music = Assets.Default.Music.music.play(0, 9999);

					this.Map.filters = null;

					this.Map.EnableItemPickup = true;
					this.Map.EgoView.EnableBlockingSprites = true;
					this.Map.HudContainer.FadeIn();

					// we now have a new body, but we have not announced it yet
					CreateLocalCoPlayerGuard();

					PlayerAdvertise();
				};

			3000.AtDelayDo(
				delegate
				{
					this.Map.FireWeapon += GhostWeapon;

					this.Map.FlashColors(0xffffffff);


				}
			);



		}
	}
}
