
using ScriptCoreLib;


namespace java.util
{
    /// <summary>
    /// public interface Map
    /// </summary>
    [Script(IsNative = true)]
    public interface Map
    {

        // Method Summary
        /// <summary>
        /// Removes all mappings from this map (optional operation).
        /// </summary>
        void clear();

        /// <summary>
        /// Returns
        /// </summary>
        bool containsKey(object key);

        /// <summary>
        /// Returns
        /// </summary>
        bool containsValue(object value);

        /// <summary>
        /// Returns a set view of the mappings contained in this map.
        /// </summary>
        Set entrySet();

        /// <summary>
        /// Compares the specified object with this map for equality.
        /// </summary>
        bool equals(object o);

        /// <summary>
        /// Returns the value to which this map maps the specified key.
        /// </summary>
        object get(object key);

        /// <summary>
        /// Returns the hash code value for this map.
        /// </summary>
        int hashCode();

        /// <summary>
        /// Returns
        /// </summary>
        bool isEmpty();

        /// <summary>
        /// Returns a set view of the keys contained in this map.
        /// </summary>
        Set keySet();

        /// <summary>
        /// Associates the specified value with the specified key in this map (optional operation).
        /// </summary>
        object put(object key, object value);

        /// <summary>
        /// Copies all of the mappings from the specified map to this map (optional operation).
        /// </summary>
        void putAll(Map t);

        /// <summary>
        /// Removes the mapping for this key from this map if it is present (optional operation).
        /// </summary>
        object remove(object key);

        /// <summary>
        /// Returns the number of key-value mappings in this map.
        /// </summary>
        int size();

        /// <summary>
        /// Returns a collection view of the values contained in this map.
        /// </summary>
        Collection values();

    }
}

