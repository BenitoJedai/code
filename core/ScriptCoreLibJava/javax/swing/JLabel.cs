// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.lang;
using javax.accessibility;
using javax.swing;
using javax.swing.plaf;

namespace javax.swing
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/JLabel.html
	[Script(IsNative = true)]
	public class JLabel : JComponent
	{
		/// <summary>
		/// Creates a <code>JLabel</code> instance with
		/// no image and with an empty string for the title.
		/// </summary>
		public JLabel()
		{
		}

		/// <summary>
		/// Creates a <code>JLabel</code> instance with the specified image.
		/// </summary>
		public JLabel(Icon @image)
		{
		}

		/// <summary>
		/// Creates a <code>JLabel</code> instance with the specified
		/// image and horizontal alignment.
		/// </summary>
		public JLabel(Icon @image, int @horizontalAlignment)
		{
		}

		/// <summary>
		/// Creates a <code>JLabel</code> instance with the specified text.
		/// </summary>
		public JLabel(string @text)
		{
		}

		/// <summary>
		/// Creates a <code>JLabel</code> instance with the specified
		/// text, image, and horizontal alignment.
		/// </summary>
		public JLabel(string @text, Icon @icon, int @horizontalAlignment)
		{
		}

		/// <summary>
		/// Creates a <code>JLabel</code> instance with the specified
		/// text and horizontal alignment.
		/// </summary>
		public JLabel(string @text, int @horizontalAlignment)
		{
		}

		/// <summary>
		/// Verify that key is a legal value for the horizontalAlignment properties.
		/// </summary>
		protected int checkHorizontalKey(int @key, string @message)
		{
			return default(int);
		}

		/// <summary>
		/// Verify that key is a legal value for the
		/// verticalAlignment or verticalTextPosition properties.
		/// </summary>
		protected int checkVerticalKey(int @key, string @message)
		{
			return default(int);
		}

		/// <summary>
		/// Get the AccessibleContext of this object
		/// </summary>
		public AccessibleContext getAccessibleContext()
		{
			return default(AccessibleContext);
		}

		/// <summary>
		/// Returns the value of the disabledIcon property if it's been set,
		/// If it hasn't been set and the value of the icon property is
		/// an ImageIcon, we compute a "grayed out" version of the icon and
		/// update the disabledIcon property with that.
		/// </summary>
		public Icon getDisabledIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Return the keycode that indicates a mnemonic key.
		/// </summary>
		public int getDisplayedMnemonic()
		{
			return default(int);
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
		/// Returns the alignment of the label's contents along the X axis.
		/// </summary>
		public int getHorizontalAlignment()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the horizontal position of the label's text,
		/// relative to its image.
		/// </summary>
		public int getHorizontalTextPosition()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the graphic image (glyph, icon) that the label displays.
		/// </summary>
		public Icon getIcon()
		{
			return default(Icon);
		}

		/// <summary>
		/// Returns the amount of space between the text and the icon
		/// displayed in this label.
		/// </summary>
		public int getIconTextGap()
		{
			return default(int);
		}

		/// <summary>
		/// Get the component this is labelling.
		/// </summary>
		public Component getLabelFor()
		{
			return default(Component);
		}

		/// <summary>
		/// Returns the text string that the label displays.
		/// </summary>
		public string getText()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the L&F object that renders this component.
		/// </summary>
		public LabelUI getUI()
		{
			return default(LabelUI);
		}

		/// <summary>
		/// Returns a string that specifies the name of the l&f class
		/// that renders this component.
		/// </summary>
		public string getUIClassID()
		{
			return default(string);
		}

		/// <summary>
		/// Returns the alignment of the label's contents along the Y axis.
		/// </summary>
		public int getVerticalAlignment()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the vertical position of the label's text,
		/// relative to its image.
		/// </summary>
		public int getVerticalTextPosition()
		{
			return default(int);
		}

		/// <summary>
		/// This is overridden to return false if the current Icon's Image is
		/// not equal to the passed in Image <code>img</code>.
		/// </summary>
		public bool imageUpdate(Image @img, int @infoflags, int @x, int @y, int @w, int @h)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a string representation of this JLabel.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

		/// <summary>
		/// Set the icon to be displayed if this JLabel is "disabled"
		/// (JLabel.setEnabled(false)).
		/// </summary>
		public void setDisabledIcon(Icon @disabledIcon)
		{
		}

		/// <summary>
		/// Specifies the displayedMnemonic as a char value.
		/// </summary>
		public void setDisplayedMnemonic(char @aChar)
		{
		}

		/// <summary>
		/// Specify a keycode that indicates a mnemonic key.
		/// </summary>
		public void setDisplayedMnemonic(int @key)
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
		/// Sets the alignment of the label's contents along the X axis.
		/// </summary>
		public void setHorizontalAlignment(int @alignment)
		{
		}

		/// <summary>
		/// Sets the horizontal position of the label's text,
		/// relative to its image.
		/// </summary>
		public void setHorizontalTextPosition(int @textPosition)
		{
		}

		/// <summary>
		/// Defines the icon this component will display.
		/// </summary>
		public void setIcon(Icon @icon)
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
		/// Set the component this is labelling.
		/// </summary>
		public void setLabelFor(Component @c)
		{
		}

		/// <summary>
		/// Defines the single line of text this component will display.
		/// </summary>
		public void setText(string @text)
		{
		}

		/// <summary>
		/// Sets the L&F object that renders this component.
		/// </summary>
		public void setUI(LabelUI @ui)
		{
		}

		/// <summary>
		/// Sets the alignment of the label's contents along the Y axis.
		/// </summary>
		public void setVerticalAlignment(int @alignment)
		{
		}

		/// <summary>
		/// Sets the vertical position of the label's text,
		/// relative to its image.
		/// </summary>
		public void setVerticalTextPosition(int @textPosition)
		{
		}

		/// <summary>
		/// Resets the UI property to a value from the current look and feel.
		/// </summary>
		public void updateUI()
		{
		}

	}
}

