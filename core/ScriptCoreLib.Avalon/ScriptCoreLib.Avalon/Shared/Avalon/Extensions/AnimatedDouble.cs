﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.Shared.Avalon.Tween;

namespace ScriptCoreLib.Shared.Avalon.Extensions
{
	[Script]
	public class AnimatedDouble
	{
		public double Value;

		public readonly Action<double> SetTarget;

		public AnimatedDouble(double value)
		{
			var a = NumericEmitter.OfDouble((x, y) => this.Value = x);


			SetTarget = x => a(x, 0);
			SetTarget(value);
		}
	}
}
