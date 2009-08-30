// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.datatransfer;
using java.awt.dnd;
using java.util;

namespace java.awt.dnd
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/dnd/DropTargetDropEvent.html
	[Script(IsNative = true)]
	public class DropTargetDropEvent : DropTargetEvent
	{
		/// <summary>
		/// Construct a <code>DropTargetDropEvent</code> given
		/// the <code>DropTargetContext</code> for this operation,
		/// the location of the drag <code>Cursor</code>'s
		/// hotspot in the <code>Component</code>'s coordinates,
		/// the currently
		/// selected user drop action, and the current set of
		/// actions supported by the source.
		/// </summary>
		public DropTargetDropEvent(DropTargetContext @dtc, Point @cursorLocn, int @dropAction, int @srcActions)
			: base(null)
		{
		}

		/// <summary>
		/// Construct a <code>DropTargetEvent</code> given the
		/// <code>DropTargetContext</code> for this operation,
		/// the location of the drag <code>Cursor</code>'s hotspot
		/// in the <code>Component</code>'s
		/// coordinates, the currently selected user drop action,
		/// the current set of actions supported by the source,
		/// and a <code>boolean</code> indicating if the source is in the same JVM
		/// as the target.
		/// </summary>
		public DropTargetDropEvent(DropTargetContext @dtc, Point @cursorLocn, int @dropAction, int @srcActions, bool @isLocal): base(null)
		{
		}

		/// <summary>
		/// accept the drop, using the specified action.
		/// </summary>
		public void acceptDrop(int @dropAction)
		{
		}

		/// <summary>
		/// This method notifies the <code>DragSource</code>
		/// that the drop transfer(s) are completed.
		/// </summary>
		public void dropComplete(bool @success)
		{
		}

		/// <summary>
		/// This method returns the current DataFlavors.
		/// </summary>
		public DataFlavor[] getCurrentDataFlavors()
		{
			return default(DataFlavor[]);
		}

		/// <summary>
		/// This method returns the currently available
		/// <code>DataFlavor</code>s as a <code>java.util.List</code>.
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
		/// location in the <code>Component</code>'s coordinates.
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
		/// This method returns the <code>Transferable</code> object
		/// associated with the drop.
		/// </summary>
		public Transferable getTransferable()
		{
			return default(Transferable);
		}

		/// <summary>
		/// This method returns a <code>boolean</code> indicating if the
		/// specified <code>DataFlavor</code> is available
		/// from the source.
		/// </summary>
		public bool isDataFlavorSupported(DataFlavor @df)
		{
			return default(bool);
		}

		/// <summary>
		/// This method returns an <code>int</code> indicating if
		/// the source is in the same JVM as the target.
		/// </summary>
		public bool isLocalTransfer()
		{
			return default(bool);
		}

		/// <summary>
		/// reject the Drop.
		/// </summary>
		public void rejectDrop()
		{
		}

	}
}

