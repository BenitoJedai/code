using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using System.Windows.Controls;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	[Script]
	public partial class Client : VirtualClient
	{
		public readonly Canvas Element;

		public Client()
		{
			Element = new Canvas
			{
				Width = Mahjong.Code.MahjongGameControl.DefaultScaledWidth,
				Height = Mahjong.Code.MahjongGameControl.DefaultScaledHeight
			};
		}
	}
}
