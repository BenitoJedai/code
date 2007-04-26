
using ScriptCoreLib;



namespace java.util
{
    /// <summary>
    /// public interface SortedMap
    /// </summary>
    [Script(IsNative = true)]
    public interface SortedMap : Map
    {

        // Method Summary
        /// <summary>
        /// Returns the comparator associated with this sorted map, or
        /// </summary>
        Comparator comparator();

        /// <summary>
        /// Returns the first (lowest) key currently in this sorted map.
        /// </summary>
        object firstKey();

        /// <summary>
        /// Returns a view of the portion of this sorted map whose keys are strictly less than toKey.
        /// </summary>
        SortedMap headMap(object toKey);

        /// <summary>
        /// Returns the last (highest) key currently in this sorted map.
        /// </summary>
        object lastKey();

        /// <summary>
        /// Returns a view of the portion of this sorted map whose keys range from
        /// </summary>
        SortedMap subMap(object fromKey, object toKey);

        /// <summary>
        /// Returns a view of the portion of this sorted map whose keys are greater than or equal to
        /// </summary>
        SortedMap tailMap(object fromKey);


    }
}

