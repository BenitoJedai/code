using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections
{
	[Script(Implements = typeof(global::System.Collections.IList))]
    internal interface __IList : __ICollection, __IEnumerable
    {
       
        bool IsFixedSize { get; }
     
        bool IsReadOnly { get; }

      
        object this[int index] { get; set; }

        int Add(object value);
    
        void Clear();
       
        bool Contains(object value);
        
        int IndexOf(object value);
      
        void Insert(int index, object value);
      
        void Remove(object value);
      
        void RemoveAt(int index);
    }
}
