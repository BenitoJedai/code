// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.util;
using javax.swing.@event;

namespace javax.swing.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/event/PopupMenuListener.html
	[Script(IsNative = true)]
	public interface PopupMenuListener : EventListener
	{
		/// <summary>
		/// This method is called when the popup menu is canceled
		/// </summary>
		void popupMenuCanceled(PopupMenuEvent @e);

		/// <summary>
		/// This method is called before the popup menu becomes invisible
		/// Note that a JPopupMenu can become invisible any time
		/// </summary>
		void popupMenuWillBecomeInvisible(PopupMenuEvent @e);

		/// <summary>
		/// This method is called before the popup menu becomes visible
		/// </summary>
		void popupMenuWillBecomeVisible(PopupMenuEvent @e);

	}
}

