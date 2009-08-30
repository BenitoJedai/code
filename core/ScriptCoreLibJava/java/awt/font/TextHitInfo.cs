// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.awt.font;
using java.lang;

namespace java.awt.font
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/font/TextHitInfo.html
	[Script(IsNative = true)]
	public class TextHitInfo
	{
		/// <summary>
		/// Creates a <code>TextHitInfo</code> at the specified offset,
		/// associated with the character after the offset.
		/// </summary>
		public TextHitInfo afterOffset(int @offset)
		{
			return default(TextHitInfo);
		}

		/// <summary>
		/// Creates a <code>TextHitInfo</code> at the specified offset,
		/// associated with the character before the offset.
		/// </summary>
		public TextHitInfo beforeOffset(int @offset)
		{
			return default(TextHitInfo);
		}

		/// <summary>
		/// Returns <code>true</code> if the specified <code>Object</code> is a
		/// <code>TextHitInfo</code> and equals this <code>TextHitInfo</code>.
		/// </summary>
		public override bool Equals(object @obj)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <code>true</code> if the specified <code>TextHitInfo</code>
		/// has the same <code>charIndex</code> and <code>isLeadingEdge</code>
		/// as this <code>TextHitInfo</code>.
		/// </summary>
		public  bool Equals(TextHitInfo @hitInfo)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the index of the character hit.
		/// </summary>
		public int getCharIndex()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the insertion index.
		/// </summary>
		public int getInsertionIndex()
		{
			return default(int);
		}

		/// <summary>
		/// Creates a <code>TextHitInfo</code> whose character index is offset
		/// by <code>delta</code> from the <code>charIndex</code> of this
		/// <code>TextHitInfo</code>.
		/// </summary>
		public TextHitInfo getOffsetHit(int @delta)
		{
			return default(TextHitInfo);
		}

		/// <summary>
		/// Creates a <code>TextHitInfo</code> on the other side of the
		/// insertion point.
		/// </summary>
		public TextHitInfo getOtherHit()
		{
			return default(TextHitInfo);
		}

		/// <summary>
		/// Returns the hash code.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns <code>true</code> if the leading edge of the character was
		/// hit.
		/// </summary>
		public bool isLeadingEdge()
		{
			return default(bool);
		}

		/// <summary>
		/// Creates a <code>TextHitInfo</code> on the leading edge of the
		/// character at the specified <code>charIndex</code>.
		/// </summary>
		public TextHitInfo leading(int @charIndex)
		{
			return default(TextHitInfo);
		}

		/// <summary>
		/// Returns a <code>String</code> representing the hit for debugging
		/// use only.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

		/// <summary>
		/// Creates a hit on the trailing edge of the character at
		/// the specified <code>charIndex</code>.
		/// </summary>
		public TextHitInfo trailing(int @charIndex)
		{
			return default(TextHitInfo);
		}

	}
}

