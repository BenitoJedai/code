// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.tree.TreeSelectionModel

using ScriptCoreLib;
using java.beans;
using javax.swing.@event;

namespace javax.swing.tree
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/tree/TreeSelectionModel.html
	[Script(IsNative = true)]
	public interface TreeSelectionModel
	{
		/// <summary>
		/// Adds a PropertyChangeListener to the listener list.
		/// </summary>
		void addPropertyChangeListener(PropertyChangeListener @listener);

		/// <summary>
		/// Adds path to the current selection.
		/// </summary>
		void addSelectionPath(TreePath @path);

		/// <summary>
		/// Adds paths to the current selection.
		/// </summary>
		void addSelectionPaths(TreePath[] @paths);

		/// <summary>
		/// Adds x to the list of listeners that are notified each time the
		/// set of selected TreePaths changes.
		/// </summary>
		void addTreeSelectionListener(TreeSelectionListener @x);

		/// <summary>
		/// Empties the current selection.
		/// </summary>
		void clearSelection();

		/// <summary>
		/// Returns the last path that was added.
		/// </summary>
		TreePath getLeadSelectionPath();

		/// <summary>
		/// Returns the lead selection index.
		/// </summary>
		int getLeadSelectionRow();

		/// <summary>
		/// Returns the largest value obtained from the RowMapper for the
		/// current set of selected TreePaths.
		/// </summary>
		int getMaxSelectionRow();

		/// <summary>
		/// Returns the smallest value obtained from the RowMapper for the
		/// current set of selected TreePaths.
		/// </summary>
		int getMinSelectionRow();

		/// <summary>
		/// Returns the RowMapper instance that is able to map a TreePath to a
		/// row.
		/// </summary>
		RowMapper getRowMapper();

		/// <summary>
		/// Returns the number of paths that are selected.
		/// </summary>
		int getSelectionCount();

		/// <summary>
		/// Returns the current selection mode, one of
		/// <code>SINGLE_TREE_SELECTION</code>,
		/// <code>CONTIGUOUS_TREE_SELECTION</code> or
		/// <code>DISCONTIGUOUS_TREE_SELECTION</code>.
		/// </summary>
		int getSelectionMode();

		/// <summary>
		/// Returns the first path in the selection.
		/// </summary>
		TreePath getSelectionPath();

		/// <summary>
		/// Returns the paths in the selection.
		/// </summary>
		TreePath[] getSelectionPaths();

		/// <summary>
		/// Returns all of the currently selected rows.
		/// </summary>
		int[] getSelectionRows();

		/// <summary>
		/// Returns true if the path, <code>path</code>, is in the current
		/// selection.
		/// </summary>
		bool isPathSelected(TreePath @path);

		/// <summary>
		/// Returns true if the row identified by <code>row</code> is selected.
		/// </summary>
		bool isRowSelected(int @row);

		/// <summary>
		/// Returns true if the selection is currently empty.
		/// </summary>
		bool isSelectionEmpty();

		/// <summary>
		/// Removes a PropertyChangeListener from the listener list.
		/// </summary>
		void removePropertyChangeListener(PropertyChangeListener @listener);

		/// <summary>
		/// Removes path from the selection.
		/// </summary>
		void removeSelectionPath(TreePath @path);

		/// <summary>
		/// Removes paths from the selection.
		/// </summary>
		void removeSelectionPaths(TreePath[] @paths);

		/// <summary>
		/// Removes x from the list of listeners that are notified each time
		/// the set of selected TreePaths changes.
		/// </summary>
		void removeTreeSelectionListener(TreeSelectionListener @x);

		/// <summary>
		/// Updates this object's mapping from TreePaths to rows.
		/// </summary>
		void resetRowSelection();

		/// <summary>
		/// Sets the RowMapper instance.
		/// </summary>
		void setRowMapper(RowMapper @newMapper);

		/// <summary>
		/// Sets the selection model, which must be one of SINGLE_TREE_SELECTION,
		/// CONTIGUOUS_TREE_SELECTION or DISCONTIGUOUS_TREE_SELECTION.
		/// </summary>
		void setSelectionMode(int @mode);

		/// <summary>
		/// Sets the selection to path.
		/// </summary>
		void setSelectionPath(TreePath @path);

		/// <summary>
		/// Sets the selection to path.
		/// </summary>
		void setSelectionPaths(TreePath[] @paths);

	}
}
