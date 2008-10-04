using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using AvalonPipeMania.Assets.Shared;
using System.Windows;

namespace AvalonPipeMania.Code
{
	[Script]
	public abstract partial class Pipe
	{
		// 1. fill color
		// 2. water
		// 3. glow
		public const double DefaultWaterOpacity = 1;


	
		public readonly Canvas Container;

		public const int Size = 64;

		public Pipe()
		{
			Container = new Canvas
			{
				Width = Size,
				Height = Size
			};
		}

		public Image Glow;


	}


}
