// This source code was generated for ScriptCoreLib
using ScriptCoreLib;
using java.lang;
using java.util;

namespace java.util
{
	// http://java.sun.com/j2se/1.4.2/docs/api/java/util/AbstractCollection.html
	[Script(IsNative = true)]
	public abstract class AbstractCollection : Collection
	{
		/// <summary>
		/// Sole constructor.
		/// </summary>
		public AbstractCollection()
		{
		}

		public abstract bool equals(object @o);
		
		/// <summary>
		/// Ensures that this collection contains the specified element (optional
		/// operation).
		/// </summary>
		public bool add(object @o)
		{
			return default(bool);
		}

		/// <summary>
		/// Adds all of the elements in the specified collection to this collection
		/// (optional operation).
		/// </summary>
		public bool addAll(Collection @c)
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
		/// Returns <tt>true</tt> if this collection contains the specified
		/// element.
		/// </summary>
		public bool contains(object @o)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <tt>true</tt> if this collection contains all of the elements
		/// in the specified collection.
		/// </summary>
		public bool containsAll(Collection @c)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns <tt>true</tt> if this collection contains no elements.
		/// </summary>
		public bool isEmpty()
		{
			return default(bool);
		}

		/// <summary>
		/// Returns an iterator over the elements contained in this collection.
		/// </summary>
		public Iterator iterator()
		{
			return default(Iterator);
		}

		/// <summary>
		/// Removes a single instance of the specified element from this
		/// collection, if it is present (optional operation).
		/// </summary>
		public bool remove(object @o)
		{
			return default(bool);
		}

		/// <summary>
		/// Removes from this collection all of its elements that are contained in
		/// the specified collection (optional operation).
		/// </summary>
		public bool removeAll(Collection @c)
		{
			return default(bool);
		}

		/// <summary>
		/// Retains only the elements in this collection that are contained in the
		/// specified collection (optional operation).
		/// </summary>
		public bool retainAll(Collection @c)
		{
			return default(bool);
		}

		/// <summary>
		/// Returns the number of elements in this collection.
		/// </summary>
		public abstract int size();

		/// <summary>
		/// Returns an array containing all of the elements in this collection.
		/// </summary>
		public System.Array toArray()
		{
			return default(System.Array);
		}

		/// <summary>
		/// Returns an array containing all of the elements in this collection;
		/// the runtime type of the returned array is that of the specified array.
		/// </summary>
		public System.Array toArray(object[] a)
		{
			return default(System.Array);
		}

		/// <summary>
		/// Returns a string representation of this collection.
		/// </summary>
		public override string ToString()
		{
			return default(string);
		}

	}
}
