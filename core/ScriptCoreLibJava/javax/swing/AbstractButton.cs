// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.beans;
using java.lang;
using javax.swing;
using javax.swing.@event;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/AbstractButton.html
	[Script(IsNative = true)]
	public class AbstractButton : JComponent
	{
		/// <summary>
		/// 
		/// </summary>
		public AbstractButton()
		{
		}

		/// <summary>
		/// Adds an <code>ActionListener</code> to the button.
		/// </summary>
		public void addActionListener(ActionListener @l)
		{
		}

		/// <summary>
		/// Adds a <code>ChangeListener</code> to the button.
		/// </summary>
		public void addChangeListener(ChangeListener @l)
		{
		}

		/// <summary>
		/// Adds an <code>ItemListener</code> to the <code>checkbox</code>.
		/// </summary>
		public void addItemListener(ItemListener @l)
		{
		}

		/// <summary>
		/// Verify that key is a legal value for the
		/// <code>horizontalAlignment</code> properties.
		/// </summary>
		protected int checkHorizontalKey(int @key, string @exception)
		{
			return default(int);
		}

		/// <summary>
		/// Ensures that the key is a valid.
		/// </summary>
		protected int checkVerticalKey(int @key, string @exception)
		{
			return default(int);
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
		/// 
		/// </summary>
		public ActionListener createActionListener()
		{
			return default(ActionListener);
		}

		/// <summary>
		/// Factory method which creates the <code>PropertyChangeListener</code>
		/// used to update the <code>ActionEvent</code> source as properties
		/// change on its <code>Action</code> instance.
		/// </summary>
		public PropertyChangeListener createActionPropertyChangeListener(Action @a)
		{
			return default(PropertyChangeListener);
		}

		/// <summary>
		/// Subclasses that want to handle <code>ChangeEvents</code> differently
		/// can override this to return another <code>ChangeListener</code>
		/// implementation.
		/// </summary>
		public ChangeListener createChangeListener()
		{
			return default(ChangeListener);
		}

		/// <summary>
		/// 
		/// </summary>
		public ItemListener createItemListener()
		{
			return default(ItemListener);
		}

		/// <summary>
		/// Programmatically perform a "click".
		/// </summary>
		public void doClick()
		{
		}

		/// <summary>
		/// Programmatically perform a "click".
		/// </summary>
		public void doClick(int @pressTime)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireActionPerformed(ActionEvent @event)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireItemStateChanged(ItemEvent @event)
		{
		}

		/// <summary>
		/// Notifies all listeners that have registered interest for
		/// notification on this event type.
		/// </summary>
		protected void fireStateChanged()
		{
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
		/// Returns the action command for this button.
		/// </summary>
		public string getActionCommand()
		{
			return default(string);
		}

		/// <summary>
		/// Returns an array of all the <code>ActionListener</code>s added
		/// to this AbstractButton with addActionListener().
		/// </summary>
		public ActionListener[] getActionListeners()
		{
			return default(ActionListener[]);
		}

		/// <summary>
		/// Returns an array of all the <code>ChangeListener</code>s added
		/// to this AbstractButton with addChangeListener().
		/// </summary>
		public ChangeListener[] getChangeListeners()
		{
			return default(ChangeListener[]);
		}

		/// <summary>
		/// Returns the icon used by the button when it's disabled.
		/// </summary>
		public Icon getDisabledIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the icon used by the button when it's disabled and selected.
		/// </summary>
		public Icon getDisabledSelectedIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the character, as an index, that the look and feel should
		/// provide decoration for as representing the mnemonic character.
		/// </summary>
		public int getDisplayedMnemonicIndex()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the horizontal alignment of the icon and text.
		/// </summary>
		public int getHorizontalAlignment()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the horizontal position of the text relative to the icon.
		/// </summary>
		public int getHorizontalTextPosition()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the default icon.
		/// </summary>
		public Icon getIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the amount of space between the text and the icon
		/// displayed in this button.
		/// </summary>
		public int getIconTextGap()
		{
			return default(int);
		}

		/// <summary>
		/// Returns an array of all the <code>ItemListener</code>s added
		/// to this AbstractButton with addItemListener().
		/// </summary>
		public ItemListener[] getItemListeners()
		{
			return default(ItemListener[]);
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>- Replaced by <code>getText</code></I>
		/// </summary>
		public string getLabel()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the margin between the button's border and
		/// the label.
		/// </summary>
		public Insets getMargin()
		{
			return default(Insets);
		}

		/// <summary>
		/// Returns the keyboard mnemonic from the the current model.
		/// </summary>
		public int getMnemonic()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the model that this button represents.
		/// </summary>
		public ButtonModel getModel()
		{
			return default(ButtonModel);
		}

		/// <summary>
		/// Gets the amount of time (in milliseconds) required between
		/// mouse press events for the button to generate the corresponding
		/// action events.
		/// </summary>
		public long getMultiClickThreshhold()
		{
			return default(long);
		}

		/// <summary>
		/// Returns the pressed icon for the button.
		/// </summary>
		public Icon getPressedIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the rollover icon for the button.
		/// </summary>
		public Icon getRolloverIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the rollover selection icon for the button.
		/// </summary>
		public Icon getRolloverSelectedIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the selected icon for the button.
		/// </summary>
		public Icon getSelectedIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns an array (length 1) containing the label or
		/// <code>null</code> if the button is not selected.
		/// </summary>
		public Object[] getSelectedObjects()
		{
			return default(Object[]);
		}

		/// <summary>
		/// Returns the button's text.
		/// </summary>
		public string getText()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the L&F object that renders this component.
		/// </summary>
		public ButtonUI getUI()
		{
			return default(ButtonUI);
		}

		/// <summary>
		/// Returns the vertical alignment of the text and icon.
		/// </summary>
		public int getVerticalAlignment()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the vertical position of the text relative to the icon.
		/// </summary>
		public int getVerticalTextPosition()
		{
			return default(int);
		}

		/// <summary>
		/// This is overridden to return false if the current <code>Icon</code>'s
		/// <code>Image</code> is not equal to the
		/// passed in <code>Image</code> <code>img</code>.
		/// </summary>
		public bool imageUpdate(Image @img, int @infoflags, int @x, int @y, int @w, int @h)
		{
			return default(bool);
		}

		/// <summary>
		/// 
		/// </summary>
		protected void init(string @text, Icon @icon)
		{
		}

		/// <summary>
		/// Gets the <code>borderPainted</code> property.
		/// </summary>
		public bool isBorderPainted()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the <code>contentAreaFilled</code> property.
		/// </summary>
		public bool isContentAreaFilled()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the <code>paintFocus</code> property.
		/// </summary>
		public bool isFocusPainted()
		{
			return default(bool);
		}

		/// <summary>
		/// Gets the <code>rolloverEnabled</code> property.
		/// </summary>
		public bool isRolloverEnabled()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the state of the button.
		/// </summary>
		public bool isSelected()
		{
			return default(bool);
		}

		/// <summary>
		/// Paint the button's border if <code>BorderPainted</code>
		/// property is true and the button has a border.
		/// </summary>
		protected void paintBorder(Graphics @g)
		{
		}

		/// <summary>
		/// Returns a string representation of this <code>AbstractButton</code>.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Removes an <code>ActionListener</code> from the button.
		/// </summary>
		public void removeActionListener(ActionListener @l)
		{
		}

		/// <summary>
		/// Removes a ChangeListener from the button.
		/// </summary>
		public void removeChangeListener(ChangeListener @l)
		{
		}

		/// <summary>
		/// Removes an <code>ItemListener</code> from the button.
		/// </summary>
		public void removeItemListener(ItemListener @l)
		{
		}

		/// <summary>
		/// Sets the <code>Action</code> for the <code>ActionEvent</code> source.
		/// </summary>
		public void setAction(Action @a)
		{
		}

		/// <summary>
		/// Sets the action command for this button.
		/// </summary>
		public void setActionCommand(string @actionCommand)
		{
		}

		/// <summary>
		/// Sets the <code>borderPainted</code> property.
		/// </summary>
		public void setBorderPainted(bool @b)
		{
		}

		/// <summary>
		/// Sets the <code>contentAreaFilled</code> property.
		/// </summary>
		public void setContentAreaFilled(bool @b)
		{
		}

		/// <summary>
		/// Sets the disabled icon for the button.
		/// </summary>
		public void setDisabledIcon(Icon @disabledIcon)
		{
		}

		/// <summary>
		/// Sets the disabled selection icon for the button.
		/// </summary>
		public void setDisabledSelectedIcon(Icon @disabledSelectedIcon)
		{
		}

		/// <summary>
		/// Provides a hint to the look and feel as to which character in the
		/// text should be decorated to represent the mnemonic.
		/// </summary>
		public void setDisplayedMnemonicIndex(int @index)
		{
		}

		/// <summary>
		/// Enables (or disables) the button.
		/// </summary>
		public void setEnabled(bool @b)
		{
		}

		/// <summary>
		/// Sets the <code>paintFocus</code> property, which must
		/// be <code>true</code> for the focus state to be painted.
		/// </summary>
		public void setFocusPainted(bool @b)
		{
		}

		/// <summary>
		/// Sets the horizontal alignment of the icon and text.
		/// </summary>
		public void setHorizontalAlignment(int @alignment)
		{
		}

		/// <summary>
		/// Sets the horizontal position of the text relative to the icon.
		/// </summary>
		public void setHorizontalTextPosition(int @textPosition)
		{
		}

		/// <summary>
		/// Sets the button's default icon.
		/// </summary>
		public void setIcon(Icon @defaultIcon)
		{
		}

		/// <summary>
		/// If both the icon and text properties are set, this property
		/// defines the space between them.
		/// </summary>
		public void setIconTextGap(int @iconTextGap)
		{
		}

		/// <summary>
		/// <B>Deprecated.</B> <I>- Replaced by <code>setText(text)</code></I>
		/// </summary>
		public void setLabel(string @label)
		{
		}

		/// <summary>
		/// Sets space for margin between the button's border and
		/// the label.
		/// </summary>
		public void setMargin(Insets @m)
		{
		}

		/// <summary>
		/// This method is now obsolete, please use <code>setMnemonic(int)</code>
		/// to set the mnemonic for a button.
		/// </summary>
		public void setMnemonic(char @mnemonic)
		{
		}

		/// <summary>
		/// Sets the keyboard mnemonic on the current model.
		/// </summary>
		public void setMnemonic(int @mnemonic)
		{
		}

		/// <summary>
		/// Sets the model that this button represents.
		/// </summary>
		public void setModel(ButtonModel @newModel)
		{
		}

		/// <summary>
		/// Sets the amount of time (in milliseconds) required between
		/// mouse press events for the button to generate the corresponding
		/// action events.
		/// </summary>
		public void setMultiClickThreshhold(long @threshhold)
		{
		}

		/// <summary>
		/// Sets the pressed icon for the button.
		/// </summary>
		public void setPressedIcon(Icon @pressedIcon)
		{
		}

		/// <summary>
		/// Sets the <code>rolloverEnabled</code> property, which
		/// must be <code>true</code> for rollover effects to occur.
		/// </summary>
		public void setRolloverEnabled(bool @b)
		{
		}

		/// <summary>
		/// Sets the rollover icon for the button.
		/// </summary>
		public void setRolloverIcon(Icon @rolloverIcon)
		{
		}

		/// <summary>
		/// Sets the rollover selected icon for the button.
		/// </summary>
		public void setRolloverSelectedIcon(Icon @rolloverSelectedIcon)
		{
		}

		/// <summary>
		/// Sets the state of the button.
		/// </summary>
		public void setSelected(bool @b)
		{
		}

		/// <summary>
		/// Sets the selected icon for the button.
		/// </summary>
		public void setSelectedIcon(Icon @selectedIcon)
		{
		}

		/// <summary>
		/// Sets the button's text.
		/// </summary>
		public void setText(string @text)
		{
		}

		/// <summary>
		/// Sets the L&F object that renders this component.
		/// </summary>
		public void setUI(ButtonUI @ui)
		{
		}

		/// <summary>
		/// Sets the vertical alignment of the icon and text.
		/// </summary>
		public void setVerticalAlignment(int @alignment)
		{
		}

		/// <summary>
		/// Sets the vertical position of the text relative to the icon.
		/// </summary>
		public void setVerticalTextPosition(int @textPosition)
		{
		}

		/// <summary>
		/// Resets the UI property to a value from the current look
		/// and feel.
		/// </summary>
		public void updateUI()
		{
		}

	}
}

