// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.Caret

using ScriptCoreLib;
using java.awt;
using javax.swing.@event;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/Caret.html
	[Script(IsNative = true)]
	public interface Caret
	{
		/// <summary>
		/// Adds a listener to track whenever the caret position
		/// has been changed.
		/// </summary>
		void addChangeListener(ChangeListener @l);

		/// <summary>
		/// Called when the UI is being removed from the
		/// interface of a JTextComponent.
		/// </summary>
		void deinstall(JTextComponent @c);

		/// <summary>
		/// Gets the blink rate of the caret.
		/// </summary>
		int getBlinkRate();

		/// <summary>
		/// Fetches the current position of the caret.
		/// </summary>
		int getDot();

		/// <summary>
		/// Gets the current caret visual location.
		/// </summary>
		Point getMagicCaretPosition();

		/// <summary>
		/// Fetches the current position of the mark.
		/// </summary>
		int getMark();

		/// <summary>
		/// Called when the UI is being installed into the
		/// interface of a JTextComponent.
		/// </summary>
		void install(JTextComponent @c);

		/// <summary>
		/// Determines if the selection is currently visible.
		/// </summary>
		bool isSelectionVisible();

		/// <summary>
		/// Determines if the caret is currently visible.
		/// </summary>
		bool isVisible();

		/// <summary>
		/// Moves the caret position (dot) to some other position,
		/// leaving behind the mark.
		/// </summary>
		void moveDot(int @dot);

		/// <summary>
		/// Renders the caret.
		/// </summary>
		void paint(Graphics @g);

		/// <summary>
		/// Removes a listener that was tracking caret position changes.
		/// </summary>
		void removeChangeListener(ChangeListener @l);

		/// <summary>
		/// Sets the blink rate of the caret.
		/// </summary>
		void setBlinkRate(int @rate);

		/// <summary>
		/// Sets the caret position to some position.
		/// </summary>
		void setDot(int @dot);

		/// <summary>
		/// Set the current caret visual location.
		/// </summary>
		void setMagicCaretPosition(Point @p);

		/// <summary>
		/// Sets the visibility of the selection
		/// </summary>
		void setSelectionVisible(bool @v);

		/// <summary>
		/// Sets the visibility of the caret.
		/// </summary>
		void setVisible(bool @v);

	}
}
