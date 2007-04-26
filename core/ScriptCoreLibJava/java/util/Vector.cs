using ScriptCoreLib;

namespace java.util
{

    [Script(IsNative = true)]
    public class Vector
    {


        // Constructor Summary
        /// <summary>
        /// Constructs an empty vector so that its internal data array has size
        /// </summary>
        public Vector()
        {
        }

        /// <summary>
        /// Constructs a vector containing the elements of the specified collection, in the order they are returned by the collection's iterator.
        /// </summary>
        public Vector(Collection c)
        {
        }

        /// <summary>
        /// Constructs an empty vector with the specified initial capacity and with its capacity increment equal to zero.
        /// </summary>
        public Vector(int initialCapacity)
        {
        }

        /// <summary>
        /// Constructs an empty vector with the specified initial capacity and capacity increment.
        /// </summary>
        public Vector(int initialCapacity, int capacityIncrement)
        {
        }

        // Method Summary
        /// <summary>
        /// Inserts the specified element at the specified position in this Vector.
        /// </summary>
        public   void  add (int index, object  element)
        {
            return ;
        }

        /// <summary>
        /// Appends the specified element to the end of this Vector.
        /// </summary>
        public   bool   add (object  o)
        {
            return default( bool  );
        }

        /// <summary>
        /// Appends all of the elements in the specified Collection to the end of this Vector, in the order that they are returned by the specified Collection's Iterator.
        /// </summary>
        public   bool   addAll (Collection c)
        {
            return default( bool  );
        }

        /// <summary>
        /// Inserts all of the elements in in the specified Collection into this Vector at the specified position.
        /// </summary>
        public   bool   addAll (int index, Collection c)
        {
            return default( bool  );
        }

        /// <summary>
        /// Adds the specified component to the end of this vector, increasing its size by one.
        /// </summary>
        public   void  addElement (object  obj)
        {
            return ;
        }

        /// <summary>
        /// Returns the current capacity of this vector.
        /// </summary>
        public   int  capacity ()
        {
            return default( int );
        }

        /// <summary>
        /// Removes all of the elements from this Vector.
        /// </summary>
        public   void  clear ()
        {
            return ;
        }

        /// <summary>
        /// Returns a clone of this vector.
        /// </summary>
        public   object   clone ()
        {
            return default( object  );
        }

        /// <summary>
        /// Tests if the specified object is a component in this vector.
        /// </summary>
        public   bool   contains (object  elem)
        {
            return default( bool  );
        }

        /// <summary>
        /// Returns true if this Vector contains all of the elements in the specified Collection.
        /// </summary>
        public   bool   containsAll (Collection c)
        {
            return default( bool  );
        }

        /// <summary>
        /// Copies the components of this vector into the specified array.
        /// </summary>
        public   void  copyInto (object [] anArray)
        {
            return;
        }

        /// <summary>
        /// Returns the component at the specified index.
        /// </summary>
        public   object   elementAt (int index)
        {
            return default( object  );
        }

        /// <summary>
        /// Returns an enumeration of the components of this vector.
        /// </summary>
        public   Enumeration  elements ()
        {
            return default( Enumeration );
        }

        /// <summary>
        /// Increases the capacity of this vector, if necessary, to ensure that it can hold at least the number of components specified by the minimum capacity argument.
        /// </summary>
        public   void  ensureCapacity (int minCapacity)
        {
            return;
        }

        /// <summary>
        /// Compares the specified Object with this Vector for equality.
        /// </summary>
        public   bool   equals (object  o)
        {
            return default( bool  );
        }

        /// <summary>
        /// Returns the first component (the item at index
        /// </summary>
        public   object   firstElement ()
        {
            return default( object  );
        }

        /// <summary>
        /// Returns the element at the specified position in this Vector.
        /// </summary>
        public   object   get (int index)
        {
            return default( object  );
        }

        /// <summary>
        /// Returns the hash code value for this Vector.
        /// </summary>
        public   int  hashCode ()
        {
            return default( int );
        }

        /// <summary>
        /// Searches for the first occurence of the given argument, testing for equality using the
        /// </summary>
        public   int  indexOf (object  elem)
        {
            return default( int );
        }

