// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.datatransfer;
using java.lang;

namespace java.awt.datatransfer
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/datatransfer/Clipboard.html
	[Script(IsNative = true)]
	public class Clipboard
	{
		/// <summary>
		/// Creates a clipboard object.
		/// </summary>
		public Clipboard(string @name)
		{
		}

		/// <summary>
		/// Returns a transferable object representing the current contents
		/// of the clipboard.
		/// </summary>
		public Transferable getContents(object @requestor)
		{
			return default(Transferable);
		}

		/// <summary>
		/// Returns the name of this clipboard object.
		/// </summary>
		public string getName()
		{
			return default(string);
		}

		/// <summary>
		/// Sets the current contents of the clipboard to the specified
		/// transferable object and registers the specified clipboard owner
		/// as the owner of the new contents.
		/// </summary>
		public void setContents(Transferable @contents, ClipboardOwner @owner)
		{
		}

	}
}

