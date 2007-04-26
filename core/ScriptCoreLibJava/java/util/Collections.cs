
using ScriptCoreLib;

namespace java.util
{
    /// <summary>
    /// This class consists exclusively of static methods that operate on or return collections. It contains polymorphic algorithms that operate on collections, "wrappers", which return a new collection backed by a specified collection, and a few other odds and ends. 
    /// </summary>
    [Script(IsNative = true)]
    public class Collections
    {
        // Field Summary
        static List EMPTY_LIST;
        static Map EMPTY_MAP;
        static Set EMPTY_SET;

        // Method Summary
        /// <summary>
        /// Searches the specified list for the specified object using the binary search algorithm.
        /// </summary>
        public static int binarySearch(List list, object key)
        {
            return default(int);
        }

        /// <summary>
        /// Searches the specified list for the specified object using the binary search algorithm.
        /// </summary>
        public static int binarySearch(List list, object key, Comparator c)
        {
            return default(int);
        }

        /// <summary>
        /// Copies all of the elements from one list into another.
        /// </summary>
        public static void copy(List dest, List src)
        {
            return;
        }

        /// <summary>
        /// Returns an enumeration over the specified collection.
        /// </summary>
        public static Enumeration enumeration(Collection c)
        {
            return default(Enumeration);
        }

        /// <summary>
        /// Replaces all of the elements of the specified list with the specified element.
        /// </summary>
        public static void fill(List list, object obj)
        {
            return;
        }

        /// <summary>
        /// Returns the starting position of the first occurrence of the specified target list within the specified source list, or -1 if there is no such occurrence.
        /// </summary>
        public static int indexOfSubList(List source, List target)
        {
            return default(int);
        }

        /// <summary>
        /// Returns the starting position of the last occurrence of the specified target list within the specified source list, or -1 if there is no such occurrence.
        /// </summary>
        public static int lastIndexOfSubList(List source, List target)
        {
            return default(int);
        }

        /// <summary>
        /// Returns an array list containing the elements returned by the specified enumeration in the order they are returned by the enumeration.
        /// </summary>
        public static ArrayList list(Enumeration e)
        {
            return default(ArrayList);
        }

        /// <summary>
        /// Returns the maximum element of the given collection, according to the
        /// </summary>
        public static object max(Collection coll)
        {
            return default(object);
        }

        /// <summary>
        /// Returns the maximum element of the given collection, according to the order induced by the specified comparator.
        /// </summary>
        public static object max(Collection coll, Comparator comp)
        {
            return default(object);
        }

        /// <summary>
        /// Returns the minimum element of the given collection, according to the
        /// </summary>
        public static object min(Collection coll)
        {
            return default(object);
        }

        /// <summary>
        /// Returns the minimum element of the given collection, according to the order induced by the specified comparator.
        /// </summary>
        public static object min(Collection coll, Comparator comp)
        {
            return default(object);
        }

        /// <summary>
        /// Returns an immutable list consisting of
        /// </summary>
        public static List nCopies(int n, object o)
        {
            return default(List);
        }

        /// <summary>
        /// Replaces all occurrences of one specified value in a list with another.
        /// </summary>
        public static bool replaceAll(List list, object oldVal, object newVal)
        {
            return default(bool);
        }

        /// <summary>
        /// Reverses the order of the elements in the specified list.
        /// </summary>
        public static void reverse(List list)
        {
            return;
        }

        /// <summary>
        /// Returns a comparator that imposes the reverse of the
        /// </summary>
        public static Comparator reverseOrder()
        {
            return default(Comparator);
        }

        /// <summary>
        /// Rotates the elements in the specified list by the specified distance.
        /// </summary>
        public static void rotate(List list, int distance)
        {
            return;
        }

        /// <summary>
        /// Randomly permutes the specified list using a default source of randomness.
        /// </summary>
        public static void shuffle(List list)
        {
            return;
        }

        /// <summary>
        /// Randomly permute the specified list using the specified source of randomness.
        /// </summary>
        public static void shuffle(List list, Random rnd)
        {
            return;
        }

        /// <summary>
        /// Returns an immutable set containing only the specified object.
        /// </summary>
        public static Set singleton(object o)
        {
            return default(Set);
        }

        /// <summary>
        /// Returns an immutable list containing only the specified object.
        /// </summary>
        public static List singletonList(object o)
        {
            return default(List);
        }

        /// <summary>
        /// Returns an immutable map, mapping only the specified key to the specified value.
        /// </summary>
        public static Map singletonMap(object key, object value)
        {
            return default(Map);
        }

        /// <summary>
        /// Sorts the specified list into ascending order, according to the
        /// </summary>
        public static void sort(List list)
        {
            return;
        }

        /// <summary>
        /// Sorts the specified list according to the order induced by the specified comparator.
        /// </summary>
        public static void sort(List list, Comparator c)
        {
            return;
        }

        /// <summary>
        /// Swaps the elements at the specified positions in the specified list.
        /// </summary>
        public static void swap(List list, int i, int j)
        {
            return;
        }

        /// <summary>
        /// Returns a synchronized (thread-safe) collection backed by the specified collection.
        /// </summary>
        public static Collection synchronizedCollection(Collection c)
        {
            return default(Collection);
        }

        /// <summary>
        /// Returns a synchronized (thread-safe) list backed by the specified list.
        /// </summary>
        public static List synchronizedList(List list)
        {
            return default(List);
        }

        /// <summary>
        /// Returns a synchronized (thread-safe) map backed by the specified map.
        /// </summary>
        public static Map synchronizedMap(Map m)
        {
            return default(Map);
        }

        /// <summary>
        /// Returns a synchronized (thread-safe) set backed by the specified set.
        /// </summary>
        public static Set synchronizedSet(Set s)
        {
            return default(Set);
        }

        /// <summary>
        /// Returns a synchronized (thread-safe) sorted map backed by the specified sorted map.
        /// </summary>
        public static SortedMap synchronizedSortedMap(SortedMap m)
        {
            return default(SortedMap);
        }

        /// <summary>
        /// Returns a synchronized (thread-safe) sorted set backed by the specified sorted set.
        /// </summary>
        public static SortedSet synchronizedSortedSet(SortedSet s)
        {
            return default(SortedSet);
        }

        /// <summary>
        /// Returns an unmodifiable view of the specified collection.
        /// </summary>
        public static Collection unmodifiableCollection(Collection c)
        {
            return default(Collection);
        }

        /// <summary>
        /// Returns an unmodifiable view of the specified list.
        /// </summary>
        public static List unmodifiableList(List list)
        {
            return default(List);
        }

        /// <summary>
        /// Returns an unmodifiable view of the specified map.
        /// </summary>
        public static Map unmodifiableMap(Map m)
        {
            return default(Map);
        }

        /// <summary>
        /// Returns an unmodifiable view of the specified set.
        /// </summary>
        public static Set unmodifiableSet(Set s)
        {
            return default(Set);
        }

        /// <summary>
        /// Returns an unmodifiable view of the specified sorted map.
        /// </summary>
        public static SortedMap unmodifiableSortedMap(SortedMap m)
        {
            return default(SortedMap);
        }

        /// <summary>
        /// Returns an unmodifiable view of the specified sorted set.
        /// </summary>
        public static SortedSet unmodifiableSortedSet(SortedSet s)
        {
            return default(SortedSet);
        }


    }
}

