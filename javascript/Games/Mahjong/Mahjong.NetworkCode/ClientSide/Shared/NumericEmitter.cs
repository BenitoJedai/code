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

		public Action<string> DiagnosticsWriteLine = delegate { };


		public static NumericEmitter Of(Action<int, int> Output)
		{
			// we could apply a rule which says
			// if distance is too far just teleport to it without animating
			// we are using sliding target to avoid single vector movement between events

			const int DefaultFrameRate = 1000 / 30;
			const double DefaultDivider = 6;
			const double DefaultTargetDivider = 4;
			const int DefaultEndConditionDelta = 2;

			var _x = 0;
			var _y = 0;
			var _dirty = false;

			var tx = 0;
			var ty = 0;

			var ttx = 0;
			var tty = 0;

			var StopTween = default(Action);
			var StopTargetTween = default(Action);

			var n = new NumericEmitter();

			n.Output = Output;

			n.Input =
				(x, y) =>
				{
					ttx = x;
					tty = y;

					//n.DiagnosticsWriteLine("emitter - new target : " + new { x, y, _dirty }.ToString());

					// we need to interpolate events here
					if (_dirty)
					{
						//n.DiagnosticsWriteLine("emitter - new silding target setup ");

						Action StartTween =
							delegate
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

													//Output(_x, _y);

												
													n.DiagnosticsWriteLine("emitter - done : " + new { _x, _y }.ToString());

													StopTween();
													StopTween = null;


													return;
												}

											_x = tx + Convert.ToInt32(dx - dx / DefaultDivider);
											_y = ty + Convert.ToInt32(dy - dy / DefaultDivider);

											Output(_x, _y);
										}
									).Stop;
								else
									n.DiagnosticsWriteLine("emitter - already running");

							};

						if (StopTargetTween == null)
							StopTargetTween = DefaultFrameRate.AtInterval(
								delegate
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

											//StartTween();

											return;
										}

									tx = ttx +  Convert.ToInt32(dx - dx / DefaultTargetDivider);
									ty = tty +  Convert.ToInt32(dy - dy / DefaultTargetDivider);

									StartTween();

									//n.DiagnosticsWriteLine("emitter - new silding target : " + new { tx, ty }.ToString());

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
				};

			return n;

		}



	}
}
