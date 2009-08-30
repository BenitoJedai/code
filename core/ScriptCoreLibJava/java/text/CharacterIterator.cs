// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;

namespace java.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/text/CharacterIterator.html
	[Script(IsNative = true)]
	public interface CharacterIterator : Cloneable
	{
		/// <summary>
		/// Create a copy of this iterator
		/// </summary>
		object clone();

		/// <summary>
		/// Gets the character at the current position (as returned by getIndex()).
		/// </summary>
		char current();

		/// <summary>
		/// Sets the position to getBeginIndex() and returns the character at that
		/// position.
		/// </summary>
		char first();

		/// <summary>
		/// Returns the start index of the text.
		/// </summary>
		int getBeginIndex();

		/// <summary>
		/// Returns the end index of the text.
		/// </summary>
		int getEndIndex();

		/// <summary>
		/// Returns the current index.
		/// </summary>
		int getIndex();

		/// <summary>
		/// Sets the position to getEndIndex()-1 (getEndIndex() if the text is empty)
		/// and returns the character at that position.
		/// </summary>
		char last();

		/// <summary>
		/// Increments the iterator's index by one and returns the character
		/// at the new index.
		/// </summary>
		char next();

		/// <summary>
		/// Decrements the iterator's index by one and returns the character
		/// at the new index.
		/// </summary>
		char previous();

		/// <summary>
		/// Sets the position to the specified position in the text and returns that
		/// character.
		/// </summary>
		char setIndex(int @position);

	}
}

