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
		const int MazeSizeMin = 5;
		const int MazeSizeMax = 10;

		const int MazeDelayResize = 2;

		int MazeSize = MazeSizeMin;
		// hud : 302x33

		public const double PlayerRadiusMargin = 0.25;

		public const int AmmoFoundInClip = 6;

		public const double GuardMaxHealth = 1.1;

		public const double GuardCriticalHealth = 0.3;

		public const int FrameRate_ShootingAnimation = 1000 / 15;
		public const int FrameRate_DeathAnimation = 1000 / 15;
		public const int FrameRate_FireWeapon = 1000 / 15;
		public const int FrameRate_PortalRefresh = 1000 / 10;

		// publishing settings:

		[Script]
		public static class PublishedInfo
		{
			public const string MochiAd = "daf8c9989dba6a98";
			public const string Dimensions = "800x400";

			public const string Title = "Treasure Hunt";
			public const string Description = "A 3D Maze game where you get to collect gold treasure. Inspired by Wolfenstein 3D shareware.";
			public const string Instructions = "Collect half of the treasure to get the compass which guides you to the next level.";

			public const string Keywords = "wolf3d, maze, gold, treasure, multiplayer";


			public const string NonobaLink = "http://nonoba.com/zproxy/treasure-hunt";

			public const string MochiAdLink = "http://games.mochiads.com/c/g/treasure-hunt/treasure-hunt.swf";
		}
	}
}