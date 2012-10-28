using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(global::System.Collections.Generic.Queue<>))]
    internal class __Queue<T>
    {
        readonly global::java.util.ArrayList<T> InternalList = new global::java.util.ArrayList<T>();

        public __Queue()
        {
        }

        public __Queue(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                Enqueue(item);
            }
        }

        public void Enqueue(T item)
        {
            this.InternalList.add(item);
        }

        public T Dequeue()
        {
            var i = this.InternalList.size() - 1;
            var v = (T)this.InternalList.get(i);

            this.InternalList.remove(i);

            return v;
        }

        public T[] ToArray()
        {
            return this.InternalList.toArray(new T[0]);
        }
    }
}


