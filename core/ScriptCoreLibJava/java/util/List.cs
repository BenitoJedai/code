// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using java.util;

namespace java.util
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/util/List.html
	[Script(IsNative = true)]
	public interface List : Collection
	{
		/// <summary>
		/// Inserts the specified element at the specified position in this list
		/// (optional operation).
		/// </summary>
		void add(int @index, object @element);

		/// <summary>
		/// Appends the specified element to the end of this list (optional
		/// operation).
		/// </summary>
		bool add(object @o);

		/// <summary>
		/// Appends all of the elements in the specified collection to the end of
		/// this list, in the order that they are returned by the specified
		/// collection's iterator (optional operation).
		/// </summary>
		bool addAll(Collection @c);

		/// <summary>
		/// Inserts all of the elements in the specified collection into this
		/// list at the specified position (optional operation).
		/// </summary>
		bool addAll(int @index, Collection @c);

		/// <summary>
		/// Removes all of the elements from this list (optional operation).
		/// </summary>
		void clear();

		/// <summary>
		/// Returns <tt>true</tt> if this list contains the specified element.
		/// </summary>
		bool contains(object @o);

		/// <summary>
		/// Returns <tt>true</tt> if this list contains all of the elements of the
		/// specified collection.
		/// </summary>
		bool containsAll(Collection @c);

		/// <summary>
		/// Compares the specified object with this list for equality.
		/// </summary>
		bool equals(object @o);

		/// <summary>
		/// Returns the element at the specified position in this list.
		/// </summary>
		object get(int @index);

		/// <summary>
		/// Returns the hash code value for this list.
		/// </summary>
		int GetHashCode();

		/// <summary>
		/// Returns the index in this list of the first occurrence of the specified
		/// element, or -1 if this list does not contain this element.
		/// </summary>
		int indexOf(object @o);

		/// <summary>
		/// Returns <tt>true</tt> if this list contains no elements.
		/// </summary>
		bool isEmpty();

		/// <summary>
		/// Returns an iterator over the elements in this list in proper sequence.
		/// </summary>
		Iterator iterator();

		/// <summary>
		/// Returns the index in this list of the last occurrence of the specified
		/// element, or -1 if this list does not contain this element.
		/// </summary>
		int lastIndexOf(object @o);

		/// <summary>
		/// Returns a list iterator of the elements in this list (in proper
		/// sequence).
		/// </summary>
		ListIterator listIterator();

		/// <summary>
		/// Returns a list iterator of the elements in this list (in proper
		/// sequence), starting at the specified position in this list.
		/// </summary>
		ListIterator listIterator(int @index);

		/// <summary>
		/// Removes the element at the specified position in this list (optional
		/// operation).
		/// </summary>
		object remove(int @index);

		/// <summary>
		/// Removes the first occurrence in this list of the specified element
		/// (optional operation).
		/// </summary>
		bool remove(object @o);

		/// <summary>
		/// Removes from this list all the elements that are contained in the
		/// specified collection (optional operation).
		/// </summary>
		bool removeAll(Collection @c);

		/// <summary>
		/// Retains only the elements in this list that are contained in the
		/// specified collection (optional operation).
		/// </summary>
		bool retainAll(Collection @c);

		/// <summary>
		/// Replaces the element at the specified position in this list with the
		/// specified element (optional operation).
		/// </summary>
		object set(int @index, object @element);

		/// <summary>
		/// Returns the number of elements in this list.
		/// </summary>
		int size();

		/// <summary>
		/// Returns a view of the portion of this list between the specified
		/// <tt>fromIndex</tt>, inclusive, and <tt>toIndex</tt>, exclusive.
		/// </summary>
		List subList(int @fromIndex, int @toIndex);

		/// <summary>
		/// Returns an array containing all of the elements in this list in proper
		/// sequence.
		/// </summary>
		System.Array toArray();

		/// <summary>
		/// Returns an array containing all of the elements in this list in proper
		/// sequence; the runtime type of the returned array is that of the
		/// specified array.
		/// </summary>
		System.Array toArray(object[] a);

	}
}

