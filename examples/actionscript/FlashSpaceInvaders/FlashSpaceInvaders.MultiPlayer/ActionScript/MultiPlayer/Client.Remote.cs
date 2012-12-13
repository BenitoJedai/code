using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using FlashSpaceInvaders.Shared;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.geom;
using System.IO;

using FlashSpaceInvaders.ActionScript.Extensions;
using FlashSpaceInvaders.ActionScript.FragileEntities;

namespace FlashSpaceInvaders.ActionScript.MultiPlayer
{
	partial class Client
	{
		readonly Property<SharedClass1.RemoteEvents.ServerPlayerHelloArguments> MyIdentity = new Property<SharedClass1.RemoteEvents.ServerPlayerHelloArguments>();


		readonly Dictionary<int, SpriteWithMovement> Cursors = new Dictionary<int, SpriteWithMovement>();
		readonly Dictionary<int, PlayerShip> CoPlayers = new Dictionary<int, PlayerShip>();


		public void InitializeEvents()
		{
			#region ServerPlayerHello

			//Events.ServerPlayerHandshake +=
			//    e =>
			//    {
			//        if (e.version == null)
			//            throw new Exception("version is null");

			//        if (e.version.Length != 2)
			//            throw new Exception("version length mismatch");
			//    };

			Events.ServerPlayerHello +=
				e =>
				{
					MyIdentity.Value = e;

	

					// now we know our player id.
					this.MapSharedState.RemoteObjects[e.user] = this.MapSharedState.LocalObjects;


					// local only
					MapRoutedActions.SendTextMessage.Direct("Howdy, " + e.name + " " + e.user);
				};
			#endregion

			// we do not respond to events before server sends us our identity
			MyIdentity.ValueChanged +=
				delegate
				{
					#region MouseMove
					Events.UserMouseMove +=
							e =>
							{
								var s = default(SpriteWithMovement);

								if (Cursors.ContainsKey(e.user))
									s = Cursors[e.user];
								else
								{
									s = new SpriteWithMovement
									{
										filters = new[] { new DropShadowFilter() },
										alpha = 0.5
									};


									var g = s.graphics;

									g.beginFill((uint)e.color);
									g.moveTo(0, 0);
									g.lineTo(14, 14);
									g.lineTo(0, 20);
									g.lineTo(0, 0);
									g.endFill();

									Cursors[e.user] = s;
								};

								s.AttachTo(this.Element).TweenMoveTo(e.x, e.y);
							};


					Events.UserMouseOut +=
					   e =>
					   {
						   if (Cursors.ContainsKey(e.color))
						   {
							   Cursors[e.color].Orphanize();
						   }
					   };
					#endregion



					#region Create and Move Players

					Events.ServerPlayerJoined +=
					  e =>
					  {
						  //CreateRemoteEgo(e.user, e.name);

						  MapRoutedActions.CreateCoPlayer.Direct(e.user, r => CoPlayers[e.user] = r);

						  MapRoutedActions.SendTextMessage.Direct("Player joined - " + e.name);

						  Messages.PlayerAdvertise(MyIdentity.Value.name);
						  Messages.VectorChanged((int)Map.Ego.GoodEgo.MoveToTarget.Value.x, (int)Map.Ego.GoodEgo.MoveToTarget.Value.y);

						  //Messages.TeleportTo((int)Map.Ego.Location.x, (int)Map.Ego.Location.y);
						  //for (int i = 1; i < Map.Ego.Parts.Count; i++)
						  //{
						  //    // there is no apple, but we just need to gain in size
						  //    Messages.EatApple((int)Map.Ego.Location.x, (int)Map.Ego.Location.y);
						  //}
					  };

					Events.UserPlayerAdvertise +=
						e =>
						{
							// if we already know about the player then we dont care
							if (CoPlayers.ContainsKey(e.user))
								return;

							MapRoutedActions.CreateCoPlayer.Direct(e.user, r => CoPlayers[e.user] = r);

							MapRoutedActions.SendTextMessage.Direct("Player here - " + e.name);
						};

					Events.ServerPlayerLeft +=
						e =>
						{
							MapRoutedActions.RemoveCoPlayer.Direct(e.user);

						};



					Events.UserVectorChanged +=
						e =>
						{
							// do we know about this player?

							if (!CoPlayers.ContainsKey(e.user))
								return;

							MapRoutedActions.MoveCoPlayer.Direct(CoPlayers[e.user], new Point(e.x, e.y));
						};


					#endregion

					#region UserFireBullet
					Events.UserFireBullet +=
						e =>
						{
							// ding ding
							// which starship?

							var starship = this.MapSharedState[e.user, e.starship].Element as StarShip;

							if (starship == null)
								throw new Exception("MapSharedState does not have a starship at offset " + e.starship + " for user " + e.user);

							MapRoutedActions.FireBullet.Direct(
								starship,
								e.multiplier,
								new Point(e.from_x, e.from_y),
								new Point(e.to_x, e.to_y),
								e.limit,

								// this is a remote bullet 
								// for syncing it has no damage - a fake bullet
								// the impact damage should be sent later
								bullet =>
								{
									// make fake bullets blue
									bullet.Element.ApplyFilter(Filters.ColorFillFilter(0xff));
									bullet.Multiplier = 0;
								}
							);
						};
					#endregion

					#region UserRestoreStarship
					Events.UserRestoreStarship +=
						e =>
						{
							MapRoutedActions.SendTextMessage.Direct("got RestoreStarship " + e.user + " " + e.starship);


							var u = this.MapSharedState[e.user, e.starship];

							if (u.Parent != this.MapSharedState.RemoteObjects)
								throw new Exception("must be remoteobject offset " + e.starship + " for user " + e.user);

							var starship = u.Element as StarShip;

							if (starship == null)
								throw new Exception("MapSharedState does not have a starship at offset " + e.starship + " for user " + e.user);

							MapRoutedActions.RestoreStarship.Direct(starship);
						};
					#endregion

					Events.ServerSendMap +=
						e =>
						{

							OnServerSendMap();

						};

					Events.UserAddDamage +=
						e =>
						{
							// damage by remote bullet
							var target = MapSharedState[e.user, e.target].Element as IFragileEntity;
							var shooter = MapSharedState[e.user, e.shooter].Element as StarShip;

							if (target == null)
								throw new Exception("invalid target " + e.target);
							if (shooter == null)
								throw new Exception("invalid shooter " + e.shooter);


							this.MapRoutedActions.SendTextMessage.Direct("got damage for " + e.target + " " + e.damage + " by shooter " + e.shooter);

							this.MapRoutedActions.AddDamage.Direct(target, e.damage, shooter);
						};

					Events.UserKillAllInvaders +=
						e =>
						{
							this.MapRoutedActions.KillAllInvaders.Direct();

						};

					#region UserSendMap
					Events.UserSendMap +=
						e =>
						{
							// we got a new map, do we need it?

							// now add apples as the new map says
							var integers_as_bytes = e.buttons.Select(i => (byte)i).ToArray();
							var m = new MemoryStream(integers_as_bytes);

							//if (e.stream.Length == 0)
							//    throw new Exception("0 bytes in stream");

							//var m = new MemoryStream(e.stream);


							m.Position = 0;

							var mr = new BinaryReader(m);

							MapRoutedActions.SendTextMessage.Direct("got map " + m.Length + " bytes");


							var cloud = Map.cloud1.Members;

							if (cloud.Count != mr.ReadByte())
								throw new Exception("cloud count mismatch");

							// if we get it the second time why do the invaders 
							// leave the scene?
							// fixed: unsigned vs signed vector
							// we need timer tick interval too!!

							Map.cloud1.TickInterval.Value = mr.ReadInt16();

							Map.cloud1.NextMove.x = mr.ReadInt16();
							Map.cloud1.NextMove.y = mr.ReadInt16();

							MapRoutedActions.SendTextMessage.Direct("got next move: " + Map.cloud1.NextMove.x + " " + Map.cloud1.NextMove.y);

							Map.cloud1.Speed = mr.ReadDouble();

							MapRoutedActions.SendTextMessage.Direct("got cloud speed " + Map.cloud1.Speed);

							foreach (var a in cloud)
							{
								var a_x = mr.ReadInt16();
								var a_y = mr.ReadInt16();

					

								MapRoutedActions.SendTextMessage.Direct("invader: " + a_x + " " + a_y);

								a.Element.TeleportTo(a_x, a_y);
								a.Element.alpha = (double)mr.ReadByte() / 255.0;
							}

							var blocks = Map.DefenseBlocks;


							if (blocks.Count != mr.ReadByte())
								throw new Exception("blocks count mismatch");

							foreach (var a in blocks)
							{
								//a.x = m.ReadByte();
								//a.y = m.ReadByte();
								a.alpha = (double)mr.ReadByte() / 255.0;
							}

							//ShowMessage("got map: " + integers_as_bytes.Length);
						};
					#endregion

				};

		}

