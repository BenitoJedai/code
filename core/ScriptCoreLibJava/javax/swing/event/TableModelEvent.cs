// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.TableModelEvent

using ScriptCoreLib;
using java.util;
using javax.swing.table;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/TableModelEvent.html
	[Script(IsNative = true)]
	public class TableModelEvent : EventObject
	{
		/// <summary>
		/// All row data in the table has changed, listeners should discard any state
		/// that was based on the rows and requery the <code>TableModel</code>
		/// to get the new row count and all the appropriate values.
		/// </summary>
		public TableModelEvent(TableModel @source)
		{
		}

		/// <summary>
		/// This row of data has been updated.
		/// </summary>
		public TableModelEvent(TableModel @source, int @row)
		{
		}

		/// <summary>
		/// The data in rows [<I>firstRow</I>, <I>lastRow</I>] have been updated.
		/// </summary>
		public TableModelEvent(TableModel @source, int @firstRow, int @lastRow)
		{
		}

		/// <summary>
		/// The cells in column <I>column</I> in the range
		/// [<I>firstRow</I>, <I>lastRow</I>] have been updated.
		/// </summary>
		public TableModelEvent(TableModel @source, int @firstRow, int @lastRow, int @column)
		{
		}

		/// <summary>
		/// The cells from (firstRow, column) to (lastRow, column) have been changed.
		/// </summary>
		public TableModelEvent(TableModel @source, int @firstRow, int @lastRow, int @column, int @type)
		{
		}

		/// <summary>
		/// Returns the column for the event.
		/// </summary>
		public int getColumn()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the first row that changed.
		/// </summary>
		public int getFirstRow()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the last row that changed.
		/// </summary>
		public int getLastRow()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the type of event - one of: INSERT, UPDATE and DELETE.
		/// </summary>
		public int getType()
		{
			return default(int);
		}

	}
}
