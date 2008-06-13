using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(ICollection))]
    internal interface __ICollection: IEnumerable
    {
        // Summary:
        //     Gets the number of elements contained in the System.Collections.ICollection.
        //
        // Returns:
        //     The number of elements contained in the System.Collections.ICollection.
        int Count { get; }
        //
        // Summary:
        //     Gets a value indicating whether access to the System.Collections.ICollection
        //     is synchronized (thread safe).
        //
        // Returns:
        //     true if access to the System.Collections.ICollection is synchronized (thread
        //     safe); otherwise, false.
        bool IsSynchronized { get; }
        //
        // Summary:
        //     Gets an object that can be used to synchronize access to the System.Collections.ICollection.
        //
        // Returns:
        //     An object that can be used to synchronize access to the System.Collections.ICollection.
        object SyncRoot { get; }

        // Summary:
        //     Copies the elements of the System.Collections.ICollection to an System.Array,
        //     starting at a particular System.Array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements
        //     copied from System.Collections.ICollection. The System.Array must have zero-based
        //     indexing.
        //
        //   index:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     array is null.
        //
        //   System.ArgumentOutOfRangeException:
        //     index is less than zero.
        //
        //   System.ArgumentException:
        //     array is multidimensional.-or- index is equal to or greater than the length
        //     of array.-or- The number of elements in the source System.Collections.ICollection
        //     is greater than the available space from index to the end of the destination
        //     array.
        //
        //   System.ArgumentException:
        //     The type of the source System.Collections.ICollection cannot be cast automatically
        //     to the type of the destination array.
        void CopyTo(global::System.Array array, int index);
    }
}
