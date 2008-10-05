using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Controls;
using AvalonPipeMania.Assets.Shared;
using System.Windows;
using ScriptCoreLib.Shared.Lambda;
using System.Windows.Markup;

namespace AvalonPipeMania.Code
{
	[Script]
	public abstract partial class Pipe
	{
		// 1. fill color
		// 2. water
		// 3. glow
		public const double DefaultWaterOpacity = 0.9;



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
		
		public Image[] Water;
		
		public Image Brown;
		public Image Green;

		public Image Outline;

		[Script]
		public class Factory
		{
			public readonly Func<string, Image> ToImage;
			public readonly ParamsFunc<string, Image[]> ToWaterImages;

			public Factory(string Path, IAddChild Container)
			{
				this.ToImage =
					k => new Image
					{
						Source = (Path + "/" + k + ".png").ToSource(),
					}.AttachTo(Container);


				this.ToWaterImages =
					a =>
						a.ToArray(
							k => new Image
							{
								Source = (Path + "/" + k + ".png").ToSource(),
								Opacity = DefaultWaterOpacity,
								Visibility = Visibility.Hidden
							}.AttachTo(Container)
						);


			}
		}
	}


}
