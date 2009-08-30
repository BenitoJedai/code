// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.image;
using java.lang;
using java.util;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/BufferedImage.html
	[Script(IsNative = true)]
	public class BufferedImage : Image
	{
		/// <summary>
		/// Constructs a new <code>BufferedImage</code> with a specified
		/// <code>ColorModel</code> and <code>Raster</code>.
		/// </summary>
		public BufferedImage(ColorModel @cm, WritableRaster @raster, bool @isRasterPremultiplied, Hashtable @properties)
		{
		}

		/// <summary>
		/// Constructs a <code>BufferedImage</code> of one of the predefined
		/// image types.
		/// </summary>
		public BufferedImage(int @width, int @height, int @imageType)
		{
		}

		/// <summary>
		/// Constructs a <code>BufferedImage</code> of one of the predefined
		/// image types:
		/// TYPE_BYTE_BINARY or TYPE_BYTE_INDEXED.
		/// </summary>
		public BufferedImage(int @width, int @height, int @imageType, IndexColorModel @cm)
		{
		}

		/// <summary>
		/// Adds a tile observer.
		/// </summary>
		public void addTileObserver(TileObserver @to)
		{
		}

		/// <summary>
		/// Forces the data to match the state specified in the
		/// <code>isAlphaPremultiplied</code> variable.
		/// </summary>
		public void coerceData(bool @isAlphaPremultiplied)
		{
		}

		/// <summary>
		/// Computes an arbitrary rectangular region of the
		/// <code>BufferedImage</code> and copies it into a specified
		/// <code>WritableRaster</code>.
		/// </summary>
		public WritableRaster copyData(WritableRaster @outRaster)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a <code>Graphics2D</code>, which can be used to draw into
		/// this <code>BufferedImage</code>.
		/// </summary>
		public Graphics2D createGraphics()
		{
			return default(Graphics2D);
		}

		/// <summary>
		/// Flushes all resources being used to cache optimization information.
		/// </summary>
		public void flush()
		{
		}

		/// <summary>
		/// Returns a <code>WritableRaster</code> representing the alpha
		/// channel for <code>BufferedImage</code> objects
		/// with <code>ColorModel</code> objects that support a separate
		/// spatial alpha channel, such as <code>ComponentColorModel</code> and
		/// <code>DirectColorModel</code>.
		/// </summary>
		public WritableRaster getAlphaRaster()
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Returns the <code>ColorModel</code>.
		/// </summary>
		public ColorModel getColorModel()
		{
			return default(ColorModel);
		}

		/// <summary>
		/// Returns the image as one large tile.
		/// </summary>
		public Raster getData()
		{
			return default(Raster);
		}

		/// <summary>
		/// Computes and returns an arbitrary region of the
		/// <code>BufferedImage</code>.
		/// </summary>
		public Raster getData(Rectangle @rect)
		{
			return default(Raster);
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
		/// Returns the height of the <code>BufferedImage</code>.
		/// </summary>
		public int getHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the height of the <code>BufferedImage</code>.
		/// </summary>
		public int getHeight(ImageObserver @observer)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the minimum tile index in the x direction.
		/// </summary>
		public int getMinTileX()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the minimum tile index in the y direction.
		/// </summary>
		public int getMinTileY()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the minimum x coordinate of this
		/// <code>BufferedImage</code>.
		/// </summary>
		public int getMinX()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the minimum y coordinate of this
		/// <code>BufferedImage</code>.
		/// </summary>
		public int getMinY()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of tiles in the x direction.
		/// </summary>
		public int getNumXTiles()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of tiles in the y direction.
		/// </summary>
		public int getNumYTiles()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a property of the image by name.
		/// </summary>
		public object getProperty(string @name)
		{
			return default(object);
		}

		/// <summary>
		/// Returns a property of the image by name.
		/// </summary>
		public object getProperty(string @name, ImageObserver @observer)
		{
			return default(object);
		}

		/// <summary>
		/// Returns an array of names recognized by
		/// <A HREF="../../../java/awt/image/BufferedImage.html#getProperty(java.lang.String)"><CODE>getProperty(String)</CODE></A>
		/// or <code>null</code>, if no property names are recognized.
		/// </summary>
		public String[] getPropertyNames()
		{
			return default(String[]);
		}

		/// <summary>
		/// Returns the <A HREF="../../../java/awt/image/WritableRaster.html" title="class in java.awt.image"><CODE>WritableRaster</CODE></A>.
		/// </summary>
		public WritableRaster getRaster()
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Returns an integer pixel in the default RGB color model
		/// (TYPE_INT_ARGB) and default sRGB colorspace.
		/// </summary>
		public int getRGB(int @x, int @y)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of integer pixels in the default RGB color model
		/// (TYPE_INT_ARGB) and default sRGB color space,
		/// from a portion of the image data.
		/// </summary>
		public int[] getRGB(int @startX, int @startY, int @w, int @h, int[] @rgbArray, int @offset, int @scansize)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the <code>SampleModel</code> associated with this
		/// <code>BufferedImage</code>.
		/// </summary>
		public SampleModel getSampleModel()
		{
			return default(SampleModel);
		}

		/// <summary>
		/// Returns the object that produces the pixels for the image.
		/// </summary>
		public ImageProducer getSource()
		{
			return default(ImageProducer);
		}

		/// <summary>
		/// Returns a <A HREF="../../../java/util/Vector.html" title="class in java.util"><CODE>Vector</CODE></A> of <A HREF="../../../java/awt/image/RenderedImage.html" title="interface in java.awt.image"><CODE>RenderedImage</CODE></A> objects that are
		/// the immediate sources, not the sources of these immediate sources,
		/// of image data for this <code>BufferedImage</code>.
		/// </summary>
		public Vector getSources()
		{
			return default(Vector);
		}

		/// <summary>
		/// Returns a subimage defined by a specified rectangular region.
		/// </summary>
		public BufferedImage getSubimage(int @x, int @y, int @w, int @h)
		{
			return default(BufferedImage);
		}

		/// <summary>
		/// Returns tile (<code>tileX</code>, <code>tileY</code>).
		/// </summary>
		public Raster getTile(int @tileX, int @tileY)
		{
			return default(Raster);
		}

		/// <summary>
		/// Returns the x offset of the tile grid relative to the origin,
		/// For example, the x coordinate of the location of tile
		/// (0, 0).
		/// </summary>
		public int getTileGridXOffset()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the y offset of the tile grid relative to the origin,
		/// For example, the y coordinate of the location of tile
		/// (0, 0).
		/// </summary>
		public int getTileGridYOffset()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the tile height in pixels.
		/// </summary>
		public int getTileHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the tile width in pixels.
		/// </summary>
		public int getTileWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the image type.
		/// </summary>
		public int getType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the width of the <code>BufferedImage</code>.
		/// </summary>
		public int getWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the width of the <code>BufferedImage</code>.
		/// </summary>
		public int getWidth(ImageObserver @observer)
		{
			return default(int);
		}

		/// <summary>
		/// Checks out a tile for writing.
		/// </summary>
		public WritableRaster getWritableTile(int @tileX, int @tileY)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Returns an array of <A HREF="../../../java/awt/Point.html" title="class in java.awt"><CODE>Point</CODE></A> objects indicating which tiles
		/// are checked out for writing.
		/// </summary>
		public Point[] getWritableTileIndices()
		{
			return default(Point[]);
		}

		/// <summary>
		/// Returns whether or not any tile is checked out for writing.
		/// </summary>
		public bool hasTileWriters()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether or not the alpha has been premultiplied.
		/// </summary>
		public bool isAlphaPremultiplied()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether or not a tile is currently checked out for writing.
		/// </summary>
		public bool isTileWritable(int @tileX, int @tileY)
		{
			return default(bool);
		}

		/// <summary>
		/// Relinquishes permission to write to a tile.
		/// </summary>
		public void releaseWritableTile(int @tileX, int @tileY)
		{
		}

		/// <summary>
		/// Removes a tile observer.
		/// </summary>
		public void removeTileObserver(TileObserver @to)
		{
		}

		/// <summary>
		/// Sets a rectangular region of the image to the contents of the
		/// specified <code>Raster</code> <code>r</code>, which is
		/// assumed to be in the same coordinate space as the
		/// <code>BufferedImage</code>.
		/// </summary>
		public void setData(Raster @r)
		{
		}

		/// <summary>
		/// Sets a pixel in this <code>BufferedImage</code> to the specified
		/// RGB value.
		/// </summary>
		public void setRGB(int @x, int @y, int @rgb)
		{
		}

		/// <summary>
		/// Sets an array of integer pixels in the default RGB color model
		/// (TYPE_INT_ARGB) and default sRGB color space,
		/// into a portion of the image data.
		/// </summary>
		public void setRGB(int @startX, int @startY, int @w, int @h, int[] @rgbArray, int @offset, int @scansize)
		{
		}

		/// <summary>
		/// Returns a <code>String</code> representation of this
		/// <code>BufferedImage</code> object and its values.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}

