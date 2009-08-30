// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.font;
using java.awt.geom;
using java.lang;

namespace java.awt.font
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/font/FontRenderContext.html
	[Script(IsNative = true)]
	public class FontRenderContext
	{
		/// <summary>
		/// Constructs a new <code>FontRenderContext</code>
		/// object.
		/// </summary>
		public FontRenderContext()
		{
		}

		/// <summary>
		/// Constructs a <code>FontRenderContext</code> object from an
		/// optional <A HREF="../../../java/awt/geom/AffineTransform.html" title="class in java.awt.geom"><CODE>AffineTransform</CODE></A> and two <code>boolean</code>
		/// values that determine if the newly constructed object has
		/// anti-aliasing or fractional metrics.
		/// </summary>
		public FontRenderContext(AffineTransform @tx, bool @isAntiAliased, bool @usesFractionalMetrics)
		{
		}

		/// <summary>
		/// Return true if rhs has the same transform, antialiasing,
		/// and fractional metrics values as this.
		/// </summary>
		public bool Equals(FontRenderContext @rhs)
		{
			return default(bool);
		}

		/// <summary>
		/// Return true if obj is an instance of FontRenderContext and has the same
		/// transform, antialiasing, and fractional metrics values as this.
		/// </summary>
		public override bool Equals(object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the transform that is used to scale typographical points
		/// to pixels in this <code>FontRenderContext</code>.
		/// </summary>
		public AffineTransform getTransform()
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Return a hashcode for this FontRenderContext.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the text anti-aliasing mode used in this
		/// <code>FontRenderContext</code>.
		/// </summary>
		public bool isAntiAliased()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the text fractional metrics mode requested by the application
		/// for use in this <code>FontRenderContext</code>.
		/// </summary>
		public bool usesFractionalMetrics()
		{
			return default(bool);
		}

	}
}

