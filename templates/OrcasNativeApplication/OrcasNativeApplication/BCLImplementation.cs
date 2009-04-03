﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace OrcasNativeApplication
{
	[Script(Implements = typeof(global::System.String))]
	internal class __String
	{
		[Script(OptimizedCode = @"return e[o];")]
		internal static char StringChar(string e, int o)
		{
			return default(char);
		}

		public char get_Chars(int i)
		{
			return StringChar((string)(object)this, i);
		}

		public int Length
		{
			get
			{
				return string_h.strlen((string)(object)this);
			}
		}
	}

	[Script(Implements = typeof(global::System.Console))]
	internal class __Console
	{
		public static void Beep(int f, int d)
		{
			windows_h.Beep(f, d);
		}

		public static void Write(int i)
		{
			stdio_h.printf("%d", __arglist(i));
		}

		public static void Write(char c)
		{
			stdio_h.putchar(c);
		}

		public static void Write(string e)
		{
			foreach (var c in e)
			{
				Write(c);
			}
		}

		public static void WriteLine()
		{
			WriteLine("");
		}

		public static void WriteLine(int e)
		{
			Write(e);
			WriteLine();
		}
		public static void WriteLine(string e)
		{
			stdio_h.puts(e);
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
