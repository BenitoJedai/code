// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.image;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/PaintContext.html
	[Script(IsNative = true)]
	public interface PaintContext
	{
		/// <summary>
		/// Releases the resources allocated for the operation.
		/// </summary>
		void dispose();

		/// <summary>
		/// Returns the <code>ColorModel</code> of the output.
		/// </summary>
		ColorModel getColorModel();

		/// <summary>
		/// Returns a <code>Raster</code> containing the colors generated for
		/// the graphics operation.
		/// </summary>
		Raster getRaster(int @x, int @y, int @w, int @h);

	}
}

