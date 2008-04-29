using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib.ActionScript.flash.geom;
using ScriptCoreLib.ActionScript.flash.filters;
using ScriptCoreLib.ActionScript.flash.utils;

namespace ScriptCoreLib.ActionScript.flash.display
{
    // http://livedocs.adobe.com/flex/3/langref/flash/display/BitmapData.html
    [Script(IsNative = true)]
    public class BitmapData : IBitmapDrawable
    {
        #region Properties
        /// <summary>
        /// [read-only] The height of the bitmap image in pixels.
        /// </summary>
        public int height { get; private set; }

        /// <summary>
        /// [read-only] The rectangle that defines the size and location of the bitmap image.
        /// </summary>
        public Rectangle rect { get; private set; }

        /// <summary>
        /// [read-only] Defines whether the bitmap image supports per-pixel transparency.
        /// </summary>
        public bool transparent { get; private set; }

        /// <summary>
        /// [read-only] The width of the bitmap image in pixels.
        /// </summary>
        public int width { get; private set; }

        #endregion

        #region Methods
        /// <summary>
        /// Takes a source image and a filter object and generates the filtered image.
        /// </summary>
        public void applyFilter(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, BitmapFilter filter)
        {
        }

        /// <summary>
        /// Returns a new BitmapData object that is a clone of the original instance with an exact copy of the contained bitmap.
        /// </summary>
        public BitmapData clone()
        {
            return default(BitmapData);
        }

        /// <summary>
        /// Adjusts the color values in a specified area of a bitmap image by using a ColorTransform object.
        /// </summary>
        public void colorTransform(Rectangle rect, ColorTransform colorTransform)
        {
        }

        /// <summary>
        /// Compares two BitmapData objects.
        /// </summary>
        public object compare(BitmapData otherBitmapData)
        {
            return default(object);
        }

        /// <summary>
        /// Transfers data from one channel of another BitmapData object or the current BitmapData object into a channel of the current BitmapData object.
        /// </summary>
        public void copyChannel(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, uint sourceChannel, uint destChannel)
        {
        }

        /// <summary>
        /// Provides a fast routine to perform pixel manipulation between images with no stretching, rotation, or color effects.
        /// </summary>
        public void copyPixels(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, BitmapData alphaBitmapData, Point alphaPoint, bool mergeAlpha)
        {
        }

        /// <summary>
        /// Provides a fast routine to perform pixel manipulation between images with no stretching, rotation, or color effects.
        /// </summary>
        public void copyPixels(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, BitmapData alphaBitmapData, Point alphaPoint)
        {
        }

        /// <summary>
        /// Provides a fast routine to perform pixel manipulation between images with no stretching, rotation, or color effects.
        /// </summary>
        public void copyPixels(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, BitmapData alphaBitmapData)
        {
        }

        /// <summary>
        /// Provides a fast routine to perform pixel manipulation between images with no stretching, rotation, or color effects.
        /// </summary>
        public void copyPixels(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint)
        {
        }

        /// <summary>
        /// Frees memory that is used to store the BitmapData object.
        /// </summary>
        public void dispose()
        {
        }

        /// <summary>
        /// Draws the source display object onto the bitmap image, using the Flash Player or AIR vector renderer.
        /// </summary>
        public void draw(IBitmapDrawable source, Matrix matrix, ColorTransform colorTransform, string blendMode, Rectangle clipRect, bool smoothing)
        {
        }

        /// <summary>
        /// Draws the source display object onto the bitmap image, using the Flash Player or AIR vector renderer.
        /// </summary>
        public void draw(IBitmapDrawable source, Matrix matrix, ColorTransform colorTransform, string blendMode, Rectangle clipRect)
        {
        }

        /// <summary>
        /// Draws the source display object onto the bitmap image, using the Flash Player or AIR vector renderer.
        /// </summary>
        public void draw(IBitmapDrawable source, Matrix matrix, ColorTransform colorTransform, string blendMode)
        {
        }

        /// <summary>
        /// Draws the source display object onto the bitmap image, using the Flash Player or AIR vector renderer.
        /// </summary>
        public void draw(IBitmapDrawable source, Matrix matrix, ColorTransform colorTransform)
        {
        }

        /// <summary>
        /// Draws the source display object onto the bitmap image, using the Flash Player or AIR vector renderer.
        /// </summary>
        public void draw(IBitmapDrawable source, Matrix matrix)
        {
        }

        /// <summary>
        /// Draws the source display object onto the bitmap image, using the Flash Player or AIR vector renderer.
        /// </summary>
        public void draw(IBitmapDrawable source)
        {
        }

        /// <summary>
        /// Fills a rectangular area of pixels with a specified ARGB color.
        /// </summary>
        public void fillRect(Rectangle rect, uint color)
        {
        }

