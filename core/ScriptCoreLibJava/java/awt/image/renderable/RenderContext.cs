// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.geom;
using java.lang;

namespace java.awt.image.renderable
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/renderable/RenderContext.html
	[Script(IsNative = true)]
	public class RenderContext
	{
		/// <summary>
		/// Constructs a RenderContext with a given transform.
		/// </summary>
		public RenderContext(AffineTransform @usr2dev)
		{
		}

		/// <summary>
		/// Constructs a RenderContext with a given transform and rendering hints.
		/// </summary>
		public RenderContext(AffineTransform @usr2dev, RenderingHints @hints)
		{
		}

		/// <summary>
		/// Constructs a RenderContext with a given transform and area of interest.
		/// </summary>
		public RenderContext(AffineTransform @usr2dev, Shape @aoi)
		{
		}

		/// <summary>
		/// Constructs a RenderContext with a given transform.
		/// </summary>
		public RenderContext(AffineTransform @usr2dev, Shape @aoi, RenderingHints @hints)
		{
		}

		/// <summary>
		/// Makes a copy of a RenderContext.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Modifies the current user-to-device transform by appending another
		/// transform.
		/// </summary>
		public void concatenateTransform(AffineTransform @modTransform)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>replaced by
		/// <code>concatenateTransform(AffineTransform)</code>.</I>
		/// </summary>
		public void concetenateTransform(AffineTransform @modTransform)
		{
		}

		/// <summary>
		/// Gets the ares of interest currently contained in the
		/// RenderContext.
		/// </summary>
		public Shape getAreaOfInterest()
		{
			return default(Shape);
		}

		/// <summary>
		/// Gets the rendering hints of this <code>RenderContext</code>.
		/// </summary>
		public RenderingHints getRenderingHints()
		{
			return default(RenderingHints);
		}

		/// <summary>
		/// Gets the current user-to-device AffineTransform.
		/// </summary>
		public AffineTransform getTransform()
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Modifies the current user-to-device transform by prepending another
		/// transform.
		/// </summary>
		public void preConcatenateTransform(AffineTransform @modTransform)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>replaced by
		/// <code>preConcatenateTransform(AffineTransform)</code>.</I>
		/// </summary>
		public void preConcetenateTransform(AffineTransform @modTransform)
		{
		}

		/// <summary>
		/// Sets the current area of interest.
		/// </summary>
		public void setAreaOfInterest(Shape @newAoi)
		{
		}

		/// <summary>
		/// Sets the rendering hints of this <code>RenderContext</code>.
		/// </summary>
		public void setRenderingHints(RenderingHints @hints)
		{
		}

		/// <summary>
		/// Sets the current user-to-device AffineTransform contained
		/// in the RenderContext to a given transform.
		/// </summary>
		public void setTransform(AffineTransform @newTransform)
		{
		}

	}
}

