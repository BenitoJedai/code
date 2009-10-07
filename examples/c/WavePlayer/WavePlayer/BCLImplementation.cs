using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace WavePlayer
{

	[Script(Implements = typeof(global::System.IO.File))]
	internal class __File
	{
		// http://www.cplusplus.com/reference/clibrary/cstdio/fopen.html

		public static void WriteAllText(string path, string contents)
		{
			var handle = stdio_h.fopen(path, "w+");
			stdio_h.fputs(contents, handle);
			stdio_h.fclose(handle);
		}
	}



	[Script(Implements = typeof(global::System.Threading.Thread))]
	internal class __Thread
	{
		public static void Sleep(int p)
		{
			windows_h.Sleep(p);
		}
	}


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

	[Script(Implements = typeof(global::System.IntPtr))]
	internal class __IntPtr
	{
		public static int Size
		{
			[Script(OptimizedCode = "return sizeof(void*);")]
			get
			{
				return default(int);
			}
		}

		[Script(OptimizedCode = "return a==b;")]
		static public bool operator ==(__IntPtr a, __IntPtr b)
		{
			return default(bool);
		}

		[Script(OptimizedCode = "return a!=b;")]
		static public bool operator !=(__IntPtr a, __IntPtr b)
		{
			return default(bool);
		}

		public override bool Equals(object obj)
		{
			return this == obj as __IntPtr;
		}

		public override int GetHashCode()
		{
			return default(int);
		}
	}
}
