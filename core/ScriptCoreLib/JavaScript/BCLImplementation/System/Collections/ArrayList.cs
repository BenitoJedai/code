using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Collections;
namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections
{
    // http://referencesource.microsoft.com/#mscorlib/system/collections/arraylist.cs
    // https://github.com/dotnet/corefx/blob/master/src/System.Collections.NonGeneric/src/System/Collections/ArrayList.cs

	[Script(Implements = typeof(global::System.Collections.ArrayList))]
	internal class __ArrayList
	{
		readonly IArray<object> InternalList = new IArray<object>();

		public virtual int Add(object e)
		{
			InternalList.push(e);

			return InternalList.length - 1;
		}

		public int IndexOf(object e)
		{
			return InternalList.indexOf(e);
		}

        public virtual void RemoveAt(int index)
        {
            this.InternalList.splice(index, 1);
        }

		public virtual int Count
		{
			get
			{
				return InternalList.length;
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
			return (object[])(object)this.InternalList.slice(0);
		}

        public virtual global::System.Array ToArray(Type type)
        {
            // we are really ignorant about types at this time
            return (global::System.Array)(object)ToArray();
        }
	}
}
