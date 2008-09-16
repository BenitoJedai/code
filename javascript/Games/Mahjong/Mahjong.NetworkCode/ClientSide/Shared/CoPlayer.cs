using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Cursors;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	[Script]
	public class CoPlayer
	{
		public int user;

		public ArrowCursorControl Cursor;

		// remote -> local
		public Action<int, int> MouseMove;
	}
}