		public void OnServerSendMap()
		{
			// server has chosen me to send a map to the new users

			// for now lets do a manual serialization

			var ms = new MemoryStream();
			var mw = new BinaryWriter(ms);

			var cloud = Map.cloud1.Members;

			mw.Write((byte)cloud.Count);

			// we need to send movement info too
			mw.Write((short)Map.cloud1.TickInterval.Value);
			mw.Write((short)Map.cloud1.NextMove.x);
			mw.Write((short)Map.cloud1.NextMove.y);
			mw.Write((double)Map.cloud1.Speed);

			MapRoutedActions.SendTextMessage.Direct("sent cloud speed " + Map.cloud1.Speed);

			foreach (var a in cloud)
			{
				mw.Write((short)a.Element.MoveToTarget.Value.x);
				mw.Write((short)a.Element.MoveToTarget.Value.y);
				mw.Write((byte)(a.Element.alpha * 255));
			}

			var blocks = Map.DefenseBlocks;

			mw.Write((byte)blocks.Count);

			foreach (var a in blocks)
			{
				mw.Write((byte)(a.alpha * 255));
			}


			// proxy expects int[], at the moment, so we need to cast for now (overkill)

			var bytes_as_integers = ms.ToArray().Select(i => (int)i).ToArray();

			//ShowMessage("sent map: " + bytes_as_integers.Length);

			MapRoutedActions.SendTextMessage.Direct("sent map " + ms.Length + " bytes");

			//Messages.SendMap(ms.ToArray());
			Messages.SendMap(bytes_as_integers);
		}
	}
}
