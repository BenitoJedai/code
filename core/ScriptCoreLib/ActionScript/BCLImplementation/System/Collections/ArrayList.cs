using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections
{
	[Script(Implements = typeof(global::System.Collections.ArrayList))]
	internal class __ArrayList : __IList, __ICollection, __IEnumerable, __ICloneable
	{
		// nongeneric inteface implemented by generic one, funny huh?

		readonly List<object> InternalList = new List<object>();

		public virtual int Add(object value)
		{
			var i = InternalList.Count;
			InternalList.Add(value);
			return i;
		}

		public void Clear()
		{
			this.InternalList.Clear();
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
		public virtual int Count
		{
			get
			{
				return InternalList.Count;
			}
		}

		public virtual void RemoveAt(int index)
		{
			this.InternalList.RemoveAt(index);
		}

		public virtual object[] ToArray()
		{
			return InternalList.ToArray();
		}

		public virtual global::System.Array ToArray(Type type)
		{
			// we are really ignorant about types at this time
			// we should not be if we were to use vector<> instead
			return (global::System.Array)(object)ToArray();
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

		#region __IList Members

		public bool IsFixedSize
		{
			get { throw new NotImplementedException(); }
		}

		public bool IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		public bool Contains(object value)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(object value)
		{
			return this.InternalList.IndexOf(value);
		}

		public void Insert(int index, object value)
		{
			throw new NotImplementedException();
		}

		public void Remove(object value)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
