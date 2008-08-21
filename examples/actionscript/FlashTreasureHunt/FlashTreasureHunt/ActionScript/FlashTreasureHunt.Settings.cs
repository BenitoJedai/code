using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.RayCaster;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	[ScriptApplicationEntryPoint(Width = DefaultControlWidth, Height = DefaultControlHeight)]
	[SWF(width = DefaultControlWidth, height = DefaultControlHeight, frameRate = 60, backgroundColor = 0)]
	partial class FlashTreasureHunt
	{
		const int DefaultControlWidth = DefaultWidth * DefaultScale;
		const int DefaultControlHeight = DefaultHeight * DefaultScale ;

		// 120x90
		// 160x120

		const int DefaultWidth = 302;
		const int DefaultHeight = DefaultWidth * 2 / 3;

		const int DefaultScale = 2;

		// hud : 302x33
	}
}