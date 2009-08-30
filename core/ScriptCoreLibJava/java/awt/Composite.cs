// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.image;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Composite.html
	[Script(IsNative = true)]
	public interface Composite
	{
		/// <summary>
		/// Creates a context containing state that is used to perform
		/// the compositing operation.
		/// </summary>
		CompositeContext createContext(ColorModel @srcColorModel, ColorModel @dstColorModel, RenderingHints @hints);

	}
}

