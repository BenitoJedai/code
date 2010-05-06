using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.JavaScript.DOM;
using System.Collections;
namespace ScriptCoreLib.JavaScript.BCLImplementation.System.Collections
{


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

	}
}
