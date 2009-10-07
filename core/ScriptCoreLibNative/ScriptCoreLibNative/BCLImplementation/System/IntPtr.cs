using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace ScriptCoreLibNative.BCLImplementation.System
{
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
