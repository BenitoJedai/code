// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.@event;
using java.lang;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/ItemSelectable.html
	[Script(IsNative = true)]
	public interface ItemSelectable
	{
		/// <summary>
		/// Adds a listener to receive item events when the state of an item is
		/// changed by the user.
		/// </summary>
		void addItemListener(ItemListener @l);

		/// <summary>
		/// Returns the selected items or <code>null</code> if no
		/// items are selected.
		/// </summary>
		object[] getSelectedObjects();

		/// <summary>
		/// Removes an item listener.
		/// </summary>
		void removeItemListener(ItemListener @l);

	}
}

