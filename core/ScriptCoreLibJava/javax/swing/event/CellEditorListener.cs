// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.event.CellEditorListener

using ScriptCoreLib;
using java.util;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/CellEditorListener.html
	[Script(IsNative = true)]
	public interface CellEditorListener : EventListener
	{
		/// <summary>
		/// This tells the listeners the editor has canceled editing
		/// </summary>
		void editingCanceled(ChangeEvent @e);

		/// <summary>
		/// This tells the listeners the editor has ended editing
		/// </summary>
		void editingStopped(ChangeEvent @e);

	}
}
