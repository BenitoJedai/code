using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Cursors;
using System.Windows.Media;
using ScriptCoreLib.Shared.Lambda;
using System.IO;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	partial class Client
	{
		public readonly Future<Communication.RemoteEvents.ServerPlayerHelloArguments> Identity = new Future<Communication.RemoteEvents.ServerPlayerHelloArguments>();

		/// <summary>
		/// When null or signalled then we do not have a lock. One can use .Continue
		/// to run after lock is released or does not exist at all.
		/// </summary>
		public Future UserLockEnter_ByLocal;


		public Future UserLockEnter_ByRemote;

		public CoPlayerGroup CoPlayers;

		public void InitializeEvents()
		{
			Action<string> DiagnosticsWriteLine = text => this.Map.DiagnosticsWriteLine(text);

			// this should be called once!
			#region  CoPlayers
			this.CoPlayers = new CoPlayerGroup(
				user =>
				{
					var n = new CoPlayer
					{
						user = user,
						ToPlayer = new Communication.RemoteEvents.WithUserArgumentsRouter_SinglecastView
						{
							user = user,
							Target = this.Messages
						}
					};

					n.Cursor = new ArrowCursorControl();
					n.Cursor.Blue.Opacity = 0.9;
					n.Cursor.Container.AttachTo(this.Map.CoPlayerMouseContainer);

					#region MouseMove
					var MouseMove = NumericEmitter.Of(
						// dynamically generated events
						(x, y) =>
						{
							n.Cursor.Container.MoveTo(x, y);
							//this.Map.DiagnosticsWriteLine("emit: " + new { x, y }.ToString());
						}
					);


					// entry point for rare events
					n.MouseMove = MouseMove;
					#endregion


					return n;
				}
			);
			#endregion


			this.Identity.Continue(
				delegate
				{
					this.Map.DiagnosticsWriteLine("Identity: " + Identity.Value.ToString());
				}
			);

			this.Events.UserMouseMove +=
				e =>
				{
					//this.Map.DiagnosticsWriteLine("read: " + e.ToString());

					CoPlayers[e.user].MouseMove(e.x, e.y);
				};

			this.Events.ServerPlayerLeft +=
				e =>
				{
					CoPlayers[e.user].Cursor.Container.Orphanize();

					CoPlayers.Remove(e.user);
				};

			var UserPlayerAdvertise = new Future<CoPlayer>();
			var UserMapResponse = default(Future<Communication.RemoteEvents.UserMapResponseArguments>);

			#region ServerPlayerHello
			this.Events.ServerPlayerHello +=
				e =>
				{
					this.Identity.Value = e;

					if (e.others == 0)
					{
						DiagnosticsWriteLine("we are the first on this game");

						#region load first layout
						this.Map.Layouts.FirstLoaded.Continue(
							value =>
							{
								this.Map.MyLayout.Layout = value;
							}
						);
						#endregion

					}
					else
					{
						DiagnosticsWriteLine("Somebody must say they are in the room so we could ask the map");

						UserPlayerAdvertise.Continue(
							(CoPlayer FirstFoundCoPlayerToAskAMapFrom) =>
							{
								UserPlayerAdvertise = null;

								DiagnosticsWriteLine("Asked for map from: " + FirstFoundCoPlayerToAskAMapFrom.Name);

								UserMapResponse = new Future<Communication.RemoteEvents.UserMapResponseArguments>();

								// what if it takes too long or that player leaves?
								FirstFoundCoPlayerToAskAMapFrom.ToPlayer.UserMapRequest();

								// we currently do not check if the same user responds...
								UserMapResponse.Continue(
									LayoutToBeLoaded =>
									{
										DiagnosticsWriteLine("We got the map now we will soon load it: " + FirstFoundCoPlayerToAskAMapFrom.Name);

										// this delay could be 0 in release mode
										1500.AtDelay(
											delegate
											{
												UserMapResponse = null;

												var MemoryStream_UInt8 = LayoutToBeLoaded.bytes.Select(i => (byte)i).ToArray();
												var m = new MemoryStream(MemoryStream_UInt8);

												this.Map.MyLayout.ReadFrom(m);
											}
										);

									}
								);
							}
						);
					}
				};
			#endregion

			#region ServerPlayerJoined
			this.Events.ServerPlayerJoined +=
				e =>
				{
					// we got the name of the user that is currently joining the game
					CoPlayers[e.user].Name = e.name;



					this.Map.DiagnosticsWriteLine("ServerPlayerJoined: " + e.name);

					// we will introduse ourself directly to the new user
					CoPlayers[e.user].ToPlayer.UserPlayerAdvertise(Identity.Value.name);

				};
			#endregion

			#region UserPlayerAdvertise
			this.Events.UserPlayerAdvertise +=
				e =>
				{
					// a user was already in the room before we joined
					CoPlayers[e.user].Name = e.name;

					this.Map.DiagnosticsWriteLine("UserPlayerAdvertise: " + e.name);

					// first coplayer found that was already in room when we joined
					if (UserPlayerAdvertise != null)
						UserPlayerAdvertise.Value = CoPlayers[e.user];

				};
			#endregion

			#region UserMapRequest
			this.Events.UserMapRequest +=
				e =>
				{
					var c = CoPlayers[e.user];

					this.Map.DiagnosticsWriteLine("UserMapRequest: " + c.Name);

					// if we are loading we need to wait - we can test it by making the map
					// to load real slow
					this.Map.MyLayout.LayoutProgress.Continue(
						delegate
						{
							// what if the guy leaves without waiting for response?

							var m = new MemoryStream();

							this.Map.MyLayout.WriteTo(m);

							// we will waste 3 bytes - 0xffffff00 cuz memorystream isn't supported
							var MemoryStream_Int32 = m.ToArray().Select(i => (int)i).ToArray();

							c.ToPlayer.UserMapResponse(MemoryStream_Int32);
						}
					);
				};
			#endregion


			this.Events.UserMapResponse +=
				e =>
				{
					var c = CoPlayers[e.user];

					DiagnosticsWriteLine("UserMapResponse: " + c.Name);

					if (UserMapResponse != null)
						UserMapResponse.Value = e;
				};

			this.Events.UserLockEnter +=
				e =>
				{
					var c = CoPlayers[e.user];

					DiagnosticsWriteLine("UserLockEnter: " + c.Name);

					// are we trying to get a lock by ourselves?
					this.UserLockEnter_ByLocal.Continue(
						delegate
						{
							DiagnosticsWriteLine("UserLockEnter (we were not trying to lock): " + c.Name);

							this.UserLockEnter_ByRemote.Continue(
								delegate
								{
									DiagnosticsWriteLine("UserLockEnter (we were not locked by remote): " + c.Name);

									this.UserLockEnter_ByRemote = new Future();

									this.Map.MyLayout.LayoutProgress.Continue(
										delegate
										{
											DiagnosticsWriteLine("UserLockEnter (we have loaded the map): " + c.Name);

											// we cannot give lock to the user while we are still loading the map
											c.ToPlayer.UserLockValidate(e.id);
										}
									);
								}
							);
						}
					);
				};

			this.Events.UserLockExit +=
				e =>
				{
					var c = CoPlayers[e.user];

					DiagnosticsWriteLine("UserLockExit: " + c.Name);

					var s = this.UserLockEnter_ByRemote;
					this.UserLockEnter_ByRemote = null;
					s.Signal();
				};

			#region UserLockValidate
			this.Events.UserLockValidate +=
				e =>
				{
					var c = CoPlayers[e.user];

					DiagnosticsWriteLine("UserLockValidate: " + c.Name);

					if (c.LockValidate != null)
						c.LockValidate(e.id);
				};
			#endregion

			this.Events.UserRemovePair +=
				e =>
				{
					var c = CoPlayers[e.user];

					if (this.UserLockEnter_ByRemote == null)
						throw new Exception("UserRemovePair needs a lock");

					DiagnosticsWriteLine("UserRemovePair: " + c.Name);

					// we should probably check here if that user has "focus"

					this.Map.MyLayout.Remove(
						this.Map.MyLayout.Tiles[e.a].Tile.Value,
						this.Map.MyLayout.Tiles[e.b].Tile.Value
						);


				};
		}
	}
}
