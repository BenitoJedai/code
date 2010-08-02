using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCoreLib.PHP.BCLImplementation.System.Collections
{
    [Script(Implements = typeof(global::System.Collections.ArrayList))]
    internal class __ArrayList
    {
        readonly List<object> InternalList = new List<object>();

        public __ArrayList()
        {

        }

        public virtual int Add(object e)
		{
			InternalList.Add(e);

			return InternalList.Count - 1;
		}

		public int IndexOf(object e)
		{
			return InternalList.IndexOf(e);
		}

		public virtual int Count
		{
			get
			{
				return InternalList.Count;
			}
		}

		public virtual object this[int index]
		{
			get
			{
				return InternalList[index];
			}
			set
			{
				InternalList[index] = value;
			}
		}

		// Do we support IDisposable ?
		// we should add extra metadata for is interface

		//public global::System.Collections.IEnumerator GetEnumerator()
		//{
		//}


		public object[] ToArray()
		{
			return this.InternalList.ToArray();
        }

        public virtual Array ToArray(Type type)
        {
            return ToArray();
        }
    }
}
