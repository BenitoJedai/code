// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.dnd;
using java.util;

namespace java.awt.dnd
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/dnd/DropTargetListener.html
	[Script(IsNative = true)]
	public interface DropTargetListener : EventListener
	{
		/// <summary>
		/// Called while a drag operation is ongoing, when the mouse pointer enters
		/// the operable part of the drop site for the <code>DropTarget</code>
		/// registered with this listener.
		/// </summary>
		void dragEnter(DropTargetDragEvent @dtde);

		/// <summary>
		/// Called while a drag operation is ongoing, when the mouse pointer has
		/// exited the operable part of the drop site for the
		/// <code>DropTarget</code> registered with this listener.
		/// </summary>
		void dragExit(DropTargetEvent @dte);

		/// <summary>
		/// Called when a drag operation is ongoing, while the mouse pointer is still
		/// over the operable part of the drop site for the <code>DropTarget</code>
		/// registered with this listener.
		/// </summary>
		void dragOver(DropTargetDragEvent @dtde);

		/// <summary>
		/// Called when the drag operation has terminated with a drop on
		/// the operable part of the drop site for the <code>DropTarget</code>
		/// registered with this listener.
		/// </summary>
		void drop(DropTargetDropEvent @dtde);

		/// <summary>
		/// Called if the user has modified
		/// the current drop gesture.
		/// </summary>
		void dropActionChanged(DropTargetDragEvent @dtde);

	}
}

