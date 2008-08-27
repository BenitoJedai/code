using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlashTreasureHunt.Shared;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.ActionScript.flash.utils;
using ScriptCoreLib.ActionScript.flash.geom;

namespace FlashTreasureHunt.ActionScript
{

	[Script]
	public class CoPlayer
	{
		public SharedClass1.RemoteEvents.UserPlayerAdvertiseArguments Identity;

		public SpriteInfoExtended Guard;

		Timer WalkTo_Timer;
		Timer WalkTo_Smooth;

		Point WalkTo_Target = new Point();

		public double WalkToDistance
		{
			get
			{
				return WalkTo_Target.GetDistance(this.Guard.Position);
			}
		}

		public CoPlayer WalkTo(double x, double y)
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
					Guard.StartWalkingAnimation();
				}

				WalkTo_Smooth = (1000 / 24).AtInterval(
					t =>
					{
						if (this.Guard == null)
						{
							t.stop();
							WalkTo_Timer = null;
							WalkTo_Smooth = null;
							return;
						}

						var z = WalkToDistance;

						var IsCloseEnough = z < Step;
						var IsInNeedForTeleport = z > 1.0;

						if (IsCloseEnough || IsInNeedForTeleport)
						{
							this.Guard.Position = WalkTo_Target;
							t.stop();
							WalkTo_Smooth = null;
							WalkTo_Timer = 500.AtDelayDo(
								delegate
								{
									WalkTo_Timer = null;

									if (this.Guard != null)
										this.Guard.StopWalkingAnimation();
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

						var arc = (WalkTo_Target - this.Guard.Position).GetRotation();
						var speed = Step;

						if (z > 0.2)
							speed *= 2;

						if (z > 0.4)
							speed *= 2;

						if (z > 0.6)
							speed *= 2;

						if (z > 0.8)
							speed *= 2;

						this.Guard.Position = this.Guard.Position.MoveToArc(arc, speed);

					}
				);
			}

			return this;
		}

		public event Action WalkToTeleported;
		public event Action WalkToDone;
		public event Action WalkToStart;
	}

}
