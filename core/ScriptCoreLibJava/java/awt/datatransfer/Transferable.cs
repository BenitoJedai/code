// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.datatransfer;
using java.lang;

namespace java.awt.datatransfer
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/datatransfer/Transferable.html
	[Script(IsNative = true)]
	public interface Transferable
	{
		/// <summary>
		/// Returns an object which represents the data to be transferred.
		/// </summary>
		object getTransferData(DataFlavor @flavor);

		/// <summary>
		/// Returns an array of DataFlavor objects indicating the flavors the data
		/// can be provided in.
		/// </summary>
		DataFlavor[] getTransferDataFlavors();

		/// <summary>
		/// Returns whether or not the specified data flavor is supported for
		/// this object.
		/// </summary>
		bool isDataFlavorSupported(DataFlavor @flavor);

	}
}

