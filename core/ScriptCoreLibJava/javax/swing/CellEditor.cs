// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.CellEditor

using ScriptCoreLib;
using java.lang;
using java.util;
using javax.swing.@event;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/CellEditor.html
	[Script(IsNative = true)]
	public interface CellEditor
	{
		/// <summary>
		/// Adds a listener to the list that's notified when the editor
		/// stops, or cancels editing.
		/// </summary>
		void addCellEditorListener(CellEditorListener @l);

		/// <summary>
		/// Tells the editor to cancel editing and not accept any partially
		/// edited value.
		/// </summary>
		void cancelCellEditing();

		/// <summary>
		/// Returns the value contained in the editor.
		/// </summary>
		object getCellEditorValue();

		/// <summary>
		/// Asks the editor if it can start editing using <code>anEvent</code>.
		/// </summary>
		bool isCellEditable(EventObject @anEvent);

		/// <summary>
		/// Removes a listener from the list that's notified
		/// </summary>
		void removeCellEditorListener(CellEditorListener @l);

		/// <summary>
		/// Returns true if the editing cell should be selected, false otherwise.
		/// </summary>
		bool shouldSelectCell(EventObject @anEvent);

		/// <summary>
		/// Tells the editor to stop editing and accept any partially edited
		/// value as the value of the editor.
		/// </summary>
		bool stopCellEditing();

	}
}
