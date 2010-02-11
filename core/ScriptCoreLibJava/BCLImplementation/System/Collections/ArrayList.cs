using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using System.Collections;

namespace ScriptCoreLibJava.BCLImplementation.System.Collections
{
	[Script(Implements = typeof(global::System.Collections.ArrayList))]
	internal class __ArrayList : __IList, __ICollection, __IEnumerable, __ICloneable
	{
		readonly java.util.ArrayList InternalList = new java.util.ArrayList();

		public virtual int Add(object value)
		{
			var i = InternalList.size();

			InternalList.add(value);

			return i;
		}

		public virtual int IndexOf(object value)
		{
			return this.InternalList.indexOf(value);
		}

		public virtual object this[int index]
		{
			get
			{
				return InternalList.get(index);
			}
			set
			{
				InternalList.set(index, value);
			}
		}
		public virtual int Count
		{
			get
			{
				return InternalList.size();
			}
		}


		public virtual void Remove(object o)
		{
			this.InternalList.remove(o);
		}

		public virtual void RemoveAt(int index)
		{
			this.InternalList.remove(index);
		}

		public virtual object[] ToArray()
		{
			return (object[])InternalList.toArray();
		}

		public virtual Array ToArray(Type type)
		{
			return (Array)InternalList.toArray((object[])Array.CreateInstance(type, Count));
		}

		#region __IEnumerable Members

		public global::System.Collections.IEnumerator GetEnumerator()
		{
			return new __Enumerator { Target = this };
		}

		#endregion

		[Script]
		class __Enumerator : IEnumerator
		{
			public __ArrayList Target;

			object InternalCurrent;
			int InternalIndex = -1;

			#region __IEnumerator Members

			public object Current
			{
				get { return InternalCurrent; }
			}

			public bool MoveNext()
			{
				InternalIndex++;

				if (InternalIndex < Target.Count)
				{
					InternalCurrent = Target[InternalIndex];
					return true;
				}

				InternalCurrent = null;
				return false;
			}

			public void Reset()
			{
				throw new NotImplementedException();
			}

			#endregion
		}
	}
}
