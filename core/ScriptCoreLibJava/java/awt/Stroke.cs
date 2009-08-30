// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Stroke.html
	[Script(IsNative = true)]
	public interface Stroke
	{
		/// <summary>
		/// Returns an outline <code>Shape</code> which encloses the area that
		/// should be painted when the <code>Shape</code> is stroked according
		/// to the rules defined by the
		/// object implementing the <code>Stroke</code> interface.
		/// </summary>
		Shape createStrokedShape(Shape @p);

	}
}

