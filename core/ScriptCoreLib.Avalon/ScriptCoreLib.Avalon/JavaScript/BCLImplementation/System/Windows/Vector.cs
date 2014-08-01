﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Windows
{
    // http://referencesource.microsoft.com/#WindowsBase/src/Base/System/Windows/Vector.cs

	[Script(Implements = typeof(global::System.Windows.Vector))]
	internal class __Vector
	{
		public double X { get; set; }
		public double Y { get; set; }

		public __Vector() : this(0, 0)
		{

		}

		public __Vector(double X, double Y)
		{
			this.X = X;
			this.Y = Y;
		}

		public double Length
		{
			get
			{
				return global::System.Math.Sqrt(LengthSquared);
			}
		}


		public double LengthSquared
		{
			get
			{
				return ((this.X * this.X) + (this.Y * this.Y));
			}
		}
 
	}
}
