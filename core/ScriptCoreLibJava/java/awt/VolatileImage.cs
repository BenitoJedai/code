// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.image;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/VolatileImage.html
	[Script(IsNative = true)]
	public abstract class VolatileImage : Image
	{
		/// <summary>
		/// 
		/// </summary>
		public VolatileImage()
		{
		}

		/// <summary>
		/// Returns <code>true</code> if rendering data was lost since last
		/// <code>validate</code> call.
		/// </summary>
		abstract public bool contentsLost();

		/// <summary>
		/// Creates a <code>Graphics2D</code>, which can be used to draw into
		/// this <code>VolatileImage</code>.
		/// </summary>
		public Graphics2D createGraphics()
		{
			return default(Graphics2D);
		}

		/// <summary>
		/// Releases system resources currently consumed by this image.
		/// </summary>
		public void flush()
		{
		}

		/// <summary>
		/// Returns an ImageCapabilities object which can be
		/// inquired as to the specific capabilities of this
		/// VolatileImage.
		/// </summary>
		public ImageCapabilities getCapabilities()
		{
			return default(ImageCapabilities);
		}

		/// <summary>
		/// This method returns a <A HREF="../../../java/awt/Graphics2D.html" title="class in java.awt"><CODE>Graphics2D</CODE></A>, but is here
		/// for backwards compatibility.
		/// </summary>
		public Graphics getGraphics()
		{
			return default(Graphics);
		}

		/// <summary>
		/// Returns the height of the <code>VolatileImage</code>.
		/// </summary>
		abstract public int getHeight();

		/// <summary>
		/// Returns a static snapshot image of this object.
		/// </summary>
		public BufferedImage getSnapshot()
		{
			return default(BufferedImage);
		}

		/// <summary>
		/// This returns an ImageProducer for this VolatileImage.
		/// </summary>
		public ImageProducer getSource()
		{
			return default(ImageProducer);
		}

		/// <summary>
		/// Returns the width of the <code>VolatileImage</code>.
		/// </summary>
		abstract public int getWidth();

		/// <summary>
		/// Attempts to restore the drawing surface of the image if the surface
		/// had been lost since the last <code>validate</code> call.
		/// </summary>
		abstract public int validate(GraphicsConfiguration @gc);

	}
}

