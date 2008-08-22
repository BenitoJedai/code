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
				g.Direction = 0;

				// state machine for AI guard

				// each 3 secs turn 90 while not walking
				3000.AtInterval(
					tt =>
					{
						if (!EgoView.Sprites.Contains(g))
						{
							tt.stop();
							return;
						}

						if (g.WalkingAnimationRunning)
							return;
						
						var PossibleDestination = g.Position.MoveToArc(g.Direction, 1);

						var AsMapLocation = new PointInt32
						{
							X = PossibleDestination.x.Floor(),
							Y = PossibleDestination.y.Floor()
						};

						if (EgoView.Map.WallMap[AsMapLocation.X, AsMapLocation.Y] == 0)
						{
							// whee we can walk at this direction
							g.StartWalkingAnimation();

							const int StepsToBeTaken  = 100;

							(1000 / 15).AtInterval(
								t =>
								{
									g.Position = g.Position.MoveToArc(g.Direction, 1.0 / (double)StepsToBeTaken);

									if (t.currentCount == StepsToBeTaken)
									{
										t.stop();
										g.StopWalkingAnimation();
									}
								}
							);
							return;
						}

						// can we walk at that direction?
						g.Direction += 90.DegreesToRadians();

					}
				);
			}
		}


	}
}