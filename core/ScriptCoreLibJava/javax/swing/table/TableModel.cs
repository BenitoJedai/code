// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.table.TableModel

using ScriptCoreLib;
using java.lang;
using javax.swing.@event;

namespace javax.swing.table
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/table/TableModel.html
	[Script(IsNative = true)]
	public interface TableModel
	{
		/// <summary>
		/// Adds a listener to the list that is notified each time a change
		/// to the data model occurs.
		/// </summary>
		void addTableModelListener(TableModelListener @l);

		/// <summary>
		/// Returns the most specific superclass for all the cell values
		/// in the column.
		/// </summary>
		Class getColumnClass(int @columnIndex);

		/// <summary>
		/// Returns the number of columns in the model.
		/// </summary>
		int getColumnCount();

		/// <summary>
		/// Returns the name of the column at <code>columnIndex</code>.
		/// </summary>
		string getColumnName(int @columnIndex);

		/// <summary>
		/// Returns the number of rows in the model.
		/// </summary>
		int getRowCount();

		/// <summary>
		/// Returns the value for the cell at <code>columnIndex</code> and
		/// <code>rowIndex</code>.
		/// </summary>
		object getValueAt(int @rowIndex, int @columnIndex);

		/// <summary>
		/// Returns true if the cell at <code>rowIndex</code> and
		/// <code>columnIndex</code>
		/// is editable.
		/// </summary>
		bool isCellEditable(int @rowIndex, int @columnIndex);

		/// <summary>
		/// Removes a listener from the list that is notified each time a
		/// change to the data model occurs.
		/// </summary>
		void removeTableModelListener(TableModelListener @l);

		/// <summary>
		/// Sets the value in the cell at <code>columnIndex</code> and
		/// <code>rowIndex</code> to <code>aValue</code>.
		/// </summary>
		void setValueAt(object @aValue, int @rowIndex, int @columnIndex);

	}
}
