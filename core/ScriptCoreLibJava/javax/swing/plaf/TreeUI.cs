// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.plaf.TreeUI

using ScriptCoreLib;
using java.awt;
using javax.swing;
using javax.swing.tree;

namespace javax.swing.plaf
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/plaf/TreeUI.html
	[Script(IsNative = true)]
	public abstract class TreeUI : ComponentUI
	{
		/// <summary>
		/// 
		/// </summary>
		public TreeUI()
		{
		}

		/// <summary>
		/// Cancels the current editing session.
		/// </summary>
		abstract public void cancelEditing(JTree @tree);

		/// <summary>
		/// Returns the path to the node that is closest to x,y.
		/// </summary>
		abstract public TreePath getClosestPathForLocation(JTree @tree, int @x, int @y);

		/// <summary>
		/// Returns the path to the element that is being edited.
		/// </summary>
		abstract public TreePath getEditingPath(JTree @tree);

		/// <summary>
		/// Returns the Rectangle enclosing the label portion that the
		/// last item in path will be drawn into.
		/// </summary>
		abstract public Rectangle getPathBounds(JTree @tree, TreePath @path);

		/// <summary>
		/// Returns the path for passed in row.
		/// </summary>
		abstract public TreePath getPathForRow(JTree @tree, int @row);

		/// <summary>
		/// Returns the number of rows that are being displayed.
		/// </summary>
		abstract public int getRowCount(JTree @tree);

		/// <summary>
		/// Returns the row that the last item identified in path is visible
		/// at.
		/// </summary>
		abstract public int getRowForPath(JTree @tree, TreePath @path);

		/// <summary>
		/// Returns true if the tree is being edited.
		/// </summary>
		abstract public bool isEditing(JTree @tree);

		/// <summary>
		/// Selects the last item in path and tries to edit it.
		/// </summary>
		abstract public void startEditingAtPath(JTree @tree, TreePath @path);

		/// <summary>
		/// Stops the current editing session.
		/// </summary>
		abstract public bool stopEditing(JTree @tree);

	}
}
