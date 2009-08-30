// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;
using java.util;
using javax.accessibility;
using javax.swing;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JLayeredPane.html
	[Script(IsNative = true)]
	public class JLayeredPane : JComponent
	{
		/// <summary>
		/// Create a new JLayeredPane
		/// </summary>
		public JLayeredPane()
		{
		}

		/// <summary>
		/// Adds the specified component to this container at the specified
		/// index.
		/// </summary>
		protected void addImpl(Component @comp, object @constraints, int @index)
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JLayeredPane.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the number of children currently in the specified layer.
		/// </summary>
		public int getComponentCountInLayer(int @layer)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of the components in the specified layer.
		/// </summary>
		public Component getComponentsInLayer(int @layer)
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the hashtable that maps components to layers.
		/// </summary>
		public Hashtable getComponentToLayer()
		{
			return default(Hashtable);
		}

		/// <summary>
		/// Returns the index of the specified Component.
		/// </summary>
		public int getIndexOf(Component @c)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the layer attribute for the specified Component.
		/// </summary>
		public int getLayer(Component @c)
		{
			return default(int);
		}

		/// <summary>
		/// Gets the layer property for a JComponent, it
		/// does not cause any side effects like setLayer().
		/// </summary>
		static public int getLayer(JComponent @c)
		{
			return default(int);
		}

		/// <summary>
		/// Convenience method that returns the first JLayeredPane which
		/// contains the specified component.
		/// </summary>
		public JLayeredPane getLayeredPaneAbove(Component @c)
		{
			return default(JLayeredPane);
		}

		/// <summary>
		/// Returns the Integer object associated with a specified layer.
		/// </summary>
		public Integer getObjectForLayer(int @layer)
		{
			return default(Integer);
		}

		/// <summary>
		/// Get the relative position of the component within its layer.
		/// </summary>
		public int getPosition(Component @c)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the highest layer value from all current children.
		/// </summary>
		public int highestLayer()
		{
			return default(int);
		}

		/// <summary>
		/// Primitive method that determines the proper location to
		/// insert a new child based on layer and position requests.
		/// </summary>
		protected int insertIndexForLayer(int @layer, int @position)
		{
			return default(int);
		}

		/// <summary>
		/// Returns false if components in the pane can overlap, which makes
		/// optimized drawing impossible.
		/// </summary>
		public bool isOptimizedDrawingEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the lowest layer value from all current children.
		/// </summary>
		public int lowestLayer()
		{
			return default(int);
		}

		/// <summary>
		/// Moves the component to the bottom of the components in its current layer
		/// (position -1).
		/// </summary>
		public void moveToBack(Component @c)
		{
		}

		/// <summary>
		/// Moves the component to the top of the components in its current layer
		/// (position 0).
		/// </summary>
		public void moveToFront(Component @c)
		{
		}

		/// <summary>
		/// Paints this JLayeredPane within the specified graphics context.
		/// </summary>
		public void paint(Graphics @g)
		{
		}

		/// <summary>
		/// Returns a string representation of this JLayeredPane.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Sets the layer property on a JComponent.
		/// </summary>
		static public void putLayer(JComponent @c, int @layer)
		{
		}

		/// <summary>
		/// Remove the indexed component from this pane.
		/// </summary>
		public void remove(int @index)
		{
		}

		/// <summary>
		/// Sets the layer attribute on the specified component,
		/// making it the bottommost component in that layer.
		/// </summary>
		public void setLayer(Component @c, int @layer)
		{
		}

		/// <summary>
		/// Sets the layer attribute for the specified component and
		/// also sets its position within that layer.
		/// </summary>
		public void setLayer(Component @c, int @layer, int @position)
		{
		}

		/// <summary>
		/// Moves the component to <code>position</code> within its current layer,
		/// where 0 is the topmost position within the layer and -1 is the bottommost
		/// position.
		/// </summary>
		public void setPosition(Component @c, int @position)
		{
		}

	}
}
