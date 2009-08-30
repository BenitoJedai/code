// This source code was generated for ScriptCoreLib
using ScriptCoreLib;

namespace java.awt.font
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/awt/font/LineMetrics.html
	[Script(IsNative = true)]
	public abstract class LineMetrics
	{
		/// <summary>
		/// 
		/// </summary>
		public LineMetrics()
		{
		}

		/// <summary>
		/// Returns the ascent of the text.
		/// </summary>
		abstract public float getAscent();

		/// <summary>
		/// Returns the baseline index of the text.
		/// </summary>
		abstract public int getBaselineIndex();

		/// <summary>
		/// Returns the baseline offsets of the text,
		/// relative to the baseline of the text.
		/// </summary>
		abstract public float[] getBaselineOffsets();

		/// <summary>
		/// Returns the descent of the text.
		/// </summary>
		abstract public float getDescent();

		/// <summary>
		/// Returns the height of the text.
		/// </summary>
		abstract public float getHeight();

		/// <summary>
		/// Returns the leading of the text.
		/// </summary>
		abstract public float getLeading();

		/// <summary>
		/// Returns the number of characters in the text whose
		/// metrics are encapsulated by this <code>LineMetrics</code>
		/// object.
		/// </summary>
		abstract public int getNumChars();

		/// <summary>
		/// Returns the position of the strike-through line
		/// relative to the baseline.
		/// </summary>
		abstract public float getStrikethroughOffset();

		/// <summary>
		/// Returns the thickness of the strike-through line.
		/// </summary>
		abstract public float getStrikethroughThickness();

		/// <summary>
		/// Returns the position of the underline relative to
		/// the baseline.
		/// </summary>
		abstract public float getUnderlineOffset();

		/// <summary>
		/// Returns the thickness of the underline.
		/// </summary>
		abstract public float getUnderlineThickness();

	}
}

