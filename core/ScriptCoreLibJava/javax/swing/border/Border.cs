// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;

namespace javax.swing.border
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/border/Border.html
	[Script(IsNative = true)]
	public interface Border
	{
		/// <summary>
		/// Returns the insets of the border.
		/// </summary>
		Insets getBorderInsets(Component @c);

		/// <summary>
		/// Returns whether or not the border is opaque.
		/// </summary>
		bool isBorderOpaque();

		/// <summary>
		/// Paints the border for the specified component with the specified
		/// position and size.
		/// </summary>
		void paintBorder(Component @c, Graphics @g, int @x, int @y, int @width, int @height);

	}
}

