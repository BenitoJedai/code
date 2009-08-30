// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.geom;
using java.lang;

namespace java.awt.geom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/geom/AffineTransform.html
	[Script(IsNative = true)]
	public class AffineTransform
	{
		/// <summary>
		/// Constructs a new <code>AffineTransform</code> representing the
		/// Identity transformation.
		/// </summary>
		public AffineTransform()
		{
		}

		/// <summary>
		/// Constructs a new <code>AffineTransform</code> that is a copy of
		/// the specified <code>AffineTransform</code> object.
		/// </summary>
		public AffineTransform(AffineTransform @Tx)
		{
		}

		/// <summary>
		/// Constructs a new <code>AffineTransform</code> from an array of
		/// double precision values representing either the 4 non-translation
		/// entries or the 6 specifiable entries of the 3x3 transformation
		/// matrix.
		/// </summary>
		public AffineTransform(double[] @flatmatrix)
		{
		}

		/// <summary>
		/// Constructs a new <code>AffineTransform</code> from 6 double
		/// precision values representing the 6 specifiable entries of the 3x3
		/// transformation matrix.
		/// </summary>
		public AffineTransform(double @m00, double @m10, double @m01, double @m11, double @m02, double @m12)
		{
		}

		/// <summary>
		/// Constructs a new <code>AffineTransform</code> from an array of
		/// floating point values representing either the 4 non-translation
		/// enries or the 6 specifiable entries of the 3x3 transformation
		/// matrix.
		/// </summary>
		public AffineTransform(float[] @flatmatrix)
		{
		}

		/// <summary>
		/// Constructs a new <code>AffineTransform</code> from 6 floating point
		/// values representing the 6 specifiable entries of the 3x3
		/// transformation matrix.
		/// </summary>
		public AffineTransform(float @m00, float @m10, float @m01, float @m11, float @m02, float @m12)
		{
		}

		/// <summary>
		/// Returns a copy of this <code>AffineTransform</code> object.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Concatenates an <code>AffineTransform</code> <code>Tx</code> to
		/// this <code>AffineTransform</code> Cx in the most commonly useful
		/// way to provide a new user space
		/// that is mapped to the former user space by <code>Tx</code>.
		/// </summary>
		public void concatenate(AffineTransform @Tx)
		{
		}

		/// <summary>
		/// Returns an <code>AffineTransform</code> object representing the
		/// inverse transformation.
		/// </summary>
		public AffineTransform createInverse()
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Returns a new <A HREF="../../../java/awt/Shape.html" title="interface in java.awt"><CODE>Shape</CODE></A> object defined by the geometry of the
		/// specified <code>Shape</code> after it has been transformed by
		/// this transform.
		/// </summary>
		public Shape createTransformedShape(Shape @pSrc)
		{
			return default(Shape);
		}

		/// <summary>
		/// Transforms an array of relative distance vectors by this
		/// transform.
		/// </summary>
		public void deltaTransform(double[] @srcPts, int @srcOff, double[] @dstPts, int @dstOff, int @numPts)
		{
		}

		/// <summary>
		/// Transforms the relative distance vector specified by
		/// <code>ptSrc</code> and stores the result in <code>ptDst</code>.
		/// </summary>
		public Point2D deltaTransform(Point2D @ptSrc, Point2D @ptDst)
		{
			return default(Point2D);
		}

		/// <summary>
		/// Returns <code>true</code> if this <code>AffineTransform</code>
		/// represents the same affine coordinate transform as the specified
		/// argument.
		/// </summary>
		public override bool Equals(object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the determinant of the matrix representation of the transform.
		/// </summary>
		public double getDeterminant()
		{
			return default(double);
		}

		/// <summary>
		/// Retrieves the 6 specifiable values in the 3x3 affine transformation
		/// matrix and places them into an array of double precisions values.
		/// </summary>
		public void getMatrix(double[] @flatmatrix)
		{
		}

		/// <summary>
		/// Returns a transform representing a rotation transformation.
		/// </summary>
		public AffineTransform getRotateInstance(double @theta)
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Returns a transform that rotates coordinates around an anchor point.
		/// </summary>
		public AffineTransform getRotateInstance(double @theta, double @x, double @y)
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Returns a transform representing a scaling transformation.
		/// </summary>
		public AffineTransform getScaleInstance(double @sx, double @sy)
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Returns the X coordinate scaling element (m00) of the 3x3
		/// affine transformation matrix.
		/// </summary>
		public double getScaleX()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the Y coordinate scaling element (m11) of the 3x3
		/// affine transformation matrix.
		/// </summary>
		public double getScaleY()
		{
			return default(double);
		}

		/// <summary>
		/// Returns a transform representing a shearing transformation.
		/// </summary>
		public AffineTransform getShearInstance(double @shx, double @shy)
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Returns the X coordinate shearing element (m01) of the 3x3
		/// affine transformation matrix.
		/// </summary>
		public double getShearX()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the Y coordinate shearing element (m10) of the 3x3
		/// affine transformation matrix.
		/// </summary>
		public double getShearY()
		{
			return default(double);
		}

		/// <summary>
		/// Returns a transform representing a translation transformation.
		/// </summary>
		public AffineTransform getTranslateInstance(double @tx, double @ty)
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Returns the X coordinate of the translation element (m02) of the
		/// 3x3 affine transformation matrix.
		/// </summary>
		public double getTranslateX()
		{
			return default(double);
		}

		/// <summary>
		/// Returns the Y coordinate of the translation element (m12) of the
		/// 3x3 affine transformation matrix.
		/// </summary>
		public double getTranslateY()
		{
			return default(double);
		}

		/// <summary>
		/// Retrieves the flag bits describing the conversion properties of
		/// this transform.
		/// </summary>
		public int getType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the hashcode for this transform.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Inverse transforms an array of double precision coordinates by
		/// this transform.
		/// </summary>
		public void inverseTransform(double[] @srcPts, int @srcOff, double[] @dstPts, int @dstOff, int @numPts)
		{
		}

		/// <summary>
		/// Inverse transforms the specified <code>ptSrc</code> and stores the
		/// result in <code>ptDst</code>.
		/// </summary>
		public Point2D inverseTransform(Point2D @ptSrc, Point2D @ptDst)
		{
			return default(Point2D);
		}

		/// <summary>
		/// Returns <code>true</code> if this <code>AffineTransform</code> is
		/// an identity transform.
		/// </summary>
		public bool isIdentity()
		{
			return default(bool);
		}

		/// <summary>
		/// Concatenates an <code>AffineTransform</code> <code>Tx</code> to
		/// this <code>AffineTransform</code> Cx
		/// in a less commonly used way such that <code>Tx</code> modifies the
		/// coordinate transformation relative to the absolute pixel
		/// space rather than relative to the existing user space.
		/// </summary>
		public void preConcatenate(AffineTransform @Tx)
		{
		}

		/// <summary>
		/// Concatenates this transform with a rotation transformation.
		/// </summary>
		public void rotate(double @theta)
		{
		}

		/// <summary>
		/// Concatenates this transform with a transform that rotates
		/// coordinates around an anchor point.
		/// </summary>
		public void rotate(double @theta, double @x, double @y)
		{
		}

		/// <summary>
		/// Concatenates this transform with a scaling transformation.
		/// </summary>
		public void scale(double @sx, double @sy)
		{
		}

		/// <summary>
		/// Resets this transform to the Identity transform.
		/// </summary>
		public void setToIdentity()
		{
		}

		/// <summary>
		/// Sets this transform to a rotation transformation.
		/// </summary>
		public void setToRotation(double @theta)
		{
		}

		/// <summary>
		/// Sets this transform to a translated rotation transformation.
		/// </summary>
		public void setToRotation(double @theta, double @x, double @y)
		{
		}

		/// <summary>
		/// Sets this transform to a scaling transformation.
		/// </summary>
		public void setToScale(double @sx, double @sy)
		{
		}

		/// <summary>
		/// Sets this transform to a shearing transformation.
		/// </summary>
		public void setToShear(double @shx, double @shy)
		{
		}

		/// <summary>
		/// Sets this transform to a translation transformation.
		/// </summary>
		public void setToTranslation(double @tx, double @ty)
		{
		}

		/// <summary>
		/// Sets this transform to a copy of the transform in the specified
		/// <code>AffineTransform</code> object.
		/// </summary>
		public void setTransform(AffineTransform @Tx)
		{
		}

		/// <summary>
		/// Sets this transform to the matrix specified by the 6
		/// double precision values.
		/// </summary>
		public void setTransform(double @m00, double @m10, double @m01, double @m11, double @m02, double @m12)
		{
		}

		/// <summary>
		/// Concatenates this transform with a shearing transformation.
		/// </summary>
		public void shear(double @shx, double @shy)
		{
		}

		/// <summary>
		/// Returns a <code>String</code> that represents the value of this
		/// <A HREF="../../../java/lang/Object.html" title="class in java.lang"><CODE>Object</CODE></A>.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Transforms an array of double precision coordinates by this transform.
		/// </summary>
		public void transform(double[] @srcPts, int @srcOff, double[] @dstPts, int @dstOff, int @numPts)
		{
		}

		/// <summary>
		/// Transforms an array of double precision coordinates by this transform
		/// and stores the results into an array of floats.
		/// </summary>
		public void transform(double[] @srcPts, int @srcOff, float[] @dstPts, int @dstOff, int @numPts)
		{
		}

		/// <summary>
		/// Transforms an array of floating point coordinates by this transform
		/// and stores the results into an array of doubles.
		/// </summary>
		public void transform(float[] @srcPts, int @srcOff, double[] @dstPts, int @dstOff, int @numPts)
		{
		}

		/// <summary>
		/// Transforms an array of floating point coordinates by this transform.
		/// </summary>
		public void transform(float[] @srcPts, int @srcOff, float[] @dstPts, int @dstOff, int @numPts)
		{
		}

		/// <summary>
		/// Transforms an array of point objects by this transform.
		/// </summary>
		public void transform(Point2D[] @ptSrc, int @srcOff, Point2D[] @ptDst, int @dstOff, int @numPts)
		{
		}

		/// <summary>
		/// Transforms the specified <code>ptSrc</code> and stores the result
		/// in <code>ptDst</code>.
		/// </summary>
		public Point2D transform(Point2D @ptSrc, Point2D @ptDst)
		{
			return default(Point2D);
		}

		/// <summary>
		/// Concatenates this transform with a translation transformation.
		/// </summary>
		public void translate(double @tx, double @ty)
		{
		}

	}
}

