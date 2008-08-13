using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using ScriptCoreLib.ActionScript.Extensions;
using System.Collections.Generic;
using System;

namespace FlashAvalonExample.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[Script, ScriptApplicationEntryPoint]
	public class FlashAvalonExample : Sprite
	{

		/// <summary>
		/// Default constructor
		/// </summary>
		public FlashAvalonExample()
		{
			AvalonExtensions.AttachTo(new MyCanvas(), this);
			//new MyCanvas().AttachTo(this);
		}
	}
}