// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.font;
using java.text;

namespace java.awt.im
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/im/InputMethodRequests.html
	[Script(IsNative = true)]
	public interface InputMethodRequests
	{
		/// <summary>
		/// Gets the latest committed text from the text editing component and
		/// removes it from the component's text body.
		/// </summary>
		//AttributedCharacterIterator cancelLatestCommittedText(Attribute[] @attributes);

		/// <summary>
		/// Gets an iterator providing access to the entire text and attributes
		/// contained in the text editing component except for uncommitted
		/// text.
		/// </summary>
		//AttributedCharacterIterator getCommittedText(int @beginIndex, int @endIndex, Attribute[] @attributes);

		/// <summary>
		/// Gets the length of the entire text contained in the text
		/// editing component except for uncommitted (composed) text.
		/// </summary>
		int getCommittedTextLength();

		/// <summary>
		/// Gets the offset of the insert position in the committed text contained
		/// in the text editing component.
		/// </summary>
		int getInsertPositionOffset();

		/// <summary>
		/// Gets the offset within the composed text for the specified absolute x
		/// and y coordinates on the screen.
		/// </summary>
		TextHitInfo getLocationOffset(int @x, int @y);

		/// <summary>
		/// Gets the currently selected text from the text editing component.
		/// </summary>
		//AttributedCharacterIterator getSelectedText(Attribute[] @attributes);

		/// <summary>
		/// Gets the location of a specified offset in the current composed text,
		/// or of the selection in committed text.
		/// </summary>
		Rectangle getTextLocation(TextHitInfo @offset);

	}
}

