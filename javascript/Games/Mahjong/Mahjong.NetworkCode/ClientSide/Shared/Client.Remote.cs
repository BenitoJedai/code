using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Avalon.Cursors;
using System.Windows.Media;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	partial class Client
	{
		public void InitializeEvents()
		{
			// this should be called once!
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
		}
	}
}
