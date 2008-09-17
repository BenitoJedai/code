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

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	partial class Client
	{
		public readonly Future<Communication.RemoteEvents.ServerPlayerHelloArguments> Identity = new Future<Communication.RemoteEvents.ServerPlayerHelloArguments>();


		public void InitializeEvents()
		{
			// this should be called once!
			#region  CoPlayers
			var CoPlayers = new CoPlayerGroup(
				user =>
				{
					var n = new CoPlayer { user = user };

					n.Cursor =   new ArrowCursorControl();
					n.Cursor.Blue.Opacity  = 0.9;
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

					MouseMove.DiagnosticsWriteLine = this.Map.DiagnosticsWriteLine;

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
					this.Map.DiagnosticsWriteLine("read: " + e.ToString());
 
					CoPlayers[e.user].MouseMove(e.x, e.y);
				};

			this.Events.ServerPlayerLeft +=
				e =>
				{
					CoPlayers[e.user].Cursor.Container.Orphanize();

					CoPlayers.Remove(e.user);
				};

			this.Events.ServerPlayerHello += this.Identity;

			
			this.Events.ServerPlayerJoined +=
				e =>
				{
					if (this.Identity.CanSignal)
					{
						this.Map.DiagnosticsWriteLine("ServerPlayerJoined: " + e.name + " to not ready");
						return;
					}

					// we got the name of the user that is currently joining the game
					CoPlayers[e.user].Name = e.name;

					// we will introduse ourself directly to the new user
					this.Messages.UserPlayerAdvertise(e.user, Identity.Value.name);

					this.Map.DiagnosticsWriteLine("ServerPlayerJoined: " + e.name);
				};

			this.Events.UserPlayerAdvertise +=
				e =>
				{
					// a user was already in the room before we joined
					CoPlayers[e.user].Name = e.name;

					this.Map.DiagnosticsWriteLine("UserPlayerAdvertise: " + e.name);
				};

		}
	}
}
