// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.image;
using java.lang;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/SampleModel.html
	[Script(IsNative = true)]
	public abstract class SampleModel
	{
		/// <summary>
		/// Constructs a SampleModel with the specified parameters.
		/// </summary>
		public SampleModel(int @dataType, int @w, int @h, int @numBands)
		{
		}

		/// <summary>
		/// Creates a SampleModel which describes data in this SampleModel's
		/// format, but with a different width and height.
		/// </summary>
		public SampleModel createCompatibleSampleModel(int @w, int @h)
		{
			return default(SampleModel);
		}

		/// <summary>
		/// Creates a DataBuffer that corresponds to this SampleModel.
		/// </summary>
		public DataBuffer createDataBuffer()
		{
			return default(DataBuffer);
		}

		/// <summary>
		/// Creates a new SampleModel
		/// with a subset of the bands of this
		/// SampleModel.
		/// </summary>
		public SampleModel createSubsetSampleModel(int[] @bands)
		{
			return default(SampleModel);
		}

		/// <summary>
		/// Returns the pixel data for the specified rectangle of pixels in a
		/// primitive array of type TransferType.
		/// </summary>
		public object getDataElements(int @x, int @y, int @w, int @h, object @obj, DataBuffer @data)
		{
			return default(object);
		}

		/// <summary>
		/// Returns data for a single pixel in a primitive array of type
		/// TransferType.
		/// </summary>
		public object getDataElements(int @x, int @y, object @obj, DataBuffer @data)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the data type of the DataBuffer storing the pixel data.
		/// </summary>
		public int getDataType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the height in pixels.
		/// </summary>
		public int getHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the total number of bands of image data.
		/// </summary>
		public int getNumBands()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of data elements needed to transfer a pixel
		/// via the getDataElements and setDataElements methods.
		/// </summary>
		abstract public int getNumDataElements();

		/// <summary>
		/// Returns the samples for the specified pixel in an array of double.
		/// </summary>
		public double[] getPixel(int @x, int @y, double[] @dArray, DataBuffer @data)
		{
			return default(double[]);
		}

		/// <summary>
		/// Returns the samples for the specified pixel in an array of float.
		/// </summary>
		public float[] getPixel(int @x, int @y, float[] @fArray, DataBuffer @data)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns the samples for a specified pixel in an int array,
		/// one sample per array element.
		/// </summary>
		public int[] getPixel(int @x, int @y, int[] @iArray, DataBuffer @data)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns all samples for a rectangle of pixels in a double
		/// array, one sample per array element.
		/// </summary>
		public double[] getPixels(int @x, int @y, int @w, int @h, double[] @dArray, DataBuffer @data)
		{
			return default(double[]);
		}

		/// <summary>
		/// Returns all samples for a rectangle of pixels in a float
		/// array, one sample per array element.
		/// </summary>
		public float[] getPixels(int @x, int @y, int @w, int @h, float[] @fArray, DataBuffer @data)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns all samples for a rectangle of pixels in an
		/// int array, one sample per array element.
		/// </summary>
		public int[] getPixels(int @x, int @y, int @w, int @h, int[] @iArray, DataBuffer @data)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the sample in a specified band for the pixel located
		/// at (x,y) as an int.
		/// </summary>
		abstract public int getSample(int @x, int @y, int @b, DataBuffer @data);

		/// <summary>
		/// Returns the sample in a specified band
		/// for a pixel located at (x,y) as a double.
		/// </summary>
		public double getSampleDouble(int @x, int @y, int @b, DataBuffer @data)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the sample in a specified band
		/// for the pixel located at (x,y) as a float.
		/// </summary>
		public float getSampleFloat(int @x, int @y, int @b, DataBuffer @data)
		{
			return default(float);
		}

		/// <summary>
		/// Returns the samples for a specified band for a specified rectangle
		/// of pixels in a double array, one sample per array element.
		/// </summary>
		public double[] getSamples(int @x, int @y, int @w, int @h, int @b, double[] @dArray, DataBuffer @data)
		{
			return default(double[]);
		}

		/// <summary>
		/// Returns the samples for a specified band for the specified rectangle
		/// of pixels in a float array, one sample per array element.
		/// </summary>
		public float[] getSamples(int @x, int @y, int @w, int @h, int @b, float[] @fArray, DataBuffer @data)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns the samples for a specified band for the specified rectangle
		/// of pixels in an int array, one sample per array element.
		/// </summary>
		public int[] getSamples(int @x, int @y, int @w, int @h, int @b, int[] @iArray, DataBuffer @data)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the size in bits of samples for all bands.
		/// </summary>
		abstract public int[] getSampleSize();

		/// <summary>
		/// Returns the size in bits of samples for the specified band.
		/// </summary>
		abstract public int getSampleSize(int @band);

		/// <summary>
		/// Returns the TransferType used to transfer pixels via the
		/// getDataElements and setDataElements methods.
		/// </summary>
		public int getTransferType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the width in pixels.
		/// </summary>
		public int getWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Sets the data for a rectangle of pixels in the specified DataBuffer
		/// from a primitive array of type TransferType.
		/// </summary>
		public void setDataElements(int @x, int @y, int @w, int @h, object @obj, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets the data for a single pixel in the specified DataBuffer from a
		/// primitive array of type TransferType.
		/// </summary>
		abstract public void setDataElements(int @x, int @y, object @obj, DataBuffer @data);

		/// <summary>
		/// Sets a pixel in the DataBuffer using a double array of samples
		/// for input.
		/// </summary>
		public void setPixel(int @x, int @y, double[] @dArray, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets a pixel in the DataBuffer using a float array of samples for input.
		/// </summary>
		public void setPixel(int @x, int @y, float[] @fArray, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets a pixel in 	the DataBuffer using an int array of samples for input.
		/// </summary>
		public void setPixel(int @x, int @y, int[] @iArray, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets all samples for a rectangle of pixels from a double array
		/// containing one sample per array element.
		/// </summary>
		public void setPixels(int @x, int @y, int @w, int @h, double[] @dArray, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets all samples for a rectangle of pixels from a float array containing
		/// one sample per array element.
		/// </summary>
		public void setPixels(int @x, int @y, int @w, int @h, float[] @fArray, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets all samples for a rectangle of pixels from an int array containing
		/// one sample per array element.
		/// </summary>
		public void setPixels(int @x, int @y, int @w, int @h, int[] @iArray, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets a sample in the specified band for the pixel located at (x,y)
		/// in the DataBuffer using a double for input.
		/// </summary>
		public void setSample(int @x, int @y, int @b, double @s, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets a sample in the specified band for the pixel located at (x,y)
		/// in the DataBuffer using a float for input.
		/// </summary>
		public void setSample(int @x, int @y, int @b, float @s, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets a sample in the specified band for the pixel located at (x,y)
		/// in the DataBuffer using an int for input.
		/// </summary>
		abstract public void setSample(int @x, int @y, int @b, int @s, DataBuffer @data);

		/// <summary>
		/// Sets the samples in the specified band for the specified rectangle
		/// of pixels from a double array containing one sample per array element.
		/// </summary>
		public void setSamples(int @x, int @y, int @w, int @h, int @b, double[] @dArray, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets the samples in the specified band for the specified rectangle
		/// of pixels from a float array containing one sample per array element.
		/// </summary>
		public void setSamples(int @x, int @y, int @w, int @h, int @b, float[] @fArray, DataBuffer @data)
		{
		}

		/// <summary>
		/// Sets the samples in the specified band for the specified rectangle
		/// of pixels from an int array containing one sample per array element.
		/// </summary>
		public void setSamples(int @x, int @y, int @w, int @h, int @b, int[] @iArray, DataBuffer @data)
		{
		}

	}
}

