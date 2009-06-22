using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/MouseListener.html
	[Script(IsNative = true)]
	public interface MouseListener
	{
		/// <summary>
		/// Invoked when the mouse button has been clicked (pressed and released) on a component.
		/// </summary>
		/// <param name="e"></param>
		void mouseClicked(MouseEvent e);
          
		/// <summary>
		/// Invoked when the mouse enters a component.
		/// </summary>
		/// <param name="e"></param>
		void mouseEntered(MouseEvent e);
          

		/// <summary>
		/// Invoked when the mouse exits a component.
		/// </summary>
		/// <param name="e"></param>
		void mouseExited(MouseEvent e);

		/// <summary>
		/// Invoked when a mouse button has been pressed on a component.
		/// </summary>
		/// <param name="e"></param>
		void mousePressed(MouseEvent e);

		/// <summary>
		/// Invoked when a mouse button has been released on a component.
		/// </summary>
		/// <param name="e"></param>
		void mouseReleased(MouseEvent e);
          
	}
}
