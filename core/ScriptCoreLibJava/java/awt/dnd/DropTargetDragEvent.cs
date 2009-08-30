// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.datatransfer;
using java.awt.dnd;
using java.util;

namespace java.awt.dnd
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/dnd/DropTargetDragEvent.html
	[Script(IsNative = true)]
	public class DropTargetDragEvent : DropTargetEvent
	{
		/// <summary>
		/// Construct a <code>DropTargetDragEvent</code> given the
		/// <code>DropTargetContext</code> for this operation,
		/// the location of the "Drag" <code>Cursor</code>'s hotspot
		/// in the <code>Component</code>'s coordinates, the
		/// user drop action, and the source drop actions.
		/// </summary>
		public DropTargetDragEvent(DropTargetContext @dtc, Point @cursorLocn, int @dropAction, int @srcActions)
			: base(null)
		{
		}

		/// <summary>
		/// Accepts the drag.
		/// </summary>
		public void acceptDrag(int @dragOperation)
		{
		}

		/// <summary>
		/// This method returns the current <code>DataFlavor</code>s from the
		/// <code>DropTargetContext</code>.
		/// </summary>
		public DataFlavor[] getCurrentDataFlavors()
		{
			return default(DataFlavor[]);
		}

		/// <summary>
		/// This method returns the current <code>DataFlavor</code>s
		/// as a <code>java.util.List</code>
		/// </summary>
		public List getCurrentDataFlavorsAsList()
		{
			return default(List);
		}

		/// <summary>
		/// This method returns the user drop action.
		/// </summary>
		public int getDropAction()
		{
			return default(int);
		}

		/// <summary>
		/// This method returns a <code>Point</code>
		/// indicating the <code>Cursor</code>'s current
		/// location within the <code>Component'</code>s
		/// coordinates.
		/// </summary>
		public Point getLocation()
		{
			return default(Point);
		}

		/// <summary>
		/// This method returns the source drop actions.
		/// </summary>
		public int getSourceActions()
		{
			return default(int);
		}

		/// <summary>
		/// This method returns a <code>boolean</code> indicating
		/// if the specified <code>DataFlavor</code> is supported.
		/// </summary>
		public bool isDataFlavorSupported(DataFlavor @df)
		{
			return default(bool);
		}

		/// <summary>
		/// Rejects the drag as a result of examining either the
		/// <code>dropAction</code> or the available <code>DataFlavor</code>
		/// types.
		/// </summary>
		public void rejectDrag()
		{
		}

	}
}

