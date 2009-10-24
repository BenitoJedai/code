using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Drawing
{
	[Script(Implements = typeof(global::System.Drawing.Point))]
	internal class __Point
	{
		public __Point()
			: this(0, 0)
		{

		}

		public __Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		public int X { get; set; }
		public int Y { get; set; }
	}
}
