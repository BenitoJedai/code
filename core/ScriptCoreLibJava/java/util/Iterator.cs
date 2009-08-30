// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;

namespace java.util
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/util/Iterator.html
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
