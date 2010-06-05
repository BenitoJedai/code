using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InteractiveAffineCube.AffineEngine;

namespace InteractiveAffineCube.AffineEngine
{
	public class AffinePoint
	{
		public double X;
		public double Y;
		public double Z;

		public object Tag;

		public AffinePoint()
			: this(0, 0, 0)
		{

		}

		public AffinePoint(double X, double Y, double Z)
		{
			this.X = X;
			this.Y = Y;
			this.Z = Z;

		}

		public static AffinePoint operator +(AffinePoint a, AffinePoint b)
		{
			return new AffinePoint
			{
				X = a.X + b.X,
				Y = a.Y + b.Y,
				Z = a.Z + b.Z,
			};
		}

		public static AffinePoint operator *(AffinePoint a, AffineZoom b)
		{
			return new AffinePoint
			{
				X = a.X * b.X,
				Y = a.Y * b.Y,
				Z = a.Z * b.Z,
			};
		}
	}
}
