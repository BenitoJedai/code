using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.Shared.Avalon.Extensions;

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
			const int DefaultDivider = 4;
			const int DefaultTargetDivider = 4;
			const int DefaultEndConditionDelta = 1;

			var _x = 0;
			var _y = 0;
			var _dirty = false;

			var tx = 0;
			var ty = 0;

			var ttx = 0;
			var tty = 0;

			var StopTween = default(Action);
			var StopTargetTween = default(Action);

			return new NumericEmitter
			{
				Output = Output,
				Input =
					(x, y) =>
					{
						ttx = x;
						tty = y;

						//Console.WriteLine(new { x, y, _dirty }.ToString());

						// we need to interpolate events here
						if (_dirty)
						{
							if (StopTween == null)
								StopTween = DefaultFrameRate.AtInterval(
									delegate
									{
										var dx = (_x - tx);
										var dy = (_y - ty);

											if (Math.Abs(dx) <= DefaultEndConditionDelta)
												if (Math.Abs(dy) <= DefaultEndConditionDelta)
												{
													_x = tx;
													_y = ty;

													Output(_x, _y);

													StopTween();
													StopTween = null;


													return;
												}

										_x = tx + dx - dx / DefaultDivider;
										_y = ty + dy - dy / DefaultDivider;

										Output(_x, _y);
									}
								).Stop;

							if (StopTargetTween == null)
								StopTargetTween = DefaultFrameRate.AtIntervalWithCounter(
										c =>
										{
											var dx = (tx - ttx);
											var dy = (ty - tty);

											if (Math.Abs(dx) <= DefaultEndConditionDelta)
												if (Math.Abs(dy) <= DefaultEndConditionDelta)
												{
													tx = ttx;
													ty = tty;

													StopTargetTween();
													StopTargetTween = null;

													return;
												}

											tx = ttx + dx - dx / DefaultTargetDivider;
											ty = tty + dy - dy / DefaultTargetDivider;



											//Console.WriteLine(new { tx, ty, c, x, y }.ToString());
										}
									).Stop;
						}
						else
						{


							_dirty = true;
							_x = x;
							_y = y;

							tx = x;
							ty = y;

							Output(_x, _y);
						}
					}
			};
		}



	}
}
