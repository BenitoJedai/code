using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Query;
using ScriptCoreLib.Shared.Query;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    [Script(Implements = typeof(Stack<>))]
    internal class __Stack<T> : IEnumerable<T>
    {
        readonly IArray<T> items = new IArray<T>();

        public __Stack()
            : this(null)
        {

        }

        public __Stack(IEnumerable<T> collection)
        {
            // cannot have this check as the default ctor will pass null anyway
            //if (collection == null)
            //    throw new global::System.Exception("collection is null");

            if (collection != null)
                this.AddRange(collection);
        }

        internal void AddRange(IEnumerable<T> collection)
        {
            foreach (var item in InternalSequenceImplementation.AsEnumerable(collection))
            {
                this.Push(item);
            }
        }

        public T Peek()
        {
            return (T)items[items.length - 1];
        }

        public T Pop()
        {
            return (T)items.pop();
        }

        public void Push(T item)
        {
            items.push(item);
        }

        public int Count { get { return (int)items.length; } }


        public void Clear()
        {
            items.splice(0, items.length);
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            var a = (T[])(object)items;

            return new SZArrayEnumerator<T>(a);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
