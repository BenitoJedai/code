// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.table.TableColumnModel

using ScriptCoreLib;
using java.lang;
using java.util;
using javax.swing;
using javax.swing.@event;

namespace javax.swing.table
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/table/TableColumnModel.html
	[Script(IsNative = true)]
	public interface TableColumnModel
	{
		/// <summary>
		/// Appends <code>aColumn</code> to the end of the
		/// <code>tableColumns</code> array.
		/// </summary>
		void addColumn(TableColumn @aColumn);

		/// <summary>
		/// Adds a listener for table column model events.
		/// </summary>
		void addColumnModelListener(TableColumnModelListener @x);

		/// <summary>
		/// Returns the <code>TableColumn</code> object for the column at
		/// <code>columnIndex</code>.
		/// </summary>
		TableColumn getColumn(int @columnIndex);

		/// <summary>
		/// Returns the number of columns in the model.
		/// </summary>
		int getColumnCount();

		/// <summary>
		/// Returns the index of the first column in the table
		/// whose identifier is equal to <code>identifier</code>,
		/// when compared using <code>equals</code>.
		/// </summary>
		int getColumnIndex(object @columnIdentifier);

		/// <summary>
		/// Returns the index of the column that lies on the
		/// horizontal point, <code>xPosition</code>;
		/// or -1 if it lies outside the any of the column's bounds.
		/// </summary>
		int getColumnIndexAtX(int @xPosition);

		/// <summary>
		/// Returns the width between the cells in each column.
		/// </summary>
		int getColumnMargin();

		/// <summary>
		/// Returns an <code>Enumeration</code> of all the columns in the model.
		/// </summary>
		Enumeration getColumns();

		/// <summary>
		/// Returns true if columns may be selected.
		/// </summary>
		bool getColumnSelectionAllowed();

		/// <summary>
		/// Returns the number of selected columns.
		/// </summary>
		int getSelectedColumnCount();

		/// <summary>
		/// Returns an array of indicies of all selected columns.
		/// </summary>
		int[] getSelectedColumns();

		/// <summary>
		/// Returns the current selection model.
		/// </summary>
		ListSelectionModel getSelectionModel();

		/// <summary>
		/// Returns the total width of all the columns.
		/// </summary>
		int getTotalColumnWidth();

		/// <summary>
		/// Moves the column and its header at <code>columnIndex</code> to
		/// <code>newIndex</code>.
		/// </summary>
		void moveColumn(int @columnIndex, int @newIndex);

		/// <summary>
		/// Deletes the <code>TableColumn</code> <code>column</code> from the
		/// <code>tableColumns</code> array.
		/// </summary>
		void removeColumn(TableColumn @column);

		/// <summary>
		/// Removes a listener for table column model events.
		/// </summary>
		void removeColumnModelListener(TableColumnModelListener @x);

		/// <summary>
		/// Sets the <code>TableColumn</code>'s column margin to
		/// <code>newMargin</code>.
		/// </summary>
		void setColumnMargin(int @newMargin);

		/// <summary>
		/// Sets whether the columns in this model may be selected.
		/// </summary>
		void setColumnSelectionAllowed(bool @flag);

		/// <summary>
		/// Sets the selection model.
		/// </summary>
		void setSelectionModel(ListSelectionModel @newModel);

	}
}
