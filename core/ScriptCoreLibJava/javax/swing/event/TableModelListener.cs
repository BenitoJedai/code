// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.TableModelListener

using ScriptCoreLib;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/TableModelListener.html
	[Script(IsNative = true)]
	public interface TableModelListener : EventListener
	{
		/// <summary>
		/// This fine grain notification tells listeners the exact range
		/// of cells, rows, or columns that changed.
		/// </summary>
		void tableChanged(TableModelEvent @e);

	}
}
