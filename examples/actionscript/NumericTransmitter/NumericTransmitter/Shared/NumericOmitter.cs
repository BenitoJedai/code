using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Windows;
using ScriptCoreLib.Shared.Avalon.Extensions;

namespace Mahjong.NetworkCode.ClientSide.Shared
{
	[Script]
	public class NumericOmitter : NumericTransmitter
	{

		public static NumericOmitter Of(Action<int, int> Output)
		{
			// possbible parameters:
			const int DefaultTresholdDistance = 8;
			const int DefaultTresholdTimeout = 300;
			const int DefaultUpdateTimeout = 150;

			// we are not going to send updates when the happen too often
			// or they are too near to last update 

			var _x = 0;
			var _y = 0;
			var _dirty = false;

			#region our test that allows us to decide when to drop an event
			Func<int, int, int> GetDistance =
				(x, y) =>
				{
					var dx = x - _x;
					var dy = y - _y;

					return Convert.ToInt32(Math.Sqrt(dx * dx + dy * dy));
				};
			#endregion


			var StopInputViaTimeout = default(Action);

			Action<int, int> OutputAndRemember =
				(x, y) =>
				{
					#region we need to stop any pending timeouts
					if (StopInputViaTimeout != null)
					{
						StopInputViaTimeout();
						StopInputViaTimeout = null;
					}
					#endregion


					_x = x;
					_y = y;
					_dirty = true;

					Output(x, y);
				};

			Action DelayedOutput = delegate { };
			Action StopDelayedOutput = null;

			return new NumericOmitter
			{
				Output = OutputAndRemember,
				Input =
					(x, y) =>
					{
						// we need drop events

						if (StopDelayedOutput == null)
						{
							StopDelayedOutput = DefaultUpdateTimeout.AtDelay(
								delegate
								{
									DelayedOutput();
									StopDelayedOutput = null;
								}
							).Stop;

							if (_dirty)
								if (GetDistance(x, y) < DefaultTresholdDistance)
								{
									DelayedOutput = () => OutputAndRemember(x, y);

									return;
								}

							OutputAndRemember(x, y);
						}
						else
						{
							DelayedOutput = () => OutputAndRemember(x, y);
						}

					}
			};

		}


	}
}
