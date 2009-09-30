// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.table.JTableHeader

using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using javax.accessibility;
using javax.swing;
using javax.swing.@event;
using javax.swing.plaf;

namespace javax.swing.table
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/table/JTableHeader.html
	[Script(IsNative = true)]
	public class JTableHeader : JComponent
	{
		/// <summary>
		/// Constructs a <code>JTableHeader</code> with a default
		/// <code>TableColumnModel</code>.
		/// </summary>
		public JTableHeader()
		{
		}

		/// <summary>
		/// Constructs a <code>JTableHeader</code> which is initialized with
		/// <code>cm</code> as the column model.
		/// </summary>
		public JTableHeader(TableColumnModel @cm)
		{
		}

		/// <summary>
		/// Invoked when a column is added to the table column model.
		/// </summary>
		public void columnAdded(TableColumnModelEvent @e)
		{
		}

		/// <summary>
		/// Returns the index of the column that <code>point</code> lies in, or -1 if it
		/// lies out of bounds.
		/// </summary>
		public int columnAtPoint(Point @point)
		{
			return default(int);
		}

		/// <summary>
		/// Invoked when a column is moved due to a margin change.
		/// </summary>
		public void columnMarginChanged(ChangeEvent @e)
		{
		}

		/// <summary>
		/// Invoked when a column is repositioned.
		/// </summary>
		public void columnMoved(TableColumnModelEvent @e)
		{
		}

		/// <summary>
		/// Invoked when a column is removed from the table column model.
		/// </summary>
		public void columnRemoved(TableColumnModelEvent @e)
		{
		}

		/// <summary>
		/// Invoked when the selection model of the <code>TableColumnModel</code>
		/// is changed.
		/// </summary>
		public void columnSelectionChanged(ListSelectionEvent @e)
		{
		}

		/// <summary>
		/// Returns the default column model object which is
		/// a <code>DefaultTableColumnModel</code>.
		/// </summary>
		protected TableColumnModel createDefaultColumnModel()
		{
			return default(TableColumnModel);
		}

		/// <summary>
		/// Returns a default renderer to be used when no header renderer
		/// is defined by a <code>TableColumn</code>.
		/// </summary>
		protected TableCellRenderer createDefaultRenderer()
		{
			return default(TableCellRenderer);
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this JTableHeader.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the <code>TableColumnModel</code> that contains all column information
		/// of this table header.
		/// </summary>
		public TableColumnModel getColumnModel()
		{
			return default(TableColumnModel);
		}

		/// <summary>
		/// Returns the default renderer used when no <code>headerRenderer</code>
		/// is defined by a <code>TableColumn</code>.
		/// </summary>
		public TableCellRenderer getDefaultRenderer()
		{
			return default(TableCellRenderer);
		}

		/// <summary>
		/// Returns the the dragged column, if and only if, a drag is in
		/// process, otherwise returns <code>null</code>.
		/// </summary>
		public TableColumn getDraggedColumn()
		{
			return default(TableColumn);
		}

		/// <summary>
		/// Returns the column's horizontal distance from its original
		/// position, if and only if, a drag is in process.
		/// </summary>
		public int getDraggedDistance()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the rectangle containing the header tile at <code>column</code>.
		/// </summary>
		public Rectangle getHeaderRect(int @column)
		{
			return default(Rectangle);
		}

		/// <summary>
		/// Returns true if the user is allowed to rearrange columns by
		/// dragging their headers, false otherwise.
		/// </summary>
		public bool getReorderingAllowed()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns true if the user is allowed to resize columns by dragging
		/// between their headers, false otherwise.
		/// </summary>
		public bool getResizingAllowed()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the resizing column.
		/// </summary>
		public TableColumn getResizingColumn()
		{
			return default(TableColumn);
		}

		/// <summary>
		/// Returns the table associated with this header.
		/// </summary>
		public JTable getTable()
		{
			return default(JTable);
		}

		/// <summary>
		/// Allows the renderer's tips to be used if there is text set.
		/// </summary>
		public string getToolTipText(MouseEvent @event)
		{
			return default(string);
		}

		/// <summary>
		/// Returns the look and feel (L&F) object that renders this component.
		/// </summary>
		public TableHeaderUI getUI()
		{
			return default(TableHeaderUI);
		}

		/// <summary>
		/// Returns the suffix used to construct the name of the look and feel
		/// (L&F) class used to render this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Obsolete as of Java 2 platform v1.3.
		/// </summary>
		public bool getUpdateTableInRealTime()
		{
			return default(bool);
		}

		/// <summary>
		/// Initializes the local variables and properties with default values.
		/// </summary>
		protected void initializeLocalVars()
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>JTableHeader</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Sizes the header and marks it as needing display.
		/// </summary>
		public void resizeAndRepaint()
		{
		}

		/// <summary>
		/// Sets the column model for this table to <code>newModel</code> and registers
		/// for listener notifications from the new column model.
		/// </summary>
		public void setColumnModel(TableColumnModel @columnModel)
		{
		}

		/// <summary>
		/// Sets the default renderer to be used when no <code>headerRenderer</code>
		/// is defined by a <code>TableColumn</code>.
		/// </summary>
		public void setDefaultRenderer(TableCellRenderer @defaultRenderer)
		{
		}

		/// <summary>
		/// Sets the header's <code>draggedColumn</code> to <code>aColumn</code>.
		/// </summary>
		public void setDraggedColumn(TableColumn @aColumn)
		{
		}

		/// <summary>
		/// Sets the header's <code>draggedDistance</code> to <code>distance</code>.
		/// </summary>
		public void setDraggedDistance(int @distance)
		{
		}

		/// <summary>
		/// Sets whether the user can drag column headers to reorder columns.
		/// </summary>
		public void setReorderingAllowed(bool @reorderingAllowed)
		{
		}

		/// <summary>
		/// Sets whether the user can resize columns by dragging between headers.
		/// </summary>
		public void setResizingAllowed(bool @resizingAllowed)
		{
		}

		/// <summary>
		/// Sets the header's <code>resizingColumn</code> to <code>aColumn</code>.
		/// </summary>
		public void setResizingColumn(TableColumn @aColumn)
		{
		}

		/// <summary>
		/// Sets the table associated with this header.
		/// </summary>
		public void setTable(JTable @table)
		{
		}

		/// <summary>
		/// Sets the look and feel (L&F) object that renders this component.
		/// </summary>
		public void setUI(TableHeaderUI @ui)
		{
		}

		/// <summary>
		/// Obsolete as of Java 2 platform v1.3.
		/// </summary>
		public void setUpdateTableInRealTime(bool @flag)
		{
		}

		/// <summary>
		/// Notification from the <code>UIManager</code> that the look and feel
		/// (L&F) has changed.
		/// </summary>
		public void updateUI()
		{
		}

	}
}
