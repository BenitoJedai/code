using ScriptCoreLib;

using csharp;

using java.util;
using java.lang;
using java;
using javax.common.runtime;


using OutOfMemoryException = global::System.OutOfMemoryException;
using NullReferenceException = global::System.NullReferenceException;
using IndexOutOfRangeException = global::System.IndexOutOfRangeException;
using IDisposable = global::System.IDisposable;
using ScriptCoreLibJava.BCLImplementation.System;

namespace jni
{
	[Script]
	public static class CPtrLibrary
	{
		public static string LibraryPath = "lib_jnistb10";
	}

	[Script]
    public class CPtr : IConvertToInt64
	{
		
		public static readonly int SIZE;
		public static readonly CPtr NULL;

		/// <summary>
		/// Pointer value of the real C pointer. Use long to be 64-bit safe.
		/// </summary>
		protected long peer;

		public bool IsNull
		{
			get
			{
				return peer == 0;
			}
		}

		public long Pointer
		{
			get
			{
				return peer;
			}
		}

        public override string ToString()
        {
            return "" + Pointer;
        }

		public static implicit operator long(CPtr e)
		{
			return e.Pointer;
		}

		public static implicit operator string(CPtr e)
		{
			return e.getString(0);
		}

        public static explicit operator System.IntPtr(CPtr e)
        {
            var u = new __IntPtr { PointerToken = e };

            return (System.IntPtr)(object)u;
        }

        public static explicit operator CPtr(System.IntPtr e)
        {
            var u = (__IntPtr)(object)e;

            return u.PointerToken as CPtr;
        }

