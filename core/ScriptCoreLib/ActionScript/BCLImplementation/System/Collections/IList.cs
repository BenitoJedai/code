using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(IList))]
    public interface __IList : ICollection, IEnumerable
    {
        // Summary:
        //     Gets a value indicating whether the System.Collections.IList has a fixed
        //     size.
        //
        // Returns:
        //     true if the System.Collections.IList has a fixed size; otherwise, false.
        bool IsFixedSize { get; }
        //
        // Summary:
        //     Gets a value indicating whether the System.Collections.IList is read-only.
        //
        // Returns:
        //     true if the System.Collections.IList is read-only; otherwise, false.
        bool IsReadOnly { get; }

        // Summary:
        //     Gets or sets the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to get or set.
        //
        // Returns:
        //     The element at the specified index.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is not a valid index in the System.Collections.IList.
        //
        //   System.NotSupportedException:
        //     The property is set and the System.Collections.IList is read-only.
        object this[int index] { get; set; }

        // Summary:
        //     Adds an item to the System.Collections.IList.
        //
        // Parameters:
        //   value:
        //     The System.Object to add to the System.Collections.IList.
        //
        // Returns:
        //     The position into which the new element was inserted.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The System.Collections.IList is read-only.-or- The System.Collections.IList
        //     has a fixed size.
        int Add(object value);
        //
        // Summary:
        //     Removes all items from the System.Collections.IList.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The System.Collections.IList is read-only.
        void Clear();
        //
        // Summary:
        //     Determines whether the System.Collections.IList contains a specific value.
        //
        // Parameters:
        //   value:
        //     The System.Object to locate in the System.Collections.IList.
        //
        // Returns:
        //     true if the System.Object is found in the System.Collections.IList; otherwise,
        //     false.
        bool Contains(object value);
        //
        // Summary:
        //     Determines the index of a specific item in the System.Collections.IList.
        //
        // Parameters:
        //   value:
        //     The System.Object to locate in the System.Collections.IList.
        //
        // Returns:
        //     The index of value if found in the list; otherwise, -1.
        int IndexOf(object value);
        //
        // Summary:
        //     Inserts an item to the System.Collections.IList at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which value should be inserted.
        //
        //   value:
        //     The System.Object to insert into the System.Collections.IList.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is not a valid index in the System.Collections.IList.
        //
        //   System.NotSupportedException:
        //     The System.Collections.IList is read-only.-or- The System.Collections.IList
        //     has a fixed size.
        //
        //   System.NullReferenceException:
        //     value is null reference in the System.Collections.IList.
        void Insert(int index, object value);
        //
        // Summary:
        //     Removes the first occurrence of a specific object from the System.Collections.IList.
        //
        // Parameters:
        //   value:
        //     The System.Object to remove from the System.Collections.IList.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     The System.Collections.IList is read-only.-or- The System.Collections.IList
        //     has a fixed size.
        void Remove(object value);
        //
        // Summary:
        //     Removes the System.Collections.IList item at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the item to remove.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is not a valid index in the System.Collections.IList.
        //
        //   System.NotSupportedException:
        //     The System.Collections.IList is read-only.-or- The System.Collections.IList
        //     has a fixed size.
        void RemoveAt(int index);
    }
}
