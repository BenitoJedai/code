// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using java.util;

namespace java.util
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/util/AbstractList.html
	[Script(IsNative = true)]
	public abstract class AbstractList : AbstractCollection
	{
		/// <summary>
		/// Sole constructor.
		/// </summary>
		public AbstractList()
		{
		}

		/// <summary>
		/// Inserts the specified element at the specified position in this list
		/// (optional operation).
		/// </summary>
		public void add(int @index, object @element)
		{
		}

		/// <summary>
		/// Appends the specified element to the end of this List (optional
		/// operation).
		/// </summary>
		public bool add(object @o)
		{
			return default(bool);
		}

		/// <summary>
		/// Inserts all of the elements in the specified collection into this list
		/// at the specified position (optional operation).
		/// </summary>
		public bool addAll(int @index, Collection @c)
		{
			return default(bool);
		}

		/// <summary>
		/// Removes all of the elements from this collection (optional operation).
		/// </summary>
		public void clear()
		{
		}

		/// <summary>
		/// Compares the specified object with this list for equality.
		/// </summary>
		public override bool equals(object @o)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the element at the specified position in this list.
		/// </summary>
		public object get(int @index)
		{
			return default(object);
		}

		/// <summary>
		/// Returns the hash code value for this list.
		/// </summary>
		public override int GetHashCode()
		{
			return default(int);
		}

		/// <summary>
		/// Returns the index in this list of the first occurence of the specified
		/// element, or -1 if the list does not contain this element.
		/// </summary>
		public int indexOf(object @o)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an iterator over the elements in this list in proper
		/// sequence.
		/// </summary>
		public Iterator iterator()
		{
			return default(Iterator);
		}

		/// <summary>
		/// Returns the index in this list of the last occurence of the specified
		/// element, or -1 if the list does not contain this element.
		/// </summary>
		public int lastIndexOf(object @o)
		{
			return default(int);
		}

		/// <summary>
		/// Returns an iterator of the elements in this list (in proper sequence).
		/// </summary>
		public ListIterator listIterator()
		{
			return default(ListIterator);
		}

		/// <summary>
		/// Returns a list iterator of the elements in this list (in proper
		/// sequence), starting at the specified position in the list.
		/// </summary>
		public ListIterator listIterator(int @index)
		{
			return default(ListIterator);
		}

		/// <summary>
		/// Removes the element at the specified position in this list (optional
		/// operation).
		/// </summary>
		public object remove(int @index)
		{
			return default(object);
		}

		/// <summary>
		/// Removes from this list all of the elements whose index is between
		/// <tt>fromIndex</tt>, inclusive, and <tt>toIndex</tt>, exclusive.
		/// </summary>
		protected void removeRange(int @fromIndex, int @toIndex)
		{
		}

		/// <summary>
		/// Replaces the element at the specified position in this list with the
		/// specified element (optional operation).
		/// </summary>
		public object set(int @index, object @element)
		{
			return default(object);
		}

		/// <summary>
		/// Returns a view of the portion of this list between <tt>fromIndex</tt>,
		/// inclusive, and <tt>toIndex</tt>, exclusive.
		/// </summary>
		public List subList(int @fromIndex, int @toIndex)
		{
			return default(List);
		}

	}
}
