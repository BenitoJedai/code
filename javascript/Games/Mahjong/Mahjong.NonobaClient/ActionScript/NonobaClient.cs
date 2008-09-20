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
	[Script, ScriptApplicationEntryPoint(Width = DefaultWidth, Height = DefaultHeight)]
	[SWF(width = DefaultWidth, height = DefaultHeight)]
	public class NonobaClient : Sprite
	{
		public const int DefaultWidth = MahjongGameControl.DefaultScaledWidth + global::Mahjong.NetworkCode.ClientSide.ActionScript.NonobaClient.NonobaChatWidth;
		public const int DefaultHeight = MahjongGameControl.DefaultScaledHeight;

		public NonobaClient()
		{
			var c = new global::Mahjong.NetworkCode.ClientSide.ActionScript.NonobaClient("arvo-pc");

			c.PlaySoundFuture.Value = global::Mahjong.ActionScript.__Assets.Default.PlaySound;

			// spawn the wpf control
			AvalonExtensions.AttachToContainer(c.Element, this);
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