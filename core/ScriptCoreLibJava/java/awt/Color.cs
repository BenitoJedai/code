// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
//using java.awt.color;
//using java.awt.geom;
using java.awt.image;
using java.lang;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Color.html
	[Script(IsNative = true)]
	public class Color
	{
		/// <summary>
		/// Creates a color in the specified <code>ColorSpace</code>
		/// with the color components specified in the <code>float</code>
		/// array and the specified alpha.
		/// </summary>
		//public Color(ColorSpace @cspace, float[] @components, float @alpha)
		//{
		//}

		/// <summary>
		/// Creates an opaque sRGB color with the specified red, green, and blue
		/// values in the range (0.0 - 1.0).
		/// </summary>
		public Color(float @r, float @g, float @b)
		{
		}

		/// <summary>
		/// Creates an sRGB color with the specified red, green, blue, and
		/// alpha values in the range (0.0 - 1.0).
		/// </summary>
		public Color(float @r, float @g, float @b, float @a)
		{
		}

		/// <summary>
		/// Creates an opaque sRGB color with the specified combined RGB value
		/// consisting of the red component in bits 16-23, the green component
		/// in bits 8-15, and the blue component in bits 0-7.
		/// </summary>
		public Color(int @rgb)
		{
		}

		/// <summary>
		/// Creates an sRGB color with the specified combined RGBA value consisting
		/// of the alpha component in bits 24-31, the red component in bits 16-23,
		/// the green component in bits 8-15, and the blue component in bits 0-7.
		/// </summary>
		public Color(int @rgba, bool @hasalpha)
		{
		}

		/// <summary>
		/// Creates an opaque sRGB color with the specified red, green,
		/// and blue values in the range (0 - 255).
		/// </summary>
		public Color(int @r, int @g, int @b)
		{
		}

		/// <summary>
		/// Creates an sRGB color with the specified red, green, blue, and alpha
		/// values in the range (0 - 255).
		/// </summary>
		public Color(int @r, int @g, int @b, int @a)
		{
		}

		/// <summary>
		/// Creates a new <code>Color</code> that is a brighter version of this
		/// <code>Color</code>.
		/// </summary>
		public Color brighter()
		{
			return default(Color);
		}

		/// <summary>
		/// Creates and returns a <A HREF="../../java/awt/PaintContext.html" title="interface in java.awt"><CODE>PaintContext</CODE></A> used to generate a solid
		/// color pattern.
		/// </summary>
		//public PaintContext createContext(ColorModel @cm, Rectangle @r, Rectangle2D @r2d, AffineTransform @xform, RenderingHints @hints)
		//{
		//    return default(PaintContext);
		//}

		/// <summary>
		/// Creates a new <code>Color</code> that is a darker version of this
		/// <code>Color</code>.
		/// </summary>
		public Color darker()
		{
			return default(Color);
		}

		/// <summary>
		/// Converts a <code>String</code> to an integer and returns the
		/// specified opaque <code>Color</code>.
		/// </summary>
		public Color decode(string @nm)
		{
			return default(Color);
		}

		/// <summary>
		/// Determines whether another object is equal to this
		/// <code>Color</code>.
		/// </summary>
		public bool equals(Object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the alpha component in the range 0-255.
		/// </summary>
		public int getAlpha()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the blue component in the range 0-255 in the default sRGB
		/// space.
		/// </summary>
		public int getBlue()
		{
			return default(int);
		}

		/// <summary>
		/// Finds a color in the system properties.
		/// </summary>
		public Color getColor(string @nm)
		{
			return default(Color);
		}

		/// <summary>
		/// Finds a color in the system properties.
		/// </summary>
		public Color getColor(string @nm, Color @v)
		{
			return default(Color);
		}

		/// <summary>
		/// Finds a color in the system properties.
		/// </summary>
		public Color getColor(string @nm, int @v)
		{
			return default(Color);
		}

		/// <summary>
		/// Returns a <code>float</code> array containing only the color
		/// components of the <code>Color</code> in the
		/// <code>ColorSpace</code> specified by the <code>cspace</code>
		/// parameter.
		/// </summary>
		//public float[] getColorComponents(ColorSpace @cspace, float[] @compArray)
		//{
		//    return default(float[]);
		//}

		/// <summary>
		/// Returns a <code>float</code> array containing only the color
		/// components of the <code>Color</code>, in the
		/// <code>ColorSpace</code> of the <code>Color</code>.
		/// </summary>
		public float[] getColorComponents(float[] @compArray)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns the <code>ColorSpace</code> of this <code>Color</code>.
		/// </summary>
		//public ColorSpace getColorSpace()
		//{
		//    return default(ColorSpace);
		//}

		/// <summary>
		/// Returns a <code>float</code> array containing the color and alpha
		/// components of the <code>Color</code>, in the
		/// <code>ColorSpace</code> specified by the <code>cspace</code>
		/// parameter.
		/// </summary>
		//public float[] getComponents(ColorSpace @cspace, float[] @compArray)
		//{
		//    return default(float[]);
		//}

		/// <summary>
		/// Returns a <code>float</code> array containing the color and alpha
		/// components of the <code>Color</code>, in the
		/// <code>ColorSpace</code> of the <code>Color</code>.
		/// </summary>
		public float[] getComponents(float[] @compArray)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns the green component in the range 0-255 in the default sRGB
		/// space.
		/// </summary>
		public int getGreen()
		{
			return default(int);
		}

		/// <summary>
		/// Creates a <code>Color</code> object based on the specified values
		/// for the HSB color model.
		/// </summary>
		public Color getHSBColor(float @h, float @s, float @b)
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the red component in the range 0-255 in the default sRGB
		/// space.
		/// </summary>
		public int getRed()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the RGB value representing the color in the default sRGB
		/// <A HREF="../../java/awt/image/ColorModel.html" title="class in java.awt.image"><CODE>ColorModel</CODE></A>.
		/// </summary>
		public int getRGB()
		{
			return default(int);
		}

		/// <summary>
		/// Returns a <code>float</code> array containing only the color
		/// components of the <code>Color</code>, in the default sRGB color
		/// space.
		/// </summary>
		public float[] getRGBColorComponents(float[] @compArray)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns a <code>float</code> array containing the color and alpha
		/// components of the <code>Color</code>, as represented in the default
		/// sRGB color space.
		/// </summary>
		public float[] getRGBComponents(float[] @compArray)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns the transparency mode for this <code>Color</code>.
		/// </summary>
		public int getTransparency()
		{
			return default(int);
		}

		/// <summary>
		/// Computes the hash code for this <code>Color</code>.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Converts the components of a color, as specified by the HSB
		/// model, to an equivalent set of values for the default RGB model.
		/// </summary>
		static public int HSBtoRGB(float @hue, float @saturation, float @brightness)
		{
			return default(int);
		}

		/// <summary>
		/// Converts the components of a color, as specified by the default RGB
		/// model, to an equivalent set of values for hue, saturation, and
		/// brightness that are the three components of the HSB model.
		/// </summary>
		static public float[] RGBtoHSB(int @r, int @g, int @b, float[] @hsbvals)
		{
			return default(float[]);
		}

		/// <summary>
		/// Returns a string representation of this <code>Color</code>.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}

