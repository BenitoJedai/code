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
	partial class Pipe
	{
	

		[Script]
		public class LeftToDrain : Pipe
		{


			public LeftToDrain()
			{
				var f = new Factory(KnownAssets.Path.Pipe.LeftToDrain, this.Container);

				this.Outline = f.ToImage("outline");

				this.Brown = f.ToImage("brown");

				this.Green = f.ToImage("green");
				this.Green.Visibility = Visibility.Hidden;

				this.Water = f.ToWaterImages(
					"0_8",
					"8_16",
					"16_24"
				);

				this.Glow = f.ToImage("glow");
			}
		}

		
	}


}
