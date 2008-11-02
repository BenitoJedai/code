using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Windows
{
	[Script(Implements = typeof(global::System.Windows.Point))]
	internal class __Point
	{
		public double X { get; set; }
		public double Y { get; set; }

		public __Point()
		{
			this.X = 0;
			this.Y = 0;
		}

		public static Vector operator -(__Point point1, __Point point2)
		{
			return new Vector { X = point1.X - point2.X, Y = point1.Y - point2.Y };
		}
	}
}
