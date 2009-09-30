// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.TableColumnModelListener

using ScriptCoreLib;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/TableColumnModelListener.html
	[Script(IsNative = true)]
	public interface TableColumnModelListener : EventListener
	{
		/// <summary>
		/// Tells listeners that a column was added to the model.
		/// </summary>
		void columnAdded(TableColumnModelEvent @e);

		/// <summary>
		/// Tells listeners that a column was moved due to a margin change.
		/// </summary>
		void columnMarginChanged(ChangeEvent @e);

		/// <summary>
		/// Tells listeners that a column was repositioned.
		/// </summary>
		void columnMoved(TableColumnModelEvent @e);

		/// <summary>
		/// Tells listeners that a column was removed from the model.
		/// </summary>
		void columnRemoved(TableColumnModelEvent @e);

		/// <summary>
		/// Tells listeners that the selection model of the
		/// TableColumnModel changed.
		/// </summary>
		void columnSelectionChanged(ListSelectionEvent @e);

	}
}
