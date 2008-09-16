using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Mahjong.Code;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	partial class Client
	{

		public MahjongGameControl Map;

		public void InitializeMap()
		{
			if (this.Map != null)
				throw new NotSupportedException();

			this.Map = new MahjongGameControl();
			this.Map.AttachTo(Element);

			// we need to use a treshold and throttle too frequent updates
			var MouseMove = NumericOmitter.Of(
				(x, y) =>
				{
					this.Messages.MouseMove(x, y);

					this.Map.DiagnosticsWriteLine("write: " + new { x, y }.ToString());
				}
			);

			this.Map.Sync_MouseMove += MouseMove;

		}
	}
}
