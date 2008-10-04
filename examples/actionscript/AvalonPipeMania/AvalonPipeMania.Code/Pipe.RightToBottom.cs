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
		public class RightToBottom : Pipe
		{
			public Image Pipe_0_16;
			public Image Pipe_16_32;
			public Image Pipe_32_48;
			public Image Pipe_48_64;


			public RightToBottom()
			{
				var Pipe = new Image
				{
					Source = (KnownAssets.Path.Pipe.RightToBottom + "/brown.png").ToSource(),
				}.AttachTo(this.Container);

				#region water
				this.Pipe_0_16 = new Image
				{
					Source = (KnownAssets.Path.Pipe.RightToBottom + "/0_16.png").ToSource(),
					Opacity = DefaultWaterOpacity,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);

				this.Pipe_16_32 = new Image
				{
					Source = (KnownAssets.Path.Pipe.RightToBottom + "/16_32.png").ToSource(),
					Opacity = DefaultWaterOpacity,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);

				this.Pipe_32_48 = new Image
				{
					Source = (KnownAssets.Path.Pipe.RightToBottom + "/32_48.png").ToSource(),
					Opacity = DefaultWaterOpacity,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);

				this.Pipe_48_64 = new Image
				{
					Source = (KnownAssets.Path.Pipe.RightToBottom + "/48_64.png").ToSource(),
					Opacity = DefaultWaterOpacity,
					Visibility = Visibility.Hidden
				}.AttachTo(this.Container);

				#endregion


				this.Glow = new Image
				{
					Source = (KnownAssets.Path.Pipe.RightToBottom + "/glow.png").ToSource(),
				}.AttachTo(this.Container);
			}
		}

		
	}


}
