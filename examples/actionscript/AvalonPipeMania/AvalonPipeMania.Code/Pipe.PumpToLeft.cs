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
		public class PumpToLeft : Pipe
		{
			public PumpToLeft()
			{
				var f = new Factory(KnownAssets.Path.Pipe.PumpToLeft, this.Container);

				this.Outline = f.ToImage("outline");

				this.Brown = f.ToImage("brown");

				this.Green = f.ToImage("green");
				this.Green.Visibility = Visibility.Hidden;


				this.Water = f.ToWaterImages(
					"0_8",
					"8_16"
				);

				this.Glow = f.ToImage("glow");
			}
		}


	}


}
