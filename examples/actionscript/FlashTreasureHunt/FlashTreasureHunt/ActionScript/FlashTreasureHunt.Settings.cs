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
		public const int DefaultControlWidth = DefaultWidth * DefaultScale;
		public const int DefaultControlHeight = DefaultHeight * DefaultScale;

		// 120x90
		// 160x120

		const int DefaultWidth = 600 / DefaultScale;
		const int DefaultHeight = DefaultWidth * 2 / 3;

		const int DefaultScale = 4;


		//const int MazeSize = 5;
		//const int MazeSize = 9;
		const int MazeSizeMin = 6;
		const int MazeSizeMax = 17;

		int MazeSize = MazeSizeMin;
		// hud : 302x33

		public const double PlayerRadiusMargin = 0.25;

		public const int AmmoFoundInClip = 6;


		public const int FrameRate_ShootingAnimation = 1000 / 15;
		public const int FrameRate_DeathAnimation = 1000 / 15;
		public const int FrameRate_FireWeapon = 1000 / 15;
	}
}