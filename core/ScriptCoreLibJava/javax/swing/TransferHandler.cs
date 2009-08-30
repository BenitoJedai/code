// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.datatransfer;
using java.awt.@event;
using java.lang;
using javax.swing;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/TransferHandler.html
	[Script(IsNative = true)]
	public class TransferHandler
	{
		/// <summary>
		/// Convenience constructor for subclasses.
		/// </summary>
		public TransferHandler()
		{
		}

		/// <summary>
		/// Constructs a transfer handler that can transfer a Java Bean property
		/// from one component to another via the clipboard or a drag and drop
		/// operation.
		/// </summary>
		public TransferHandler(string @property)
		{
		}

		/// <summary>
		/// Indicates whether a component would accept an import of the given
		/// set of data flavors prior to actually attempting to import it.
		/// </summary>
		public bool canImport(JComponent @comp, DataFlavor[] @transferFlavors)
		{
			return default(bool);
		}

		/// <summary>
		/// Creates a <code>Transferable</code> to use as the source for
		/// a data transfer.
		/// </summary>
		public Transferable createTransferable(JComponent @c)
		{
			return default(Transferable);
		}

		/// <summary>
		/// Causes the Swing drag support to be initiated.
		/// </summary>
		public void exportAsDrag(JComponent @comp, InputEvent @e, int @action)
		{
		}

		/// <summary>
		/// Invoked after data has been exported.
		/// </summary>
		protected void exportDone(JComponent @source, Transferable @data, int @action)
		{
		}

		/// <summary>
		/// Causes a transfer from the given component to the
		/// given clipboard.
		/// </summary>
		public void exportToClipboard(JComponent @comp, Clipboard @clip, int @action)
		{
		}

		/// <summary>
		/// Returns an <code>Action</code> that behaves like a 'copy' operation.
		/// </summary>
		public Action getCopyAction()
		{
			return default(Action);
		}

		/// <summary>
		/// Returns an <code>Action</code> that behaves like a 'cut' operation.
		/// </summary>
		public Action getCutAction()
		{
			return default(Action);
		}

		/// <summary>
		/// Returns an <code>Action</code> that behaves like a 'paste' operation.
		/// </summary>
		public Action getPasteAction()
		{
			return default(Action);
		}

		/// <summary>
		/// Returns the type of transfer actions supported by the source.
		/// </summary>
		public int getSourceActions(JComponent @c)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an object that establishes the look of a transfer.
		/// </summary>
		public Icon getVisualRepresentation(Transferable @t)
		{
			return default(Icon);
		}

		/// <summary>
		/// Causes a transfer to a component from a clipboard or a
		/// DND drop operation.
		/// </summary>
		public bool importData(JComponent @comp, Transferable @t)
		{
			return default(bool);
		}

	}
}

