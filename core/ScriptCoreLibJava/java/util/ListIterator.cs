// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;

namespace java.util
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/util/ListIterator.html
	[Script(IsNative = true)]
	public interface ListIterator
	{
		/// <summary>
		/// Inserts the specified element into the list (optional operation).
		/// </summary>
		void add(object @o);

		/// <summary>
		/// Returns <tt>true</tt> if this list iterator has more elements when
		/// traversing the list in the forward direction.
		/// </summary>
		bool hasNext();

		/// <summary>
		/// Returns <tt>true</tt> if this list iterator has more elements when
		/// traversing the list in the reverse direction.
		/// </summary>
		bool hasPrevious();

		/// <summary>
		/// Returns the next element in the list.
		/// </summary>
		object next();

		/// <summary>
		/// Returns the index of the element that would be returned by a subsequent
		/// call to <tt>next</tt>.
		/// </summary>
		int nextIndex();

		/// <summary>
		/// Returns the previous element in the list.
		/// </summary>
		object previous();

		/// <summary>
		/// Returns the index of the element that would be returned by a subsequent
		/// call to <tt>previous</tt>.
		/// </summary>
		int previousIndex();

		/// <summary>
		/// Removes from the list the last element that was returned by
		/// <tt>next</tt> or <tt>previous</tt> (optional operation).
		/// </summary>
		void remove();

		/// <summary>
		/// Replaces the last element returned by <tt>next</tt> or
		/// <tt>previous</tt> with the specified element (optional operation).
		/// </summary>
		void set(object @o);

	}
}

