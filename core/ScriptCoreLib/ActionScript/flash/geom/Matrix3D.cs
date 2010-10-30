using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.flash.geom
{
	// http://help.adobe.com/en_US/AS3LCR/Flash_10.0/flash/geom/Matrix3D.html
	[Script(IsNative = true)]
	public class Matrix3D
	{
		// do we support vector.<> ?  

		#region Properties
		/// <summary>
		/// [read-only] A Number that determines whether a matrix is invertible.
		/// </summary>
		public double determinant { get; private set; }

		/// <summary>
		/// A Vector3D object that holds the position, the three-dimensional coordinate (x,y,z) of a display object within the transformation's frame of reference.
		/// </summary>
		public Vector3D position { get; set; }

		/// <summary>
		/// A Vector of 16 Numbers, where every four elements can be a row or a column of a 4x4 matrix.
		/// </summary>
		public Vector<double> rawData { get; set; }

		#endregion

		#region Methods
		/// <summary>
		/// Appends the matrix by multiplying another Matrix3D object by the current Matrix3D object.
		/// </summary>
		public void append(Matrix3D lhs)
		{
		}

		/// <summary>
		/// Appends an incremental rotation to a Matrix3D object.
		/// </summary>
		public void appendRotation(double degrees, Vector3D axis, Vector3D pivotPoint)
		{
		}

		/// <summary>
		/// Appends an incremental rotation to a Matrix3D object.
		/// </summary>
		public void appendRotation(double degrees, Vector3D axis)
		{
		}

		/// <summary>
		/// Appends an incremental scale change along the x, y, and z axes to a Matrix3D object.
		/// </summary>
		public void appendScale(double xScale, double yScale, double zScale)
		{
		}

		/// <summary>
		/// Appends an incremental translation, a repositioning along the x, y, and z axes, to a Matrix3D object.
		/// </summary>
		public void appendTranslation(double x, double y, double z)
		{
		}

		/// <summary>
		/// Returns a new Matrix3D object that is an exact copy of the current Matrix3D object.
		/// </summary>
		public Matrix3D clone()
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Returns the transformation matrix's translation, rotation, and scale settings as a Vector of three Vector3D objects.
		/// </summary>
		public Vector<Vector3D> decompose(string orientationStyle)
		{
			return default(Vector<Vector3D>);
		}

		/// <summary>
		/// Returns the transformation matrix's translation, rotation, and scale settings as a Vector of three Vector3D objects.
		/// </summary>
		public Vector<Vector3D> decompose()
		{
			return default(Vector<Vector3D>);
		}

		/// <summary>
		/// Uses the transformation matrix without its translation elements to transforms a Vector3D object from one space coordinate to another.
		/// </summary>
		public Vector3D deltaTransformVector(Vector3D v)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Converts the current matrix to an identity or unit matrix.
		/// </summary>
		public void identity()
		{
		}

		/// <summary>
		/// [static] Simplifies the interpolation from one frame of reference to another by interpolating a display object a percent point closer to a target display object.
		/// </summary>
		public static Matrix3D interpolate(Matrix3D thisMat, Matrix3D toMat, double percent)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Interpolates the display object's matrix a percent closer to a target's matrix.
		/// </summary>
		public void interpolateTo(Matrix3D toMat, double percent)
		{
		}

		/// <summary>
		/// Inverts the current matrix.
		/// </summary>
		public bool invert()
		{
			return default(bool);
		}

		/// <summary>
		/// Rotates the display object so that it faces a specified position.
		/// </summary>
		public void pointAt(Vector3D pos, Vector3D at, Vector3D up)
		{
		}

		/// <summary>
		/// Rotates the display object so that it faces a specified position.
		/// </summary>
		public void pointAt(Vector3D pos, Vector3D at)
		{
		}

		/// <summary>
		/// Rotates the display object so that it faces a specified position.
		/// </summary>
		public void pointAt(Vector3D pos)
		{
		}

		/// <summary>
		/// Prepends a matrix by multiplying the current Matrix3D object by another Matrix3D object.
		/// </summary>
		public void prepend(Matrix3D rhs)
		{
		}

		/// <summary>
		/// Prepends an incremental rotation to a Matrix3D object.
		/// </summary>
		public void prependRotation(double degrees, Vector3D axis, Vector3D pivotPoint)
		{
		}

		/// <summary>
		/// Prepends an incremental rotation to a Matrix3D object.
		/// </summary>
		public void prependRotation(double degrees, Vector3D axis)
		{
		}

		/// <summary>
		/// Prepends an incremental scale change along the x, y, and z axes to a Matrix3D object.
		/// </summary>
		public void prependScale(double xScale, double yScale, double zScale)
		{
		}

		/// <summary>
		/// Prepends an incremental translation, a repositioning along the x, y, and z axes, to a Matrix3D object.
		/// </summary>
		public void prependTranslation(double x, double y, double z)
		{
		}

		/// <summary>
		/// Sets the transformation matrix's translation, rotation, and scale setting.
		/// </summary>
		public bool recompose(Vector<Vector3D> components, string orientationStyle)
		{
			return default(bool);
		}

		/// <summary>
		/// Sets the transformation matrix's translation, rotation, and scale setting.
		/// </summary>
		public bool recompose(Vector<Vector3D> components)
		{
			return default(bool);
		}

		/// <summary>
		/// Uses the transformation matrix to transform a Vector3D object from one space coordinate to another.
		/// </summary>
		public Vector3D transformVector(Vector3D v)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Uses the transformation matrix to transform a Vector of Numbers from one coordinate space to another.
		/// </summary>
		public void transformVectors(Vector<Vector3D> vin, Vector<Vector3D> vout)
		{
		}

		/// <summary>
		/// Converts the current Matrix3D object to a matrix where the rows and columns are swapped.
		/// </summary>
		public void transpose()
		{
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Creates a Matrix3D object.
		/// </summary>
		public Matrix3D(Vector<double> v)
		{
		}

		/// <summary>
		/// Creates a Matrix3D object.
		/// </summary>
		public Matrix3D()
		{
		}

		#endregion

	}
}
