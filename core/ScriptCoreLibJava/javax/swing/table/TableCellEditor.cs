// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.table.TableCellEditor

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.swing;

namespace javax.swing.table
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/table/TableCellEditor.html
	[Script(IsNative = true)]
	public interface TableCellEditor : CellEditor
	{
		/// <summary>
		/// Sets an initial <code>value</code> for the editor.
		/// </summary>
		Component getTableCellEditorComponent(JTable @table, object @value, bool @isSelected, int @row, int @column);

	}
}
