using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript;
using Mahjong.Code;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.display;

namespace Mahjong.ActionScript
{
	[Script, ScriptApplicationEntryPoint(Width = MyCanvas.DefaultWidth, Height = MyCanvas.DefaultHeight)]
	[SWF(width = MyCanvas.DefaultWidth, height = MyCanvas.DefaultHeight)]
	public class Mahjong : Sprite
	{



		/// <summary>
		/// Default constructor
		/// </summary>
		public Mahjong()
		{


			// spawn the wpf control
			AvalonExtensions.AttachToContainer(new MyCanvas(), this);
		}

		static Mahjong()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedResources.Default.Handlers.Add(
				e => global::Mahjong.ActionScript.Assets.Default[e]
			);

		}
	}
}
