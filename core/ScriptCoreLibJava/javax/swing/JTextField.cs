// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JTextField

using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.beans;
using java.lang;
using javax.accessibility;
using javax.swing.text;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JTextField.html
	[Script(IsNative = true)]
	public class JTextField : JTextComponent
	{
		/// <summary>
		/// Constructs a new <code>TextField</code>.
		/// </summary>
		public JTextField()
		{
		}

		/// <summary>
		/// Constructs a new <code>JTextField</code> that uses the given text
		/// storage model and the given number of columns.
		/// </summary>
		public JTextField(Document @doc, string @text, int @columns)
		{
		}

		/// <summary>
		/// Constructs a new empty <code>TextField</code> with the specified
		/// number of columns.
		/// </summary>
		public JTextField(int @columns)
		{
		}

		/// <summary>
		/// Constructs a new <code>TextField</code> initialized with the
		/// specified text.
		/// </summary>
		public JTextField(string @text)
		{
		}

		/// <summary>
		/// Constructs a new <code>TextField</code> initialized with the
		/// specified text and columns.
		/// </summary>
		public JTextField(string @text, int @columns)
		{
		}

		/// <summary>
		/// Adds the specified action listener to receive
		/// action events from this textfield.
		/// </summary>
		public void addActionListener(ActionListener @l)
		{
		}

		/// <summary>
		/// Factory method which sets the <code>ActionEvent</code>
		/// source's properties according to values from the
		/// <code>Action</code> instance.
		/// </summary>
		protected void configurePropertiesFromAction(Action @a)
		{
		}

		/// <summary>
		/// Factory method which creates the <code>PropertyChangeListener</code>
		/// used to update the <code>ActionEvent</code> source as
		/// properties change on its <code>Action</code> instance.
		/// </summary>
		protected PropertyChangeListener createActionPropertyChangeListener(Action @a)
		{
			return default(PropertyChangeListener);
		}

		/// <summary>
		/// Creates the default implementation of the model
		/// to be used at construction if one isn't explicitly
		/// given.
		/// </summary>
		protected Document createDefaultModel()
		{
			return default(Document);
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireActionPerformed()
		{
		}

		/// <summary>
		/// Gets the <code>AccessibleContext</code> associated with this
		/// <code>JTextField</code>.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the currently set <code>Action</code> for this
		/// <code>ActionEvent</code> source, or <code>null</code>
		/// if no <code>Action</code> is set.
		/// </summary>
		public Action getAction()
		{
			return default(Action);
		}

		/// <summary>
		/// Returns an array of all the <code>ActionListener</code>s added
		/// to this JTextField with addActionListener().
		/// </summary>
		public ActionListener[] getActionListeners()
		{
			return default(ActionListener[]);
		}

		/// <summary>
		/// Fetches the command list for the editor.
		/// </summary>
		public Action[] getActions()
		{
			return default(Action[]);
		}

		/// <summary>
		/// Returns the number of columns in this <code>TextField</code>.
		/// </summary>
		public int getColumns()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the column width.
		/// </summary>
		protected int getColumnWidth()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the horizontal alignment of the text.
		/// </summary>
		public int getHorizontalAlignment()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the visibility of the text field.
		/// </summary>
		public BoundedRangeModel getHorizontalVisibility()
		{
			return default(BoundedRangeModel);
		}

		/// <summary>
		/// Returns the preferred size <code>Dimensions</code> needed for this
		/// <code>TextField</code>.
		/// </summary>
		public Dimension getPreferredSize()
		{
			return default(Dimension);
		}

		/// <summary>
		/// Gets the scroll offset, in pixels.
		/// </summary>
		public int getScrollOffset()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the class ID for a UI.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Calls to <code>revalidate</code> that come from within the
		/// textfield itself will
		/// be handled by validating the textfield, unless the textfield
		/// is contained within a <code>JViewport</code>,
		/// in which case this returns false.
		/// </summary>
		public bool isValidateRoot()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this <code>JTextField</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Processes action events occurring on this textfield by
		/// dispatching them to any registered <code>ActionListener</code> objects.
		/// </summary>
		public void postActionEvent()
		{
		}

		/// <summary>
		/// Removes the specified action listener so that it no longer
		/// receives action events from this textfield.
		/// </summary>
		public void removeActionListener(ActionListener @l)
		{
		}

		/// <summary>
		/// Scrolls the field left or right.
		/// </summary>
		public void scrollRectToVisible(Rectangle @r)
		{
		}

		/// <summary>
		/// Sets the <code>Action</code> for the <code>ActionEvent</code> source.
		/// </summary>
		public void setAction(Action @a)
		{
		}

		/// <summary>
		/// Sets the command string used for action events.
		/// </summary>
		public void setActionCommand(string @command)
		{
		}

		/// <summary>
		/// Sets the number of columns in this <code>TextField</code>,
		/// and then invalidate the layout.
		/// </summary>
		public void setColumns(int @columns)
		{
		}

		/// <summary>
		/// Associates the editor with a text document.
		/// </summary>
		public void setDocument(Document @doc)
		{
		}

		/// <summary>
		/// Sets the current font.
		/// </summary>
		public void setFont(Font @f)
		{
		}

		/// <summary>
		/// Sets the horizontal alignment of the text.
		/// </summary>
		public void setHorizontalAlignment(int @alignment)
		{
		}

		/// <summary>
		/// Sets the scroll offset, in pixels.
		/// </summary>
		public void setScrollOffset(int @scrollOffset)
		{
		}

	}
}
