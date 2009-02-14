using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.ICollection<>))]
	internal interface __ICollection<T> : __IEnumerable<T>, __IEnumerable
    {
        int Count { get; }
        
        bool IsReadOnly { get; }

        void Add(T item);
       
        void Clear();
     
        bool Contains(T item);
      
        void CopyTo(T[] array, int arrayIndex);
     
        bool Remove(T item);
    }
}
