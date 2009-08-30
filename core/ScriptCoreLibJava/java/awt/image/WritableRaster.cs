// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.image;
using java.lang;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/WritableRaster.html
	[Script(IsNative = true)]
	public class WritableRaster : Raster
	{
		/// <summary>
		/// Constructs a WritableRaster with the given SampleModel and DataBuffer.
		/// </summary>
		public WritableRaster(SampleModel @sampleModel, DataBuffer @dataBuffer, Point @origin)
			: base(sampleModel, origin)
		{
		}

		/// <summary>
		/// Constructs a WritableRaster with the given SampleModel, DataBuffer,
		/// and parent.
		/// </summary>
		public WritableRaster(SampleModel @sampleModel, DataBuffer @dataBuffer, Rectangle @aRegion, Point @sampleModelTranslate, WritableRaster @parent)
			: base(sampleModel, null)
		{
		}

		/// <summary>
		/// Constructs a WritableRaster with the given SampleModel.
		/// </summary>
		public WritableRaster(SampleModel @sampleModel, Point @origin) : base(sampleModel, origin)
		{
		}

		/// <summary>
		/// Returns a new WritableRaster which shares all or part of this
		/// WritableRaster's DataBuffer.
		/// </summary>
		public WritableRaster createWritableChild(int @parentX, int @parentY, int @w, int @h, int @childMinX, int @childMinY, int[] @bandList)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Create a WritableRaster with the same size, SampleModel and DataBuffer
		/// as this one, but with a different location.
		/// </summary>
		public WritableRaster createWritableTranslatedChild(int @childMinX, int @childMinY)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Returns the parent WritableRaster (if any) of this WritableRaster,
		/// or else null.
		/// </summary>
		public WritableRaster getWritableParent()
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Sets the data for a rectangle of pixels from a
		/// primitive array of type TransferType.
		/// </summary>
		public void setDataElements(int @x, int @y, int @w, int @h, object @inData)
		{
		}

		/// <summary>
		/// Sets the data for a single pixel from a
		/// primitive array of type TransferType.
		/// </summary>
		public void setDataElements(int @x, int @y, object @inData)
		{
		}

		/// <summary>
		/// Sets the data for a rectangle of pixels from an input Raster.
		/// </summary>
		public void setDataElements(int @x, int @y, Raster @inRaster)
		{
		}

		/// <summary>
		/// Sets a pixel in the DataBuffer using a double array of samples for input.
		/// </summary>
		public void setPixel(int @x, int @y, double[] @dArray)
		{
		}

		/// <summary>
		/// Sets a pixel in the DataBuffer using a float array of samples for input.
		/// </summary>
		public void setPixel(int @x, int @y, float[] @fArray)
		{
		}

		/// <summary>
		/// Sets a pixel in the DataBuffer using an int array of samples for input.
		/// </summary>
		public void setPixel(int @x, int @y, int[] @iArray)
		{
		}

		/// <summary>
		/// Sets all samples for a rectangle of pixels from a double array containing
		/// one sample per array element.
		/// </summary>
		public void setPixels(int @x, int @y, int @w, int @h, double[] @dArray)
		{
		}

		/// <summary>
		/// Sets all samples for a rectangle of pixels from a float array containing
		/// one sample per array element.
		/// </summary>
		public void setPixels(int @x, int @y, int @w, int @h, float[] @fArray)
		{
		}

		/// <summary>
		/// Sets all samples for a rectangle of pixels from an int array containing
		/// one sample per array element.
		/// </summary>
		public void setPixels(int @x, int @y, int @w, int @h, int[] @iArray)
		{
		}

		/// <summary>
		/// Copies pixels from Raster srcRaster to this WritableRaster.
		/// </summary>
		public void setRect(int @dx, int @dy, Raster @srcRaster)
		{
		}

		/// <summary>
		/// Copies pixels from Raster srcRaster to this WritableRaster.
		/// </summary>
		public void setRect(Raster @srcRaster)
		{
		}

		/// <summary>
		/// Sets a sample in the specified band for the pixel located at (x,y)
		/// in the DataBuffer using a double for input.
		/// </summary>
		public void setSample(int @x, int @y, int @b, double @s)
		{
		}

		/// <summary>
		/// Sets a sample in the specified band for the pixel located at (x,y)
		/// in the DataBuffer using a float for input.
		/// </summary>
		public void setSample(int @x, int @y, int @b, float @s)
		{
		}

		/// <summary>
		/// Sets a sample in the specified band for the pixel located at (x,y)
		/// in the DataBuffer using an int for input.
		/// </summary>
		public void setSample(int @x, int @y, int @b, int @s)
		{
		}

		/// <summary>
		/// Sets the samples in the specified band for the specified rectangle
		/// of pixels from a double array containing one sample per array element.
		/// </summary>
		public void setSamples(int @x, int @y, int @w, int @h, int @b, double[] @dArray)
		{
		}

		/// <summary>
		/// Sets the samples in the specified band for the specified rectangle
		/// of pixels from a float array containing one sample per array element.
		/// </summary>
		public void setSamples(int @x, int @y, int @w, int @h, int @b, float[] @fArray)
		{
		}

		/// <summary>
		/// Sets the samples in the specified band for the specified rectangle
		/// of pixels from an int array containing one sample per array element.
		/// </summary>
		public void setSamples(int @x, int @y, int @w, int @h, int @b, int[] @iArray)
		{
		}

	}
}

