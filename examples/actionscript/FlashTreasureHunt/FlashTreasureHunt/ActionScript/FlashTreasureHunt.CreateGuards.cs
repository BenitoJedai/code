using ScriptCoreLib;
using ScriptCoreLib.ActionScript.flash.display;
using ScriptCoreLib.ActionScript.flash.text;
using System.Collections.Generic;
using System;
using System.Linq;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Archive.Extensions;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.Shared.Maze;
using ScriptCoreLib.Shared.Lambda;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{


		private void CreateGuards(IEnumerator<TextureBase.Entry> FreeSpaceForStuff)
		{
			for (int i = 0; i < 3; i++)
			{
				var g = CreateGuard();
				g.Position = FreeSpaceForStuff.Take().Do(kk => new Point(kk.XIndex + 0.5, kk.YIndex + 0.5));

				// state machine for AI guard

				// each 3 secs turn 90 while not walking
				3000.AtInterval(
					delegate
					{
						if (g.WalkingAnimationRunning)
							return;

						g.Direction += 90.DegreesToRadians();

					}
				);
			}
		}


	}
}