// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.font;
using java.awt.geom;
using java.lang;
using java.text;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/FontMetrics.html
	[Script(IsNative = true)]
	public class FontMetrics
	{
		/// <summary>
		/// Creates a new <code>FontMetrics</code> object for finding out
		/// height and width information about the specified <code>Font</code>
		/// and specific character glyphs in that <code>Font</code>.
		/// </summary>
		public FontMetrics(Font @font)
		{
		}

		/// <summary>
		/// Returns the total advance width for showing the specified array
		/// of bytes in this <code>Font</code>.
		/// </summary>
		public int bytesWidth(byte[] @data, int @off, int @len)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the total advance width for showing the specified array
		/// of characters in this <code>Font</code>.
		/// </summary>
		public int charsWidth(char[] @data, int @off, int @len)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the advance width of the specified character in this
		/// <code>Font</code>.
		/// </summary>
		public int charWidth(char @ch)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the advance width of the specified character in this
		/// <code>Font</code>.
		/// </summary>
		public int charWidth(int @ch)
		{
			return default(int);
		}

		/// <summary>
		/// Determines the <em>font ascent</em> of the <code>Font</code>
		/// described by this <code>FontMetrics</code> object.
		/// </summary>
		public int getAscent()
		{
			return default(int);
		}

		/// <summary>
		/// Determines the <em>font descent</em> of the <code>Font</code>
		/// described by this
		/// <code>FontMetrics</code> object.
		/// </summary>
		public int getDescent()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the <code>Font</code> described by this
		/// <code>FontMetrics</code> object.
		/// </summary>
		public Font getFont()
		{
			return default(Font);
		}

		/// <summary>
		/// Gets the standard height of a line of text in this font.
		/// </summary>
		public int getHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Determines the <em>standard leading</em> of the
		/// <code>Font</code> described by this <code>FontMetrics</code>
		/// object.
		/// </summary>
		public int getLeading()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <A HREF="../../java/awt/font/LineMetrics.html" title="class in java.awt.font"><CODE>LineMetrics</CODE></A> object for the specified
		/// character array in the specified <A HREF="../../java/awt/Graphics.html" title="class in java.awt"><CODE>Graphics</CODE></A> context.
		/// </summary>
		public LineMetrics getLineMetrics(char[] @chars, int @beginIndex, int @limit, Graphics @context)
		{
			return default(LineMetrics);
		}

		/// <summary>
		/// Returns the <A HREF="../../java/awt/font/LineMetrics.html" title="class in java.awt.font"><CODE>LineMetrics</CODE></A> object for the specified
		/// <A HREF="../../java/text/CharacterIterator.html" title="interface in java.text"><CODE>CharacterIterator</CODE></A> in the specified <A HREF="../../java/awt/Graphics.html" title="class in java.awt"><CODE>Graphics</CODE></A>
		/// context.
		/// </summary>
		public LineMetrics getLineMetrics(CharacterIterator @ci, int @beginIndex, int @limit, Graphics @context)
		{
			return default(LineMetrics);
		}

		/// <summary>
		/// Returns the <A HREF="../../java/awt/font/LineMetrics.html" title="class in java.awt.font"><CODE>LineMetrics</CODE></A> object for the specified
		/// <code>String</code> in the specified <A HREF="../../java/awt/Graphics.html" title="class in java.awt"><CODE>Graphics</CODE></A> context.
		/// </summary>
		public LineMetrics getLineMetrics(string @str, Graphics @context)
		{
			return default(LineMetrics);
		}

		/// <summary>
		/// Returns the <A HREF="../../java/awt/font/LineMetrics.html" title="class in java.awt.font"><CODE>LineMetrics</CODE></A> object for the specified
		/// <code>String</code> in the specified <A HREF="../../java/awt/Graphics.html" title="class in java.awt"><CODE>Graphics</CODE></A> context.
		/// </summary>
		public LineMetrics getLineMetrics(string @str, int @beginIndex, int @limit, Graphics @context)
		{
			return default(LineMetrics);
		}

		/// <summary>
		/// Gets the maximum advance width of any character in this
		/// <code>Font</code>.
		/// </summary>
		public int getMaxAdvance()
		{
			return default(int);
		}

		/// <summary>
		/// Determines the maximum ascent of the <code>Font</code>
		/// described by this <code>FontMetrics</code> object.
		/// </summary>
		public int getMaxAscent()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the bounds for the character with the maximum bounds
		/// in the specified <code>Graphics</code> context.
		/// </summary>
		public Rectangle2D getMaxCharBounds(Graphics @context)
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1.1,
		/// replaced by <code>getMaxDescent()</code>.</I>
		/// </summary>
		public int getMaxDecent()
		{
			return default(int);
		}

		/// <summary>
		/// Determines the maximum descent of the <code>Font</code>
		/// described by this <code>FontMetrics</code> object.
		/// </summary>
		public int getMaxDescent()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the bounds of the specified array of characters
		/// in the specified <code>Graphics</code> context.
		/// </summary>
		public Rectangle2D getStringBounds(char[] @chars, int @beginIndex, int @limit, Graphics @context)
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Returns the bounds of the characters indexed in the specified
		/// <code>CharacterIterator</code> in the
		/// specified <code>Graphics</code> context.
		/// </summary>
		public Rectangle2D getStringBounds(CharacterIterator @ci, int @beginIndex, int @limit, Graphics @context)
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Returns the bounds of the specified <code>String</code> in the
		/// specified <code>Graphics</code> context.
		/// </summary>
		public Rectangle2D getStringBounds(string @str, Graphics @context)
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Returns the bounds of the specified <code>String</code> in the
		/// specified <code>Graphics</code> context.
		/// </summary>
		public Rectangle2D getStringBounds(string @str, int @beginIndex, int @limit, Graphics @context)
		{
			return default(Rectangle2D);
		}

		/// <summary>
		/// Gets the advance widths of the first 256 characters in the
		/// <code>Font</code>.
		/// </summary>
		public int[] getWidths()
		{
			return default(int[]);
		}

		/// <summary>
		/// Checks to see if the <code>Font</code> has uniform line metrics.
		/// </summary>
		public bool hasUniformLineMetrics()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the total advance width for showing the specified
		/// <code>String</code> in this <code>Font</code>.
		/// </summary>
		public int stringWidth(string @str)
		{
			return default(int);
		}

		/// <summary>
		/// Returns a representation of this <code>FontMetrics</code>
		/// object's values as a <code>String</code>.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}

