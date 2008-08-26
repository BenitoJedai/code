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

namespace FlashTreasureHunt.ActionScript
{
	partial class Client
	{
		private void PlayerAdvertise()
		{
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

		[Script]
		public class CoPlayer
		{
			public SharedClass1.RemoteEvents.UserPlayerAdvertiseArguments Identity;

			public SpriteInfoExtended Guard;
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

				c = new CoPlayer { Identity = e, Guard = this.Map.CreateGuard().AddTo(this.Map.EgoView.BlockingSprites) }.AddTo(CoPlayers);

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
			}
			#endregion

			

			var MemoryStream_UInt8 = e.vector.Select(i => (byte)i).ToArray();
			var ms = new MemoryStream(MemoryStream_UInt8);
			var mr = new BinaryReader(ms);

			c.Guard.Health = mr.ReadDouble();
			c.Guard.Direction = mr.ReadDouble();
			c.Guard.Position = new Point(mr.ReadDouble(), mr.ReadDouble());

			this.Map.WriteLine("present: " + e.name);

		}

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
					},
				Guard = this.Map.CreateGuard()
			}.AddTo(CoPlayers);


			
			c.Guard.RemoveFrom(Map.EgoView.Sprites);

			// we will never render ourself

			c.Guard.TakeDamageDone +=
				damage =>
				{
					this.Map.FlashColors(0xffff0000);

					if (this.LocalCoPlayer.Guard.Health <= 0)
					{
						// we die!

						this.Map.filters = new[] { Filters.RedChannelFilter };
						this.Map.MovementEnabled_IsAlive = false;
						this.Map.HudContainer.FadeOut();

						// should we respawn soon?
					}
				};


			LocalCoPlayer = c;

		}
	}
}
