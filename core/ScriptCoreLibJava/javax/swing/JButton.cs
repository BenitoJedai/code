// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using javax.accessibility;
using javax.swing;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JButton.html
	[Script(IsNative = true)]
	public class JButton : AbstractButton
	{
		/// <summary>
		/// Creates a button with no set text or icon.
		/// </summary>
		public JButton()
		{
		}

		/// <summary>
		/// Creates a button where properties are taken from the
		/// <code>Action</code> supplied.
		/// </summary>
		public JButton(Action @a)
		{
		}

		/// <summary>
		/// Creates a button with an icon.
		/// </summary>
		public JButton(Icon @icon)
		{
		}

		/// <summary>
		/// Creates a button with text.
		/// </summary>
		public JButton(string @text)
		{
		}

		/// <summary>
		/// Creates a button with initial text and an icon.
		/// </summary>
		public JButton(string @text, Icon @icon)
		{
		}

		/// <summary>
		/// Factory method which sets the <code>AbstractButton</code>'s properties
		/// according to values from the <code>Action</code> instance.
		/// </summary>
		protected void configurePropertiesFromAction(Action @a)
		{
		}

		/// <summary>
		/// Gets the <code>AccessibleContext</code> associated with this
		/// <code>JButton</code>.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns a string that specifies the name of the L&F class
		/// that renders this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Gets the value of the <code>defaultButton</code> property,
		/// which if <code>true</code> means that this button is the current
		/// default button for its <code>JRootPane</code>.
		/// </summary>
		public bool isDefaultButton()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the value of the <code>defaultCapable</code> property.
		/// </summary>
		public bool isDefaultCapable()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this <code>JButton</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Overrides <code>JComponent.removeNotify</code> to check if
		/// this button is currently set as the default button on the
		/// <code>RootPane</code>, and if so, sets the <code>RootPane</code>'s
		/// default button to <code>null</code> to ensure the
		/// <code>RootPane</code> doesn't hold onto an invalid button reference.
		/// </summary>
		public void removeNotify()
		{
		}

		/// <summary>
		/// Sets the <code>defaultCapable</code> property,
		/// which determines whether this button can be
		/// made the default button for its root pane.
		/// </summary>
		public void setDefaultCapable(bool @defaultCapable)
		{
		}

		/// <summary>
		/// Resets the UI property to a value from the current look and
		/// feel.
		/// </summary>
		public void updateUI()
		{
		}

	}
}

