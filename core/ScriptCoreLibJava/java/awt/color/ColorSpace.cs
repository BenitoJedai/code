// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.color;
using java.lang;

namespace java.awt.color
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/color/ColorSpace.html
	[Script(IsNative = true)]
	public abstract class ColorSpace
	{
		/// <summary>
		/// Constructs a ColorSpace object given a color space type
		/// and the number of components.
		/// </summary>
		public ColorSpace(int @type, int @numcomponents)
		{
		}

		/// <summary>
		/// Transforms a color value assumed to be in the CS_CIEXYZ conversion
		/// color space into this ColorSpace.
		/// </summary>
		abstract public float[] fromCIEXYZ(float[] @colorvalue);

		/// <summary>
		/// Transforms a color value assumed to be in the default CS_sRGB
		/// color space into this ColorSpace.
		/// </summary>
		abstract public float[] fromRGB(float[] @rgbvalue);

		/// <summary>
		/// Returns a ColorSpace representing one of the specific
		/// predefined color spaces.
		/// </summary>
		public ColorSpace getInstance(int @colorspace)
		{
			return default(ColorSpace);
		}

		/// <summary>
		/// Returns the maximum normalized color component value for the
		/// specified component.
		/// </summary>
		public float getMaxValue(int @component)
		{
			return default(float);
		}

		/// <summary>
		/// Returns the minimum normalized color component value for the
		/// specified component.
		/// </summary>
		public float getMinValue(int @component)
		{
			return default(float);
		}

		/// <summary>
		/// Returns the name of the component given the component index.
		/// </summary>
		public string getName(int @idx)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the number of components of this ColorSpace.
		/// </summary>
		public int getNumComponents()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the color space type of this ColorSpace (for example
		/// TYPE_RGB, TYPE_XYZ, ...).
		/// </summary>
		public int getType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns true if the ColorSpace is CS_sRGB.
		/// </summary>
		public bool isCS_sRGB()
		{
			return default(bool);
		}

		/// <summary>
		/// Transforms a color value assumed to be in this ColorSpace
		/// into the CS_CIEXYZ conversion color space.
		/// </summary>
		abstract public float[] toCIEXYZ(float[] @colorvalue);

		/// <summary>
		/// Transforms a color value assumed to be in this ColorSpace
		/// into a value in the default CS_sRGB color space.
		/// </summary>
		abstract public float[] toRGB(float[] @colorvalue);

	}
}

