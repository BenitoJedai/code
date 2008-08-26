using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{
	/// <summary>
	/// Default flash player entrypoint class. See 'tools/build.bat' for adding more entrypoints.
	/// </summary>
	partial class FlashTreasureHunt
	{
		public Sprite CompassContainer;

		public void InitializeCompass()
		{
			var compass = Assets.Default.compasscolor;
			var container = new Sprite();

			container.alpha = 0;

			CompassContainer = container;

			compass.AttachTo(container);
			container.AttachTo(HudContainer);

			Action Update =
				delegate
				{
					// compass must show us where the goal is

					var delta = new Point(maze.Width - 1.5 - EgoView.ViewPositionX, maze.Height - 1.5 - EgoView.ViewPositionY);

					container.rotation = (delta.GetRotation() - EgoView.ViewDirection).RadiansToDegrees();


				};

			EgoView.ViewDirectionChanged += Update;

			EgoView.ViewPositionChanged += Update;

			Update();

			compass.scaleX = 0.5;
			compass.scaleY = 0.5;

			compass.x = -compass.width / 2;
			compass.y = -compass.height / 2;

			container.filters = new[] { new ScriptCoreLib.ActionScript.flash.filters.DropShadowFilter() };

			container.x = DefaultControlWidth - compass.width / 2;
			container.y = compass.height / 2;
		}
	}
}