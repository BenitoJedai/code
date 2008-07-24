using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(IEqualityComparer<>))]
    internal interface __IEqualityComparer<T>
    {
        // Summary:
        //     Determines whether the specified objects are equal.
        //
        // Parameters:
        //   x:
        //     The first object of type T to compare.
        //
        //   y:
        //     The second object of type T to compare.
        //
        // Returns:
        //     true if the specified objects are equal; otherwise, false.
        bool Equals(T x, T y);
        //
        // Summary:
        //     Returns a hash code for the specified object.
        //
        // Parameters:
        //   obj:
        //     The System.Object for which a hash code is to be returned.
        //
        // Returns:
        //     A hash code for the specified object.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The type of obj is a reference type and obj is null.
        int GetHashCode(T obj);
    }
}