        /// <summary>
        /// Performs a flood fill operation on an image starting at an (x, y) coordinate and filling with a certain color.
        /// </summary>
        public void floodFill(int x, int y, uint color)
        {
        }

        /// <summary>
        /// Determines the destination rectangle that the applyFilter() method call affects, given a BitmapData object, a source rectangle, and a filter object.
        /// </summary>
        public Rectangle generateFilterRect(Rectangle sourceRect, BitmapFilter filter)
        {
            return default(Rectangle);
        }

        /// <summary>
        /// Determines a rectangular region that either fully encloses all pixels of a specified color within the bitmap image (if the findColor parameter is set to true) or fully encloses all pixels that do not include the specified color (if the findColor parameter is set to false).
        /// </summary>
        public Rectangle getColorBoundsRect(uint mask, uint color, bool findColor)
        {
            return default(Rectangle);
        }

        /// <summary>
        /// Determines a rectangular region that either fully encloses all pixels of a specified color within the bitmap image (if the findColor parameter is set to true) or fully encloses all pixels that do not include the specified color (if the findColor parameter is set to false).
        /// </summary>
        public Rectangle getColorBoundsRect(uint mask, uint color)
        {
            return default(Rectangle);
        }

        /// <summary>
        /// Returns an integer that represents an RGB pixel value from a BitmapData object at a specific point (x, y).
        /// </summary>
        public uint getPixel(int x, int y)
        {
            return default(uint);
        }

        /// <summary>
        /// Returns an ARGB color value that contains alpha channel data and RGB data.
        /// </summary>
        public uint getPixel32(int x, int y)
        {
            return default(uint);
        }

        /// <summary>
        /// Generates a byte array from a rectangular region of pixel data.
        /// </summary>
        public ByteArray getPixels(Rectangle rect)
        {
            return default(ByteArray);
        }

        /// <summary>
        /// Performs pixel-level hit detection between one bitmap image and a point, rectangle, or other bitmap image.
        /// </summary>
        public bool hitTest(Point firstPoint, uint firstAlphaThreshold, object secondObject, Point secondBitmapDataPoint, uint secondAlphaThreshold)
        {
            return default(bool);
        }

        /// <summary>
        /// Performs pixel-level hit detection between one bitmap image and a point, rectangle, or other bitmap image.
        /// </summary>
        public bool hitTest(Point firstPoint, uint firstAlphaThreshold, object secondObject, Point secondBitmapDataPoint)
        {
            return default(bool);
        }

        /// <summary>
        /// Performs pixel-level hit detection between one bitmap image and a point, rectangle, or other bitmap image.
        /// </summary>
        public bool hitTest(Point firstPoint, uint firstAlphaThreshold, object secondObject)
        {
            return default(bool);
        }

        /// <summary>
        /// Locks an image so that any objects that reference the BitmapData object, such as Bitmap objects, are not updated when this BitmapData object changes.
        /// </summary>
        public void @lock()
        {
        }

        /// <summary>
        /// Performs per-channel blending from a source image to a destination image.
        /// </summary>
        public void merge(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, uint redMultiplier, uint greenMultiplier, uint blueMultiplier, uint alphaMultiplier)
        {
        }

        /// <summary>
        /// Fills an image with pixels representing random noise.
        /// </summary>
        public void noise(int randomSeed, uint low, uint high, uint channelOptions, bool grayScale)
        {
        }

        /// <summary>
        /// Fills an image with pixels representing random noise.
        /// </summary>
        public void noise(int randomSeed, uint low, uint high, uint channelOptions)
        {
        }

        /// <summary>
        /// Fills an image with pixels representing random noise.
        /// </summary>
        public void noise(int randomSeed, uint low, uint high)
        {
        }

        /// <summary>
        /// Fills an image with pixels representing random noise.
        /// </summary>
        public void noise(int randomSeed, uint low)
        {
        }

        /// <summary>
        /// Fills an image with pixels representing random noise.
        /// </summary>
        public void noise(int randomSeed)
        {
        }

        /// <summary>
        /// Remaps the color channel values in an image that has up to four arrays of color palette data, one for each channel.
        /// </summary>
        public void paletteMap(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, Array redArray, Array greenArray, Array blueArray, Array alphaArray)
        {
        }

        /// <summary>
        /// Remaps the color channel values in an image that has up to four arrays of color palette data, one for each channel.
        /// </summary>
        public void paletteMap(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, Array redArray, Array greenArray, Array blueArray)
        {
        }

        /// <summary>
        /// Remaps the color channel values in an image that has up to four arrays of color palette data, one for each channel.
        /// </summary>
        public void paletteMap(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, Array redArray, Array greenArray)
        {
        }

        /// <summary>
        /// Remaps the color channel values in an image that has up to four arrays of color palette data, one for each channel.
        /// </summary>
        public void paletteMap(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, Array redArray)
        {
        }

