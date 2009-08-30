// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.image;
using java.awt.image.renderable;
using java.lang;
using java.util;

namespace java.awt.image.renderable
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/image/renderable/RenderableImage.html
	[Script(IsNative = true)]
	public interface RenderableImage
	{
		/// <summary>
		/// Returnd a RenderedImage instance of this image with a default
		/// width and height in pixels.
		/// </summary>
		RenderedImage createDefaultRendering();

		/// <summary>
		/// Creates a RenderedImage that represented a rendering of this image
		/// using a given RenderContext.
		/// </summary>
		RenderedImage createRendering(RenderContext @renderContext);

		/// <summary>
		/// Creates a RenderedImage instance of this image with width w, and
		/// height h in pixels.
		/// </summary>
		RenderedImage createScaledRendering(int @w, int @h, RenderingHints @hints);

		/// <summary>
		/// Gets the height in user coordinate space.
		/// </summary>
		float getHeight();

		/// <summary>
		/// Gets the minimum X coordinate of the rendering-independent image data.
		/// </summary>
		float getMinX();

		/// <summary>
		/// Gets the minimum Y coordinate of the rendering-independent image data.
		/// </summary>
		float getMinY();

		/// <summary>
		/// Gets a property from the property set of this image.
		/// </summary>
		object getProperty(string @name);

		/// <summary>
		/// Returns a list of names recognized by getProperty.
		/// </summary>
		String[] getPropertyNames();

		/// <summary>
		/// Returns a vector of RenderableImages that are the sources of
		/// image data for this RenderableImage.
		/// </summary>
		Vector getSources();

		/// <summary>
		/// Gets the width in user coordinate space.
		/// </summary>
		float getWidth();

		/// <summary>
		/// Returns true if successive renderings (that is, calls to
		/// createRendering() or createScaledRendering()) with the same arguments
		/// may produce different results.
		/// </summary>
		bool isDynamic();

	}
}

