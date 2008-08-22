using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlashTreasureHunt.ActionScript
{
	[Script]
	public class PointInt32
	{
		public int X;
		public int Y;

		public override string ToString()
		{
			return "{ X = " + X + ", Y = " + Y + " }";
		}

		public PointInt32 Clone()
		{
			return new PointInt32 { X = this.X, Y = this.Y };
		}
	}
}
