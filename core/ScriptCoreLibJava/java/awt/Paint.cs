// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.geom;
using java.awt.image;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Paint.html
	[Script(IsNative = true)]
	public interface Paint : Transparency
	{
		/// <summary>
		/// Creates and returns a <A HREF="../../java/awt/PaintContext.html" title="interface in java.awt"><CODE>PaintContext</CODE></A> used to
		/// generate the color pattern.
		/// </summary>
		PaintContext createContext(ColorModel @cm, Rectangle @deviceBounds, Rectangle2D @userBounds, AffineTransform @xform, RenderingHints @hints);

	}
}

