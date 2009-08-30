// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using java.util;

namespace java.util
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/util/Collection.html
	[Script(IsNative = true)]
	public interface Collection
	{
		/// <summary>
		/// Ensures that this collection contains the specified element (optional
		/// operation).
		/// </summary>
		bool add(object @o);

		/// <summary>
		/// Adds all of the elements in the specified collection to this collection
		/// (optional operation).
		/// </summary>
		bool addAll(Collection @c);

		/// <summary>
		/// Removes all of the elements from this collection (optional operation).
		/// </summary>
		void clear();

		/// <summary>
		/// Returns <tt>true</tt> if this collection contains the specified
		/// element.
		/// </summary>
		bool contains(object @o);

		/// <summary>
		/// Returns <tt>true</tt> if this collection contains all of the elements
		/// in the specified collection.
		/// </summary>
		bool containsAll(Collection @c);

		/// <summary>
		/// Compares the specified object with this collection for equality.
		/// </summary>
		bool equals(object @o);

		/// <summary>
		/// Returns the hash code value for this collection.
		/// </summary>
		int GetHashCode();

		/// <summary>
		/// Returns <tt>true</tt> if this collection contains no elements.
		/// </summary>
		bool isEmpty();

		/// <summary>
		/// Returns an iterator over the elements in this collection.
		/// </summary>
		Iterator iterator();

		/// <summary>
		/// Removes a single instance of the specified element from this
		/// collection, if it is present (optional operation).
		/// </summary>
		bool remove(object @o);

		/// <summary>
		/// Removes all this collection's elements that are also contained in the
		/// specified collection (optional operation).
		/// </summary>
		bool removeAll(Collection @c);

		/// <summary>
		/// Retains only the elements in this collection that are contained in the
		/// specified collection (optional operation).
		/// </summary>
		bool retainAll(Collection @c);

		/// <summary>
		/// Returns the number of elements in this collection.
		/// </summary>
		int size();

		/// <summary>
		/// Returns an array containing all of the elements in this collection.
		/// </summary>
		System.Array toArray();

		/// <summary>
		/// Returns an array containing all of the elements in this collection;
		/// the runtime type of the returned array is that of the specified array.
		/// </summary>
		System.Array toArray(object[] a);

	}
}

