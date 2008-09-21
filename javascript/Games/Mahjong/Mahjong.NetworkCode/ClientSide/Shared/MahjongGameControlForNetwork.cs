using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Mahjong.Code;
using Mahjong.NetworkCode.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Media;
using System.Windows;
using System.Collections;
using System.Windows.Shapes;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	[Script]
	public partial class MahjongGameControlForNetwork : MahjongGameControl
	{
		public override void WhatToDoWhenFirstLayoutIsLoaded()
		{
			// nothing cuz maybe we need to load from the network instead
		}

	
	}
}
