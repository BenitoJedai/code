// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using javax.swing;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/MenuElement.html
	[Script(IsNative = true)]
	public interface MenuElement
	{
		/// <summary>
		/// This method should return the java.awt.Component used to paint the receiving element.
		/// </summary>
		Component getComponent();

		/// <summary>
		/// This method should return an array containing the sub-elements for the receiving menu element
		/// </summary>
		MenuElement[] getSubElements();

		/// <summary>
		/// Call by the MenuSelection when the MenuElement is added or remove from
		/// the menu selection.
		/// </summary>
		void menuSelectionChanged(bool @isIncluded);

		/// <summary>
		/// Process a key event.
		/// </summary>
		void processKeyEvent(KeyEvent @event, MenuElement[] @path, MenuSelectionManager @manager);

		/// <summary>
		/// Process a mouse event.
		/// </summary>
		void processMouseEvent(MouseEvent @event, MenuElement[] @path, MenuSelectionManager @manager);

	}
}

