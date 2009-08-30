// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.font;
using java.awt.geom;
using java.awt.image;
using java.awt.image.renderable;
using java.lang;
using java.text;
using java.util;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Graphics2D.html
	[Script(IsNative = true)]
	public abstract class Graphics2D : Graphics
	{
		/// <summary>
		/// Constructs a new <code>Graphics2D</code> object.
		/// </summary>
		public Graphics2D()
		{
		}

		/// <summary>
		/// Sets the values of an arbitrary number of preferences for the
		/// rendering algorithms.
		/// </summary>
		abstract public void addRenderingHints(Map @hints);

		/// <summary>
		/// Intersects the current <code>Clip</code> with the interior of the
		/// specified <code>Shape</code> and sets the <code>Clip</code> to the
		/// resulting intersection.
		/// </summary>
		abstract public void clip(Shape @s);

		/// <summary>
		/// Strokes the outline of a <code>Shape</code> using the settings of the
		/// current <code>Graphics2D</code> context.
		/// </summary>
		abstract public void draw(Shape @s);

		/// <summary>
		/// Draws a 3-D highlighted outline of the specified rectangle.
		/// </summary>
		public void draw3DRect(int @x, int @y, int @width, int @height, bool @raised)
		{
		}

		/// <summary>
		/// Renders the text of the specified
		/// <A HREF="../../java/awt/font/GlyphVector.html" title="class in java.awt.font"><CODE>GlyphVector</CODE></A> using
		/// the <code>Graphics2D</code> context's rendering attributes.
		/// </summary>
		abstract public void drawGlyphVector(GlyphVector @g, float @x, float @y);

		/// <summary>
		/// Renders a <code>BufferedImage</code> that is
		/// filtered with a
		/// <A HREF="../../java/awt/image/BufferedImageOp.html" title="interface in java.awt.image"><CODE>BufferedImageOp</CODE></A>.
		/// </summary>
		abstract public void drawImage(BufferedImage @img, BufferedImageOp @op, int @x, int @y);

		/// <summary>
		/// Renders an image, applying a transform from image space into user space
		/// before drawing.
		/// </summary>
		abstract public bool drawImage(Image @img, AffineTransform @xform, ImageObserver @obs);

		/// <summary>
		/// Renders a
		/// <A HREF="../../java/awt/image/renderable/RenderableImage.html" title="interface in java.awt.image.renderable"><CODE>RenderableImage</CODE></A>,
		/// applying a transform from image space into user space before drawing.
		/// </summary>
		abstract public void drawRenderableImage(RenderableImage @img, AffineTransform @xform);

		/// <summary>
		/// Renders a <A HREF="../../java/awt/image/RenderedImage.html" title="interface in java.awt.image"><CODE>RenderedImage</CODE></A>,
		/// applying a transform from image
		/// space into user space before drawing.
		/// </summary>
		abstract public void drawRenderedImage(RenderedImage @img, AffineTransform @xform);

		/// <summary>
		/// Renders the text of the specified iterator, using the
		/// <code>Graphics2D</code> context's current <code>Paint</code>.
		/// </summary>
		abstract public void drawString(AttributedCharacterIterator @iterator, float @x, float @y);

		/// <summary>
		/// Renders the text of the specified iterator, using the
		/// <code>Graphics2D</code> context's current <code>Paint</code>.
		/// </summary>
		abstract public override void drawString(AttributedCharacterIterator @iterator, int @x, int @y);

		/// <summary>
		/// Renders the text specified by the specified <code>String</code>,
		/// using the current text attribute state in the <code>Graphics2D</code> context.
		/// </summary>
		abstract public void drawString(string @s, float @x, float @y);

		/// <summary>
		/// Renders the text of the specified <code>String</code>, using the
		/// current text attribute state in the <code>Graphics2D</code> context.
		/// </summary>
		abstract public override void drawString(string @str, int @x, int @y);

		/// <summary>
		/// Fills the interior of a <code>Shape</code> using the settings of the
		/// <code>Graphics2D</code> context.
		/// </summary>
		abstract public void fill(Shape @s);

		/// <summary>
		/// Paints a 3-D highlighted rectangle filled with the current color.
		/// </summary>
		public void fill3DRect(int @x, int @y, int @width, int @height, bool @raised)
		{
		}

		/// <summary>
		/// Returns the background color used for clearing a region.
		/// </summary>
		public Color getBackground()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the current <code>Composite</code> in the
		/// <code>Graphics2D</code> context.
		/// </summary>
		public Composite getComposite()
		{
			return default(Composite);
		}

		/// <summary>
		/// Returns the device configuration associated with this
		/// <code>Graphics2D</code>.
		/// </summary>
		public GraphicsConfiguration getDeviceConfiguration()
		{
			return default(GraphicsConfiguration);
		}

		/// <summary>
		/// Get the rendering context of the <code>Font</code> within this
		/// <code>Graphics2D</code> context.
		/// </summary>
		public FontRenderContext getFontRenderContext()
		{
			return default(FontRenderContext);
		}

		/// <summary>
		/// Returns the current <code>Paint</code> of the
		/// <code>Graphics2D</code> context.
		/// </summary>
		public Paint getPaint()
		{
			return default(Paint);
		}

		/// <summary>
		/// Returns the value of a single preference for the rendering algorithms.
		/// </summary>
		public object getRenderingHint(RenderingHints.Key @hintKey)
		{
			return default(object);
		}

		/// <summary>
		/// Gets the preferences for the rendering algorithms.
		/// </summary>
		public RenderingHints getRenderingHints()
		{
			return default(RenderingHints);
		}

		/// <summary>
		/// Returns the current <code>Stroke</code> in the
		/// <code>Graphics2D</code> context.
		/// </summary>
		public Stroke getStroke()
		{
			return default(Stroke);
		}

		/// <summary>
		/// Returns a copy of the current <code>Transform</code> in the
		/// <code>Graphics2D</code> context.
		/// </summary>
		public AffineTransform getTransform()
		{
			return default(AffineTransform);
		}

		/// <summary>
		/// Checks whether or not the specified <code>Shape</code> intersects
		/// the specified <A HREF="../../java/awt/Rectangle.html" title="class in java.awt"><CODE>Rectangle</CODE></A>, which is in device
		/// space.
		/// </summary>
		abstract public bool hit(Rectangle @rect, Shape @s, bool @onStroke);

		/// <summary>
		/// Concatenates the current <code>Graphics2D</code>
		/// <code>Transform</code> with a rotation transform.
		/// </summary>
		abstract public void rotate(double @theta);

		/// <summary>
		/// Concatenates the current <code>Graphics2D</code>
		/// <code>Transform</code> with a translated rotation
		/// transform.
		/// </summary>
		abstract public void rotate(double @theta, double @x, double @y);

		/// <summary>
		/// Concatenates the current <code>Graphics2D</code>
		/// <code>Transform</code> with a scaling transformation
		/// Subsequent rendering is resized according to the specified scaling
		/// factors relative to the previous scaling.
		/// </summary>
		abstract public void scale(double @sx, double @sy);

		/// <summary>
		/// Sets the background color for the <code>Graphics2D</code> context.
		/// </summary>
		abstract public void setBackground(Color @color);

		/// <summary>
		/// Sets the <code>Composite</code> for the <code>Graphics2D</code> context.
		/// </summary>
		abstract public void setComposite(Composite @comp);

		/// <summary>
		/// Sets the <code>Paint</code> attribute for the
		/// <code>Graphics2D</code> context.
		/// </summary>
		abstract public void setPaint(Paint @paint);

		/// <summary>
		/// Sets the value of a single preference for the rendering algorithms.
		/// </summary>
		abstract public void setRenderingHint(RenderingHints.Key @hintKey, object @hintValue);

		/// <summary>
		/// Replaces the values of all preferences for the rendering
		/// algorithms with the specified <code>hints</code>.
		/// </summary>
		abstract public void setRenderingHints(Map @hints);

		/// <summary>
		/// Sets the <code>Stroke</code> for the <code>Graphics2D</code> context.
		/// </summary>
		abstract public void setStroke(Stroke @s);

		/// <summary>
		/// Overwrites the Transform in the <code>Graphics2D</code> context.
		/// </summary>
		abstract public void setTransform(AffineTransform @Tx);

		/// <summary>
		/// Concatenates the current <code>Graphics2D</code>
		/// <code>Transform</code> with a shearing transform.
		/// </summary>
		abstract public void shear(double @shx, double @shy);

		/// <summary>
		/// Composes an <code>AffineTransform</code> object with the
		/// <code>Transform</code> in this <code>Graphics2D</code> according
		/// to the rule last-specified-first-applied.
		/// </summary>
		abstract public void transform(AffineTransform @Tx);

		/// <summary>
		/// Concatenates the current
		/// <code>Graphics2D</code> <code>Transform</code>
		/// with a translation transform.
		/// </summary>
		abstract public void translate(double @tx, double @ty);

		/// <summary>
		/// Translates the origin of the <code>Graphics2D</code> context to the
		/// point (<i>x</i>, <i>y</i>) in the current coordinate system.
		/// </summary>
		abstract public override void translate(int @x, int @y);

	}
}

