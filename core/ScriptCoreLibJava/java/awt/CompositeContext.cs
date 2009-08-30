// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.image;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/CompositeContext.html
	[Script(IsNative = true)]
	public interface CompositeContext
	{
		/// <summary>
		/// Composes the two source <A HREF="../../java/awt/image/Raster.html" title="class in java.awt.image"><CODE>Raster</CODE></A> objects and
		/// places the result in the destination
		/// <A HREF="../../java/awt/image/WritableRaster.html" title="class in java.awt.image"><CODE>WritableRaster</CODE></A>.
		/// </summary>
		void compose(Raster @src, Raster @dstIn, WritableRaster @dstOut);

		/// <summary>
		/// Releases resources allocated for a context.
		/// </summary>
		void dispose();

	}
}

