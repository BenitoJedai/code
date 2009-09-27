// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/java.awt.Dialog

using ScriptCoreLib;
using java.lang;
using javax.accessibility;

namespace java.awt
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Dialog.html
	[Script(IsNative = true)]
	public class Dialog : Window
	{
		/// <summary>
		/// Constructs an initially invisible, non-modal Dialog with
		/// an empty title and the specified owner dialog.
		/// </summary>
		public Dialog(Dialog @owner) : base(owner)
		{
		}

		/// <summary>
		/// Constructs an initially invisible, non-modal Dialog
		/// with the specified owner dialog and title.
		/// </summary>
		public Dialog(Dialog @owner, string @title)
			: base(owner)
		{
		}

		/// <summary>
		/// Constructs an initially invisible <code>Dialog</code> with the
		/// specified owner dialog, title, and modality.
		/// </summary>
		public Dialog(Dialog @owner, string @title, bool @modal)
			: base(owner)
		{
		}

		/// <summary>
		/// Constructs an initially invisible <code>Dialog</code> with the
		/// specified owner dialog, title, modality, and
		/// <code>GraphicsConfiguration</code>.
		/// </summary>
		public Dialog(Dialog @owner, string @title, bool @modal, GraphicsConfiguration @gc)
			: base(owner)
		{
		}

		/// <summary>
		/// Constructs an initially invisible, non-modal <code>Dialog</code> with
		/// an empty title and the specified owner frame.
		/// </summary>
		public Dialog(Frame @owner)
			: base(owner)
		{
		}

		/// <summary>
		/// Constructs an initially invisible <code>Dialog</code> with an empty title,
		/// the specified owner frame and modality.
		/// </summary>
		public Dialog(Frame @owner, bool @modal)
			: base(owner)
		{
		}

		/// <summary>
		/// Constructs an initially invisible, non-modal <code>Dialog</code> with
		/// the specified owner frame and title.
		/// </summary>
		public Dialog(Frame @owner, string @title)
			: base(owner)
		{
		}

		/// <summary>
		/// Constructs an initially invisible <code>Dialog</code> with the
		/// specified owner frame, title, and modality.
		/// </summary>
		public Dialog(Frame @owner, string @title, bool @modal)
			: base(owner)
		{
		}

		/// <summary>
		/// Constructs an initially invisible Dialog with the
		/// specified owner frame, title, modality, and
		/// <code>GraphicsConfiguration</code>.
		/// </summary>
		public Dialog(Frame @owner, string @title, bool @modal, GraphicsConfiguration @gc)
			: base(owner)
		{
		}

		/// <summary>
		/// Makes this Dialog displayable by connecting it to
		/// a native screen resource.
		/// </summary>
		public void addNotify()
		{
		}

		/// <summary>
		/// Disposes the Dialog and then causes show() to return if it is currently
		/// blocked.
		/// </summary>
		public void dispose()
		{
		}

		/// <summary>
		/// Gets the AccessibleContext associated with this Dialog.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Gets the title of the dialog.
		/// </summary>
		public string getTitle()
		{
			return default(string);
		}

		/// <summary>
		/// Hides the Dialog and then causes show() to return if it is currently
		/// blocked.
		/// </summary>
		public void hide()
		{
		}

		/// <summary>
		/// Indicates whether the dialog is modal.
		/// </summary>
		public bool isModal()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether this dialog is resizable by the user.
		/// </summary>
		public bool isResizable()
		{
			return default(bool);
		}

		/// <summary>
		/// Indicates whether this dialog is undecorated.
		/// </summary>
		public bool isUndecorated()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representing the state of this dialog.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Specifies whether this dialog should be modal.
		/// </summary>
		public void setModal(bool @b)
		{
		}

		/// <summary>
		/// Sets whether this dialog is resizable by the user.
		/// </summary>
		public void setResizable(bool @resizable)
		{
		}

		/// <summary>
		/// Sets the title of the Dialog.
		/// </summary>
		public void setTitle(string @title)
		{
		}

		/// <summary>
		/// Disables or enables decorations for this dialog.
		/// </summary>
		public void setUndecorated(bool @undecorated)
		{
		}

		/// <summary>
		/// Makes the Dialog visible.
		/// </summary>
		public void show()
		{
		}

	}
}
