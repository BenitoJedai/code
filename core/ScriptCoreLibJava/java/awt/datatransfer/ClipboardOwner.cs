// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.datatransfer;

namespace java.awt.datatransfer
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/datatransfer/ClipboardOwner.html
	[Script(IsNative = true)]
	public interface ClipboardOwner
	{
		/// <summary>
		/// Notifies this object that it is no longer the clipboard owner.
		/// </summary>
		void lostOwnership(Clipboard @clipboard, Transferable @contents);

	}
}

