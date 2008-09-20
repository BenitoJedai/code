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

		public readonly FutureLock MapLoading = new FutureLock().Acquire();



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

					new Handshake().Verify(e.handshake);

					DiagnosticsWriteLine("handshake ok");

					Messages.ServerPlayerHello(e.user, e.name, e.others, new Handshake().Bytes);

					this.Identity.Value = e;

					if (e.others == 0)
					{
						DiagnosticsWriteLine("we are the first on this game");

						#region load first layout
						this.Map.Layouts.FirstLoaded.Continue(
							value =>
							{
								this.Map.MyLayout.Layout = value;


								this.Map.MyLayout.LayoutProgress.Continue(
											delegate
											{
												MapLoading.Release();
											}
										);
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

												var bytes = LayoutToBeLoaded.bytes;

												DeserializeMap(bytes);

												this.Map.MyLayout.LayoutProgress.Continue(
													delegate
													{
														MapLoading.Release();
													}
												);
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

					this.SynchronizedAsync(
					// if we are loading we need to wait - we can test it by making the map
					// to load real slow
						Done =>
						{
							// what if the guy leaves without waiting for response?

							LagBeforeRespondingToMapRequest.AtDelay(
								delegate
								{
									c.ToPlayer.UserMapResponse(SerializeMap());

									Done();
								}
							);

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

			this.Events.UserMapReload +=
				e =>
				{
					var c = CoPlayers[e.user];

					if (!this.UserLock_ByRemote.IsAcquired)
						throw new Exception("UserMapReload needs a lock");

					DiagnosticsWriteLine("UserMapReload: " + c.Name);

					MapLoading.Acquire(
						delegate
						{
							DeserializeMap(e.bytes);

							this.Map.MyLayout.LayoutProgress.Continue(
												delegate
												{
													MapLoading.Release();
												}
											);
						}
					);
				};

			#region Lock management

			this.Events.UserLockEnter +=
				e =>
				{
					var c = CoPlayers[e.user];

					//DiagnosticsWriteLine("UserLockEnter: " + c.Name);

					//// are we trying to get a lock by ourselves?
					// we cannot give lock to the user while we are still loading the map
					this.UserLock_ByRemote[
						this.UserLock_ByLocal,
						this.Map.MyLayout.LayoutProgress
					](
						delegate
						{
							// what if this player leaves?
							c.ToPlayer.UserLockValidate(e.id);
						}
					);

					
				};

			this.Events.UserLockExit +=
				e =>
				{
					var c = CoPlayers[e.user];

					//DiagnosticsWriteLine("UserLockExit: " + c.Name);

					if (!this.UserLock_ByRemote.IsAcquired)
						throw new Exception("UserLockExit needs a lock");

					// maybe we should wait a bit?
					this.UserLock_ByRemote.Release();
				};

			#region UserLockValidate
			this.Events.UserLockValidate +=
				e =>
				{
					var c = CoPlayers[e.user];

					//DiagnosticsWriteLine("UserLockValidate: " + c.Name);

					if (c.LockValidate != null)
						c.LockValidate(e.id);
				};
			#endregion
			#endregion

			this.Events.UserRemovePair +=
				e =>
				{
					var c = CoPlayers[e.user];

					if (!this.UserLock_ByRemote.IsAcquired)
						throw new Exception("UserRemovePair needs a lock");

					DiagnosticsWriteLine("UserRemovePair: " + c.Name);

					MapLoading.Continue(
						delegate
						{
							// we should probably check here if that user has "focus"

							this.Map.MyLayout.Remove(
								this.Map.MyLayout.Tiles[e.a].Tile.Value,
								this.Map.MyLayout.Tiles[e.b].Tile.Value
							);
						}
					);


				};

			this.Events.UserGoBack +=
				e =>
				{
					var c = CoPlayers[e.user];
					if (!this.UserLock_ByRemote.IsAcquired)
						throw new Exception("UserGoBack needs a lock");

					DiagnosticsWriteLine("UserGoBack: " + c.Name);

					MapLoading.Continue(
						delegate
						{
							this.Map.MyLayout.GoBack();
						}
					);
				};


			this.Events.UserGoForward +=
				e =>
				{
					var c = CoPlayers[e.user];
					if (!this.UserLock_ByRemote.IsAcquired)
						throw new Exception("UserGoForward needs a lock");

					DiagnosticsWriteLine("UserGoForward: " + c.Name);

					MapLoading.Continue(
						delegate
						{
							this.Map.MyLayout.GoForward();
						}
					);
				};
		}

		private void DeserializeMap(int[] bytes)
		{
			var MemoryStream_UInt8 = bytes.Select(i => (byte)i).ToArray();
			var m = new MemoryStream(MemoryStream_UInt8);

			//DiagnosticsWriteLine("read map " + m.Length);

			if (m.Length == 0)
				throw new Exception("no map data");

			m.Position = 0;

			this.Map.MyLayout.ReadFrom(m);
		}

		private int[] SerializeMap()
		{
			var m = new MemoryStream();

			this.Map.MyLayout.WriteTo(m);

			// we will waste 3 bytes - 0xffffff00 cuz memorystream isn't supported
			var MemoryStream_Int32 = m.ToArray().Select(i => (int)i).ToArray();
			return MemoryStream_Int32;
		}
	}
}
