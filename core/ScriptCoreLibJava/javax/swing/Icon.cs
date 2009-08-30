// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/Icon.html
	[Script(IsNative = true)]
	public interface Icon
	{
		/// <summary>
		/// Returns the icon's height.
		/// </summary>
		int getIconHeight();

		/// <summary>
		/// Returns the icon's width.
		/// </summary>
		int getIconWidth();

		/// <summary>
		/// Draw the icon at the specified location.
		/// </summary>
		void paintIcon(Component @c, Graphics @g, int @x, int @y);

	}
}

