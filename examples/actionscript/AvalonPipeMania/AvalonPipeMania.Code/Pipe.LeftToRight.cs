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
		public class LeftToRight : Pipe
		{
			public Image Pipe_0_16;
			public Image Pipe_16_32;
			public Image Pipe_32_48;
			public Image Pipe_48_64;

			public LeftToRight()
			{
				var Pipe = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_brown_lr.png").ToSource(),
				}.AttachTo(this.Container);

				#region water
				this.Pipe_0_16 = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_blue_lr_0_16.png").ToSource(),
					Opacity = DefaultWaterOpacity,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);

				this.Pipe_16_32 = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_blue_lr_16_32.png").ToSource(),
					Opacity = DefaultWaterOpacity,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);

				this.Pipe_32_48 = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_blue_lr_32_48.png").ToSource(),
					Opacity = DefaultWaterOpacity,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);


				this.Pipe_48_64 = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_blue_lr_48_64.png").ToSource(),
					Opacity = DefaultWaterOpacity,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);
				#endregion


				this.Glow = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_glow_lr.png").ToSource(),
				}.AttachTo(this.Container);
			}
		}

		
	}


}
