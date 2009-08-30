// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/BorderLayout.html
	[Script(IsNative = true)]
	public class BorderLayout : LayoutManager
	{
		/// <summary>
		/// Constructs a new border layout with
		/// no gaps between components.
		/// </summary>
		public BorderLayout()
		{
		}

		/// <summary>
		/// Constructs a border layout with the specified gaps
		/// between components.
		/// </summary>
		public BorderLayout(int @hgap, int @vgap)
		{
		}

		/// <summary>
		/// Adds the specified component to the layout, using the specified
		/// constraint object.
		/// </summary>
		public void addLayoutComponent(Component @comp, object @constraints)
		{
		}

		/// <summary>
		/// If the layout manager uses a per-component string,
		/// adds the component <code>comp</code> to the layout,
		/// associating it
		/// with the string specified by <code>name</code>.
		/// </summary>
		public void addLayoutComponent(string @name, Component @comp)
		{
		}

		/// <summary>
		/// Returns the horizontal gap between components.
		/// </summary>
		public int getHgap()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the alignment along the x axis.
		/// </summary>
		public float getLayoutAlignmentX(Container @parent)
		{
			return default(float);
		}

		/// <summary>
		/// Returns the alignment along the y axis.
		/// </summary>
		public float getLayoutAlignmentY(Container @parent)
		{
			return default(float);
		}

		/// <summary>
		/// Returns the vertical gap between components.
		/// </summary>
		public int getVgap()
		{
			return default(int);
		}

		/// <summary>
		/// Invalidates the layout, indicating that if the layout manager
		/// has cached information it should be discarded.
		/// </summary>
		public void invalidateLayout(Container @target)
		{
		}

		/// <summary>
		/// Lays out the container argument using this border layout.
		/// </summary>
		public void layoutContainer(Container @target)
		{
		}

		/// <summary>
		/// Returns the maximum dimensions for this layout given the components
		/// in the specified target container.
		/// </summary>
		public Dimension maximumLayoutSize(Container @target)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Determines the minimum size of the <code>target</code> container
		/// using this layout manager.
		/// </summary>
		public Dimension minimumLayoutSize(Container @target)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Determines the preferred size of the <code>target</code>
		/// container using this layout manager, based on the components
		/// in the container.
		/// </summary>
		public Dimension preferredLayoutSize(Container @target)
		{
			return default(Dimension);
		}

		/// <summary>
		/// Removes the specified component from this border layout.
		/// </summary>
		public void removeLayoutComponent(Component @comp)
		{
		}

		/// <summary>
		/// Sets the horizontal gap between components.
		/// </summary>
		public void setHgap(int @hgap)
		{
		}

		/// <summary>
		/// Sets the vertical gap between components.
		/// </summary>
		public void setVgap(int @vgap)
		{
		}

		/// <summary>
		/// Returns a string representation of the state of this border layout.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}

