// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.font;
using java.awt.geom;

namespace java.awt.font
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/font/GlyphVector.html
	[Script(IsNative = true)]
	public abstract class GlyphVector
	{
		/// <summary>
		/// 
		/// </summary>
		public GlyphVector()
		{
		}

		/// <summary>
		/// Tests if the specified <code>GlyphVector</code> exactly
		/// equals this <code>GlyphVector</code>.
		/// </summary>
		abstract public bool Equals(GlyphVector @set);

		/// <summary>
		/// Returns the <code>Font</code> associated with this
		/// <code>GlyphVector</code>.
		/// </summary>
		public Font getFont()
		{
			return default(Font);
		}

		/// <summary>
		/// Returns the <A HREF="../../../java/awt/font/FontRenderContext.html" title="class in java.awt.font"><CODE>FontRenderContext</CODE></A> associated with this
		/// <code>GlyphVector</code>.
		/// </summary>
		public FontRenderContext getFontRenderContext()
		{
			return default(FontRenderContext);
		}

		/// <summary>
		/// Returns the character index of the specified glyph.
		/// </summary>
		public int getGlyphCharIndex(int @glyphIndex)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the character indices of the specified glyphs.
		/// </summary>
		public int[] getGlyphCharIndices(int @beginGlyphIndex, int @numEntries, int[] @codeReturn)
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the glyphcode of the specified glyph.
		/// </summary>
		abstract public int getGlyphCode(int @glyphIndex);

		/// <summary>
		/// Returns an array of glyphcodes for the specified glyphs.
		/// </summary>
		abstract public int[] getGlyphCodes(int @beginGlyphIndex, int @numEntries, int[] @codeReturn);

		/// <summary>
		/// Returns the justification information for the glyph at
		/// the specified index into this <code>GlyphVector</code>.
		/// </summary>
		public GlyphJustificationInfo getGlyphJustificationInfo(int @glyphIndex)
		{
			return default(GlyphJustificationInfo);
		}

		/// <summary>
		/// Returns the logical bounds of the specified glyph within this
		/// <code>GlyphVector</code>.
		/// </summary>
		public Shape getGlyphLogicalBounds(int @glyphIndex)
		{
			return default(Shape);
		}

		/// <summary>
		/// Returns the metrics of the glyph at the specified index into
		/// this <code>GlyphVector</code>.
		/// </summary>
		public GlyphMetrics getGlyphMetrics(int @glyphIndex)
		{
			return default(GlyphMetrics);
		}

		/// <summary>
		/// Returns a <code>Shape</code> whose interior corresponds to the
		/// visual representation of the specified glyph
		/// within this <code>GlyphVector</code>.
		/// </summary>
		public Shape getGlyphOutline(int @glyphIndex)
		{
			return default(Shape);
		}

		/// <summary>
		/// Returns a <code>Shape</code> whose interior corresponds to the
		/// visual representation of the specified glyph
		/// within this <code>GlyphVector</code>, offset to x, y.
		/// </summary>
		public Shape getGlyphOutline(int @glyphIndex, float @x, float @y)
		{
			return default(Shape);
		}

		/// <summary>
		/// Returns the pixel bounds of the glyph at index when this
		/// <code>GlyphVector</code> is rendered in a <code>Graphics</code> with the
		/// given <code>FontRenderContext</code> at the given location.
		/// </summary>
		public Rectangle getGlyphPixelBounds(int @index, FontRenderContext @renderFRC, float @x, float @y)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the position of the specified glyph relative to the
		/// origin of this <code>GlyphVector</code>.
		/// </summary>
		public Point2D getGlyphPosition(int @glyphIndex)
		{
			return default(Point2D);
		}

		/// <summary>
		/// Returns an array of glyph positions for the specified glyphs.
		/// </summary>
		abstract public float[] getGlyphPositions(int @beginGlyphIndex, int @numEntries, float[] @positionReturn);

		/// <summary>
		/// Returns the transform of the specified glyph within this
		/// <code>GlyphVector</code>.
		/// </summary>
		public AffineTransform getGlyphTransform(int @glyphIndex)
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Returns the visual bounds of the specified glyph within the
		/// <code>GlyphVector</code>.
		/// </summary>
		public Shape getGlyphVisualBounds(int @glyphIndex)
		{
			return default(Shape);
		}

		/// <summary>
		/// Returns flags describing the global state of the GlyphVector.
		/// </summary>
		public int getLayoutFlags()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the logical bounds of this <code>GlyphVector</code>.
		/// </summary>
		public Rectangle2D getLogicalBounds()
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Returns the number of glyphs in this <code>GlyphVector</code>.
		/// </summary>
		abstract public int getNumGlyphs();

		/// <summary>
		/// Returns a <code>Shape</code> whose interior corresponds to the
		/// visual representation of this <code>GlyphVector</code>.
		/// </summary>
		public Shape getOutline()
		{
			return default(Shape);
		}

		/// <summary>
		/// Returns a <code>Shape</code> whose interior corresponds to the
		/// visual representation of this <code>GlyphVector</code> when
		/// rendered at x, y.
		/// </summary>
		public Shape getOutline(float @x, float @y)
		{
			return default(Shape);
		}

		/// <summary>
		/// Returns the pixel bounds of this <code>GlyphVector</code> when
		/// rendered in a graphics with the given
		/// <code>FontRenderContext</code> at the given location.
		/// </summary>
		public Rectangle getPixelBounds(FontRenderContext @renderFRC, float @x, float @y)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the visual bounds of this <code>GlyphVector</code>
		/// The visual bounds is the bounding box of the outline of this
		/// <code>GlyphVector</code>.
		/// </summary>
		public Rectangle2D getVisualBounds()
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Assigns default positions to each glyph in this
		/// <code>GlyphVector</code>.
		/// </summary>
		abstract public void performDefaultLayout();

		/// <summary>
		/// Sets the position of the specified glyph within this
		/// <code>GlyphVector</code>.
		/// </summary>
		abstract public void setGlyphPosition(int @glyphIndex, Point2D @newPos);

		/// <summary>
		/// Sets the transform of the specified glyph within this
		/// <code>GlyphVector</code>.
		/// </summary>
		abstract public void setGlyphTransform(int @glyphIndex, AffineTransform @newTX);

	}
}

