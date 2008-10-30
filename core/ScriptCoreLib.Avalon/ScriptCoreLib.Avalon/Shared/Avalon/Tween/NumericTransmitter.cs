using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLib.Shared.Avalon.Tween
{
	[Script]
	public class NumericTransmitter
	{
		internal Action<int, int> Output;
		internal Action<int, int> Input;

		public static implicit operator Action<int, int>(NumericTransmitter e)
		{
			return e.Input;
		}
	}
}
