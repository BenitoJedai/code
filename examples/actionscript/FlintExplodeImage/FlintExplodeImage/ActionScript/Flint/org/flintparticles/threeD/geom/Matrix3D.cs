using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace FlintExplodeImage.ActionScript.Flint.org.flintparticles.threeD.geom
{
	[Script(IsNative = true)]
	public class Matrix3D
	{
		#region Fields
		/// <summary>
		/// [read-only] The determinant of the matrix
		/// </summary>
		public readonly double determinant;

		/// <summary>
		/// [read-only] The inverse of this matrix, or null if no inverse exists
		/// </summary>
		public readonly Matrix3D inverse;

		/// <summary>
		/// The value in row 1 column 1 of the matrix.
		/// </summary>
		public double n11;

		/// <summary>
		/// The value in row 1 column 2 of the matrix.
		/// </summary>
		public double n12;

		/// <summary>
		/// The value in row 1 column 3 of the matrix.
		/// </summary>
		public double n13;

		/// <summary>
		/// The value in row 1 column 4 of the matrix.
		/// </summary>
		public double n14;

		/// <summary>
		/// The value in row 2 column 1 of the matrix.
		/// </summary>
		public double n21;

		/// <summary>
		/// The value in row 2 column 2 of the matrix.
		/// </summary>
		public double n22;

		/// <summary>
		/// The value in row 2 column 3 of the matrix.
		/// </summary>
		public double n23;

		/// <summary>
		/// The value in row 2 column 4 of the matrix.
		/// </summary>
		public double n24;

		/// <summary>
		/// The value in row 3 column 1 of the matrix.
		/// </summary>
		public double n31;

		/// <summary>
		/// The value in row 3 column 2 of the matrix.
		/// </summary>
		public double n32;

		/// <summary>
		/// The value in row 3 column 3 of the matrix.
		/// </summary>
		public double n33;

		/// <summary>
		/// The value in row 3 column 4 of the matrix.
		/// </summary>
		public double n34;

		/// <summary>
		/// The value in row 4 column 1 of the matrix.
		/// </summary>
		public double n41;

		/// <summary>
		/// The value in row 4 column 2 of the matrix.
		/// </summary>
		public double n42;

		/// <summary>
		/// The value in row 4 column 3 of the matrix.
		/// </summary>
		public double n43;

		/// <summary>
		/// The value in row 4 column 4 of the matrix.
		/// </summary>
		public double n44;

		/// <summary>
		/// The positionelements of the matrix.
		/// </summary>
		public Vector3D position;

		/// <summary>
		/// An array containing the sixteen values in the matrix, in row-major form.
		/// </summary>
		public Array rawData;

		#endregion
		#region Methods
		/// <summary>
		/// Add another transformation matrix to this one, applying the new transformation after the transformations already in this matrix.
		/// </summary>
		public Matrix3D append(Matrix3D m)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Append a coordinate system transformation such that the vectors indicated are transformed to the x, y and z axes.
		/// </summary>
		public Matrix3D appendBasisTransform(Vector3D axisX, Vector3D axisY, Vector3D axisZ)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Append a rotation about an axis transformation to this matrix, applying the rotation after the transformations already in this matrix.
		/// </summary>
		public Matrix3D appendRotate(double angle, Vector3D axis, Vector3D pivotPoint)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Append a rotation about an axis transformation to this matrix, applying the rotation after the transformations already in this matrix.
		/// </summary>
		public Matrix3D appendRotate(double angle, Vector3D axis)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Append a scale transformation to this matrix, applying the scale after the transformations already in this matrix.
		/// </summary>
		public Matrix3D appendScale(double scaleX, double scaleY, double scaleZ)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Append a translation transformation to this matrix, applying the translation after the transformations already in this matrix.
		/// </summary>
		public Matrix3D appendTranslation(double x, double y, double z)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Make a duplicate of this matrix
		/// </summary>
		public Matrix3D clone()
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Copy another matrix into this one
		/// </summary>
		public Matrix3D copy(Matrix3D m)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Compare another matrix with this one
		/// </summary>
		public bool equals(Matrix3D m)
		{
			return default(bool);
		}

		/// <summary>
		/// Invert this matrix.
		/// </summary>
		public Matrix3D invert()
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Compare another matrix with this one
		/// </summary>
		public bool nearEquals(Matrix3D m, double e)
		{
			return default(bool);
		}

		/// <summary>
		/// [static] Creates a coordinate system transformation such that the vectors indicated are transformed to the x, y and z axes.
		/// </summary>
		public static Matrix3D newBasisTransform(Vector3D axisX, Vector3D axisY, Vector3D axisZ)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// [static] Creates a new Matrix3D for rotation about an axis.
		/// </summary>
		public static Matrix3D newRotate(double angle, Vector3D axis, Vector3D pivotPoint)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// [static] Creates a new Matrix3D for rotation about an axis.
		/// </summary>
		public static Matrix3D newRotate(double angle, Vector3D axis)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// [static] Creates a new Matrix3D for scaling.
		/// </summary>
		public static Matrix3D newScale(double scaleX, double scaleY, double scaleZ)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// [static] Creates a new Matrix3D for translation.
		/// </summary>
		public static Matrix3D newTranslation(double x, double y, double z)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Add another transformation matrix to this one, applying the new transformation before the transformations already in this matrix.
		/// </summary>
		public Matrix3D prepend(Matrix3D m)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Prepend a coordinate system transformation such that the vectors indicated are transformed to the x, y and z axes.
		/// </summary>
		public Matrix3D prependBasisTransform(Vector3D axisX, Vector3D axisY, Vector3D axisZ)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Prepend a rotation about an axis transformation to this matrix, applying the rotation before the transformations already in this matrix.
		/// </summary>
		public Matrix3D prependRotate(double angle, Vector3D axis, Vector3D pivotPoint)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Prepend a rotation about an axis transformation to this matrix, applying the rotation before the transformations already in this matrix.
		/// </summary>
		public Matrix3D prependRotate(double angle, Vector3D axis)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Prepend a scale transformation to this matrix, applying the scale before the transformations already in this matrix.
		/// </summary>
		public Matrix3D prependScale(double scaleX, double scaleY, double scaleZ)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Prepend a translation transformation to this matrix, applying the translation before the transformations already in this matrix.
		/// </summary>
		public Matrix3D prependTranslation(double x, double y, double z)
		{
			return default(Matrix3D);
		}

		/// <summary>
		/// Transform an array of Vector3D objects using this matrix.
		/// </summary>
		public Vector3D[] transformArrayVectors(Vector3D[] vectors)
		{
			return default(Vector3D[]);
		}

		/// <summary>
		/// Transform an array of Vector3D objects using this matrix.
		/// </summary>
		public Vector3D[] transformArrayVectorsSelf(Vector3D[] vectors)
		{
			return default(Vector3D[]);
		}

		/// <summary>
		/// Transform a Vector3D using this matrix, returning a new, transformed vector.
		/// </summary>
		public Vector3D transformVector(Vector3D v)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Transform a Vector3D using this matrix, storing the result in a second vector.
		/// </summary>
		public Vector3D transformVectorOther(Vector3D v, Vector3D u)
		{
			return default(Vector3D);
		}

		/// <summary>
		/// Transform a Vector3D using this matrix, storing the result in the original vector.
		/// </summary>
		public Vector3D transformVectorSelf(Vector3D v)
		{
			return default(Vector3D);
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Creates a Matrix3D object from an array of numbers.
		/// </summary>
		public Matrix3D(double[] values)
		{
		}

		/// <summary>
		/// Creates a Matrix3D object from an array of numbers.
		/// </summary>
		public Matrix3D()
		{
		}

		#endregion

	}
}
