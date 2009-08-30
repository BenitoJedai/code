// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.geom;
using java.awt.image;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/GraphicsConfiguration.html
	[Script(IsNative = true)]
	public class GraphicsConfiguration
	{
		/// <summary>
		/// This is an abstract class that cannot be instantiated directly.
		/// </summary>
		public GraphicsConfiguration()
		{
		}

		/// <summary>
		/// Returns a <A HREF="../../java/awt/image/BufferedImage.html" title="class in java.awt.image"><CODE>BufferedImage</CODE></A> with a data layout and color model
		/// compatible with this <code>GraphicsConfiguration</code>.
		/// </summary>
		public BufferedImage createCompatibleImage(int @width, int @height)
		{
			return default(BufferedImage);
		}

		/// <summary>
		/// Returns a <code>BufferedImage</code> that supports the specified
		/// transparency and has a data layout and color model
		/// compatible with this <code>GraphicsConfiguration</code>.
		/// </summary>
		public BufferedImage createCompatibleImage(int @width, int @height, int @transparency)
		{
			return default(BufferedImage);
		}

		/// <summary>
		/// Returns a <A HREF="../../java/awt/image/VolatileImage.html" title="class in java.awt.image"><CODE>VolatileImage</CODE></A> with a data layout and color model
		/// compatible with this <code>GraphicsConfiguration</code>.
		/// </summary>
		public VolatileImage createCompatibleVolatileImage(int @width, int @height)
		{
			return default(VolatileImage);
		}

		/// <summary>
		/// Returns a <A HREF="../../java/awt/image/VolatileImage.html" title="class in java.awt.image"><CODE>VolatileImage</CODE></A> with a data layout and color model
		/// compatible with this <code>GraphicsConfiguration</code>, using
		/// the specified image capabilities.
		/// </summary>
		public VolatileImage createCompatibleVolatileImage(int @width, int @height, ImageCapabilities @caps)
		{
			return default(VolatileImage);
		}

		/// <summary>
		/// Returns the bounds of the <code>GraphicsConfiguration</code>
		/// in the device coordinates.
		/// </summary>
		public Rectangle getBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the buffering capabilities of this
		/// <code>GraphicsConfiguration</code>.
		/// </summary>
		public BufferCapabilities getBufferCapabilities()
		{
			return default(BufferCapabilities);
		}

		/// <summary>
		/// Returns the <A HREF="../../java/awt/image/ColorModel.html" title="class in java.awt.image"><CODE>ColorModel</CODE></A> associated with this
		/// <code>GraphicsConfiguration</code>.
		/// </summary>
		public ColorModel getColorModel()
		{
			return default(ColorModel);
		}

		/// <summary>
		/// Returns the <code>ColorModel</code> associated with this
		/// <code>GraphicsConfiguration</code> that supports the specified
		/// transparency.
		/// </summary>
		public ColorModel getColorModel(int @transparency)
		{
			return default(ColorModel);
		}

		/// <summary>
		/// Returns the default <A HREF="../../java/awt/geom/AffineTransform.html" title="class in java.awt.geom"><CODE>AffineTransform</CODE></A> for this
		/// <code>GraphicsConfiguration</code>.
		/// </summary>
		public AffineTransform getDefaultTransform()
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Returns the <A HREF="../../java/awt/GraphicsDevice.html" title="class in java.awt"><CODE>GraphicsDevice</CODE></A> associated with this
		/// <code>GraphicsConfiguration</code>.
		/// </summary>
		public GraphicsDevice getDevice()
		{
			return default(GraphicsDevice);
		}

		/// <summary>
		/// Returns the image capabilities of this
		/// <code>GraphicsConfiguration</code>.
		/// </summary>
		public ImageCapabilities getImageCapabilities()
		{
			return default(ImageCapabilities);
		}

		/// <summary>
		/// Returns a <code>AffineTransform</code> that can be concatenated
		/// with the default <code>AffineTransform</code>
		/// of a <code>GraphicsConfiguration</code> so that 72 units in user
		/// space equals 1 inch in device space.
		/// </summary>
		public AffineTransform getNormalizingTransform()
		{
			return default(AffineTransform);
		}

	}
}

