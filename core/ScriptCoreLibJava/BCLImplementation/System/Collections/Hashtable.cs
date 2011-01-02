using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.Hashtable))]
    internal class __Hashtable : __IDictionary, __ICollection, __IEnumerable
    {
        // http://download.oracle.com/javase/1.4.2/docs/api/java/util/Hashtable.html

        readonly global::java.util.Hashtable InternalElement = new global::java.util.Hashtable();

        public object this[object key]
        {
            get
            {
                return this.InternalElement.get(key);
            }
            set
            {
                this.InternalElement.put(key, value);
            }
        }

        public global::System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
