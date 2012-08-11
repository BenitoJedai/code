// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;

namespace java.util
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/util/Iterator.html
    // http://developer.android.com/reference/java/util/Iterator.html
	[Script(IsNative = true)]
	public interface Iterator
	{
		/// <summary>
		/// Returns <tt>true</tt> if the iteration has more elements.
		/// </summary>
		bool hasNext();

		/// <summary>
		/// Returns the next element in the iteration.
		/// </summary>
		object next();

		/// <summary>
		/// Removes from the underlying collection the last element returned by the
		/// iterator (optional operation).
		/// </summary>
		void remove();

	}
}
