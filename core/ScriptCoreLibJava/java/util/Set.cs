// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using java.util;

namespace java.util
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/util/Set.html
	[Script(IsNative = true)]
	public interface Set : Collection
	{
		/// <summary>
		/// Adds the specified element to this set if it is not already present
		/// (optional operation).
		/// </summary>
		bool add(object @o);

		/// <summary>
		/// Adds all of the elements in the specified collection to this set if
		/// they're not already present (optional operation).
		/// </summary>
		bool addAll(Collection @c);

		/// <summary>
		/// Removes all of the elements from this set (optional operation).
		/// </summary>
		void clear();

		/// <summary>
		/// Returns <tt>true</tt> if this set contains the specified element.
		/// </summary>
		bool contains(object @o);

		/// <summary>
		/// Returns <tt>true</tt> if this set contains all of the elements of the
		/// specified collection.
		/// </summary>
		bool containsAll(Collection @c);

		/// <summary>
		/// Compares the specified object with this set for equality.
		/// </summary>
		bool equals(object @o);

		/// <summary>
		/// Returns the hash code value for this set.
		/// </summary>
		int GetHashCode();

		/// <summary>
		/// Returns <tt>true</tt> if this set contains no elements.
		/// </summary>
		bool isEmpty();

		/// <summary>
		/// Returns an iterator over the elements in this set.
		/// </summary>
		Iterator iterator();

		/// <summary>
		/// Removes the specified element from this set if it is present (optional
		/// operation).
		/// </summary>
		bool remove(object @o);

		/// <summary>
		/// Removes from this set all of its elements that are contained in the
		/// specified collection (optional operation).
		/// </summary>
		bool removeAll(Collection @c);

		/// <summary>
		/// Retains only the elements in this set that are contained in the
		/// specified collection (optional operation).
		/// </summary>
		bool retainAll(Collection @c);

		/// <summary>
		/// Returns the number of elements in this set (its cardinality).
		/// </summary>
		int size();

		/// <summary>
		/// Returns an array containing all of the elements in this set.
		/// </summary>
		System.Array toArray();

		/// <summary>
		/// Returns an array containing all of the elements in this set; the
		/// runtime type of the returned array is that of the specified array.
		/// </summary>
		System.Array toArray(object[] a);

	}
}

