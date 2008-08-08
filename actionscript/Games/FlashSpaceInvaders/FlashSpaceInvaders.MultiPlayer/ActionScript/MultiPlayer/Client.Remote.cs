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

namespace FlashSpaceInvaders.ActionScript.MultiPlayer
{
	partial class Client
	{
		readonly Property<SharedClass1.RemoteEvents.ServerPlayerHelloArguments> MyIdentity = new Property<SharedClass1.RemoteEvents.ServerPlayerHelloArguments>();


		readonly Dictionary<int, SpriteWithMovement> Cursors = new Dictionary<int, SpriteWithMovement>();
		readonly Dictionary<int, PlayerShip> CoPlayers = new Dictionary<int, PlayerShip>();


		public override void InitializeEvents()
		{
			#region ServerPlayerHello
			Events.ServerPlayerHello +=
				e =>
				{
					MyIdentity.Value = e;

					//ShowMessage("Howdy, " + e.name);


					// local only
					MapRoutedActions.SendTextMessage.Direct("Howdy, " + e.name);
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

					Events.UserVectorChanged +=
						e =>
						{
							// do we know about this player?

							if (!CoPlayers.ContainsKey(e.user))
								return;

							MapRoutedActions.MoveCoPlayer.Direct(CoPlayers[e.user], new Point(e.x, e.y));
						};


					#endregion

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
								null
							);
						};

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
				};

		}
	}
}
