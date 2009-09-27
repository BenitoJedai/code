// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.JOptionPane

using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JOptionPane.html
	[Script(IsNative = true)]
	public class JOptionPane : JComponent
	{
		/// <summary>
		/// Creates a <code>JOptionPane</code> with a test message.
		/// </summary>
		public JOptionPane()
		{
		}

		/// <summary>
		/// Creates a instance of <code>JOptionPane</code> to display a
		/// message using the
		/// plain-message message type and the default options delivered by
		/// the UI.
		/// </summary>
		public JOptionPane(object @message)
		{
		}

		/// <summary>
		/// Creates an instance of <code>JOptionPane</code> to display a message
		/// with the specified message type and the default options,
		/// </summary>
		public JOptionPane(object @message, int @messageType)
		{
		}

		/// <summary>
		/// Creates an instance of <code>JOptionPane</code> to display a message
		/// with the specified message type and options.
		/// </summary>
		public JOptionPane(object @message, int @messageType, int @optionType)
		{
		}

		/// <summary>
		/// Creates an instance of <code>JOptionPane</code> to display a message
		/// with the specified message type, options, and icon.
		/// </summary>
		public JOptionPane(object @message, int @messageType, int @optionType, Icon @icon)
		{
		}

		/// <summary>
		/// Creates an instance of <code>JOptionPane</code> to display a message
		/// with the specified message type, icon, and options.
		/// </summary>
		public JOptionPane(object @message, int @messageType, int @optionType, Icon @icon, object[] @options)
		{
		}

		/// <summary>
		/// Creates an instance of <code>JOptionPane</code> to display a message
		/// with the specified message type, icon, and options, with the
		/// initially-selected option specified.
		/// </summary>
		public JOptionPane(object @message, int @messageType, int @optionType, Icon @icon, object[] @options, object @initialValue)
		{
		}

		/// <summary>
		/// Creates and returns a new <code>JDialog</code> wrapping
		/// <code>this</code> centered on the <code>parentComponent</code>
		/// in the <code>parentComponent</code>'s frame.
		/// </summary>
		public JDialog createDialog(Component @parentComponent, string @title)
		{
			return default(JDialog);
		}

		/// <summary>
		/// Creates and returns an instance of <code>JInternalFrame</code>.
		/// </summary>
		public JInternalFrame createInternalFrame(Component @parentComponent, string @title)
		{
			return default(JInternalFrame);
		}

		/// <summary>
		/// Returns the <code>AccessibleContext</code> associated with this JOptionPane.
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the specified component's desktop pane.
		/// </summary>
		static public JDesktopPane getDesktopPaneForComponent(Component @parentComponent)
		{
			return default(JDesktopPane);
		}

		/// <summary>
		/// Returns the specified component's <code>Frame</code>.
		/// </summary>
		static public Frame getFrameForComponent(Component @parentComponent)
		{
			return default(Frame);
		}

		/// <summary>
		/// Returns the icon this pane displays.
		/// </summary>
		public Icon getIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the input value that is displayed as initially selected to the user.
		/// </summary>
		public object getInitialSelectionValue()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the initial value.
		/// </summary>
		public object getInitialValue()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the value the user has input, if <code>wantsInput</code>
		/// is true.
		/// </summary>
		public object getInputValue()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the maximum number of characters to place on a line in a
		/// message.
		/// </summary>
		public int getMaxCharactersPerLineCount()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the message-object this pane displays.
		/// </summary>
		public object getMessage()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the message type.
		/// </summary>
		public int getMessageType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the choices the user can make.
		/// </summary>
		public object[] getOptions()
		{
			return default(object[]);
		}

		/// <summary>
		/// Returns the type of options that are displayed.
		/// </summary>
		public int getOptionType()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the <code>Frame</code> to use for the class methods in
		/// which a frame is not provided.
		/// </summary>
		static public Frame getRootFrame()
		{
			return default(Frame);
		}

		/// <summary>
		/// Returns the input selection values.
		/// </summary>
		public object[] getSelectionValues()
		{
			return default(object[]);
		}

		/// <summary>
		/// Returns the UI object which implements the L&F for this component.
		/// </summary>
		public OptionPaneUI getUI()
		{
			return default(OptionPaneUI);
		}

		/// <summary>
		/// Returns the name of the UI class that implements the
		/// L&F for this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the value the user has selected.
		/// </summary>
		public object getValue()
		{
			return default(object);
		}

		/// <summary>
		/// Returns the value of the <code>wantsInput</code> property.
		/// </summary>
		public bool getWantsInput()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this <code>JOptionPane</code>.
		/// </summary>
		protected string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Requests that the initial value be selected, which will set
		/// focus to the initial value.
		/// </summary>
		public void selectInitialValue()
		{
		}

		/// <summary>
		/// Sets the icon to display.
		/// </summary>
		public void setIcon(Icon @newIcon)
		{
		}

		/// <summary>
		/// Sets the input value that is initially displayed as selected to the user.
		/// </summary>
		public void setInitialSelectionValue(object @newValue)
		{
		}

		/// <summary>
		/// Sets the initial value that is to be enabled -- the
		/// <code>Component</code>
		/// that has the focus when the pane is initially displayed.
		/// </summary>
		public void setInitialValue(object @newInitialValue)
		{
		}

		/// <summary>
		/// Sets the input value that was selected or input by the user.
		/// </summary>
		public void setInputValue(object @newValue)
		{
		}

		/// <summary>
		/// Sets the option pane's message-object.
		/// </summary>
		public void setMessage(object @newMessage)
		{
		}

		/// <summary>
		/// Sets the option pane's message type.
		/// </summary>
		public void setMessageType(int @newType)
		{
		}

		/// <summary>
		/// Sets the options this pane displays.
		/// </summary>
		public void setOptions(object[] @newOptions)
		{
		}

		/// <summary>
		/// Sets the options to display.
		/// </summary>
		public void setOptionType(int @newType)
		{
		}

		/// <summary>
		/// Sets the frame to use for class methods in which a frame is
		/// not provided.
		/// </summary>
		static public void setRootFrame(Frame @newRootFrame)
		{
		}

		/// <summary>
		/// Sets the input selection values for a pane that provides the user
		/// with a list of items to choose from.
		/// </summary>
		public void setSelectionValues(object[] @newValues)
		{
		}

		/// <summary>
		/// Sets the UI object which implements the L&F for this component.
		/// </summary>
		public void setUI(OptionPaneUI @ui)
		{
		}

		/// <summary>
		/// Sets the value the user has chosen.
		/// </summary>
		public void setValue(object @newValue)
		{
		}

		/// <summary>
		/// Sets the <code>wantsInput</code> property.
		/// </summary>
		public void setWantsInput(bool @newValue)
		{
		}

		/// <summary>
		/// Brings up a dialog with the options <i>Yes</i>,
		/// <i>No</i> and <i>Cancel</i>; with the
		/// title, <b>Select an Option</b>.
		/// </summary>
		static public int showConfirmDialog(Component @parentComponent, object @message)
		{
			return default(int);
		}

		/// <summary>
		/// Brings up a dialog where the number of choices is determined
		/// by the <code>optionType</code> parameter.
		/// </summary>
		static public int showConfirmDialog(Component @parentComponent, object @message, string @title, int @optionType)
		{
			return default(int);
		}

		/// <summary>
		/// Brings up a dialog where the number of choices is determined
		/// by the <code>optionType</code> parameter, where the
		/// <code>messageType</code>
		/// parameter determines the icon to display.
		/// </summary>
		static public int showConfirmDialog(Component @parentComponent, object @message, string @title, int @optionType, int @messageType)
		{
			return default(int);
		}

		/// <summary>
		/// Brings up a dialog with a specified icon, where the number of
		/// choices is determined by the <code>optionType</code> parameter.
		/// </summary>
		static public int showConfirmDialog(Component @parentComponent, object @message, string @title, int @optionType, int @messageType, Icon @icon)
		{
			return default(int);
		}

		/// <summary>
		/// Shows a question-message dialog requesting input from the user
		/// parented to <code>parentComponent</code>.
		/// </summary>
		static public string showInputDialog(Component @parentComponent, object @message)
		{
			return default(string);
		}

		/// <summary>
		/// Shows a question-message dialog requesting input from the user and
		/// parented to <code>parentComponent</code>.
		/// </summary>
		static public string showInputDialog(Component @parentComponent, object @message, object @initialSelectionValue)
		{
			return default(string);
		}

		/// <summary>
		/// Shows a dialog requesting input from the user parented to
		/// <code>parentComponent</code> with the dialog having the title
		/// <code>title</code> and message type <code>messageType</code>.
		/// </summary>
		static public string showInputDialog(Component @parentComponent, object @message, string @title, int @messageType)
		{
			return default(string);
		}

		/// <summary>
		/// Prompts the user for input in a blocking dialog where the
		/// initial selection, possible selections, and all other options can
		/// be specified.
		/// </summary>
		static public object showInputDialog(Component @parentComponent, object @message, string @title, int @messageType, Icon @icon, object[] @selectionValues, object @initialSelectionValue)
		{
			return default(object);
		}

		/// <summary>
		/// Shows a question-message dialog requesting input from the user.
		/// </summary>
		static public string showInputDialog(object @message)
		{
			return default(string);
		}

		/// <summary>
		/// Shows a question-message dialog requesting input from the user, with
		/// the input value initialized to <code>initialSelectionValue</code>.
		/// </summary>
		static public string showInputDialog(object @message, object @initialSelectionValue)
		{
			return default(string);
		}

		/// <summary>
		/// Brings up an internal dialog panel with the options <i>Yes</i>, <i>No</i>
		/// and <i>Cancel</i>; with the title, <b>Select an Option</b>.
		/// </summary>
		static public int showInternalConfirmDialog(Component @parentComponent, object @message)
		{
			return default(int);
		}

		/// <summary>
		/// Brings up a internal dialog panel where the number of choices
		/// is determined by the <code>optionType</code> parameter.
		/// </summary>
		static public int showInternalConfirmDialog(Component @parentComponent, object @message, string @title, int @optionType)
		{
			return default(int);
		}

		/// <summary>
		/// Brings up an internal dialog panel where the number of choices
		/// is determined by the <code>optionType</code> parameter, where
		/// the <code>messageType</code> parameter determines the icon to display.
		/// </summary>
		static public int showInternalConfirmDialog(Component @parentComponent, object @message, string @title, int @optionType, int @messageType)
		{
			return default(int);
		}

		/// <summary>
		/// Brings up an internal dialog panel with a specified icon, where
		/// the number of choices is determined by the <code>optionType</code>
		/// parameter.
		/// </summary>
		static public int showInternalConfirmDialog(Component @parentComponent, object @message, string @title, int @optionType, int @messageType, Icon @icon)
		{
			return default(int);
		}

		/// <summary>
		/// Shows an internal question-message dialog requesting input from
		/// the user parented to <code>parentComponent</code>.
		/// </summary>
		static public string showInternalInputDialog(Component @parentComponent, object @message)
		{
			return default(string);
		}

		/// <summary>
		/// Shows an internal dialog requesting input from the user parented
		/// to <code>parentComponent</code> with the dialog having the title
		/// <code>title</code> and message type <code>messageType</code>.
		/// </summary>
		static public string showInternalInputDialog(Component @parentComponent, object @message, string @title, int @messageType)
		{
			return default(string);
		}

		/// <summary>
		/// Prompts the user for input in a blocking internal dialog where
		/// the initial selection, possible selections, and all other
		/// options can be specified.
		/// </summary>
		static public object showInternalInputDialog(Component @parentComponent, object @message, string @title, int @messageType, Icon @icon, object[] @selectionValues, object @initialSelectionValue)
		{
			return default(object);
		}

		/// <summary>
		/// Brings up an internal confirmation dialog panel.
		/// </summary>
		static public void showInternalMessageDialog(Component @parentComponent, object @message)
		{
		}

		/// <summary>
		/// Brings up an internal dialog panel that displays a message
		/// using a default icon determined by the <code>messageType</code>
		/// parameter.
		/// </summary>
		static public void showInternalMessageDialog(Component @parentComponent, object @message, string @title, int @messageType)
		{
		}

		/// <summary>
		/// Brings up an internal dialog panel displaying a message,
		/// specifying all parameters.
		/// </summary>
		static public void showInternalMessageDialog(Component @parentComponent, object @message, string @title, int @messageType, Icon @icon)
		{
		}

		/// <summary>
		/// Brings up an internal dialog panel with a specified icon, where
		/// the initial choice is determined by the <code>initialValue</code>
		/// parameter and the number of choices is determined by the
		/// <code>optionType</code> parameter.
		/// </summary>
		static public int showInternalOptionDialog(Component @parentComponent, object @message, string @title, int @optionType, int @messageType, Icon @icon, object[] @options, object @initialValue)
		{
			return default(int);
		}

		/// <summary>
		/// Brings up an information-message dialog titled "Message".
		/// </summary>
		static public void showMessageDialog(Component @parentComponent, object @message)
		{
		}

		/// <summary>
		/// Brings up a dialog that displays a message using a default
		/// icon determined by the <code>messageType</code> parameter.
		/// </summary>
		static public void showMessageDialog(Component @parentComponent, object @message, string @title, int @messageType)
		{
		}

		/// <summary>
		/// Brings up a dialog displaying a message, specifying all parameters.
		/// </summary>
		static public void showMessageDialog(Component @parentComponent, object @message, string @title, int @messageType, Icon @icon)
		{
		}

		/// <summary>
		/// Brings up a dialog with a specified icon, where the initial
		/// choice is determined by the <code>initialValue</code> parameter and
		/// the number of choices is determined by the <code>optionType</code>
		/// parameter.
		/// </summary>
		static public int showOptionDialog(Component @parentComponent, object @message, string @title, int @optionType, int @messageType, Icon @icon, object[] @options, object @initialValue)
		{
			return default(int);
		}

		/// <summary>
		/// Notification from the <code>UIManager</code> that the L&F has changed.
		/// </summary>
		public void updateUI()
		{
		}

	}
}