		#region pinvoke
		[Script(IsPInvoke = true)]
		public void copyIn(int bOff, sbyte[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyIn(int bOff, short[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyIn(int bOff, char[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyIn(int bOff, int[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyIn(int bOff, long[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyIn(int bOff, float[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyIn(int bOff, double[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyOut(int bOff, sbyte[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyOut(int bOff, short[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyOut(int bOff, char[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyOut(int bOff, int[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyOut(int bOff, long[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyOut(int bOff, float[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public void copyOut(int bOff, double[] buf, int index, int length) { }
		[Script(IsPInvoke = true)]
		public sbyte getByte(int offset) { return default(sbyte); }
		[Script(IsPInvoke = true)]
		public short getShort(int offset) { return default(short); }
		[Script(IsPInvoke = true)]
		public int getInt(int offset) { return default(int); }
		[Script(IsPInvoke = true)]
		public long getLong(int offset) { return default(long); }
		[Script(IsPInvoke = true)]
		public float getFloat(int offset) { return default(float); }
		[Script(IsPInvoke = true)]
		public double getDouble(int offset) { return default(double); }
		[Script(IsPInvoke = true)]
		public CPtr getCPtr(int offset) { return default(CPtr); }
		[Script(IsPInvoke = true)]
		public string getString(int offset) { return default(string); }
		[Script(IsPInvoke = true)]
		public void setByte(int offset, sbyte value) { }
		[Script(IsPInvoke = true)]
		public void setShort(int offset, short value) { }
		[Script(IsPInvoke = true)]
		public void setInt(int offset, int value) { }
		[Script(IsPInvoke = true)]
		public void setLong(int offset, long value) { }
		[Script(IsPInvoke = true)]
		public void setFloat(int offset, float value) { }
		[Script(IsPInvoke = true)]
		public void setDouble(int offset, double value) { }
		[Script(IsPInvoke = true)]
		virtual public void setCPtr(int offset, CPtr value) { }
		[Script(IsPInvoke = true)]
		virtual public void setString(int offset, string value) { }
		[Script(IsPInvoke = true)]
		private static int initIDs(CPtr p) { return default(int); }

		#endregion

		protected CPtr()
		{
		}

		public CPtr(long p)
		{
			this.peer = p;
		}

		static CPtr()
		{
			// http://support.teamdev.com/thread/405
			JavaSystem.loadLibrary(CPtrLibrary.LibraryPath);

			NULL = new CPtr();

			SIZE = initIDs(NULL);
		}

		private static void LoadLibrary()
		{
			
		}

		public static CPtr ReportNullReference(CPtr cPtr)
		{
			if (cPtr.IsNull)
				throw new NullReferenceException();

			return cPtr;
		}

		public static bool IsNullReferenceOrPointer(CPtr e)
		{
			if (e == null)
				return true;

			if (e.IsNull)
				return true;

			return false;
		}



		public void setString(string p)
		{
			this.setString(0, p);
		}

		public string getString()
		{
			return getString(0);
		}

		public string PointerToHexString()
		{
			return "0x" + Convert.ToHexString(this.Pointer, 8);
		}

        public long ToInt64()
        {
            return this.Pointer;
        }
    }


	[Script]
	public class CFunc : CPtr
	{
		/// <summary>
		/// warning: jni must be placed under java path
		/// any other shall be placed on system32 path
		/// </summary>
		/// <param name="lib"></param>
		/// <param name="fname"></param>
		public CFunc(string lib, string fname)
		{
			try
			{
				peer = find(lib, fname);

				if (jni.CFunc.IsVerbose)
					global::System.Console.WriteLine("jni: extern " + lib + "+" + fname + " at " + this.PointerToHexString());

			}
			catch (csharp.UnsatisfiedLinkError u)
			{
				throw new csharp.RuntimeException(u.Message);
			}
		}

		[Script(IsPInvoke = true)]
		private long find(string lib, string fname) { return default(long); }

		[Script(IsPInvoke = true)]
		public int callInt(params object[] args) { return default(int); }
		[Script(IsPInvoke = true)]
		public void callVoid(params object[] args) { return; }
		[Script(IsPInvoke = true)]
		public float callFloat(params object[] args) { return default(float); }
		[Script(IsPInvoke = true)]
		public double callDouble(params object[] args) { return default(double); }
		[Script(IsPInvoke = true)]
		public CPtr callCPtr(params object[] args) { return default(CPtr); }

		public string callString(params object[] args)
		{
			return callCPtr(args).getString(0);
		}

		public bool callBoolean(params object[] args)
		{
			return callInt(args) == 1;
		}

		public long callLong(params object[] args)
		{
			return callCPtr(args).Pointer;
		}

		[Script(IsPInvoke = true)]
		private static void SetVerbose(bool p)
		{

		}

		private static bool _IsVerbose;

		public static bool IsVerbose
		{
			get
			{
				return _IsVerbose;
			}
			set
			{
				_IsVerbose = value;
				SetVerbose(value);
			}
		}




		public static void ReportNonZero(string methodname, int p)
		{
			if (p != 0)
				throw new csharp.RuntimeException("function '" + methodname + "' returned 0x" + Convert.ToHexString(p, 4) + " (" + p + ")");

		}
	}

	[Script]
	public class CMalloc : CPtr
	{


		/// <summary>
		/// collects all memory created by this collector at the event of dispose
		/// </summary>
		[Script]
		public class Collector : IDisposable
		{
			public bool IsVerbose;

			ArrayList list = new ArrayList();

			public long Size
			{
				get
				{
					int mem = 0;

					for (int i = 0; i < list.size(); i++)
					{
						CMalloc m = (CMalloc)list.get(i);

						mem += m.Size;
					}

					return mem;
				}
			}

			public void Add(CMalloc m)
			{
				if (IsVerbose)
				{
					global::System.Console.WriteLine("memc: " + Size + " bytes + " + m.Size + " bytes at  0x" + Convert.ToHexString(m.Pointer, 8));
				}

				list.add(m);
			}

			public static Collector operator +(Collector e, CMalloc m)
			{
				e.Add(m);

				return e;

			}

			public void Collect()
			{
				if (IsVerbose)
					global::System.Console.WriteLine("memc: " + Size + " bytes");

				while (list.size() > 0)
				{
					CMalloc m = (CMalloc)list.get(0);


					m.free();

					list.remove(0);


				}
				if (IsVerbose)
					global::System.Console.WriteLine("memc: " + Size + " bytes");

			}

			public CMalloc OfSize(int mb)
			{
				CMalloc m = new CMalloc(mb);

				for (int i = 0; i < mb; i++)
				{
					m.setByte(i, 0);
				}

				Add(m);

				return m;
			}

			#region IDisposable Members

			public void Dispose()
			{
				Collect();
			}

			#endregion

			public CMalloc OfString(string p)
			{
				CMalloc c = CMalloc.Of(p);

				Add(c);

				return c;
			}

			/// <summary>
			/// allocates jint and sets it
			/// </summary>
			/// <param name="p"></param>
			/// <returns></returns>
			public CMalloc OfInt32(int p)
			{
				CMalloc c = CMalloc.Of(p);

				Add(c);

				return c;
			}

			public CMalloc OfInt64(long p)
			{
				CMalloc c = new CMalloc(jni.CMalloc.sizeof_jlong());

				c.setLong(0, p);

				Add(c);

				return c;

			}

			public CMalloc OfBytes(params int[] e)
			{
				CMalloc c = CMalloc.Of(Convert.ToByteArray(e));

				Add(c);

				return c;
			}

			public CPtr OfBytes(params sbyte[] e)
			{
				CMalloc c = CMalloc.Of(e);

				Add(c);

				return c;
			}

			public CMalloc OfHexString(string p)
			{
				return CMalloc.Of(Convert.FromHexString(p));
			}
		}

		private int size;

		public int Size
		{
			get { return size; }
		}

		[Script(IsPInvoke = true)]
		private static long malloc(int size) { return default(long); }
		[Script(IsPInvoke = true)]
		private static void free(long ptr) { return; }

		[Script(IsPInvoke = true)]
		public static int sizeof_jint() { return default(int); }
		[Script(IsPInvoke = true)]
		public static int sizeof_jbyte() { return default(int); }
		[Script(IsPInvoke = true)]
		public static int sizeof_jlong() { return default(int); }


		public sbyte[] Bytes
		{
			get
			{
				sbyte[] n = new sbyte[Size];

				copyOut(0, n, 0, Size);

				return n;
			}
			set
			{
				if (value.Length != size)
					throw new IndexOutOfRangeException("buffer size does not match");

				copyIn(0, value, 0, size);
			}
		}

		public static CMalloc Of(string e)
		{
			CMalloc x = new CMalloc(e.Length + 1);

			x.setString(0, e);

			return x;
		}

		/// <summary>
		/// allocates jint and sets it
		/// </summary>
		/// <param name="e"></param>
		/// <returns></returns>
		public static CMalloc Of(int e)
		{
			CMalloc x = new CMalloc(CMalloc.sizeof_jint());

			x.setInt(0, e);

			return x;
		}

		public static CMalloc Of(params sbyte[] e)
		{
			CMalloc x = new CMalloc(e.Length);

			for (int i = 0; i < e.Length; i++)
			{
				x.setByte(i, e[i]);
			}

			return x;
		}

		public CMalloc(int size)
		{
			this.size = size;
			this.peer = malloc(size);

			if (this.peer == 0)
			{
				throw new OutOfMemoryException();
			}
		}

		public void free()
		{
			if (peer != 0)
			{
				free(peer);
				peer = 0;
			}
		}

		private void boundsCheck(int off, int sz)
		{
			bool lo = off < 0;
			bool hi = off + sz > size;

			if (lo || hi)
			{
				throw new IndexOutOfRangeException("offset " + off + " size " + sz + ", malloced size " + size);
			}
		}

		public override void setCPtr(int offset, CPtr value)
		{
			boundsCheck(offset, SIZE);
			base.setCPtr(offset, value);
		}

		public override void setString(int offset, string value)
		{
			sbyte[] bytes = Convert.ToByteArray(value);
			int length = bytes.Length;
			boundsCheck(offset, length + 1);
			base.copyIn(offset, bytes, 0, length);
			base.setByte(offset + length, (sbyte)0);
		}

		public void ToConsole()
		{
			Console.WriteHexDump(this.Bytes);
		}

		public string ToHexString()
		{
			return Convert.ToHexString(this.Bytes);
		}



		public string ToHexString(int offset, int length)
		{
			sbyte[] n = new sbyte[length];

			copyOut(0, n, 0, length);

			return Convert.ToHexString(n);
		}
	}

}