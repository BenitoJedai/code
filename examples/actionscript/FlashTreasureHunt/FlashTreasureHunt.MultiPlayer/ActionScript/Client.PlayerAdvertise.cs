﻿using System;
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

			Timer WalkTo_Timer;
			Timer WalkTo_Smooth;

			Point WalkTo_Target = new Point();

			public double WalkToDistance
			{
				get
				{
					return WalkTo_Target.GetDistance(this.Guard.Position);
				}
			}

			public CoPlayer WalkTo(double x, double y)
			{
				WalkTo_Target = new Point(x, y);

				// should do a smooth movement now

				const double Step = 1.0 / 60.0;

				if (WalkTo_Smooth == null)
				{
					if (WalkToStart != null)
					{
						WalkToStart();
					}

					if (WalkTo_Timer != null)
					{
						// reset the timer, so that the counting begins from now
						WalkTo_Timer.stop();
					}
					else
					{
						Guard.StartWalkingAnimation();
					}

					WalkTo_Smooth = (1000 / 24).AtInterval(
						t =>
						{
							var z = WalkToDistance;

							var IsCloseEnough = z < Step;
							var IsInNeedForTeleport = z > 1.0;

							if (IsCloseEnough || IsInNeedForTeleport)
							{
								this.Guard.Position = WalkTo_Target;
								t.stop();
								WalkTo_Smooth = null;
								WalkTo_Timer = 500.AtDelayDo(
									delegate
									{
										WalkTo_Timer = null;

										Guard.StopWalkingAnimation();
									}
								);

								if (IsCloseEnough)
								{
									if (WalkToDone != null)
										WalkToDone();
								}

								if (IsInNeedForTeleport)
								{
									if (WalkToTeleported != null)
										WalkToTeleported();
								}

								return;
							}

							var arc = (WalkTo_Target - this.Guard.Position).GetRotation();
							var speed = Step;

							if (z > 0.2)
								speed *= 2;

							if (z > 0.4)
								speed *= 2;

							if (z > 0.6)
								speed *= 2;

							if (z > 0.8)
								speed *= 2;

							this.Guard.Position = this.Guard.Position.MoveToArc(arc, speed);

						}
					);
				}

				return this;
			}

			public event Action WalkToTeleported;
			public event Action WalkToDone;
			public event Action WalkToStart;
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
			c.Guard.RemoveFrom(Map.EgoView.BlockingSprites);

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
