// This source code was generated for ScriptCoreLib
// http://services.zproxybuzz.info/java/javax.swing.text.Segment

using ScriptCoreLib;
using java.lang;

namespace javax.swing.text
{
	// http://java.sun.com/j2se/1.4.2/docs/api/javax/swing/text/Segment.html
	[Script(IsNative = true)]
	public class Segment
	{
		/// <summary>
		/// Creates a new segment.
		/// </summary>
		public Segment()
		{
		}

		/// <summary>
		/// Creates a new segment referring to an existing array.
		/// </summary>
		public Segment(char[] @array, int @offset, int @count)
		{
		}

		/// <summary>
		/// Creates a shallow copy.
		/// </summary>
		public object clone()
		{
			return default(object);
		}

		/// <summary>
		/// Gets the character at the current position (as returned by getIndex()).
		/// </summary>
		public char current()
		{
			return default(char);
		}

		/// <summary>
		/// Sets the position to getBeginIndex() and returns the character at that
		/// position.
		/// </summary>
		public char first()
		{
			return default(char);
		}

		/// <summary>
		/// Returns the start index of the text.
		/// </summary>
		public int getBeginIndex()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the end index of the text.
		/// </summary>
		public int getEndIndex()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the current index.
		/// </summary>
		public int getIndex()
		{
			return default(int);
		}

		/// <summary>
		/// Flag to indicate that partial returns are valid.
		/// </summary>
		public bool isPartialReturn()
		{
			return default(bool);
		}

		/// <summary>
		/// Sets the position to getEndIndex()-1 (getEndIndex() if the text is empty)
		/// and returns the character at that position.
		/// </summary>
		public char last()
		{
			return default(char);
		}

		/// <summary>
		/// Increments the iterator's index by one and returns the character
		/// at the new index.
		/// </summary>
		public char next()
		{
			return default(char);
		}

		/// <summary>
		/// Decrements the iterator's index by one and returns the character
		/// at the new index.
		/// </summary>
		public char previous()
		{
			return default(char);
		}

		/// <summary>
		/// Sets the position to the specified position in the text and returns that
		/// character.
		/// </summary>
		public char setIndex(int @position)
		{
			return default(char);
		}

		/// <summary>
		/// Flag to indicate that partial returns are valid.
		/// </summary>
		public void setPartialReturn(bool @p)
		{
		}

		/// <summary>
		/// Converts a segment into a String.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
