using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;
using System.Windows.Threading;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	[Script]
	public class NumericEmitter : NumericTransmitter
	{
		// 1. erratic movement
		//  - current
		//  - current + treshold distance
		//  - current + treshold time

		// 2. tweened movement based on erratic movement input


	
		public static NumericEmitter Of(Action<int, int> Output)
		{
			// we could apply a rule which says
			// if distance is too far just teleport to it without animating
			// we are using sliding target to avoid single vector movement between events

			const int DefaultFrameRate = 1000 / 30;
			const double DefaultDivider = 6;
			const int DefaultEndConditionDelta = 2;

			var n = new NumericEmitter();

			n.Output = Output;

			double nx = 0;
			double ny = 0;

			double tx = 0;
			double ty = 0;

			bool dirty = false;

			#region GetDistance
			Func<double, double, double> GetDistance =
				(dx, dy) =>
				{
					return Math.Sqrt(dx * dx + dy * dy);
				};
			#endregion


			var Stop = default(Action);

			n.Input =
				(x, y) =>
				{
					tx = x;
					ty = y;

					if (!dirty)
					{
						dirty = true;

						nx = tx;
						ny = ty;

						n.Output(
									x,
									y
								);

						return;
					}

					if (Stop == null)
						Stop = DefaultFrameRate.AtInterval(
							delegate
							{
								var dx = tx - nx;
								var dy = ty - ny;

								if (GetDistance(dx, dy) < DefaultEndConditionDelta)
								{
									Stop();
									Stop = null;

									nx = tx;
									ny = ty;
								}
								else
								{
									nx += dx / DefaultDivider;
									ny += dy / DefaultDivider;
								}

								n.Output(
									Convert.ToInt32(nx), 
									Convert.ToInt32(ny)
								);
							}
						).Stop;
				};

			return n;

		}



	}
}
