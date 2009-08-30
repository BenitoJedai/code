// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.geom;
using java.awt.image;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/BufferedImageOp.html
	[Script(IsNative = true)]
	public interface BufferedImageOp
	{
		/// <summary>
		/// Creates a zeroed destination image with the correct size and number of
		/// bands.
		/// </summary>
		BufferedImage createCompatibleDestImage(BufferedImage @src, ColorModel @destCM);

		/// <summary>
		/// Performs a single-input/single-output operation on a
		/// <CODE>BufferedImage</CODE>.
		/// </summary>
		BufferedImage filter(BufferedImage @src, BufferedImage @dest);

		/// <summary>
		/// Returns the bounding box of the filtered destination image.
		/// </summary>
		Rectangle2D getBounds2D(BufferedImage @src);

		/// <summary>
		/// Returns the location of the corresponding destination point given a
		/// point in the source image.
		/// </summary>
		Point2D getPoint2D(Point2D @srcPt, Point2D @dstPt);

		/// <summary>
		/// Returns the rendering hints for this operation.
		/// </summary>
		RenderingHints getRenderingHints();

	}
}

