// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using javax.accessibility;
using javax.swing;
using javax.swing.plaf;

namespace javax.swing.plaf
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/plaf/ComponentUI.html
	[Script(IsNative = true)]
	public class ComponentUI
	{
		/// <summary>
		/// Sole constructor.
		/// </summary>
		public ComponentUI()
		{
		}

		/// <summary>
		/// Returns <code>true</code> if the specified <i>x,y</i> location is
		/// contained within the look and feel's defined shape of the specified
		/// component.
		/// </summary>
		public bool contains(JComponent @c, int @x, int @y)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns an instance of the UI delegate for the specified component.
		/// </summary>
		public ComponentUI createUI(JComponent @c)
		{
			return default(ComponentUI);
		}

		/// <summary>
		/// Returns the <code>i</code>th <code>Accessible</code> child of the object.
		/// </summary>
		public Accessible getAccessibleChild(JComponent @c, int @i)
		{
			return default(Accessible);
		}

		/// <summary>
		/// Returns the number of accessible children in the object.
		/// </summary>
		public int getAccessibleChildrenCount(JComponent @c)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the specified component's maximum size appropriate for
		/// the look and feel.
		/// </summary>
		public Dimension getMaximumSize(JComponent @c)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the specified component's minimum size appropriate for
		/// the look and feel.
		/// </summary>
		public Dimension getMinimumSize(JComponent @c)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the specified component's preferred size appropriate for
		/// the look and feel.
		/// </summary>
		public Dimension getPreferredSize(JComponent @c)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Configures the specified component appropriate for the look and feel.
		/// </summary>
		public void installUI(JComponent @c)
		{
		}

		/// <summary>
		/// Paints the specified component appropriate for the look and feel.
		/// </summary>
		public void paint(Graphics @g, JComponent @c)
		{
		}

		/// <summary>
		/// Reverses configuration which was done on the specified component during
		/// <code>installUI</code>.
		/// </summary>
		public void uninstallUI(JComponent @c)
		{
		}

		/// <summary>
		/// Notifies this UI delegate that it's time to paint the specified
		/// component.
		/// </summary>
		public void update(Graphics @g, JComponent @c)
		{
		}

	}
}