        /// <summary>
        /// Searches for the first occurence of the given argument, beginning the search at
        /// </summary>
        public   int  indexOf (object  elem, int index)
        {
            return default( int );
        }

        /// <summary>
        /// Inserts the specified object as a component in this vector at the specified
        /// </summary>
        public   void  insertElementAt (object  obj, int index)
        {
            return;
        }

        /// <summary>
        /// Tests if this vector has no components.
        /// </summary>
        public   bool   isEmpty ()
        {
            return default( bool  );
        }

        /// <summary>
        /// Returns the last component of the vector.
        /// </summary>
        public   object   lastElement ()
        {
            return default( object  );
        }

        /// <summary>
        /// Returns the index of the last occurrence of the specified object in this vector.
        /// </summary>
        public   int  lastIndexOf (object  elem)
        {
            return default( int );
        }

        /// <summary>
        /// Searches backwards for the specified object, starting from the specified index, and returns an index to it.
        /// </summary>
        public   int  lastIndexOf (object  elem, int index)
        {
            return default( int );
        }

        /// <summary>
        /// Removes the element at the specified position in this Vector.
        /// </summary>
        public   object   remove (int index)
        {
            return default( object  );
        }

        /// <summary>
        /// Removes the first occurrence of the specified element in this Vector If the Vector does not contain the element, it is unchanged.
        /// </summary>
        public   bool   remove (object  o)
        {
            return default( bool  );
        }

        /// <summary>
        /// Removes from this Vector all of its elements that are contained in the specified Collection.
        /// </summary>
        public   bool   removeAll (Collection c)
        {
            return default( bool  );
        }

        /// <summary>
        /// Removes all components from this vector and sets its size to zero.
        /// </summary>
        public   void  removeAllElements ()
        {
            return;
        }

        /// <summary>
        /// Removes the first (lowest-indexed) occurrence of the argument from this vector.
        /// </summary>
        public   bool   removeElement (object  obj)
        {
            return default( bool  );
        }

        /// <summary>
        /// Deletes the component at the specified index.
        /// </summary>
        public   void  removeElementAt (int index)
        {
            return;
        }

        /// <summary>
        /// Removes from this List all of the elements whose index is between fromIndex, inclusive and toIndex, exclusive.
        /// </summary>
         protected  void  removeRange (int fromIndex, int toIndex)
        {
            return;
        }

        /// <summary>
        /// Retains only the elements in this Vector that are contained in the specified Collection.
        /// </summary>
        public   bool   retainAll (Collection c)
        {
            return default( bool  );
        }

        /// <summary>
        /// Replaces the element at the specified position in this Vector with the specified element.
        /// </summary>
        public   object   set (int index, object  element)
        {
            return default( object  );
        }

        /// <summary>
        /// Sets the component at the specified
        /// </summary>
        public   void  setElementAt (object  obj, int index)
        {
            return ;
        }

        /// <summary>
        /// Sets the size of this vector.
        /// </summary>
        public   void  setSize (int newSize)
        {
            return ;
        }

        /// <summary>
        /// Returns the number of components in this vector.
        /// </summary>
        public   int  size ()
        {
            return default( int );
        }

        /// <summary>
        /// Returns a view of the portion of this List between fromIndex, inclusive, and toIndex, exclusive.
        /// </summary>
        public   List  subList (int fromIndex, int toIndex)
        {
            return default( List );
        }

        /// <summary>
        /// Returns an array containing all of the elements in this Vector in the correct order.
        /// </summary>
        public   object []  toArray ()
        {
            return default( object [] );
        }

        /// <summary>
        /// Returns an array containing all of the elements in this Vector in the correct order; the runtime type of the returned array is that of the specified array.
        /// </summary>
        public   object []  toArray (object [] a)
        {
            return default( object [] );
        }

        /// <summary>
        /// Returns a string representation of this Vector, containing the String representation of each element.
        /// </summary>
        public   string   toString ()
        {
            return default( string  );
        }

        /// <summary>
        /// Trims the capacity of this vector to be the vector's current size.
        /// </summary>
        public   void  trimToSize ()
        {
            return  ;
        }


    }
}
