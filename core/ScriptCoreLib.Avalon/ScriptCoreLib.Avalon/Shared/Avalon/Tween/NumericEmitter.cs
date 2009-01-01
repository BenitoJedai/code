using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace ScriptCoreLib.Shared.Avalon.Tween
{
	[Script]
	public class NumericEmitter : NumericTransmitter
	{
		// 1. erratic movement
		//  - current
		//  - current + treshold distance
		//  - current + treshold time

		// 2. tweened movement based on erratic movement input

		public event Action InputComplete;

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


						if (n.InputComplete != null)
							n.InputComplete();

						return;
					}

					if (Stop == null)
						Stop = DefaultFrameRate.AtInterval(
							delegate
							{
								var dx = tx - nx;
								var dy = ty - ny;
								var TriggerInputComplete = false;
								if (GetDistance(dx, dy) < DefaultEndConditionDelta)
								{
									Stop();
									Stop = null;
									TriggerInputComplete = true;

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

								if (TriggerInputComplete)
								{

									if (n.InputComplete != null)
										n.InputComplete();

								}
							}
						).Stop;
				};

			return n;

		}


		public static implicit operator Action<int, int, Action>(NumericEmitter e)
		{
			Action<int, int> move = e;


			return (x, y, done) =>
			{
				if (done != null)
				{
					var once = default(Action);


					once =
						delegate
						{
							done();

							e.InputComplete -= once;
						};

					e.InputComplete += once;
				}

				move(x, y);
			};
		}

		public static Action<double, double> OfDouble(Action<double, double> h)
		{
			return OfDouble(h, 0.01);
		}

		public static Action<double, double> OfDouble(Action<double, double> h, double scale)
		{
			Action<int, int> u = NumericEmitter.Of(
				(x, y) =>
				{
					h(x * scale, y * scale);
				}
			);

			return
				(x, y) =>
				{
					u(Convert.ToInt32(x / scale), Convert.ToInt32(y / scale));
				};

		}
	}

}
