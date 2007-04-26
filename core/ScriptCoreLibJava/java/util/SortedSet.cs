using ScriptCoreLib;

namespace java.util
{

        /// <summary>
        /// public interface SortedSet
        /// </summary>
        [Script(IsNative = true)]
        public interface SortedSet : Set
        {
            // Method Summary
            /// <summary>
            /// Returns the comparator associated with this sorted set, or
            /// </summary>
            Comparator comparator();

            /// <summary>
            /// Returns the first (lowest) element currently in this sorted set.
            /// </summary>
            object first();

            /// <summary>
            /// Returns a view of the portion of this sorted set whose elements are strictly less than
            /// </summary>
            SortedSet headSet(object toElement);

            /// <summary>
            /// Returns the last (highest) element currently in this sorted set.
            /// </summary>
            object last();

            /// <summary>
            /// Returns a view of the portion of this sorted set whose elements range from
            /// </summary>
            SortedSet subSet(object fromElement, object toElement);

            /// <summary>
            /// Returns a view of the portion of this sorted set whose elements are greater than or equal to
            /// </summary>
            SortedSet tailSet(object fromElement);


        }


}
