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
using ScriptCoreLib.ActionScript.flash.utils;

namespace FlashTreasureHunt.ActionScript
{
	partial class FlashTreasureHunt
	{


		private void CreateGuards(IEnumerator<TextureBase.Entry> FreeSpaceForStuff)
		{
			for (int i = 0; i < 5; i++)
			{
				var g = CreateGuard();

				EgoView.BlockingSprites.Add(g);


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

						#region death
						if (g.Health <= 0)
						{
							tt.stop();
							return;
						}
						#endregion


						if (!g.AIEnabled)
							return;

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

							const int StepsToBeTaken  = 80;

							(1000 / 15).AtInterval(
								t =>
								{

									#region death
									if (g.Health <= 0)
									{
										t.stop();
										return;
									}
									#endregion

									if (!g.AIEnabled)
										return;

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



		public SpriteInfoExtended CreateWalkingDummy(Texture64[] Stand, Texture64[][] Walk, Texture64[] Hit, Texture64[] Death)
		{
			var tt = default(Timer);
			var s = new SpriteInfoExtended();

			Action start =
				delegate
				{
					s.WalkingAnimationRunning = true;

					if (Walk.Length > 0)
						tt = (200).AtInterval(
							t =>
							{
								#region dead people do not walk
								if (s.Health <= 0)
								{
									t.stop();
									return;
								}
								#endregion


								if (!s.AIEnabled)
									return;

								s.Frames = Walk[t.currentCount % Walk.Length];
							}
						);
				};

			Action stop =
				delegate
				{
					s.WalkingAnimationRunning = false;

					if (tt != null)
						tt.stop();

					s.Frames = Stand;
				};


			s.Position = new Point { x = EgoView.ViewPositionX, y = EgoView.ViewPositionY };
			s.Frames = Stand;
			s.Direction = EgoView.ViewDirection;
			s.StartWalkingAnimation = start;
			s.StopWalkingAnimation = stop;
			s.Range = PlayerRadiusMargin;

			s.AddTo(EgoView.Sprites);

			if (Hit != null)
				s.TakeDamage =
					DamageToBeTaken =>
					{
						s.Health -= DamageToBeTaken;

						if (s.Health > 0)
						{
							Assets.Default.Sounds.hit.play();
							 
							#region just show being hurt for a short moment
							s.AIEnabled = false;

							var q = s.Frames;
							s.Frames = Hit;

							300.AtDelayDo(
								delegate
								{
									s.Frames = q;
									s.AIEnabled = true;
								}
							);
							#endregion

						}
						else
						{
							Assets.Default.Sounds.death.play();

							// player wont be blocked by a corpse
							s.Range = 0;

							// animate death
							(1000 / 10).AtInterval(
								ttt =>
								{
									if (Death.Length == ttt.currentCount)
									{
										ttt.stop();
										return;
									}

									s.Frames = new[] { Death[ttt.currentCount] };
								}
							);
						}
							
					};


			return s;
		}



		public SpriteInfoExtended CreateDummy(Texture64 Stand)
		{
			return CreateWalkingDummy(new[] { Stand }, null, null, null);

		}
	}
}