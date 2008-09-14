using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using Mahjong.Code;

namespace Mahjong.NonobaClient.ActionScript
{

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = ControlWidth, Height = ControlHeight)]
	[SWF(width = ControlWidth, height = ControlHeight)]
	public class NonobaClient : Sprite
	{
		public const int ControlWidth = MahjongGameControl.DefaultScaledWidth + global::Mahjong.NetworkCode.ClientSide.ActionScript.NonobaClient.NonobaChatWidth;
		public const int ControlHeight = MahjongGameControl.DefaultScaledHeight;

		public NonobaClient()
		{
			var n = new global::Mahjong.NetworkCode.ClientSide.ActionScript.NonobaClient("arvo-pc");


			// spawn the wpf control
			AvalonExtensions.AttachToContainer(n.Element, this);
		}

		static NonobaClient()
		{
			// add resources to be found by ImageSource
			KnownEmbeddedResources.Default.Handlers.AddRange(
				global::Mahjong.ActionScript.__Assets.ReferencedKnownEmbeddedResources()
			);


		}
	}
}