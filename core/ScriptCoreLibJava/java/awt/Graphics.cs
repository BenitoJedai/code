// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.image;
using java.lang;
using java.text;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Graphics.html
	[Script(IsNative = true)]
	public abstract class Graphics
	{
		/// <summary>
		/// Constructs a new <code>Graphics</code> object.
		/// </summary>
		public Graphics()
		{
		}

		/// <summary>
		/// Clears the specified rectangle by filling it with the background
		/// color of the current drawing surface.
		/// </summary>
		abstract public void clearRect(int @x, int @y, int @width, int @height);

		/// <summary>
		/// Intersects the current clip with the specified rectangle.
		/// </summary>
		abstract public void clipRect(int @x, int @y, int @width, int @height);

		/// <summary>
		/// Copies an area of the component by a distance specified by
		/// <code>dx</code> and <code>dy</code>.
		/// </summary>
		abstract public void copyArea(int @x, int @y, int @width, int @height, int @dx, int @dy);

		/// <summary>
		/// Creates a new <code>Graphics</code> object that is
		/// a copy of this <code>Graphics</code> object.
		/// </summary>
		public Graphics create()
		{
			return default(Graphics);
		}

		/// <summary>
		/// Creates a new <code>Graphics</code> object based on this
		/// <code>Graphics</code> object, but with a new translation and clip area.
		/// </summary>
		public Graphics create(int @x, int @y, int @width, int @height)
		{
			return default(Graphics);
		}

		/// <summary>
		/// Disposes of this graphics context and releases
		/// any system resources that it is using.
		/// </summary>
		abstract public void dispose();

		/// <summary>
		/// Draws a 3-D highlighted outline of the specified rectangle.
		/// </summary>
		public void draw3DRect(int @x, int @y, int @width, int @height, bool @raised)
		{
		}

		/// <summary>
		/// Draws the outline of a circular or elliptical arc
		/// covering the specified rectangle.
		/// </summary>
		abstract public void drawArc(int @x, int @y, int @width, int @height, int @startAngle, int @arcAngle);

		/// <summary>
		/// Draws the text given by the specified byte array, using this
		/// graphics context's current font and color.
		/// </summary>
		public void drawBytes(byte[] @data, int @offset, int @length, int @x, int @y)
		{
		}

		/// <summary>
		/// Draws the text given by the specified character array, using this
		/// graphics context's current font and color.
		/// </summary>
		public void drawChars(char[] @data, int @offset, int @length, int @x, int @y)
		{
		}

		/// <summary>
		/// Draws as much of the specified image as is currently available.
		/// </summary>
		abstract public bool drawImage(Image @img, int @x, int @y, Color @bgcolor, ImageObserver @observer);

		/// <summary>
		/// Draws as much of the specified image as is currently available.
		/// </summary>
		abstract public bool drawImage(Image @img, int @x, int @y, ImageObserver @observer);

		/// <summary>
		/// Draws as much of the specified image as has already been scaled
		/// to fit inside the specified rectangle.
		/// </summary>
		abstract public bool drawImage(Image @img, int @x, int @y, int @width, int @height, Color @bgcolor, ImageObserver @observer);

		/// <summary>
		/// Draws as much of the specified image as has already been scaled
		/// to fit inside the specified rectangle.
		/// </summary>
		abstract public bool drawImage(Image @img, int @x, int @y, int @width, int @height, ImageObserver @observer);

		/// <summary>
		/// Draws as much of the specified area of the specified image as is
		/// currently available, scaling it on the fly to fit inside the
		/// specified area of the destination drawable surface.
		/// </summary>
		abstract public bool drawImage(Image @img, int @dx1, int @dy1, int @dx2, int @dy2, int @sx1, int @sy1, int @sx2, int @sy2, Color @bgcolor, ImageObserver @observer);

		/// <summary>
		/// Draws as much of the specified area of the specified image as is
		/// currently available, scaling it on the fly to fit inside the
		/// specified area of the destination drawable surface.
		/// </summary>
		abstract public bool drawImage(Image @img, int @dx1, int @dy1, int @dx2, int @dy2, int @sx1, int @sy1, int @sx2, int @sy2, ImageObserver @observer);

		/// <summary>
		/// Draws a line, using the current color, between the points
		/// <code>(x1, y1)</code> and <code>(x2, y2)</code>
		/// in this graphics context's coordinate system.
		/// </summary>
		abstract public void drawLine(int @x1, int @y1, int @x2, int @y2);

		/// <summary>
		/// Draws the outline of an oval.
		/// </summary>
		abstract public void drawOval(int @x, int @y, int @width, int @height);

		/// <summary>
		/// Draws a closed polygon defined by
		/// arrays of <i>x</i> and <i>y</i> coordinates.
		/// </summary>
		abstract public void drawPolygon(int[] @xPoints, int[] @yPoints, int @nPoints);

		/// <summary>
		/// Draws the outline of a polygon defined by the specified
		/// <code>Polygon</code> object.
		/// </summary>
		public void drawPolygon(Polygon @p)
		{
		}

		/// <summary>
		/// Draws a sequence of connected lines defined by
		/// arrays of <i>x</i> and <i>y</i> coordinates.
		/// </summary>
		abstract public void drawPolyline(int[] @xPoints, int[] @yPoints, int @nPoints);

		/// <summary>
		/// Draws the outline of the specified rectangle.
		/// </summary>
		public void drawRect(int @x, int @y, int @width, int @height)
		{
		}

		/// <summary>
		/// Draws an outlined round-cornered rectangle using this graphics
		/// context's current color.
		/// </summary>
		abstract public void drawRoundRect(int @x, int @y, int @width, int @height, int @arcWidth, int @arcHeight);

		/// <summary>
		/// Draws the text given by the specified iterator, using this
		/// graphics context's current color.
		/// </summary>
		abstract public void drawString(AttributedCharacterIterator @iterator, int @x, int @y);

		/// <summary>
		/// Draws the text given by the specified string, using this
		/// graphics context's current font and color.
		/// </summary>
		abstract public void drawString(string @str, int @x, int @y);

		/// <summary>
		/// Paints a 3-D highlighted rectangle filled with the current color.
		/// </summary>
		public void fill3DRect(int @x, int @y, int @width, int @height, bool @raised)
		{
		}

		/// <summary>
		/// Fills a circular or elliptical arc covering the specified rectangle.
		/// </summary>
		abstract public void fillArc(int @x, int @y, int @width, int @height, int @startAngle, int @arcAngle);

		/// <summary>
		/// Fills an oval bounded by the specified rectangle with the
		/// current color.
		/// </summary>
		abstract public void fillOval(int @x, int @y, int @width, int @height);

		/// <summary>
		/// Fills a closed polygon defined by
		/// arrays of <i>x</i> and <i>y</i> coordinates.
		/// </summary>
		abstract public void fillPolygon(int[] @xPoints, int[] @yPoints, int @nPoints);

		/// <summary>
		/// Fills the polygon defined by the specified Polygon object with
		/// the graphics context's current color.
		/// </summary>
		public void fillPolygon(Polygon @p)
		{
		}

		/// <summary>
		/// Fills the specified rectangle.
		/// </summary>
		abstract public void fillRect(int @x, int @y, int @width, int @height);

		/// <summary>
		/// Fills the specified rounded corner rectangle with the current color.
		/// </summary>
		abstract public void fillRoundRect(int @x, int @y, int @width, int @height, int @arcWidth, int @arcHeight);

		/// <summary>
		/// Disposes of this graphics context once it is no longer referenced.
		/// </summary>
		public void finalize()
		{
		}

		/// <summary>
		/// Gets the current clipping area.
		/// </summary>
		public Shape getClip()
		{
			return default(Shape);
		}

		/// <summary>
		/// Returns the bounding rectangle of the current clipping area.
		/// </summary>
		public Rectangle getClipBounds()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the bounding rectangle of the current clipping area.
		/// </summary>
		public Rectangle getClipBounds(Rectangle @r)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>As of JDK version 1.1,
		/// replaced by <code>getClipBounds()</code>.</I>
		/// </summary>
		public Rectangle getClipRect()
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Gets this graphics context's current color.
		/// </summary>
		public Color getColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Gets the current font.
		/// </summary>
		public Font getFont()
		{
			return default(Font);
		}

		/// <summary>
		/// Gets the font metrics of the current font.
		/// </summary>
		public FontMetrics getFontMetrics()
		{
			return default(FontMetrics);
		}

		/// <summary>
		/// Gets the font metrics for the specified font.
		/// </summary>
		public FontMetrics getFontMetrics(Font @f)
		{
			return default(FontMetrics);
		}

		/// <summary>
		/// Returns true if the specified rectangular area might intersect
		/// the current clipping area.
		/// </summary>
		public bool hitClip(int @x, int @y, int @width, int @height)
		{
			return default(bool);
		}

		/// <summary>
		/// Sets the current clip to the rectangle specified by the given
		/// coordinates.
		/// </summary>
		abstract public void setClip(int @x, int @y, int @width, int @height);

		/// <summary>
		/// Sets the current clipping area to an arbitrary clip shape.
		/// </summary>
		abstract public void setClip(Shape @clip);

		/// <summary>
		/// Sets this graphics context's current color to the specified
		/// color.
		/// </summary>
		abstract public void setColor(Color @c);

		/// <summary>
		/// Sets this graphics context's font to the specified font.
		/// </summary>
		abstract public void setFont(Font @font);

		/// <summary>
		/// Sets the paint mode of this graphics context to overwrite the
		/// destination with this graphics context's current color.
		/// </summary>
		abstract public void setPaintMode();

		/// <summary>
		/// Sets the paint mode of this graphics context to alternate between
		/// this graphics context's current color and the new specified color.
		/// </summary>
		abstract public void setXORMode(Color @c1);

		/// <summary>
		/// Returns a <code>String</code> object representing this
		/// <code>Graphics</code> object's value.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Translates the origin of the graphics context to the point
		/// (<i>x</i>, <i>y</i>) in the current coordinate system.
		/// </summary>
		abstract public void translate(int @x, int @y);

	}
}

