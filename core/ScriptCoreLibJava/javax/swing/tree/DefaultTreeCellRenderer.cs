// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.DefaultTreeCellRenderer

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.swing;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/DefaultTreeCellRenderer.html
	[Script(IsNative = true)]
	public class DefaultTreeCellRenderer : JLabel, TreeCellRenderer
	{
		/// <summary>
		/// Returns a new instance of DefaultTreeCellRenderer.
		/// </summary>
		public DefaultTreeCellRenderer()
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void firePropertyChange(string @propertyName, bool @oldValue, bool @newValue)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void firePropertyChange(string @propertyName, sbyte @oldValue, sbyte @newValue)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void firePropertyChange(string @propertyName, char @oldValue, char @newValue)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void firePropertyChange(string @propertyName, double @oldValue, double @newValue)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void firePropertyChange(string @propertyName, float @oldValue, float @newValue)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void firePropertyChange(string @propertyName, int @oldValue, int @newValue)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void firePropertyChange(string @propertyName, long @oldValue, long @newValue)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		protected void firePropertyChange(string @propertyName, object @oldValue, object @newValue)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void firePropertyChange(string @propertyName, short @oldValue, short @newValue)
		{
		}

		/// <summary>
		/// Returns the background color to be used for non selected nodes.
		/// </summary>
		public Color getBackgroundNonSelectionColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the color to use for the background if node is selected.
		/// </summary>
		public Color getBackgroundSelectionColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the color the border is drawn.
		/// </summary>
		public Color getBorderSelectionColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the icon used to represent non-leaf nodes that are not
		/// expanded.
		/// </summary>
		public Icon getClosedIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the default icon, for the current laf, that is used to
		/// represent non-leaf nodes that are not expanded.
		/// </summary>
		public Icon getDefaultClosedIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the default icon, for the current laf, that is used to
		/// represent leaf nodes.
		/// </summary>
		public Icon getDefaultLeafIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the default icon, for the current laf, that is used to
		/// represent non-leaf nodes that are expanded.
		/// </summary>
		public Icon getDefaultOpenIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Gets the font of this component.
		/// </summary>
		public Font getFont()
		{
			return default(Font);
		}

		/// <summary>
		/// Returns the icon used to represent leaf nodes.
		/// </summary>
		public Icon getLeafIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the icon used to represent non-leaf nodes that are expanded.
		/// </summary>
		public Icon getOpenIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Overrides <code>JComponent.getPreferredSize</code> to
		/// return slightly wider preferred size value.
		/// </summary>
		public Dimension getPreferredSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the color the text is drawn with when the node isn't selected.
		/// </summary>
		public Color getTextNonSelectionColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Returns the color the text is drawn with when the node is selected.
		/// </summary>
		public Color getTextSelectionColor()
		{
			return default(Color);
		}

		/// <summary>
		/// Configures the renderer based on the passed in components.
		/// </summary>
		public Component getTreeCellRendererComponent(JTree @tree, object @value, bool @sel, bool @expanded, bool @leaf, int @row, bool @hasFocus)
		{
			return default(Component);
		}

		/// <summary>
		/// Paints the value.
		/// </summary>
		public void paint(Graphics @g)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void repaint(long @tm, int @x, int @y, int @width, int @height)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void repaint(Rectangle @r)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void revalidate()
		{
		}

		/// <summary>
		/// Subclassed to map <code>ColorUIResource</code>s to null.
		/// </summary>
		public void setBackground(Color @color)
		{
		}

		/// <summary>
		/// Sets the background color to be used for non selected nodes.
		/// </summary>
		public void setBackgroundNonSelectionColor(Color @newColor)
		{
		}

		/// <summary>
		/// Sets the color to use for the background if node is selected.
		/// </summary>
		public void setBackgroundSelectionColor(Color @newColor)
		{
		}

		/// <summary>
		/// Sets the color to use for the border.
		/// </summary>
		public void setBorderSelectionColor(Color @newColor)
		{
		}

		/// <summary>
		/// Sets the icon used to represent non-leaf nodes that are not expanded.
		/// </summary>
		public void setClosedIcon(Icon @newIcon)
		{
		}

		/// <summary>
		/// Subclassed to map <code>FontUIResource</code>s to null.
		/// </summary>
		public void setFont(Font @font)
		{
		}

		/// <summary>
		/// Sets the icon used to represent leaf nodes.
		/// </summary>
		public void setLeafIcon(Icon @newIcon)
		{
		}

		/// <summary>
		/// Sets the icon used to represent non-leaf nodes that are expanded.
		/// </summary>
		public void setOpenIcon(Icon @newIcon)
		{
		}

		/// <summary>
		/// Sets the color the text is drawn with when the node isn't selected.
		/// </summary>
		public void setTextNonSelectionColor(Color @newColor)
		{
		}

		/// <summary>
		/// Sets the color the text is drawn with when the node is selected.
		/// </summary>
		public void setTextSelectionColor(Color @newColor)
		{
		}

		/// <summary>
		/// Overridden for performance reasons.
		/// </summary>
		public void validate()
		{
		}

	}
}
