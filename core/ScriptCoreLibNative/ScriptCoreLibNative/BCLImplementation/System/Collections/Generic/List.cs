using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLibNative.SystemHeaders;
using ScriptCoreLib;

namespace ScriptCoreLibNative.BCLImplementation.System.Collections.Generic
{
	[Script(Implements = typeof(global::System.Collections.Generic.List<>))]
	internal class __List<T>
	{
		private int _size;

		T[] _list;

		public __List()
		{
			Allocate(0);
		}

		internal void Allocate(int i)
		{
			_list = new T[i];
			_size = i;
		}

		internal bool InBounds(int i)
		{
			return (i < 0 || i >= _size);
		}

		public T this[int i]
		{
			get
			{
				if (InBounds(i)) return default(T);

				return _list[i];
			}
			set
			{
				if (InBounds(i)) return;

				_list[i] = value;
			}
		}

		public int Count
		{
			get { return _size; }
			set
			{
				_size = value;

				_list = (T[])stdlib_h.realloc(_list, IntPtr.Size * _size);
			}
		}


		public void Add(T e)
		{
			int p = _size;

			Count++;

			this[p] = e;
		}

		/// <summary>
		/// releases internal list, and frees the list object itself
		/// </summary>
		public void Free()
		{
			Count = 0;

			stdlib_h.free(this);
		}


	}

}