        /// <summary>
        /// Remaps the color channel values in an image that has up to four arrays of color palette data, one for each channel.
        /// </summary>
        public void paletteMap(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint)
        {
        }

        /// <summary>
        /// Generates a Perlin noise image.
        /// </summary>
        public void perlinNoise(double baseX, double baseY, uint numOctaves, int randomSeed, bool stitch, bool fractalNoise, uint channelOptions, bool grayScale, Array offsets)
        {
        }

        /// <summary>
        /// Generates a Perlin noise image.
        /// </summary>
        public void perlinNoise(double baseX, double baseY, uint numOctaves, int randomSeed, bool stitch, bool fractalNoise, uint channelOptions, bool grayScale)
        {
        }

        /// <summary>
        /// Generates a Perlin noise image.
        /// </summary>
        public void perlinNoise(double baseX, double baseY, uint numOctaves, int randomSeed, bool stitch, bool fractalNoise, uint channelOptions)
        {
        }

        /// <summary>
        /// Generates a Perlin noise image.
        /// </summary>
        public void perlinNoise(double baseX, double baseY, uint numOctaves, int randomSeed, bool stitch, bool fractalNoise)
        {
        }

        /// <summary>
        /// Performs a pixel dissolve either from a source image to a destination image or by using the same image.
        /// </summary>
        public int pixelDissolve(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, int randomSeed, int numPixels, uint fillColor)
        {
            return default(int);
        }

        /// <summary>
        /// Performs a pixel dissolve either from a source image to a destination image or by using the same image.
        /// </summary>
        public int pixelDissolve(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, int randomSeed, int numPixels)
        {
            return default(int);
        }

        /// <summary>
        /// Performs a pixel dissolve either from a source image to a destination image or by using the same image.
        /// </summary>
        public int pixelDissolve(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, int randomSeed)
        {
            return default(int);
        }

        /// <summary>
        /// Performs a pixel dissolve either from a source image to a destination image or by using the same image.
        /// </summary>
        public int pixelDissolve(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint)
        {
            return default(int);
        }

        /// <summary>
        /// Scrolls an image by a certain (x, y) pixel amount.
        /// </summary>
        public void scroll(int x, int y)
        {
        }

        /// <summary>
        /// Sets a single pixel of a BitmapData object.
        /// </summary>
        public void setPixel(int x, int y, uint color)
        {
        }

        /// <summary>
        /// Sets the color and alpha transparency values of a single pixel of a BitmapData object.
        /// </summary>
        public void setPixel32(int x, int y, uint color)
        {
        }

        /// <summary>
        /// Converts a byte array into a rectangular region of pixel data.
        /// </summary>
        public void setPixels(Rectangle rect, ByteArray inputByteArray)
        {
        }

        /// <summary>
        /// Tests pixel values in an image against a specified threshold and sets pixels that pass the test to new color values.
        /// </summary>
        public uint threshold(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, string operation, uint threshold, uint color, uint mask, bool copySource)
        {
            return default(uint);
        }

        /// <summary>
        /// Tests pixel values in an image against a specified threshold and sets pixels that pass the test to new color values.
        /// </summary>
        public uint threshold(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, string operation, uint threshold, uint color, uint mask)
        {
            return default(uint);
        }

        /// <summary>
        /// Tests pixel values in an image against a specified threshold and sets pixels that pass the test to new color values.
        /// </summary>
        public uint threshold(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, string operation, uint threshold, uint color)
        {
            return default(uint);
        }

        /// <summary>
        /// Tests pixel values in an image against a specified threshold and sets pixels that pass the test to new color values.
        /// </summary>
        public uint threshold(BitmapData sourceBitmapData, Rectangle sourceRect, Point destPoint, string operation, uint threshold)
        {
            return default(uint);
        }

        /// <summary>
        /// Unlocks an image so that any objects that reference the BitmapData object, such as Bitmap objects, are updated when this BitmapData object changes.
        /// </summary>
        public void unlock(Rectangle changeRect)
        {
        }

        /// <summary>
        /// Unlocks an image so that any objects that reference the BitmapData object, such as Bitmap objects, are updated when this BitmapData object changes.
        /// </summary>
        public void unlock()
        {
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Creates a BitmapData object with a specified width and height.
        /// </summary>
        public BitmapData(int width, int height, bool transparent, uint fillColor)
        {
        }

        /// <summary>
        /// Creates a BitmapData object with a specified width and height.
        /// </summary>
        public BitmapData(int width, int height, bool transparent)
        {
        }

        /// <summary>
        /// Creates a BitmapData object with a specified width and height.
        /// </summary>
        public BitmapData(int width, int height)
        {
        }

        #endregion

    }
}
