using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InteractiveMatrixTransformF.AffineEngine;

namespace InteractiveMatrixTransformE.AffineEngine
{
	public static class AffineExtensions
	{
		public static Action<T> ToAction<T>(this T e, Action<T> h)
		{
			return h;
		}

		
		public static AffinePoint Zoom(this AffinePoint p, AffineZoom Zoom)
		{
			return new AffinePoint
			{
				X = p.X * Zoom.X,
				Y = p.Y * Zoom.Y,
				Z = p.Z * Zoom.Z,
				Tag = p.Tag
			};
		}

		public static AffinePoint Rotate(this AffinePoint p, AffineRotation Rotation)
		{
			return p.RotateXY(Rotation.XY).RotateYZ(Rotation.XZ).RotateXZ(Rotation.XZ);
		}

		public static AffinePoint RotateXY(this AffinePoint p, double Rotation)
		{
			// X,Y
			var r = new AffinePoint { X = p.X, Y = p.Y }.GetRotation() + Rotation;
			var z = new AffinePoint { X = p.X, Y = p.Y }.GetLength();

			var Z = p.Z;
			var X = Math.Cos(r) * z;
			var Y = Math.Sin(r) * z;

			return new AffinePoint { X = X, Y = Y, Z = Z, Tag = p.Tag };
		}

		public static AffinePoint RotateYZ(this AffinePoint p, double Rotation)
		{
			// Y,Z
			var r = new AffinePoint { X = p.Y, Y = p.Z }.GetRotation() + Rotation;
			var z = new AffinePoint { X = p.Y, Y = p.Z }.GetLength();

			var Z = Math.Sin(r) * z;
			var X = p.X;
			var Y = Math.Cos(r) * z;

			return new AffinePoint { X = X, Y = Y, Z = Z, Tag = p.Tag };
		}

		public static AffinePoint RotateXZ(this AffinePoint p, double Rotation)
		{
			// X,Z
			var r = new AffinePoint { X = p.X, Y = p.Z }.GetRotation() + Rotation;
			var z = new AffinePoint { X = p.X, Y = p.Z }.GetLength();

			var Z = Math.Sin(r) * z;
			var X = Math.Cos(r) * z;
			var Y = p.Y;

			return new AffinePoint { X = X, Y = Y, Z = Z, Tag = p.Tag };
		}

		public static double GetYawnLength(this AffinePoint p)
		{
			return Math.Sqrt(p.X * p.X + p.Z * p.Z);
		}

		public static double GetLength(this AffinePoint p)
		{
			return Math.Sqrt(p.X * p.X + p.Y * p.Y);
		}

		public static double DegreesToRadians(this int Degrees)
		{
			return (Math.PI * 2) * Degrees / 360;
		}

		public static int RadiansToDegrees(this double Arc)
		{
			return (int)(360 * Arc / (Math.PI * 2));
		}

		public static double GetRotation(this AffinePoint p)
		{
			var x = p.X;
			var y = p.Y;

			const double _180 = System.Math.PI;
			const double _90 = System.Math.PI / 2;
			const double _270 = System.Math.PI * 3 / 2;

			if (x == 0)
				if (y < 0)
					return _270;
				else if (y == 0)
					return 0;
				else
					return _90;

			if (y == 0)
				if (x < 0)
					return _180;
				else
					return 0;

			var a = System.Math.Atan(y / x);

			if (x < 0)
				a += _180;
			else if (y < 0)
				a += System.Math.PI * 2;


			return a;
		}

		public static double GetYawn(this AffinePoint p)
		{
			var x = p.X;
			var y = p.Z;

			const double _180 = System.Math.PI;
			const double _90 = System.Math.PI / 2;
			const double _270 = System.Math.PI * 3 / 2;

			if (x == 0)
				if (y < 0)
					return _270;
				else if (y == 0)
					return 0;
				else
					return _90;

			if (y == 0)
				if (x < 0)
					return _180;
				else
					return 0;

			var a = System.Math.Atan(y / x);

			if (x < 0)
				a += _180;
			else if (y < 0)
				a += System.Math.PI * 2;


			return a;
		}


	}
}
