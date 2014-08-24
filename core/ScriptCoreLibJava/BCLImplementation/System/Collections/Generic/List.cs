using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System.Linq;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections.Generic;
using ScriptCoreLib.Shared.BCLImplementation.System.Collections;
using ScriptCoreLib.Shared.BCLImplementation.System;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections.Generic
{
    // http://referencesource.microsoft.com/#mscorlib/system/collections/generic/list.cs
    // X:\jsc.svn\core\ScriptCoreLibJava\BCLImplementation\System\Collections\Generic\List.cs
    // X:\jsc.svn\core\ScriptCoreLib\JavaScript\BCLImplementation\System\Collections\Generic\List.cs

    [Script(Implements = typeof(global::System.Collections.Generic.List<>))]
    internal class __List<T> :
        __IList<T>,
        __IEnumerable
    {
        public IEnumerable<T> InternalCollectionForTypeOfT;


        readonly global::java.util.ArrayList<T> InternalList = new global::java.util.ArrayList<T>();

        public __List()
        {

        }

        public __List(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");

            // can we get the type even if there are no elements?
            // X:\jsc.svn\core\ScriptCoreLib\Shared\BCLImplementation\System\Linq\Enumerable\Enumerable.ToArray.cs

            InternalCollectionForTypeOfT = collection;

            this.AddRange(collection);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach (T v in collection)
            {
                this.Add(v);
            }
        }

        public int IndexOf(T item)
        {
            // https://sites.google.com/a/jsc-solutions.net/work/knowledge-base/04-monese/2014/201402/20140205
            // haha. lets add this. need it for data fill.

            return this.InternalList.indexOf(item);
        }

        public void Insert(int index, T item)
        {
            this.InternalList.add(index, item);
        }

        public void RemoveAt(int index)
        {
            this.InternalList.remove(index);
        }

        public T this[int index]
        {
            get
            {
                return (T)this.InternalList.get(index);
            }
            set
            {
                this.InternalList.set(index, value);
            }
        }

        public void Add(T item)
        {
            this.InternalList.add(item);
        }

        public void Clear()
        {
            this.InternalList.clear();
        }

        public bool Contains(T item)
        {
            return this.InternalList.contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                return InternalList.size();
            }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            return this.InternalList.remove(item);
        }

        public List<T>.Enumerator GetEnumerator()
        //public __Enumerator GetEnumerator()
        {
            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20121101/20121127

            return (List<T>.Enumerator)(object)new __Enumerator(this);
        }

        IEnumerator __IEnumerable.GetEnumerator()
        {
            return new __Enumerator(this);
        }

        IEnumerator<T> __IEnumerable<T>.GetEnumerator()
        {
            return new __Enumerator(this);
        }


        public void Reverse()
        {
            var clone = this.ToArray();

            for (int i = 0; i < clone.Length; i++)
            {
                this[clone.Length - 1 - i] = clone[i];
            }


        }


        public Type InternalGetElementType()
        {
            //Console.WriteLine("enter InternalGetElementType");

            // used by
            // X:\jsc.svn\core\ScriptCoreLibAndroid\ScriptCoreLibAndroid\BCLImplementation\System\Data\SQLite\SQLiteCommand.cs

            // enter ToArray { Count = 0, InternalCollectionForTypeOfT = ScriptCoreLib.Shared.BCLImplementation.System.__SZArrayEnumerator_1@1e97f9f }

            //InternalCollectionForTypeOfT 


            // http://javanotepad.blogspot.com/2007/09/instanceof-doesnt-work-with-generics.html


            //var xSZArrayEnumerator = InternalCollectionForTypeOfT as __SZArrayEnumerator<T>;
            #region xSZArrayEnumerator
            var xSZArrayEnumerator = InternalCollectionForTypeOfT as __SZArrayEnumerator;
            if (xSZArrayEnumerator != null)
            {
                var xSZArrayEnumeratorArray = xSZArrayEnumerator.GetArray();

                // { xSZArrayEnumeratorArray = [LScriptCoreLib.Shared.BCLImplementation.System.__Tuple_2;@288051 }

                var ElementType = xSZArrayEnumeratorArray.GetType().GetElementType();
                //{ ElementType = ScriptCoreLib.Shared.BCLImplementation.System.__Tuple_2 }
                //Console.WriteLine(new { ElementType });
                return ElementType;
            }
            #endregion


            // enter ToArray { Count = 1, ElementType = java.lang.Object }
            // do we have to guess?

            // what if we really want to have a object[]; ?
            // when will sc also start sending in typeofT info?

            var valueType0 = default(Type);

            for (int i = 0; i < Count; i++)
            {
                var value = this[i];

                // what about primitive types?
                if (value != null)
                {
                    var valueType = value.GetType();

                    if (valueType0 == null)
                        valueType0 = valueType;
                    else
                    {
                        if (valueType != valueType0)
                        {
                            // guess we want objects?
                            valueType0 = typeof(object);
                        }
                    }
                }
            }

            if (valueType0 != null)
                return valueType0;

            return typeof(object);
        }

        public T[] ToArray()
        {
            // X:\jsc.svn\examples\java\hybrid\Test\TestJVMCLRGenericConcat\TestJVMCLRGenericConcat\Program.cs

            // enter ToArray { Count = 2, InternalCollectionForTypeOfT = ScriptCoreLib.Shared.BCLImplementation.System.Linq.__Enumerable__ConcatIterator_d__5b_1@d1e89e }

            var ElementType = InternalGetElementType();

            //Console.WriteLine(
            //    "enter ToArray " + new { this.Count, ElementType }
            //    );

            var a = (T[])Array.CreateInstance(ElementType, Count);


            // whats the type? jvm wont know. will it>
            //var e = typeof(T);

            // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201408/20140816/jvm

            // http://stackoverflow.com/questions/5061640/make-arraylist-toarray-return-more-specific-types
            //return this.InternalList.toArray(new T[0]);
            return this.InternalList.toArray(a);
        }




        [Script(Implements = typeof(global::System.Collections.Generic.List<>.Enumerator))]
        public class __Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            IEnumerator<T> value;

            internal __Enumerator()
                : this(null)
            {

            }

            internal __Enumerator(__List<T> list)
            {
                if (list == null)
                    return;

                value = __Enumerable_AsEnumerable.AsEnumerable(list.ToArray()).GetEnumerator();


            }


            #region IEnumerator<T> Members

            public T Current
            {
                get { return value.Current; }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                value.Dispose();
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return value.Current; }
            }

            public bool MoveNext()
            {
                return value.MoveNext();
            }

            public void Reset()
            {
                value.Reset();
            }

            #endregion
        }

        int __IList<T>.IndexOf(T item)
        {
            throw new NotImplementedException();
        }

        void __IList<T>.Insert(int index, T item)
        {
            this.Insert(0, item);
        }

        void __IList<T>.RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        T __IList<T>.this[int index]
        {
            get
            {
                // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2014/201401/20140111-iquery
                return this[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        void __ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        void __ICollection<T>.Clear()
        {
            throw new NotImplementedException();
        }

        bool __ICollection<T>.Contains(T item)
        {
            throw new NotImplementedException();
        }

        void __ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        int __ICollection<T>.Count
        {
            get { return this.Count; }
        }

        bool __ICollection<T>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        bool __ICollection<T>.Remove(T item)
        {
            return this.Remove(item);
        }


    }
}


