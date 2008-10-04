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


		[Script]
		public class TopToBottom : Pipe
		{
			public Image Pipe_0_16;
			public Image Pipe_16_32;
			public Image Pipe_32_48;
			public Image Pipe_48_64;

			public TopToBottom()
			{
				var Pipe = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_brown_tb.png").ToSource(),
				}.AttachTo(this.Container);

				#region water
				this.Pipe_0_16 = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_blue_tb_0_16.png").ToSource(),
					Opacity = 0.5,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);

				this.Pipe_16_32 = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_blue_tb_16_32.png").ToSource(),
					Opacity = 0.5,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);

				this.Pipe_32_48 = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_blue_tb_32_48.png").ToSource(),
					Opacity = 0.5,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);


				this.Pipe_48_64 = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_blue_tb_48_64.png").ToSource(),
					Opacity = 0.5,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);
				#endregion


				this.Glow = new Image
				{
					Source = (KnownAssets.Path.Data + "/pipe_glow_tb.png").ToSource(),
				}.AttachTo(this.Container);
			}
		}

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
