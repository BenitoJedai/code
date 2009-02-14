using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.Generic;
using System.Collections;

namespace ScriptCoreLib.ActionScript.BCLImplementation.System.Collections.ObjectModel
{
	[Script(Implements = typeof(global::System.Collections.ObjectModel.Collection<>))]
	internal class __Collection<T> : __IList<T>, __ICollection<T>, __IEnumerable<T>, __IList, __ICollection, __IEnumerable
	{
		private IList<T> items;

		public __Collection()
		{
			this.items = new List<T>();
		}

		protected void InsertItemBody(int index, T item)
		{
			this.items.Insert(index, item);

		}

		protected virtual void InsertItem(int index, T item)
		{
			InsertItemBody(index, item);
		}

		protected void SetItemBody(int index, T item)
		{
			this.items[index] = item;

		}

		protected virtual void SetItem(int index, T item)
		{
			SetItemBody(index, item);
		}






		public void Add(T item)
		{
			int num;
			//    if (this.items.IsReadOnly == null)
			//    {
			//        goto Label_0014;
			//    }
			//    ThrowHelper.ThrowNotSupportedException(0x1c);
			//Label_0014:
			num = this.items.Count;
			this.InsertItem(num, item);
			//return;
		}

		public void Clear()
		{
			//    if (this.items.IsReadOnly == null)
			//    {
			//        goto Label_0014;
			//    }
			//    ThrowHelper.ThrowNotSupportedException(0x1c);
			//Label_0014:
			this.ClearItems();
			//return;
		}






		protected virtual void ClearItems()
		{
			this.items.Clear();
		}

		public bool Remove(T item)
		{
			int num;
			//    if (this.items.IsReadOnly == null)
			//    {
			//        goto Label_0014;
			//    }
			//    ThrowHelper.ThrowNotSupportedException(0x1c);
			//Label_0014:
			num = this.items.IndexOf(item);
			if (num >= 0)
			{
				this.RemoveItem(num);
				return true;

			}
			return false;
		}


		protected void RemoveItemBody(int index)
		{
			this.items.RemoveAt(index);

		}

		protected virtual void RemoveItem(int index)
		{
			RemoveItemBody(index);
		}

















		#region IList<T> Members

		public int IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			this.InsertItem(index, item);
		}

		public void RemoveAt(int index)
		{
			//if (this.items.IsReadOnly)
			//{
			//    ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ReadOnlyCollection);
			//}
			//if ((index < 0) || (index >= this.items.Count))
			//{
			//    ThrowHelper.ThrowArgumentOutOfRangeException();
			//}
			this.RemoveItem(index);

		}

		public T this[int index]
		{
			get
			{
				return this.items[index];

			}
			set
			{
				this.SetItem(index, value);

			}
		}

		#endregion

		#region ICollection<T> Members


		public bool Contains(T item)
		{
			return this.items.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			this.items.CopyTo(array, arrayIndex);
		}

		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		public bool IsReadOnly
		{
			get { return this.items.IsReadOnly; }
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		global::System.Collections.IEnumerator __IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		#endregion


		bool __IList.IsFixedSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		object __IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				throw new NotImplementedException();
			}
		}


		int __IList.Add(object value)
		{
			throw new NotImplementedException();
		}

		bool __IList.Contains(object value)
		{
			throw new NotImplementedException();
		}

		int __IList.IndexOf(object item)
		{
			throw new NotImplementedException();
		}

		void __IList.Insert(int index, object value)
		{
			throw new NotImplementedException();
		}

		void __IList.Remove(object value)
		{
			throw new NotImplementedException();



		}

		void __ICollection.CopyTo(global::System.Array array, int index)
		{

			throw new NotImplementedException();


		}


		object __ICollection.SyncRoot
		{
			get
			{
				throw new NotImplementedException();

			}
		}


		bool __ICollection.IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		
 

 


	}
}
