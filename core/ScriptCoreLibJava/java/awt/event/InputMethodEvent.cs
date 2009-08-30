// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt;
using java.awt.font;
using java.lang;
using java.text;

namespace java.awt.@event
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/event/InputMethodEvent.html
	[Script(IsNative = true)]
	public class InputMethodEvent : AWTEvent
	{
		/// <summary>
		/// Constructs an <code>InputMethodEvent</code> with the specified
		/// source component, type, text, caret, and visiblePosition.
		/// </summary>
		public InputMethodEvent(Component @source, int @id, AttributedCharacterIterator @text, int @committedCharacterCount, TextHitInfo @caret, TextHitInfo @visiblePosition)
		{
		}

		/// <summary>
		/// Constructs an <code>InputMethodEvent</code> with the specified
		/// source component, type, time, text, caret, and visiblePosition.
		/// </summary>
		public InputMethodEvent(Component @source, int @id, long @when, AttributedCharacterIterator @text, int @committedCharacterCount, TextHitInfo @caret, TextHitInfo @visiblePosition)
		{
		}

		/// <summary>
		/// Constructs an <code>InputMethodEvent</code> with the
		/// specified source component, type, caret, and visiblePosition.
		/// </summary>
		public InputMethodEvent(Component @source, int @id, TextHitInfo @caret, TextHitInfo @visiblePosition)
		{
		}

		/// <summary>
		/// Consumes this event so that it will not be processed
		/// in the default manner by the source which originated it.
		/// </summary>
		public void consume()
		{
		}

		/// <summary>
		/// Gets the caret.
		/// </summary>
		public TextHitInfo getCaret()
		{
			return default(TextHitInfo);
		}

		/// <summary>
		/// Gets the number of committed characters in the text.
		/// </summary>
		public int getCommittedCharacterCount()
		{
			return default(int);
		}

		/// <summary>
		/// Gets the combined committed and composed text.
		/// </summary>
		public AttributedCharacterIterator getText()
		{
			return default(AttributedCharacterIterator);
		}

		/// <summary>
		/// Gets the position that's most important to be visible.
		/// </summary>
		public TextHitInfo getVisiblePosition()
		{
			return default(TextHitInfo);
		}

		/// <summary>
		/// Returns the time stamp of when this event occurred.
		/// </summary>
		public long getWhen()
		{
			return default(long);
		}

		/// <summary>
		/// Returns whether or not this event has been consumed.
		/// </summary>
		public bool isConsumed()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns a parameter string identifying this event.
		/// </summary>
		public string paramString()
		{
			return default(string);
		}

	}
}

