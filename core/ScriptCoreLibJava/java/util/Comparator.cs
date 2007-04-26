
using ScriptCoreLib;

namespace java.util
{
    /// <summary>
    /// A comparison function, which imposes a total ordering on some collection of objects. Comparators can be passed to a sort method (such as Collections.sort) to allow precise control over the sort order. Comparators can also be used to control the order of certain data structures (such as TreeSet or TreeMap).
    /// </summary>
    [Script(IsNative = true)]
    public interface Comparator
    {
        // Method Summary
        /// <summary>
        /// Compares its two arguments for order.
        /// </summary>
        int compare(object o1, object o2);

        /// <summary>
        /// Indicates whether some other object is "equal to" this Comparator.
        /// </summary>
        bool equals(object obj);

    }

}
