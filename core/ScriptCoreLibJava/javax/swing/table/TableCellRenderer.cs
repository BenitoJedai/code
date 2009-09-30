// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.table.TableCellRenderer

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.swing;

namespace javax.swing.table
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/table/TableCellRenderer.html
	[Script(IsNative = true)]
	public interface TableCellRenderer
	{
		/// <summary>
		/// Returns the component used for drawing the cell.
		/// </summary>
		Component getTableCellRendererComponent(JTable @table, object @value, bool @isSelected, bool @hasFocus, int @row, int @column);

	}
}
