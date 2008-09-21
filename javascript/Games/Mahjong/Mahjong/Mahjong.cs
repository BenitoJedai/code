using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using Mahjong.Code;
using Mahjong.Specialize.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;

namespace Mahjong.ActionScript
{
	[Script, ScriptApplicationEntryPoint(Width = MahjongGameControl.DefaultScaledWidth, Height = MahjongGameControl.DefaultScaledHeight)]
	[SWF(width = MahjongGameControl.DefaultScaledWidth, height = MahjongGameControl.DefaultScaledHeight, backgroundColor = 0)]
	public class Mahjong : Sprite
	{



		/// <summary>
		/// Default constructor
		/// </summary>
		public Mahjong()
		{
			var c = new MahjongGameControl();

			c.BindToFullScreen();
			c.PlaySoundFuture.BindToPlaySound();
			c.DiagnosticsContainer.Visibility = System.Windows.Visibility.Hidden;
			c.AddSocialBookmarks();

			// spawn the wpf control
			AvalonExtensions.AttachToContainer(c, this);
		}

		static Mahjong()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedResources.Default.Handlers.AddRange(__Assets.ReferencedKnownEmbeddedResources());

		}
	}
}
