// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.@event;
using java.lang;
using javax.swing;
using javax.swing.@event;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/ButtonModel.html
	[Script(IsNative = true)]
	public interface ButtonModel : ItemSelectable
	{
		/// <summary>
		/// Adds an ActionListener to the button.
		/// </summary>
		void addActionListener(ActionListener @l);

		/// <summary>
		/// Adds a ChangeListener to the button.
		/// </summary>
		void addChangeListener(ChangeListener @l);

		/// <summary>
		/// Adds an ItemListener to the button.
		/// </summary>
		void addItemListener(ItemListener @l);

		/// <summary>
		/// Returns the action command for this button.
		/// </summary>
		string getActionCommand();

		/// <summary>
		/// Gets the keyboard mnemonic for this model
		/// </summary>
		int getMnemonic();

		/// <summary>
		/// Indicates partial commitment towards pressing the
		/// button.
		/// </summary>
		bool isArmed();

		/// <summary>
		/// Indicates if the button can be selected or pressed by
		/// an input device (such as a mouse pointer).
		/// </summary>
		bool isEnabled();

		/// <summary>
		/// Indicates if button has been pressed.
		/// </summary>
		bool isPressed();

		/// <summary>
		/// Indicates that the mouse is over the button.
		/// </summary>
		bool isRollover();

		/// <summary>
		/// Indicates if the button has been selected.
		/// </summary>
		bool isSelected();

		/// <summary>
		/// Removes an ActionListener from the button.
		/// </summary>
		void removeActionListener(ActionListener @l);

		/// <summary>
		/// Removes a ChangeListener from the button.
		/// </summary>
		void removeChangeListener(ChangeListener @l);

		/// <summary>
		/// Removes an ItemListener from the button.
		/// </summary>
		void removeItemListener(ItemListener @l);

		/// <summary>
		/// Sets the actionCommand string that gets sent as part of the
		/// event when the button is pressed.
		/// </summary>
		void setActionCommand(string @s);

		/// <summary>
		/// Marks the button as "armed".
		/// </summary>
		void setArmed(bool @b);

		/// <summary>
		/// Enables or disables the button.
		/// </summary>
		void setEnabled(bool @b);

		/// <summary>
		/// Identifies the group this button belongs to --
		/// needed for radio buttons, which are mutually
		/// exclusive within their group.
		/// </summary>
		void setGroup(ButtonGroup @group);

		/// <summary>
		/// Sets the keyboard mnemonic (shortcut key or
		/// accelerator key) for this button.
		/// </summary>
		void setMnemonic(int @key);

		/// <summary>
		/// Sets the button to pressed or unpressed.
		/// </summary>
		void setPressed(bool @b);

		/// <summary>
		/// Sets or clears the button's rollover state
		/// </summary>
		void setRollover(bool @b);

		/// <summary>
		/// Selects or deselects the button.
		/// </summary>
		void setSelected(bool @b);

	}
}

