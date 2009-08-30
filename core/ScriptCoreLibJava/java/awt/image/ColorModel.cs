// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.color;
using java.awt.image;
using java.lang;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/ColorModel.html
	[Script(IsNative = true)]
	public abstract class ColorModel
	{
		/// <summary>
		/// Constructs a <code>ColorModel</code> that translates pixels of the
		/// specified number of bits to color/alpha components.
		/// </summary>
		public ColorModel(int @bits)
		{
		}

		/// <summary>
		/// Constructs a <code>ColorModel</code> that translates pixel values
		/// to color/alpha components.
		/// </summary>
		public ColorModel(int @pixel_bits, int[] @bits, ColorSpace @cspace, bool @hasAlpha, bool @isAlphaPremultiplied, int @transparency, int @transferType)
		{
		}

		/// <summary>
		/// Forces the raster data to match the state specified in the
		/// <code>isAlphaPremultiplied</code> variable, assuming the data is
		/// currently correctly described by this <code>ColorModel</code>.
		/// </summary>
		public ColorModel coerceData(WritableRaster @raster, bool @isAlphaPremultiplied)
		{
			return default(ColorModel);
		}

		/// <summary>
		/// Creates a <code>SampleModel</code> with the specified width and
		/// height that has a data layout compatible with this
		/// <code>ColorModel</code>.
		/// </summary>
		public SampleModel createCompatibleSampleModel(int @w, int @h)
		{
			return default(SampleModel);
		}

		/// <summary>
		/// Creates a <code>WritableRaster</code> with the specified width and
		/// height that has a data layout (<code>SampleModel</code>) compatible
		/// with this <code>ColorModel</code>.
		/// </summary>
		public WritableRaster createCompatibleWritableRaster(int @w, int @h)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Tests if the specified <code>Object</code> is an instance of
		/// <code>ColorModel</code> and if it equals this
		/// <code>ColorModel</code>.
		/// </summary>
		public override bool Equals(object @obj)
		{
			return default(bool);
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
		abstract public int getAlpha(int @pixel);

		/// <summary>
		/// Returns the alpha component for the specified pixel, scaled
		/// from 0 to 255.
		/// </summary>
		public int getAlpha(object @inData)
		{
			return default(int);
		}

		/// <summary>
		/// Returns a <code>Raster</code> representing the alpha channel of an
		/// image, extracted from the input <code>Raster</code>, provided that
		/// pixel values of this <code>ColorModel</code> represent color and
		/// alpha information as separate spatial bands (e.g.
		/// </summary>
		public WritableRaster getAlphaRaster(WritableRaster @raster)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Returns the blue color component for the specified pixel, scaled
		/// from 0 to 255 in the default RGB ColorSpace, sRGB.
		/// </summary>
		abstract public int getBlue(int @pixel);

		/// <summary>
		/// Returns the blue color component for the specified pixel, scaled
		/// from 0 to 255 in the default RGB <code>ColorSpace</code>, sRGB.
		/// </summary>
		public int getBlue(object @inData)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <code>ColorSpace</code> associated with this
		/// <code>ColorModel</code>.
		/// </summary>
		public ColorSpace getColorSpace()
		{
			return default(ColorSpace);
		}

		/// <summary>
		/// Returns an array of unnormalized color/alpha components given a pixel
		/// in this <code>ColorModel</code>.
		/// </summary>
		public int[] getComponents(int @pixel, int[] @components, int @offset)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns an array of unnormalized color/alpha components given a pixel
		/// in this <code>ColorModel</code>.
		/// </summary>
		public int[] getComponents(object @pixel, int[] @components, int @offset)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns an array of the number of bits per color/alpha component.
		/// </summary>
		public int[] getComponentSize()
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the number of bits for the specified color/alpha component.
		/// </summary>
		public int getComponentSize(int @componentIdx)
		{
			return default(int);
		}

		/// <summary>
		/// Returns a pixel value represented as an <code>int</code> in this
		/// <code>ColorModel</code>, given an array of normalized color/alpha
		/// components.
		/// </summary>
		public int getDataElement(float[] @normComponents, int @normOffset)
		{
			return default(int);
		}

		/// <summary>
		/// Returns a pixel value represented as an <code>int</code> in this
		/// <code>ColorModel</code>, given an array of unnormalized color/alpha
		/// components.
		/// </summary>
		public int getDataElement(int[] @components, int @offset)
		{
			return default(int);
		}

		/// <summary>
		/// Returns a data element array representation of a pixel in this
		/// <code>ColorModel</code>, given an array of normalized color/alpha
		/// components.
		/// </summary>
		public object getDataElements(float[] @normComponents, int @normOffset, object @obj)
		{
			return default(object);
		}

		/// <summary>
		/// Returns a data element array representation of a pixel in this
		/// <code>ColorModel</code>, given an array of unnormalized color/alpha
		/// components.
		/// </summary>
		public object getDataElements(int[] @components, int @offset, object @obj)
		{
			return default(object);
		}

		/// <summary>
		/// Returns a data element array representation of a pixel in this
		/// <code>ColorModel</code>, given an integer pixel representation in
		/// the default RGB color model.
		/// </summary>
		public object getDataElements(int @rgb, object @pixel)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the green color component for the specified pixel, scaled
		/// from 0 to 255 in the default RGB ColorSpace, sRGB.
		/// </summary>
		abstract public int getGreen(int @pixel);

		/// <summary>
		/// Returns the green color component for the specified pixel, scaled
		/// from 0 to 255 in the default RGB <code>ColorSpace</code>, sRGB.
		/// </summary>
		public int getGreen(object @inData)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of all of the color/alpha components in normalized
		/// form, given an unnormalized component array.
		/// </summary>
		public float[] getNormalizedComponents(int[] @components, int @offset, float[] @normComponents, int @normOffset)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns an array of all of the color/alpha components in normalized
		/// form, given a pixel in this <code>ColorModel</code>.
		/// </summary>
		public float[] getNormalizedComponents(object @pixel, float[] @normComponents, int @normOffset)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns the number of color components in this
		/// <code>ColorModel</code>.
		/// </summary>
		public int getNumColorComponents()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of components, including alpha, in this
		/// <code>ColorModel</code>.
		/// </summary>
		public int getNumComponents()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of bits per pixel described by this
		/// <code>ColorModel</code>.
		/// </summary>
		public int getPixelSize()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the red color component for the specified pixel, scaled
		/// from 0 to 255 in the default RGB ColorSpace, sRGB.
		/// </summary>
		abstract public int getRed(int @pixel);

		/// <summary>
		/// Returns the red color component for the specified pixel, scaled
		/// from 0 to 255 in the default RGB <code>ColorSpace</code>, sRGB.
		/// </summary>
		public int getRed(object @inData)
		{
			return default(int);
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
		/// Returns the color/alpha components for the specified pixel in the
		/// default RGB color model format.
		/// </summary>
		public int getRGB(object @inData)
		{
			return default(int);
		}

		/// <summary>
		/// Returns a <code>DirectColorModel</code> that describes the default
		/// format for integer RGB values used in many of the methods in the
		/// AWT image interfaces for the convenience of the programmer.
		/// </summary>
		public ColorModel getRGBdefault()
		{
			return default(ColorModel);
		}

		/// <summary>
		/// Returns the transfer type of this <code>ColorModel</code>.
		/// </summary>
		public int getTransferType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the transparency.
		/// </summary>
		public int getTransparency()
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of all of the color/alpha components in unnormalized
		/// form, given a normalized component array.
		/// </summary>
		public int[] getUnnormalizedComponents(float[] @normComponents, int @normOffset, int[] @components, int @offset)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns whether or not alpha is supported in this
		/// <code>ColorModel</code>.
		/// </summary>
		public bool hasAlpha()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the hash code for this ColorModel.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns whether or not the alpha has been premultiplied in the
		/// pixel values to be translated by this <code>ColorModel</code>.
		/// </summary>
		public bool isAlphaPremultiplied()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if <code>raster</code> is compatible
		/// with this <code>ColorModel</code> and <code>false</code> if it is
		/// not.
		/// </summary>
		public bool isCompatibleRaster(Raster @raster)
		{
			return default(bool);
		}

		/// <summary>
		/// Checks if the <code>SampleModel</code> is compatible with this
		/// <code>ColorModel</code>.
		/// </summary>
		public bool isCompatibleSampleModel(SampleModel @sm)
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

