using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ScriptCoreLib.JavaScript.DOM;
using ScriptCoreLib.JavaScript.Query;
using ScriptCoreLib.Shared.BCLImplementation.System;
using ScriptCoreLib.Shared.BCLImplementation.System.Linq;

namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections.Generic
{
    // http://sourceforge.net/p/jsc/code/HEAD/tree/core/ScriptCoreLib/JavaScript/BCLImplementation/System/Collections/Generic/Stack.cs

    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Generic\Stack.cs
    // X:\jsc.svn\core\ScriptCoreLib\ActionScript\BCLImplementation\System\Collections\Generic\Stack.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Concurrent\ConcurrentStack.cs

    // http://referencesource.microsoft.com/#mscorlib/system/collections/stack.cs
    // https://github.com/Microsoft/referencesource/blob/master/System/compmod/system/collections/generic/stack.cs

    // https://github.com/Reactive-Extensions/IL2JS/blob/master/mscorlib/System/Collections/Generic/Stack.cs
    // https://github.com/erik-kallen/SaltarelleCompiler/blob/develop/Runtime/CoreLib/Collections/Generic/Stack.cs
    // https://github.com/kswoll/WootzJs/blob/master/WootzJs.Runtime/Collections/Generic/Stack.cs


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
            foreach (var item in __Enumerable_AsEnumerable.AsEnumerable(collection))
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

            return (IEnumerator<T>)(object)new __SZArrayEnumerator<T>(a);
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
