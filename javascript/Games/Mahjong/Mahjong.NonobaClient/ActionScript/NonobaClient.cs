using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using Mahjong.Code;
using Mahjong.Specialize.ActionScript;
using Mahjong.NetworkCode.ClientSide.Shared;

namespace Mahjong.NonobaClient.ActionScript
{

	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint(Width = DefaultWidth, Height = DefaultHeight)]
	[SWF(width = DefaultWidth, height = DefaultHeight, backgroundColor = 0)]
	public class NonobaClient : Sprite
	{
		public const int DefaultWidth = MahjongGameControl.DefaultScaledWidth + global::Mahjong.NetworkCode.ClientSide.ActionScript.NonobaClient.NonobaChatWidth;
		public const int DefaultHeight = MahjongGameControl.DefaultScaledHeight;

		public readonly Client Client;

		public NonobaClient()
		{
			var c = new global::Mahjong.NetworkCode.ClientSide.ActionScript.NonobaClient("arvo-pc");

			this.Client = c;

			c.PlaySoundFuture.BindToPlaySound();

			c.MapInitialized.Continue(
				Map => Map.BindToFullScreenExclusively());


			// spawn the wpf control
			AvalonExtensions.AttachToContainer(c.Element, this);
		}

		static NonobaClient()
		{
			Specialize.ActionScript.Specialize.AddKnownEmbeddedResources();
		


		}
	}
}