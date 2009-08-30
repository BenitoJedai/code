// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.geom;
using java.lang;

namespace java.awt.geom
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/geom/Dimension2D.html
	[Script(IsNative = true)]
	public abstract class Dimension2D
	{
		/// <summary>
		/// This is an abstract class that cannot be instantiated directly.
		/// </summary>
		public Dimension2D()
		{
		}

		/// <summary>
		/// Creates a new object of the same class as this object.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the height of this <code>Dimension</code> in double
		/// precision.
		/// </summary>
		abstract public double getHeight();

		/// <summary>
		/// Returns the width of this <code>Dimension</code> in double
		/// precision.
		/// </summary>
		abstract public double getWidth();

		/// <summary>
		/// Sets the size of this <code>Dimension2D</code> object to
		/// match the specified size.
		/// </summary>
		public void setSize(Dimension2D @d)
		{
		}

		/// <summary>
		/// Sets the size of this <code>Dimension</code> object to the
		/// specified width and height.
		/// </summary>
		abstract public void setSize(double @width, double @height);

	}
}

