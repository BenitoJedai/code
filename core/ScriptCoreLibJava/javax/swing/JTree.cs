// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JTree

using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using java.util;
using javax.accessibility;
using javax.swing.@event;
using javax.swing.plaf;
using javax.swing.text;
using javax.swing.tree;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JTree.html
	[Script(IsNative = true)]
	public class JTree : JComponent
	{
		/// <summary>
		/// Returns a <code>JTree</code> with a sample model.
		/// </summary>
		public JTree()
		{
		}

		/// <summary>
		/// Returns a <code>JTree</code> created from a <code>Hashtable</code>
		/// which does not display with root.
		/// </summary>
		public JTree(Hashtable @value)
		{
		}

		/// <summary>
		/// Returns a <code>JTree</code> with each element of the
		/// specified array as the
		/// child of a new root node which is not displayed.
		/// </summary>
		public JTree(object[] @value)
		{
		}

		/// <summary>
		/// Returns an instance of <code>JTree</code> which displays the root node
		/// -- the tree is created using the specified data model.
		/// </summary>
		public JTree(TreeModel @newModel)
		{
		}

		/// <summary>
		/// Returns a <code>JTree</code> with the specified
		/// <code>TreeNode</code> as its root,
		/// which displays the root node.
		/// </summary>
		public JTree(TreeNode @root)
		{
		}

		/// <summary>
		/// Returns a <code>JTree</code> with the specified <code>TreeNode</code>
		/// as its root, which
		/// displays the root node and which decides whether a node is a
		/// leaf node in the specified manner.
		/// </summary>
		public JTree(TreeNode @root, bool @asksAllowsChildren)
		{
		}

		/// <summary>
		/// Returns a <code>JTree</code> with each element of the specified
		/// <code>Vector</code> as the
		/// child of a new root node which is not displayed.
		/// </summary>
		public JTree(Vector @value)
		{
		}

		/// <summary>
		/// Adds the paths between index0 and index1, inclusive, to the
		/// selection.
		/// </summary>
		public void addSelectionInterval(int @index0, int @index1)
		{
		}

		/// <summary>
		/// Adds the node identified by the specified <code>TreePath</code>
		/// to the current selection.
		/// </summary>
		public void addSelectionPath(TreePath @path)
		{
		}

		/// <summary>
		/// Adds each path in the array of paths to the current selection.
		/// </summary>
		public void addSelectionPaths(TreePath[] @paths)
		{
		}

		/// <summary>
		/// Adds the path at the specified row to the current selection.
		/// </summary>
		public void addSelectionRow(int @row)
		{
		}

		/// <summary>
		/// Adds the paths at each of the specified rows to the current selection.
		/// </summary>
		public void addSelectionRows(int[] @rows)
		{
		}

		/// <summary>
		/// Adds a listener for <code>TreeExpansion</code> events.
		/// </summary>
		public void addTreeExpansionListener(TreeExpansionListener @tel)
		{
		}

		/// <summary>
		/// Adds a listener for <code>TreeSelection</code> events.
		/// </summary>
		public void addTreeSelectionListener(TreeSelectionListener @tsl)
		{
		}

		/// <summary>
		/// Adds a listener for <code>TreeWillExpand</code> events.
		/// </summary>
		public void addTreeWillExpandListener(TreeWillExpandListener @tel)
		{
		}

		/// <summary>
		/// Cancels the current editing session.
		/// </summary>
		public void cancelEditing()
		{
		}

		/// <summary>
		/// Clears the selection.
		/// </summary>
		public void clearSelection()
		{
		}

		/// <summary>
		/// Clears the cache of toggled tree paths.
		/// </summary>
		protected void clearToggledPaths()
		{
		}

		/// <summary>
		/// Ensures that the node identified by the specified path is
		/// collapsed and viewable.
		/// </summary>
		public void collapsePath(TreePath @path)
		{
		}

		/// <summary>
		/// Ensures that the node in the specified row is collapsed.
		/// </summary>
		public void collapseRow(int @row)
		{
		}

		/// <summary>
		/// Called by the renderers to convert the specified value to
		/// text.
		/// </summary>
		public string convertValueToText(object @value, bool @selected, bool @expanded, bool @leaf, int @row, bool @hasFocus)
		{
			return default(string);
		}

		/// <summary>
		/// Returns a <code>TreeModel</code> wrapping the specified object.
		/// </summary>
		static protected TreeModel createTreeModel(object @value)
		{
			return default(TreeModel);
		}

		/// <summary>
		/// Creates and returns an instance of <code>TreeModelHandler</code>.
		/// </summary>
		protected TreeModelListener createTreeModelListener()
		{
			return default(TreeModelListener);
		}

		/// <summary>
		/// Ensures that the node identified by the specified path is
		/// expanded and viewable.
		/// </summary>
		public void expandPath(TreePath @path)
		{
		}

		/// <summary>
		/// Ensures that the node in the specified row is expanded and
		/// viewable.
		/// </summary>
		public void expandRow(int @row)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		public void fireTreeCollapsed(TreePath @path)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		public void fireTreeExpanded(TreePath @path)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		public void fireTreeWillCollapse(TreePath @path)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		public void fireTreeWillExpand(TreePath @path)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireValueChanged(TreeSelectionEvent @e)
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JTree.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the path identified as the anchor.
		/// </summary>
		public TreePath getAnchorSelectionPath()
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns the editor used to edit entries in the tree.
		/// </summary>
		public TreeCellEditor getCellEditor()
		{
			return default(TreeCellEditor);
		}

		/// <summary>
		/// Returns the current <code>TreeCellRenderer</code>
		/// that is rendering each cell.
		/// </summary>
		public TreeCellRenderer getCellRenderer()
		{
			return default(TreeCellRenderer);
		}

		/// <summary>
		/// Returns the path to the node that is closest to x,y.
		/// </summary>
		public TreePath getClosestPathForLocation(int @x, int @y)
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns the row to the node that is closest to x,y.
		/// </summary>
		public int getClosestRowForLocation(int @x, int @y)
		{
			return default(int);
		}

		/// <summary>
		/// Creates and returns a sample <code>TreeModel</code>.
		/// </summary>
		static protected TreeModel getDefaultTreeModel()
		{
			return default(TreeModel);
		}

		/// <summary>
		/// Returns an <code>Enumeration</code> of <code>TreePaths</code>
		/// that have been expanded that
		/// are descendants of <code>parent</code>.
		/// </summary>
		protected Enumeration getDescendantToggledPaths(TreePath @parent)
		{
			return default(Enumeration);
		}

		/// <summary>
		/// Gets the value of the <code>dragEnabled</code> property.
		/// </summary>
		public bool getDragEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the path to the element that is currently being edited.
		/// </summary>
		public TreePath getEditingPath()
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns an <code>Enumeration</code> of the descendants of the
		/// path <code>parent</code> that
		/// are currently expanded.
		/// </summary>
		public Enumeration getExpandedDescendants(TreePath @parent)
		{
			return default(Enumeration);
		}

		/// <summary>
		/// Returns the <code>expandsSelectedPaths</code> property.
		/// </summary>
		public bool getExpandsSelectedPaths()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the indicator that tells what happens when editing is
		/// interrupted.
		/// </summary>
		public bool getInvokesStopCellEditing()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the last path component in the first node of the current
		/// selection.
		/// </summary>
		public object getLastSelectedPathComponent()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the path identified as the lead.
		/// </summary>
		public TreePath getLeadSelectionPath()
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns the row index corresponding to the lead path.
		/// </summary>
		public int getLeadSelectionRow()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the last selected row.
		/// </summary>
		public int getMaxSelectionRow()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the first selected row.
		/// </summary>
		public int getMinSelectionRow()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <code>TreeModel</code> that is providing the data.
		/// </summary>
		public TreeModel getModel()
		{
			return default(TreeModel);
		}

		///// <summary>
		///// Returns the TreePath to the next tree element that
		///// begins with a prefix.
		///// </summary>
		//public TreePath getNextMatch(string @prefix, int @startingRow, Position.Bias @bias)
		//{
		//    return default(TreePath);
		//}

		/// <summary>
		/// Returns <code>JTreePath</code> instances representing the path
		/// between index0 and index1 (including index1).
		/// </summary>
		protected TreePath[] getPathBetweenRows(int @index0, int @index1)
		{
			return default(TreePath[]);
		}

		/// <summary>
		/// Returns the <code>Rectangle</code> that the specified node will be drawn
		/// into.
		/// </summary>
		public Rectangle getPathBounds(TreePath @path)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the path for the node at the specified location.
		/// </summary>
		public TreePath getPathForLocation(int @x, int @y)
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns the path for the specified row.
		/// </summary>
		public TreePath getPathForRow(int @row)
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns the preferred display size of a <code>JTree</code>.
		/// </summary>
		public Dimension getPreferredScrollableViewportSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Returns the <code>Rectangle</code> that the node at the specified row is
		/// drawn in.
		/// </summary>
		public Rectangle getRowBounds(int @row)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns the number of rows that are currently being displayed.
		/// </summary>
		public int getRowCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the row for the specified location.
		/// </summary>
		public int getRowForLocation(int @x, int @y)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the row that displays the node identified by the specified
		/// path.
		/// </summary>
		public int getRowForPath(TreePath @path)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the height of each row.
		/// </summary>
		public int getRowHeight()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the amount for a block increment, which is the height or
		/// width of <code>visibleRect</code>, based on <code>orientation</code>.
		/// </summary>
		public int getScrollableBlockIncrement(Rectangle @visibleRect, int @orientation, int @direction)
		{
			return default(int);
		}

		/// <summary>
		/// Returns false to indicate that the height of the viewport does not
		/// determine the height of the table, unless the preferred height
		/// of the tree is smaller than the viewports height.
		/// </summary>
		public bool getScrollableTracksViewportHeight()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns false to indicate that the width of the viewport does not
		/// determine the width of the table, unless the preferred width of
		/// the tree is smaller than the viewports width.
		/// </summary>
		public bool getScrollableTracksViewportWidth()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the amount to increment when scrolling.
		/// </summary>
		public int getScrollableUnitIncrement(Rectangle @visibleRect, int @orientation, int @direction)
		{
			return default(int);
		}

		/// <summary>
		/// Returns the value of the <code>scrollsOnExpand</code> property.
		/// </summary>
		public bool getScrollsOnExpand()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the number of nodes selected.
		/// </summary>
		public int getSelectionCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the model for selections.
		/// </summary>
		public TreeSelectionModel getSelectionModel()
		{
			return default(TreeSelectionModel);
		}

		/// <summary>
		/// Returns the path to the first selected node.
		/// </summary>
		public TreePath getSelectionPath()
		{
			return default(TreePath);
		}

		/// <summary>
		/// Returns the paths of all selected values.
		/// </summary>
		public TreePath[] getSelectionPaths()
		{
			return default(TreePath[]);
		}

		/// <summary>
		/// Returns all of the currently selected rows.
		/// </summary>
		public int[] getSelectionRows()
		{
			return default(int[]);
		}

		/// <summary>
		/// Returns the value of the <code>showsRootHandles</code> property.
		/// </summary>
		public bool getShowsRootHandles()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the number of mouse clicks needed to expand or close a node.
		/// </summary>
		public int getToggleClickCount()
		{
			return default(int);
		}

		/// <summary>
		/// Overrides <code>JComponent</code>'s <code>getToolTipText</code>
		/// method in order to allow
		/// renderer's tips to be used if it has text set.
		/// </summary>
		public string getToolTipText(MouseEvent @event)
		{
			return default(string);
		}

		/// <summary>
		/// Returns an array of all the <code>TreeExpansionListener</code>s added
		/// to this JTree with addTreeExpansionListener().
		/// </summary>
		public TreeExpansionListener[] getTreeExpansionListeners()
		{
			return default(TreeExpansionListener[]);
		}

		/// <summary>
		/// Returns an array of all the <code>TreeSelectionListener</code>s added
		/// to this JTree with addTreeSelectionListener().
		/// </summary>
		public TreeSelectionListener[] getTreeSelectionListeners()
		{
			return default(TreeSelectionListener[]);
		}

		/// <summary>
		/// Returns an array of all the <code>TreeWillExpandListener</code>s added
		/// to this JTree with addTreeWillExpandListener().
		/// </summary>
		public TreeWillExpandListener[] getTreeWillExpandListeners()
		{
			return default(TreeWillExpandListener[]);
		}

		/// <summary>
		/// Returns the L&F object that renders this component.
		/// </summary>
		public TreeUI getUI()
		{
			return default(TreeUI);
		}

		/// <summary>
		/// Returns the name of the L&F class that renders this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the number of rows that are displayed in the display area.
		/// </summary>
		public int getVisibleRowCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns true if the node identified by the path has ever been
		/// expanded.
		/// </summary>
		public bool hasBeenExpanded(TreePath @path)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the node at the specified display row is collapsed.
		/// </summary>
		public bool isCollapsed(int @row)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the value identified by path is currently collapsed,
		/// this will return false if any of the values in path are currently
		/// not being displayed.
		/// </summary>
		public bool isCollapsed(TreePath @path)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the tree is editable.
		/// </summary>
		public bool isEditable()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the tree is being edited.
		/// </summary>
		public bool isEditing()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the node at the specified display row is currently
		/// expanded.
		/// </summary>
		public bool isExpanded(int @row)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the node identified by the path is currently expanded,
		/// </summary>
		public bool isExpanded(TreePath @path)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the height of each display row is a fixed size.
		/// </summary>
		public bool isFixedRowHeight()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the tree is configured for a large model.
		/// </summary>
		public bool isLargeModel()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>isEditable</code>.
		/// </summary>
		public bool isPathEditable(TreePath @path)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the item identified by the path is currently selected.
		/// </summary>
		public bool isPathSelected(TreePath @path)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the root node of the tree is displayed.
		/// </summary>
		public bool isRootVisible()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the node identified by row is selected.
		/// </summary>
		public bool isRowSelected(int @row)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the selection is currently empty.
		/// </summary>
		public bool isSelectionEmpty()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the value identified by path is currently viewable,
		/// which means it is either the root or all of its parents are expanded.
		/// </summary>
		public bool isVisible(TreePath @path)
		{
			return default(bool);
		}

		/// <summary>
		/// Ensures that the node identified by path is currently viewable.
		/// </summary>
		public void makeVisible(TreePath @path)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JTree</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Removes any paths in the selection that are descendants of
		/// <code>path</code>.
		/// </summary>
		protected bool removeDescendantSelectedPaths(TreePath @path, bool @includePath)
		{
			return default(bool);
		}

		/// <summary>
		/// Removes any descendants of the <code>TreePaths</code> in
		/// <code>toRemove</code>
		/// that have been expanded.
		/// </summary>
		protected void removeDescendantToggledPaths(Enumeration @toRemove)
		{
		}

		/// <summary>
		/// Removes the nodes between index0 and index1, inclusive, from the
		/// selection.
		/// </summary>
		public void removeSelectionInterval(int @index0, int @index1)
		{
		}

		/// <summary>
		/// Removes the node identified by the specified path from the current
		/// selection.
		/// </summary>
		public void removeSelectionPath(TreePath @path)
		{
		}

		/// <summary>
		/// Removes the nodes identified by the specified paths from the
		/// current selection.
		/// </summary>
		public void removeSelectionPaths(TreePath[] @paths)
		{
		}

		/// <summary>
		/// Removes the row at the index <code>row</code> from the current
		/// selection.
		/// </summary>
		public void removeSelectionRow(int @row)
		{
		}

		/// <summary>
		/// Removes the rows that are selected at each of the specified
		/// rows.
		/// </summary>
		public void removeSelectionRows(int[] @rows)
		{
		}

		/// <summary>
		/// Removes a listener for <code>TreeExpansion</code> events.
		/// </summary>
		public void removeTreeExpansionListener(TreeExpansionListener @tel)
		{
		}

		/// <summary>
		/// Removes a <code>TreeSelection</code> listener.
		/// </summary>
		public void removeTreeSelectionListener(TreeSelectionListener @tsl)
		{
		}

		/// <summary>
		/// Removes a listener for <code>TreeWillExpand</code> events.
		/// </summary>
		public void removeTreeWillExpandListener(TreeWillExpandListener @tel)
		{
		}

		/// <summary>
		/// Makes sure all the path components in path are expanded (except
		/// for the last path component) and scrolls so that the
		/// node identified by the path is displayed.
		/// </summary>
		public void scrollPathToVisible(TreePath @path)
		{
		}

		/// <summary>
		/// Scrolls the item identified by row until it is displayed.
		/// </summary>
		public void scrollRowToVisible(int @row)
		{
		}

		/// <summary>
		/// Sets the path identified as the anchor.
		/// </summary>
		public void setAnchorSelectionPath(TreePath @newPath)
		{
		}

		/// <summary>
		/// Sets the cell editor.
		/// </summary>
		public void setCellEditor(TreeCellEditor @cellEditor)
		{
		}

		/// <summary>
		/// Sets the <code>TreeCellRenderer</code> that will be used to
		/// draw each cell.
		/// </summary>
		public void setCellRenderer(TreeCellRenderer @x)
		{
		}

		/// <summary>
		/// Sets the <code>dragEnabled</code> property,
		/// which must be <code>true</code> to enable
		/// automatic drag handling (the first part of drag and drop)
		/// on this component.
		/// </summary>
		public void setDragEnabled(bool @b)
		{
		}

		/// <summary>
		/// Determines whether the tree is editable.
		/// </summary>
		public void setEditable(bool @flag)
		{
		}

		/// <summary>
		/// Sets the expanded state of this <code>JTree</code>.
		/// </summary>
		protected void setExpandedState(TreePath @path, bool @state)
		{
		}

		/// <summary>
		/// Configures the <code>expandsSelectedPaths</code> property.
		/// </summary>
		public void setExpandsSelectedPaths(bool @newValue)
		{
		}

		/// <summary>
		/// Determines what happens when editing is interrupted by selecting
		/// another node in the tree, a change in the tree's data, or by some
		/// other means.
		/// </summary>
		public void setInvokesStopCellEditing(bool @newValue)
		{
		}

		/// <summary>
		/// Specifies whether the UI should use a large model.
		/// </summary>
		public void setLargeModel(bool @newValue)
		{
		}

		/// <summary>
		/// Sets the path identifies as the lead.
		/// </summary>
		public void setLeadSelectionPath(TreePath @newPath)
		{
		}

		/// <summary>
		/// Sets the <code>TreeModel</code> that will provide the data.
		/// </summary>
		public void setModel(TreeModel @newModel)
		{
		}

		/// <summary>
		/// Determines whether or not the root node from
		/// the <code>TreeModel</code> is visible.
		/// </summary>
		public void setRootVisible(bool @rootVisible)
		{
		}

		/// <summary>
		/// Sets the height of each cell, in pixels.
		/// </summary>
		public void setRowHeight(int @rowHeight)
		{
		}

		/// <summary>
		/// Sets the <code>scrollsOnExpand</code> property,
		/// which determines whether the
		/// tree might scroll to show previously hidden children.
		/// </summary>
		public void setScrollsOnExpand(bool @newValue)
		{
		}

		/// <summary>
		/// Selects the nodes between index0 and index1, inclusive.
		/// </summary>
		public void setSelectionInterval(int @index0, int @index1)
		{
		}

		/// <summary>
		/// Sets the tree's selection model.
		/// </summary>
		public void setSelectionModel(TreeSelectionModel @selectionModel)
		{
		}

		/// <summary>
		/// Selects the node identified by the specified path.
		/// </summary>
		public void setSelectionPath(TreePath @path)
		{
		}

		/// <summary>
		/// Selects the nodes identified by the specified array of paths.
		/// </summary>
		public void setSelectionPaths(TreePath[] @paths)
		{
		}

		/// <summary>
		/// Selects the node at the specified row in the display.
		/// </summary>
		public void setSelectionRow(int @row)
		{
		}

		/// <summary>
		/// Selects the nodes corresponding to each of the specified rows
		/// in the display.
		/// </summary>
		public void setSelectionRows(int[] @rows)
		{
		}

		/// <summary>
		/// Sets the value of the <code>showsRootHandles</code> property,
		/// which specifies whether the node handles should be displayed.
		/// </summary>
		public void setShowsRootHandles(bool @newValue)
		{
		}

		/// <summary>
		/// Sets the number of mouse clicks before a node will expand or close.
		/// </summary>
		public void setToggleClickCount(int @clickCount)
		{
		}

		/// <summary>
		/// Sets the L&F object that renders this component.
		/// </summary>
		public void setUI(TreeUI @ui)
		{
		}

		/// <summary>
		/// Sets the number of rows that are to be displayed.
		/// </summary>
		public void setVisibleRowCount(int @newCount)
		{
		}

		/// <summary>
		/// Selects the node identified by the specified path and initiates
		/// editing.
		/// </summary>
		public void startEditingAtPath(TreePath @path)
		{
		}

		/// <summary>
		/// Ends the current editing session.
		/// </summary>
		public bool stopEditing()
		{
			return default(bool);
		}

		/// <summary>
		/// Sent when the tree has changed enough that we need to resize
		/// the bounds, but not enough that we need to remove the
		/// expanded node set (e.g nodes were expanded or collapsed, or
		/// nodes were inserted into the tree).
		/// </summary>
		public void treeDidChange()
		{
		}

		/// <summary>
		/// Notification from the <code>UIManager</code> that the L&F has changed.
		/// </summary>
		public void updateUI()
		{
		}

	}
}
