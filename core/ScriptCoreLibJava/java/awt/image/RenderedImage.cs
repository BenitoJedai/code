// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.image;
using java.lang;
using java.util;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/RenderedImage.html
	[Script(IsNative = true)]
	public interface RenderedImage
	{
		/// <summary>
		/// Computes an arbitrary rectangular region of the RenderedImage
		/// and copies it into a caller-supplied WritableRaster.
		/// </summary>
		WritableRaster copyData(WritableRaster @raster);

		/// <summary>
		/// Returns the ColorModel associated with this image.
		/// </summary>
		ColorModel getColorModel();

		/// <summary>
		/// Returns the image as one large tile (for tile based
		/// images this will require fetching the whole image
		/// and copying the image data over).
		/// </summary>
		Raster getData();

		/// <summary>
		/// Computes and returns an arbitrary region of the RenderedImage.
		/// </summary>
		Raster getData(Rectangle @rect);

		/// <summary>
		/// Returns the height of the RenderedImage.
		/// </summary>
		int getHeight();

		/// <summary>
		/// Returns the minimum tile index in the X direction.
		/// </summary>
		int getMinTileX();

		/// <summary>
		/// Returns the minimum tile index in the Y direction.
		/// </summary>
		int getMinTileY();

		/// <summary>
		/// Returns the minimum X coordinate (inclusive) of the RenderedImage.
		/// </summary>
		int getMinX();

		/// <summary>
		/// Returns the minimum Y coordinate (inclusive) of the RenderedImage.
		/// </summary>
		int getMinY();

		/// <summary>
		/// Returns the number of tiles in the X direction.
		/// </summary>
		int getNumXTiles();

		/// <summary>
		/// Returns the number of tiles in the Y direction.
		/// </summary>
		int getNumYTiles();

		/// <summary>
		/// Gets a property from the property set of this image.
		/// </summary>
		object getProperty(string @name);

		/// <summary>
		/// Returns an array of names recognized by
		/// <A HREF="../../../java/awt/image/RenderedImage.html#getProperty(java.lang.String)"><CODE>getProperty(String)</CODE></A>
		/// or <code>null</code>, if no property names are recognized.
		/// </summary>
		String[] getPropertyNames();

		/// <summary>
		/// Returns the SampleModel associated with this image.
		/// </summary>
		SampleModel getSampleModel();

		/// <summary>
		/// Returns a vector of RenderedImages that are the immediate sources of
		/// image data for this RenderedImage.
		/// </summary>
		Vector getSources();

		/// <summary>
		/// Returns tile (tileX, tileY).
		/// </summary>
		Raster getTile(int @tileX, int @tileY);

		/// <summary>
		/// Returns the X offset of the tile grid relative to the origin,
		/// i.e., the X coordinate of the upper-left pixel of tile (0, 0).
		/// </summary>
		int getTileGridXOffset();

		/// <summary>
		/// Returns the Y offset of the tile grid relative to the origin,
		/// i.e., the Y coordinate of the upper-left pixel of tile (0, 0).
		/// </summary>
		int getTileGridYOffset();

		/// <summary>
		/// Returns the tile height in pixels.
		/// </summary>
		int getTileHeight();

		/// <summary>
		/// Returns the tile width in pixels.
		/// </summary>
		int getTileWidth();

		/// <summary>
		/// Returns the width of the RenderedImage.
		/// </summary>
		int getWidth();

	}
}

