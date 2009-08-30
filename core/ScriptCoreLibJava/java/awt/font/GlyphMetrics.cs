// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.geom;

namespace java.awt.font
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/font/GlyphMetrics.html
	[Script(IsNative = true)]
	public class GlyphMetrics
	{
		/// <summary>
		/// Constructs a <code>GlyphMetrics</code> object.
		/// </summary>
		public GlyphMetrics(bool @horizontal, float @advanceX, float @advanceY, Rectangle2D @bounds, byte @glyphType)
		{
		}

		/// <summary>
		/// Constructs a <code>GlyphMetrics</code> object.
		/// </summary>
		public GlyphMetrics(float @advance, Rectangle2D @bounds, byte @glyphType)
		{
		}

		/// <summary>
		/// Returns the advance of the glyph along the baseline (either
		/// horizontal or vertical).
		/// </summary>
		public float getAdvance()
		{
			return default(float);
		}

		/// <summary>
		/// Returns the x-component of the advance of the glyph.
		/// </summary>
		public float getAdvanceX()
		{
			return default(float);
		}

		/// <summary>
		/// Returns the y-component of the advance of the glyph.
		/// </summary>
		public float getAdvanceY()
		{
			return default(float);
		}

		/// <summary>
		/// Returns the bounds of the glyph.
		/// </summary>
		public Rectangle2D getBounds2D()
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Returns the left (top) side bearing of the glyph.
		/// </summary>
		public float getLSB()
		{
			return default(float);
		}

		/// <summary>
		/// Returns the right (bottom) side bearing of the glyph.
		/// </summary>
		public float getRSB()
		{
			return default(float);
		}

		/// <summary>
		/// Returns the raw glyph type code.
		/// </summary>
		public int getType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns <code>true</code> if this is a combining glyph.
		/// </summary>
		public bool isCombining()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if this is a component glyph.
		/// </summary>
		public bool isComponent()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if this is a ligature glyph.
		/// </summary>
		public bool isLigature()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if this is a standard glyph.
		/// </summary>
		public bool isStandard()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if this is a whitespace glyph.
		/// </summary>
		public bool isWhitespace()
		{
			return default(bool);
		}

	}
}

