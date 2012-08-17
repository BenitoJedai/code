using ScriptCoreLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: Script, ScriptTypeFilter(ScriptType.Java)]

namespace TestGenericInheritance
{
    [Script]
    public interface __IList<T>
    {
        void IList_method(T e);

    }


    [Script(Implements = typeof(global::System.Collections.IEnumerable))]
    public interface __IEnumerable
    {

    }

    [Script(Implements = typeof(global::System.Collections.IEnumerator))]
    public interface __IEnumerator
    {

    }

    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerator<>))]
    public interface __IEnumerator<T>
    {

    }


    [Script(Implements = typeof(global::System.Collections.Generic.IEnumerable<>))]
    public interface __IEnumerable<T>
    {

    }

    [Script(Implements = typeof(global::System.Collections.Generic.ICollection<>))]
    public interface __ICollection<T>
    {
        void ICollection_method(T e);

    }

    [Script]
    public class __List<T> : __IList<T>, global::System.Collections.Generic.ICollection<T>
    {
        T[] e = new T[0];
 
        // https://sites.google.com/a/jsc-solutions.net/backlog/knowledge-base/2012/20120-1/20120817-uri
        public void IList_method(T e)
        {
        }

        public void ICollection_method(T e)
        {
        }

        public void Add(T item)
        {
            throw null;
        }

        public void Clear()
        {
            throw null;
        }

        public bool Contains(T item)
        {
            throw null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw null;
        }

        public int Count
        {
            get { throw null; }
        }

        public bool IsReadOnly
        {
            get { throw null; }
        }

        public bool Remove(T item)
        {
            throw null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw null;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw null;
        }
    }
}
