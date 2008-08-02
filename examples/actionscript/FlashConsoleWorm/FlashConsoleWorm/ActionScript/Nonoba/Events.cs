using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib;
using FlashConsoleWorm.Shared;
using FlashConsoleWorm.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.Nonoba.api;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.geom;
using System.IO;

namespace FlashConsoleWorm.ActionScript.Nonoba
{
	partial class Client
	{
		readonly Dictionary<int, string> CoPlayerNames = new Dictionary<int, string>();
		readonly Dictionary<int, Worm> RemoteEgos = new Dictionary<int, Worm>();
		readonly Dictionary<int, ShapeWithMovement> Cursors = new Dictionary<int, ShapeWithMovement>();


		private void InitializeEvents()
		{

			var MyIdentity = default(SharedClass1.RemoteEvents.ServerPlayerHelloArguments);

			Events.ServerPlayerHello +=
				e =>
				{
					MyIdentity = e;

					ShowMessage("Howdy, " + e.name);
				};


			Action<int, string> CreateRemoteEgo =
				(user, name) =>
				{
					// could use event instead
					// dictonary.valueset +=
					CoPlayerNames[user] = name;

					// create remote ego
					RemoteEgos[user] = new Worm
					{
						Color = 0xff,
						Canvas = Map.Canvas,
						Wrapper = Map.Wrapper,
						Location = new Point(),
						ThisNetworkInstanceCannotEat = true
					}.AddTo(Map.Worms).Grow();

					ShowMessage("remote worm created - " + Map.Worms.Count);
				};

			Events.ServerPlayerJoined +=
			  e =>
			  {
				  CreateRemoteEgo(e.user, e.name);


				  ShowMessage("Player joined - " + e.name);

				  Messages.PlayerAdvertise(MyIdentity.name);
				  Messages.TeleportTo((int)Map.Ego.Location.x, (int)Map.Ego.Location.y);
			  };



			#region ServerPlayerLeft
			Events.ServerPlayerLeft +=
				  e =>
				  {
					  if (CoPlayerNames.ContainsKey(e.user))
					  {
						  CoPlayerNames.Remove(e.user);
					  }

					  if (Cursors.ContainsKey(e.user))
					  {
						  Cursors[e.user].Orphanize();
						  Cursors.Remove(e.user);
					  }

					  if (RemoteEgos.ContainsKey(e.user))
					  {
						  var w = RemoteEgos[e.user];

						  while (w.Parts.Count > 0)
							  w.Shrink();

						  Map.Worms.Remove(w);
						  RemoteEgos.Remove(e.user);
					  }

					  ShowMessage("Player left - " + e.name);
				  };
			#endregion

			Events.UserPlayerAdvertise +=
				e =>
				{
					if (CoPlayerNames.ContainsKey(e.user))
						return;

					CreateRemoteEgo(e.user, e.name);

					// ShowMessage("Player already here - " + e.name);
				};


			#region MouseMove
			Events.UserMouseMove +=
					e =>
					{
						var s = default(ShapeWithMovement);

						if (Cursors.ContainsKey(e.user))
							s = Cursors[e.user];
						else
						{
							s = new ShapeWithMovement
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

						s.AttachTo(this).MoveTo(e.x, e.y);
					};
			#endregion

			Events.UserMouseOut +=
			   e =>
			   {
				   if (Cursors.ContainsKey(e.color))
				   {
					   Cursors[e.color].Orphanize();
				   }
			   };

			Events.UserVectorChanged +=
				e =>
				{
					if (RemoteEgos.ContainsKey(e.user))
					{
						RemoteEgos[e.user].Vector = new Point(e.x, e.y);
					}
				};

			Events.ServerSendMap +=
				e =>
				{
					// server has chosen me to send a map to the new users

					// map is the apples, so we need to serialize them
					// for now lets do a manual serialization

					var ms = new MemoryStream();

					ms.WriteByte((byte)Map.Apples.Count);

					foreach (var a in Map.Apples)
					{
						ms.WriteByte((byte)a.Location.x);
						ms.WriteByte((byte)a.Location.y);
					}

					// proxy expects int[], at the moment, so we need to cast for now (overkill)

					var bytes_as_integers = ms.ToArray().Select(i => (int)i).ToArray();

					ShowMessage("sent map: " + bytes_as_integers.Length);

					Messages.SendMap(bytes_as_integers);

				};

			Events.UserSendMap +=
				e =>
				{
					// we got a new map, do we need it?

					foreach (var v in Map.Apples)
					{
						v.Control.Orphanize();
					}

					Map.Apples.Clear();

					// now add apples as the new map says
					var integers_as_bytes = e.buttons.Select(i => (byte)i).ToArray();

					var m = new MemoryStream(integers_as_bytes);

					var AppleCount = m.ReadByte();

					for (int i = 0; i < AppleCount; i++)
					{
						new Apple
						{
							Wrapper = Map.Wrapper,
							Location = new Point(m.ReadByte(), m.ReadByte())
						}.AddTo(Map.Apples).AttachTo(Map.Canvas);
					}

					ShowMessage("got map: " + integers_as_bytes.Length);
				};

			Events.UserTeleportTo +=
				e =>
				{
					//ShowMessage(new { teleport = e.user, e.x, e.y }.ToString());

					if (RemoteEgos.ContainsKey(e.user))
					{
						var w = RemoteEgos[e.user];


						w.Location = new Point(e.x, e.y);
						w.Grow();
						w.Shrink();
					}
				};

			Events.UserEatApple +=
				e =>
				{
					if (RemoteEgos.ContainsKey(e.user))
					{
						var w = RemoteEgos[e.user];

						w.Grow();

						foreach (var v in from i in Map.Apples
										  where i.Location.IsEqual(new Point(e.x, e.y))
										  select i)
						{
							v.Control.Orphanize();
							Map.Apples.Remove(v);
						}

						Sounds.sound20.ToSoundAsset().play();
					}
				};

			Events.UserEatThisWormBegin +=
				e =>
				{
					if (RemoteEgos.ContainsKey(e.user))
					{
						var user = RemoteEgos[e.user];

						if (RemoteEgos.ContainsKey(e.food))
						{
							var food = RemoteEgos[e.food];

							food.WormWhoIsGoingToEatMe = user;
							food.Color = 0xffffff;

							// whatif async end never comes?
						}
					}
				};

			Events.UserEatThisWormEnd +=
				e =>
				{
					if (RemoteEgos.ContainsKey(e.user))
					{
						var user = RemoteEgos[e.user];

						if (RemoteEgos.ContainsKey(e.food))
						{
							var food = RemoteEgos[e.food];

							food.IsAlive = false;

							// done eating it!

							food.WormWhoIsGoingToEatMe = null;
						}
					}
				};
		}
	}
}
