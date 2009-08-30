// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.image;
using java.lang;
using java.math;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/IndexColorModel.html
	[Script(IsNative = true)]
	public class IndexColorModel : ColorModel
	{
		/// <summary>
		/// Constructs an <code>IndexColorModel</code> from the specified
		/// arrays of red, green, and blue components.
		/// </summary>
		public IndexColorModel(int @bits, int @size, byte[] @r, byte[] @g, byte[] @b)
			: base(0)
		{
		}

		/// <summary>
		/// Constructs an <code>IndexColorModel</code> from the given
		/// arrays of red, green, blue and alpha components.
		/// </summary>
		public IndexColorModel(int @bits, int @size, byte[] @r, byte[] @g, byte[] @b, byte[] @a)
			: base(0)
		{
		}

		/// <summary>
		/// Constructs an <code>IndexColorModel</code> from the given arrays
		/// of red, green, and blue components.
		/// </summary>
		public IndexColorModel(int @bits, int @size, byte[] @r, byte[] @g, byte[] @b, int @trans)
			: base(0)
		{
		}

		/// <summary>
		/// Constructs an <code>IndexColorModel</code> from a single
		/// array of interleaved red, green, blue and optional alpha
		/// components.
		/// </summary>
		public IndexColorModel(int @bits, int @size, byte[] @cmap, int @start, bool @hasalpha)
			: base(0)
		{
		}

		/// <summary>
		/// Constructs an <code>IndexColorModel</code> from a single array of
		/// interleaved red, green, blue and optional alpha components.
		/// </summary>
		public IndexColorModel(int @bits, int @size, byte[] @cmap, int @start, bool @hasalpha, int @trans)
			: base(0)
		{
		}

		/// <summary>
		/// Constructs an <code>IndexColorModel</code> from an array of
		/// ints where each int is comprised of red, green, blue, and
		/// optional alpha components in the default RGB color model format.
		/// </summary>
		public IndexColorModel(int @bits, int @size, int[] @cmap, int @start, bool @hasalpha, int @trans, int @transferType)
			: base(0)
		{
		}

		/// <summary>
		/// Constructs an <code>IndexColorModel</code> from an
		/// <code>int</code> array where each <code>int</code> is
		/// comprised of red, green, blue, and alpha
		/// components in the default RGB color model format.
		/// </summary>
		public IndexColorModel(int @bits, int @size, int[] @cmap, int @start, int @transferType, BigInteger @validBits) : base(0)
		{
		}

		/// <summary>
		/// Returns a new <code>BufferedImage</code> of TYPE_INT_ARGB or
		/// TYPE_INT_RGB that has a <code>Raster</code> with pixel data
		/// computed by expanding the indices in the source <code>Raster</code>
		/// using the color/alpha component arrays of this <code>ColorModel</code>.
		/// </summary>
		public BufferedImage convertToIntDiscrete(Raster @raster, bool @forceARGB)
		{
			return default(BufferedImage);
		}

		/// <summary>
		/// Creates a <code>SampleModel</code> with the specified
		/// width and height that has a data layout compatible with
		/// this <code>ColorModel</code>.
		/// </summary>
		public  SampleModel createCompatibleSampleModel(int @w, int @h)
		{
			return default(SampleModel);
		}

		/// <summary>
		/// Creates a <code>WritableRaster</code> with the specified width
		/// and height that has a data layout (<code>SampleModel</code>)
		/// compatible with this <code>ColorModel</code>.
		/// </summary>
		public  WritableRaster createCompatibleWritableRaster(int @w, int @h)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Disposes of system resources associated with this
		/// <code>ColorModel</code> once this <code>ColorModel</code> is no
		/// longer referenced.
		/// </summary>
		public void finalize()
		{
		}

		/// <summary>
		/// Returns the alpha component for the specified pixel, scaled
		/// from 0 to 255.
		/// </summary>
		public override int getAlpha(int @pixel)
		{
			return default(int);
		}

		/// <summary>
		/// Copies the array of alpha transparency components into the
		/// specified array.
		/// </summary>
		public void getAlphas(byte[] @a)
		{
		}

		/// <summary>
		/// Returns the blue color component for the specified pixel, scaled
		/// from 0 to 255 in the default RGB ColorSpace, sRGB.
		/// </summary>
		public override int getBlue(int @pixel)
		{
			return default(int);
		}

		/// <summary>
		/// Copies the array of blue color components into the specified array.
		/// </summary>
		public void getBlues(byte[] @b)
		{
		}

		/// <summary>
		/// Returns an array of unnormalized color/alpha components for a
		/// specified pixel in this <code>ColorModel</code>.
		/// </summary>
		public int[] getComponents(int @pixel, int[] @components, int @offset)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns an array of unnormalized color/alpha components for
		/// a specified pixel in this <code>ColorModel</code>.
		/// </summary>
		public int[] getComponents(object @pixel, int[] @components, int @offset)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns an array of the number of bits for each color/alpha component.
		/// </summary>
		public int[] getComponentSize()
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns a pixel value represented as an int in this
		/// <code>ColorModel</code> given an array of unnormalized
		/// color/alpha components.
		/// </summary>
		public int getDataElement(int[] @components, int @offset)
		{
			return default(int);
		}

		/// <summary>
		/// Returns a data element array representation of a pixel in this
		/// <code>ColorModel</code> given an array of unnormalized color/alpha
		/// components.
		/// </summary>
		public object getDataElements(int[] @components, int @offset, object @pixel)
		{
			return default(object);
		}

		/// <summary>
		/// Returns a data element array representation of a pixel in this
		/// ColorModel, given an integer pixel representation in the
		/// default RGB color model.
		/// </summary>
		public object getDataElements(int @rgb, object @pixel)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the green color component for the specified pixel, scaled
		/// from 0 to 255 in the default RGB ColorSpace, sRGB.
		/// </summary>
		public override int getGreen(int @pixel)
		{
			return default(int);
		}

		/// <summary>
		/// Copies the array of green color components into the specified array.
		/// </summary>
		public void getGreens(byte[] @g)
		{
		}

		/// <summary>
		/// Returns the size of the color/alpha component arrays in this
		/// <code>IndexColorModel</code>.
		/// </summary>
		public int getMapSize()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the red color component for the specified pixel, scaled
		/// from 0 to 255 in the default RGB ColorSpace, sRGB.
		/// </summary>
		public override int getRed(int @pixel)
		{
			return default(int);
		}

		/// <summary>
		/// Copies the array of red color components into the specified array.
		/// </summary>
		public void getReds(byte[] @r)
		{
		}

		/// <summary>
		/// Returns the color/alpha components of the pixel in the default
		/// RGB color model format.
		/// </summary>
		public int getRGB(int @pixel)
		{
			return default(int);
		}

		/// <summary>
		/// Converts data for each index from the color and alpha component
		/// arrays to an int in the default RGB ColorModel format and copies
		/// the resulting 32-bit ARGB values into the specified array.
		/// </summary>
		public void getRGBs(int[] @rgb)
		{
		}

		/// <summary>
		/// Returns the transparency.
		/// </summary>
		public int getTransparency()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index of the transparent pixel in this
		/// <code>IndexColorModel</code> or -1 if there is no transparent pixel.
		/// </summary>
		public int getTransparentPixel()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a <code>BigInteger</code> that indicates the valid/invalid
		/// pixels in the colormap.
		/// </summary>
		public BigInteger getValidPixels()
		{
			return default(BigInteger);
		}

		/// <summary>
		/// Returns <code>true</code> if <code>raster</code> is compatible
		/// with this <code>ColorModel</code> or <code>false</code> if it
		/// is not compatible with this <code>ColorModel</code>.
		/// </summary>
		public bool isCompatibleRaster(Raster @raster)
		{
			return default(bool);
		}

		/// <summary>
		/// Checks if the specified <code>SampleModel</code> is compatible
		/// with this <code>ColorModel</code>.
		/// </summary>
		public bool isCompatibleSampleModel(SampleModel @sm)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether or not all of the pixels are valid.
		/// </summary>
		public bool isValid()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns whether or not the pixel is valid.
		/// </summary>
		public bool isValid(int @pixel)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the <code>String</code> representation of the contents of
		/// this <code>ColorModel</code>object.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}

