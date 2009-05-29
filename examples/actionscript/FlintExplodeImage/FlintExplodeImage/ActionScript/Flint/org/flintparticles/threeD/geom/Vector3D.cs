using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.geom
{
	[Script(IsNative = true)]
	public class Vector3D
	{
		#region Fields
		/// <summary>
		/// [read-only] The length of this vector.
		/// </summary>
		public readonly double length;

		/// <summary>
		/// [read-only] The square of the length of this vector.
		/// </summary>
		public readonly double lengthSquared;

		/// <summary>
		/// The w coordinate of the vector.
		/// </summary>
		public double w;

		/// <summary>
		/// The x coordinate of the vector.
		/// </summary>
		public double x;

		/// <summary>
		/// The y coordinate of the vector.
		/// </summary>
		public double y;

		/// <summary>
		/// The z coordinate of the vector.
		/// </summary>
		public double z;

		#endregion

		#region Methods
		/// <summary>
		/// Adds another vector to this one, returning the result.
		/// </summary>
		public Vector3D add(Vector3D v, Vector3D result)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Adds another vector to this one, returning the result.
		/// </summary>
		public Vector3D add(Vector3D v)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Copies another vector into this one.
		/// </summary>
		public Vector3D assign(Vector3D v)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Makes a copy of this Vector3D object.
		/// </summary>
		public Vector3D clone(Vector3D result)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Makes a copy of this Vector3D object.
		/// </summary>
		public Vector3D clone()
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Calculate the cross product of this vector with another.
		/// </summary>
		public Vector3D crossProduct(Vector3D v, Vector3D result)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Calculate the cross product of this vector with another.
		/// </summary>
		public Vector3D crossProduct(Vector3D v)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Subtract another vector from this one.
		/// </summary>
		public Vector3D decrementBy(Vector3D v)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// [static] Calculates the distance between the points represented by the input vectors.
		/// </summary>
		public static double distance(Vector3D v1, Vector3D v2)
		{
			return default(double);
		}

		/// <summary>
		/// [static] Calculates the square of the distance between the points represented by the input vectors.
		/// </summary>
		public static double distanceSquared(Vector3D u, Vector3D v)
		{
			return default(double);
		}

		/// <summary>
		/// Calculate the dot product of this vector with another.
		/// </summary>
		public double dotProduct(Vector3D v)
		{
			return default(double);
		}

		/// <summary>
		/// Compare this vector to another.
		/// </summary>
		public bool equals(Vector3D v)
		{
			return default(bool);
		}

		/// <summary>
		/// Add another vector to this one.
		/// </summary>
		public Vector3D incrementBy(Vector3D v)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Multiply this vector by a number, returning the result.
		/// </summary>
		public Vector3D multiply(double s, Vector3D result)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Multiply this vector by a number, returning the result.
		/// </summary>
		public Vector3D multiply(double s)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Compare this vector to another.
		/// </summary>
		public bool nearEquals(Vector3D v, double e)
		{
			return default(bool);
		}

		/// <summary>
		/// Negate this vector.
		/// </summary>
		public Vector3D negate()
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Get the negative of this vector - a vector the same length but in the opposite direction.
		/// </summary>
		public Vector3D negative(Vector3D result)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Get the negative of this vector - a vector the same length but in the opposite direction.
		/// </summary>
		public Vector3D negative()
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Convert this vector to have length 1.
		/// </summary>
		public Vector3D normalize()
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Divide all the coordinates in this position vector by the w coordinate, producing a vector with a w coordinate of 1.
		/// </summary>
		public Vector3D project()
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Assigns new coordinates to this vector
		/// </summary>
		public Vector3D reset(double x, double y, double z, double w)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Assigns new coordinates to this vector
		/// </summary>
		public Vector3D reset(double x, double y, double z)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Assigns new coordinates to this vector
		/// </summary>
		public Vector3D reset(double x, double y)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Assigns new coordinates to this vector
		/// </summary>
		public Vector3D reset(double x)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Assigns new coordinates to this vector
		/// </summary>
		public Vector3D reset()
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Multiply this vector by a number.
		/// </summary>
		public Vector3D scaleBy(double s)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Subtract another vector from this one, returning the result.
		/// </summary>
		public Vector3D subtract(Vector3D v, Vector3D result)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Subtract another vector from this one, returning the result.
		/// </summary>
		public Vector3D subtract(Vector3D v)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Create a unit vector in the same direction as this one.
		/// </summary>
		public Vector3D unit(Vector3D result)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Create a unit vector in the same direction as this one.
		/// </summary>
		public Vector3D unit()
		{
			return default(Vector3D);
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Constructor
		/// </summary>
		public Vector3D(double x, double y, double z, double w)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Vector3D(double x, double y, double z)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Vector3D(double x, double y)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Vector3D(double x)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public Vector3D()
		{
		}

		#endregion

	}
}
