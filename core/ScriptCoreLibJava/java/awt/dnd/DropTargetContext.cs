// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.datatransfer;
using java.awt.dnd;
using java.util;

namespace java.awt.dnd
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/dnd/DropTargetContext.html
	[Script(IsNative = true)]
	public class DropTargetContext
	{
		/// <summary>
		/// accept the Drag.
		/// </summary>
		protected void acceptDrag(int @dragOperation)
		{
		}

		/// <summary>
		/// called to signal that the drop is acceptable
		/// using the specified operation.
		/// </summary>
		protected void acceptDrop(int @dropOperation)
		{
		}

		/// <summary>
		/// Called when associated with the <code>DropTargetContextPeer</code>.
		/// </summary>
		//public void addNotify(java.awt.dnd.peer.DropTargetContextPeer @dtcp)
		//{
		//}

		/// <summary>
		/// Creates a TransferableProxy to proxy for the specified
		/// Transferable.
		/// </summary>
		public Transferable createTransferableProxy(Transferable @t, bool @local)
		{
			return default(Transferable);
		}

		/// <summary>
		/// This method signals that the drop is completed and
		/// if it was successful or not.
		/// </summary>
		public void dropComplete(bool @success)
		{
		}

		/// <summary>
		/// This method returns the <code>Component</code> associated with
		/// this <code>DropTargetContext</code>.
		/// </summary>
		public Component getComponent()
		{
			return default(Component);
		}

		/// <summary>
		/// get the available DataFlavors of the
		/// <code>Transferable</code> operand of this operation.
		/// </summary>
		public DataFlavor[] getCurrentDataFlavors()
		{
			return default(DataFlavor[]);
		}

		/// <summary>
		/// This method returns a the currently available DataFlavors
		/// of the <code>Transferable</code> operand
		/// as a <code>java.util.List</code>.
		/// </summary>
		public List getCurrentDataFlavorsAsList()
		{
			return default(List);
		}

		/// <summary>
		/// This method returns the <code>DropTarget</code> associated with this
		/// <code>DropTargetContext</code>.
		/// </summary>
		public DropTarget getDropTarget()
		{
			return default(DropTarget);
		}

		/// <summary>
		/// This method returns an <code>int</code> representing the
		/// current actions this <code>DropTarget</code> will accept.
		/// </summary>
		protected int getTargetActions()
		{
			return default(int);
		}

		/// <summary>
		/// get the Transferable (proxy) operand of this operation
		/// </summary>
		public Transferable getTransferable()
		{
			return default(Transferable);
		}

		/// <summary>
		/// This method returns a <code>boolean</code>
		/// indicating if the given <code>DataFlavor</code> is
		/// supported by this <code>DropTargetContext</code>.
		/// </summary>
		protected bool isDataFlavorSupported(DataFlavor @df)
		{
			return default(bool);
		}

		/// <summary>
		/// reject the Drag.
		/// </summary>
		protected void rejectDrag()
		{
		}

		/// <summary>
		/// called to signal that the drop is unacceptable.
		/// </summary>
		protected void rejectDrop()
		{
		}

		/// <summary>
		/// Called when disassociated with the <code>DropTargetContextPeer</code>.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// This method sets the current actions acceptable to
		/// this <code>DropTarget</code>.
		/// </summary>
		protected void setTargetActions(int @actions)
		{
		}

	}
}

