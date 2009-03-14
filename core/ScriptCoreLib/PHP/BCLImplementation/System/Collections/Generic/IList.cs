using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.IList<>))]
    internal interface __IList<T> : __ICollection<T>, __IEnumerable<T>, __IEnumerable
    {
        T this[int index] { get; set; }

        int IndexOf(T item);
       
        void Insert(int index, T item);
    
        void RemoveAt(int index);
    }
}
