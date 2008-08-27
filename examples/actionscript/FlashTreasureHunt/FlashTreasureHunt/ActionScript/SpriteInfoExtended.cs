using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.RayCaster;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{
	[Script]
	public class SpriteInfoExtended : SpriteInfo
	{
		public bool IsTaken;

		public Action ItemTaken;


		public Action StopWalkingAnimation;
		public Action StartWalkingAnimation;
		public bool WalkingAnimationRunning;

		public bool AIEnabled = true;

		public double Health = 1;


		public Action<double> TakeDamage;

		public Action<double> TakeDamageDone;

		// this is used while syncing
		public int ConstructorIndexForSync = -1;

		#region Minimap
		public uint MinimapColor = 0x9f00FF00;
		public uint MinimapInactiveColor = 0x9f008000;
		public int MinimapZIndex = MinimapZIndex_Default;

		public const int MinimapZIndex_Default = 0;
		public const int MinimapZIndex_OnTop = 1;
		#endregion


		public Action PlayShootingAnimation;


		#region WalkTo

		Timer WalkTo_Timer;
		Timer WalkTo_Smooth;

		Point WalkTo_Target = new Point();

		public double WalkToDistance
		{
			get
			{
				return WalkTo_Target.GetDistance(this.Position);
			}
		}

		public void WalkTo(double x, double y)
		{
			WalkTo_Target = new Point(x, y);

			// should do a smooth movement now

			const double Step = 1.0 / 60.0;

			if (WalkTo_Smooth == null)
			{
				if (WalkToStart != null)
				{
					WalkToStart();
				}

				if (WalkTo_Timer != null)
				{
					// reset the timer, so that the counting begins from now
					WalkTo_Timer.stop();
				}
				else
				{
					this.StartWalkingAnimation();
				}

				WalkTo_Smooth = (1000 / 24).AtInterval(
					t =>
					{
						if (Health <= 0)
						{
							t.stop();
							WalkTo_Timer = null;
							WalkTo_Smooth = null;
							return;
						}

						var z = WalkToDistance;

						var speed = Step;

						if (z > 0.2)
							speed *= 2;

						if (z > 0.4)
							speed *= 2;

						if (z > 0.6)
							speed *= 2;

						if (z > 0.8)
							speed *= 2;

						var IsCloseEnough = z < (Step * 2);
						var IsInNeedForTeleport = z > 1.0;

						if (IsCloseEnough || IsInNeedForTeleport)
						{
							this.Position = WalkTo_Target;
							t.stop();
							WalkTo_Smooth = null;
							WalkTo_Timer = 500.AtDelayDo(
								delegate
								{
									WalkTo_Timer = null;

									this.StopWalkingAnimation();
								}
							);

							if (IsCloseEnough)
							{
								if (WalkToDone != null)
									WalkToDone();
							}

							if (IsInNeedForTeleport)
							{
								if (WalkToTeleported != null)
									WalkToTeleported();
							}

							return;
						}

						var arc = (WalkTo_Target - this.Position).GetRotation();
			

						this.Position = this.Position.MoveToArc(arc, speed);

					}
				);
			}

		}

		public event Action WalkToTeleported;
		public event Action WalkToDone;
		public event Action WalkToStart;

		#endregion

	}
}
