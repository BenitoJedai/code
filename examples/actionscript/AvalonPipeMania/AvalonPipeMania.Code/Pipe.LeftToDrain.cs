using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AvalonPipeMania.Assets.Shared;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using ScriptCoreLib.Shared.Lambda;

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


				var WaterDropFrames = f.ToWaterImages(
					"1",
					"2"
				);

				Action Hide = delegate { };

				(1000 / 23).AtIntervalWithCounter(
					Counter =>
					{
						Hide();

						WaterDropFrames.AtModulus(Counter).Visibility = Visibility.Visible;

						Hide = () => WaterDropFrames.AtModulus(Counter).Visibility = Visibility.Hidden;
					}
				);


				this.Outline = f.ToImage("outline");

				this.Brown = f.ToImage("brown");
				this.Brown.Visibility = Visibility.Hidden;

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
