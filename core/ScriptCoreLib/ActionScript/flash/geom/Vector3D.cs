using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.geom
{
	// http://livedocs.adobe.com/flex/3/langref/flash/geom/Vector3D.html
	[Script(IsNative = true)]
	public class Vector3D
	{
		#region Properties
		/// <summary>
		/// [read-only] The length, magnitude, of the current Vector3D object from the origin (0,0,0) to the object's x, y, and z coordinates.
		/// </summary>
		public double length { get; private set; }

		/// <summary>
		/// [read-only] The square of the length of the current Vector3D object, calculated using the x, y, and z properties.
		/// </summary>
		public double lengthSquared { get; private set; }

		/// <summary>
		/// The fourth element of a Vector3D object (in addition to the x, y, and z properties) can hold data such as the angle of rotation.
		/// </summary>
		public double w { get; set; }

		/// <summary>
		/// The first element of a Vector3D object, such as the x coordinate of a point in the three-dimensional space.
		/// </summary>
		public double x { get; set; }

		/// <summary>
		/// The second element of a Vector3D object, such as the y coordinate of a point in the three-dimensional space.
		/// </summary>
		public double y { get; set; }

		/// <summary>
		/// The third element of a Vector3D object, such as the z coordinate of a point in three-dimensional space.
		/// </summary>
		public double z { get; set; }

		#endregion

		#region Methods
		/// <summary>
		/// Adds the value of the x, y, and z elements of the current Vector3D object to the values of the x, y, and z elements of another Vector3D object.
		/// </summary>
		public Vector3D add(Vector3D a)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// [static] Returns the angle in radians between two vectors.
		/// </summary>
		public static double angleBetween(Vector3D a, Vector3D b)
		{
			return default(double);
		}

		/// <summary>
		/// Returns a new Vector3D object that is an exact copy of the current Vector3D object.
		/// </summary>
		public Vector3D clone()
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Returns a new Vector3D object that is perpendicular (at a right angle) to the current Vector3D and another Vector3D object.
		/// </summary>
		public Vector3D crossProduct(Vector3D a)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Decrements the value of the x, y, and z elements of the current Vector3D object by the values of the x, y, and z elements of specified Vector3D object.
		/// </summary>
		public void decrementBy(Vector3D a)
		{
		}

		/// <summary>
		/// [static] Returns the distance between two Vector3D objects.
		/// </summary>
		public static double distance(Vector3D pt1, Vector3D pt2)
		{
			return default(double);
		}

		/// <summary>
		/// If the current Vector3D object and the one specified as the parameter are unit vertices, this method returns the cosine of the angle between the two vertices.
		/// </summary>
		public double dotProduct(Vector3D a)
		{
			return default(double);
		}

		/// <summary>
		/// Determines whether two Vector3D objects are equal by comparing the x, y, and z elements of the current Vector3D object with a specified Vector3D object.
		/// </summary>
		public bool equals(Vector3D toCompare, bool allFour)
		{
			return default(bool);
		}

		/// <summary>
		/// Determines whether two Vector3D objects are equal by comparing the x, y, and z elements of the current Vector3D object with a specified Vector3D object.
		/// </summary>
		public bool equals(Vector3D toCompare)
		{
			return default(bool);
		}

		/// <summary>
		/// Increments the value of the x, y, and z elements of the current Vector3D object by the values of the x, y, and z elements of a specified Vector3D object.
		/// </summary>
		public void incrementBy(Vector3D a)
		{
		}

		/// <summary>
		/// Compares the elements of the current Vector3D object with the elements of a specified Vector3D object to determine whether they are nearly equal.
		/// </summary>
		public bool nearEquals(Vector3D toCompare, double tolerance, bool allFour)
		{
			return default(bool);
		}

		/// <summary>
		/// Compares the elements of the current Vector3D object with the elements of a specified Vector3D object to determine whether they are nearly equal.
		/// </summary>
		public bool nearEquals(Vector3D toCompare, double tolerance)
		{
			return default(bool);
		}

		/// <summary>
		/// Sets the current Vector3D object to its inverse.
		/// </summary>
		public void negate()
		{
		}

		/// <summary>
		/// Converts a Vector3D object to a unit vector by dividing the first three elements (x, y, z) by the length of the vector.
		/// </summary>
		public double normalize()
		{
			return default(double);
		}

		/// <summary>
		/// Divides the value of the x, y, and z properties of the current Vector3D object by the value of its w property.
		/// </summary>
		public void project()
		{
		}

		/// <summary>
		/// Scales the current Vector3D object by a scalar, a magnitude.
		/// </summary>
		public void scaleBy(double s)
		{
		}

		/// <summary>
		/// Subtracts the value of the x, y, and z elements of the current Vector3D object from the values of the x, y, and z elements of another Vector3D object.
		/// </summary>
		public Vector3D subtract(Vector3D a)
		{
			return default(Vector3D);
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Creates an instance of a Vector3D object.
		/// </summary>
		public Vector3D(double x, double y, double z, double w)
		{
		}

		/// <summary>
		/// Creates an instance of a Vector3D object.
		/// </summary>
		public Vector3D(double x, double y, double z)
		{
		}

		/// <summary>
		/// Creates an instance of a Vector3D object.
		/// </summary>
		public Vector3D(double x, double y)
		{
		}

		/// <summary>
		/// Creates an instance of a Vector3D object.
		/// </summary>
		public Vector3D(double x)
		{
		}

		/// <summary>
		/// Creates an instance of a Vector3D object.
		/// </summary>
		public Vector3D()
		{
		}

		#endregion

		#region Constants
		/// <summary>
		/// [static] The x axis defined as a Vector3D object with coordinates (1,0,0).
		/// </summary>
		public static readonly Vector3D X_AXIS;

		/// <summary>
		/// [static] The y axis defined as a Vector3D object with coordinates (0,1,0).
		/// </summary>
		public static readonly Vector3D Y_AXIS;

		/// <summary>
		/// [static] The z axis defined as a Vector3D object with coordinates (0,0,1).
		/// </summary>
		public static readonly Vector3D Z_AXIS;

		#endregion

	}
}
