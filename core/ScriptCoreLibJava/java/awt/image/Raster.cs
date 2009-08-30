// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.image;
using java.lang;

namespace java.awt.image
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/Raster.html
	[Script(IsNative = true)]
	public class Raster
	{
		/// <summary>
		/// Constructs a Raster with the given SampleModel and DataBuffer.
		/// </summary>
		public Raster(SampleModel @sampleModel, DataBuffer @dataBuffer, Point @origin)
		{
		}

		/// <summary>
		/// Constructs a Raster with the given SampleModel, DataBuffer, and
		/// parent.
		/// </summary>
		public Raster(SampleModel @sampleModel, DataBuffer @dataBuffer, Rectangle @aRegion, Point @sampleModelTranslate, Raster @parent)
		{
		}

		/// <summary>
		/// Constructs a Raster with the given SampleModel.
		/// </summary>
		public Raster(SampleModel @sampleModel, Point @origin)
		{
		}

		/// <summary>
		/// Creates a Raster based on a BandedSampleModel with the
		/// specified DataBuffer, width, height, scanline stride, bank
		/// indices, and band offsets.
		/// </summary>
		public WritableRaster createBandedRaster(DataBuffer @dataBuffer, int @w, int @h, int @scanlineStride, int[] @bankIndices, int[] @bandOffsets, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster based on a BandedSampleModel with the
		/// specified data type, width, height, scanline stride, bank
		/// indices and band offsets.
		/// </summary>
		public WritableRaster createBandedRaster(int @dataType, int @w, int @h, int @scanlineStride, int[] @bankIndices, int[] @bandOffsets, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster based on a BandedSampleModel with the
		/// specified data type, width, height, and number of bands.
		/// </summary>
		public WritableRaster createBandedRaster(int @dataType, int @w, int @h, int @bands, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Returns a new Raster which shares all or part of this Raster's
		/// DataBuffer.
		/// </summary>
		public Raster createChild(int @parentX, int @parentY, int @width, int @height, int @childMinX, int @childMinY, int[] @bandList)
		{
			return default(Raster);
		}

		/// <summary>
		/// Create a compatible WritableRaster the same size as this Raster with
		/// the same SampleModel and a new initialized DataBuffer.
		/// </summary>
		public WritableRaster createCompatibleWritableRaster()
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Create a compatible WritableRaster with the specified size, a new
		/// SampleModel, and a new initialized DataBuffer.
		/// </summary>
		public WritableRaster createCompatibleWritableRaster(int @w, int @h)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Create a compatible WritableRaster with the specified
		/// location (minX, minY) and size (width, height), a
		/// new SampleModel, and a new initialized DataBuffer.
		/// </summary>
		public WritableRaster createCompatibleWritableRaster(int @x, int @y, int @w, int @h)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Create a compatible WritableRaster with location (minX, minY)
		/// and size (width, height) specified by rect, a
		/// new SampleModel, and a new initialized DataBuffer.
		/// </summary>
		public WritableRaster createCompatibleWritableRaster(Rectangle @rect)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster based on a PixelInterleavedSampleModel with the
		/// specified DataBuffer, width, height, scanline stride, pixel
		/// stride, and band offsets.
		/// </summary>
		public WritableRaster createInterleavedRaster(DataBuffer @dataBuffer, int @w, int @h, int @scanlineStride, int @pixelStride, int[] @bandOffsets, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster based on a PixelInterleavedSampleModel with the
		/// specified data type, width, height, scanline stride, pixel
		/// stride, and band offsets.
		/// </summary>
		public WritableRaster createInterleavedRaster(int @dataType, int @w, int @h, int @scanlineStride, int @pixelStride, int[] @bandOffsets, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster based on a PixelInterleavedSampleModel with the
		/// specified data type, width, height, and number of bands.
		/// </summary>
		public WritableRaster createInterleavedRaster(int @dataType, int @w, int @h, int @bands, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster based on a SinglePixelPackedSampleModel with
		/// the specified DataBuffer, width, height, scanline stride, and
		/// band masks.
		/// </summary>
		public WritableRaster createPackedRaster(DataBuffer @dataBuffer, int @w, int @h, int @scanlineStride, int[] @bandMasks, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster based on a MultiPixelPackedSampleModel with the
		/// specified DataBuffer, width, height, and bits per pixel.
		/// </summary>
		public WritableRaster createPackedRaster(DataBuffer @dataBuffer, int @w, int @h, int @bitsPerPixel, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster based on a SinglePixelPackedSampleModel with
		/// the specified data type, width, height, and band masks.
		/// </summary>
		public WritableRaster createPackedRaster(int @dataType, int @w, int @h, int[] @bandMasks, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster based on a packed SampleModel with the
		/// specified data type, width, height, number of bands, and bits
		/// per band.
		/// </summary>
		public WritableRaster createPackedRaster(int @dataType, int @w, int @h, int @bands, int @bitsPerBand, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a Raster with the specified SampleModel and DataBuffer.
		/// </summary>
		public Raster createRaster(SampleModel @sm, DataBuffer @db, Point @location)
		{
			return default(Raster);
		}

		/// <summary>
		/// Create a Raster with the same size, SampleModel and DataBuffer
		/// as this one, but with a different location.
		/// </summary>
		public Raster createTranslatedChild(int @childMinX, int @childMinY)
		{
			return default(Raster);
		}

		/// <summary>
		/// Creates a WritableRaster with the specified SampleModel and DataBuffer.
		/// </summary>
		public WritableRaster createWritableRaster(SampleModel @sm, DataBuffer @db, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Creates a WritableRaster with the specified SampleModel.
		/// </summary>
		public WritableRaster createWritableRaster(SampleModel @sm, Point @location)
		{
			return default(WritableRaster);
		}

		/// <summary>
		/// Returns the bounding Rectangle of this Raster.
		/// </summary>
		public Rectangle getBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the DataBuffer associated with this Raster.
		/// </summary>
		public DataBuffer getDataBuffer()
		{
			return default(DataBuffer);
		}

		/// <summary>
		/// Returns the pixel data for the specified rectangle of pixels in a
		/// primitive array of type TransferType.
		/// </summary>
		public object getDataElements(int @x, int @y, int @w, int @h, object @outData)
		{
			return default(object);
		}

		/// <summary>
		/// Returns data for a single pixel in a primitive array of type
		/// TransferType.
		/// </summary>
		public object getDataElements(int @x, int @y, object @outData)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the height in pixels of the Raster.
		/// </summary>
		public int getHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the minimum valid X coordinate of the Raster.
		/// </summary>
		public int getMinX()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the minimum valid Y coordinate of the Raster.
		/// </summary>
		public int getMinY()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of bands (samples per pixel) in this Raster.
		/// </summary>
		public int getNumBands()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the number of data elements needed to transfer one pixel
		/// via the getDataElements and setDataElements methods.
		/// </summary>
		public int getNumDataElements()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the parent Raster (if any) of this Raster or null.
		/// </summary>
		public Raster getParent()
		{
			return default(Raster);
		}

		/// <summary>
		/// Returns the samples in an array of double for the specified pixel.
		/// </summary>
		public double[] getPixel(int @x, int @y, double[] @dArray)
		{
			return default(double[]);
		}

		/// <summary>
		/// Returns the samples in an array of float for the
		/// specified pixel.
		/// </summary>
		public float[] getPixel(int @x, int @y, float[] @fArray)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns the samples in an array of int for the specified pixel.
		/// </summary>
		public int[] getPixel(int @x, int @y, int[] @iArray)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns a double array containing all samples for a rectangle of pixels,
		/// one sample per array element.
		/// </summary>
		public double[] getPixels(int @x, int @y, int @w, int @h, double[] @dArray)
		{
			return default(double[]);
		}

		/// <summary>
		/// Returns a float array containing all samples for a rectangle of pixels,
		/// one sample per array element.
		/// </summary>
		public float[] getPixels(int @x, int @y, int @w, int @h, float[] @fArray)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns an int array containing all samples for a rectangle of pixels,
		/// one sample per array element.
		/// </summary>
		public int[] getPixels(int @x, int @y, int @w, int @h, int[] @iArray)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the sample in a specified band for the pixel located
		/// at (x,y) as an int.
		/// </summary>
		public int getSample(int @x, int @y, int @b)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the sample in a specified band
		/// for a pixel located at (x,y) as a double.
		/// </summary>
		public double getSampleDouble(int @x, int @y, int @b)
		{
			return default(double);
		}

		/// <summary>
		/// Returns the sample in a specified band
		/// for the pixel located at (x,y) as a float.
		/// </summary>
		public float getSampleFloat(int @x, int @y, int @b)
		{
			return default(float);
		}

		/// <summary>
		/// Returns the SampleModel that describes the layout of the image data.
		/// </summary>
		public SampleModel getSampleModel()
		{
			return default(SampleModel);
		}

		/// <summary>
		/// Returns the X translation from the coordinate system of the
		/// SampleModel to that of the Raster.
		/// </summary>
		public int getSampleModelTranslateX()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the Y translation from the coordinate system of the
		/// SampleModel to that of the Raster.
		/// </summary>
		public int getSampleModelTranslateY()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the samples for a specified band for a specified rectangle
		/// of pixels in a double array, one sample per array element.
		/// </summary>
		public double[] getSamples(int @x, int @y, int @w, int @h, int @b, double[] @dArray)
		{
			return default(double[]);
		}

		/// <summary>
		/// Returns the samples for a specified band for the specified rectangle
		/// of pixels in a float array, one sample per array element.
		/// </summary>
		public float[] getSamples(int @x, int @y, int @w, int @h, int @b, float[] @fArray)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns the samples for a specified band for the specified rectangle
		/// of pixels in an int array, one sample per array element.
		/// </summary>
		public int[] getSamples(int @x, int @y, int @w, int @h, int @b, int[] @iArray)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the TransferType used to transfer pixels via the
		/// getDataElements and setDataElements methods.
		/// </summary>
		public int getTransferType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the width in pixels of the Raster.
		/// </summary>
		public int getWidth()
		{
			return default(int);
		}

	}
}

