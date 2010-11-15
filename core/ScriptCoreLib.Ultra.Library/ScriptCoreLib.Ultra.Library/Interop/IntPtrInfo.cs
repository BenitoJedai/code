using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace ScriptCoreLib.Interop
{
    public sealed class IntPtrInfo : IDisposable
    {
        public readonly IntPtr Pointer;

        public readonly int Size;

        public IntPtrInfo(int size)
            : this(size, Marshal.AllocHGlobal(size))
        {
        }

        public static IntPtrInfo Create(int Size)
        {
            return new IntPtrInfo(Size);
        }

        public static IntPtr ToPointer(IntPtrInfo e)
        {
            return e;
        }

        public static IntPtrInfo Create(int Size, IntPtr Pointer)
        {
            return new IntPtrInfo(Size, Pointer);
        }

        public IntPtrInfo(int size, IntPtr Pointer)
        {
            this.Size = size;
            this.Pointer = Pointer;
        }

        public byte[] Bytes
        {
            get
            {
                var x = new byte[this.Size];

                Marshal.Copy(this.Pointer, x, 0, this.Size);

                return x;
            }
            set
            {
                if (value.Length != this.Size)
                    throw new InvalidOperationException();

                Marshal.Copy(value, 0, this.Pointer, value.Length);
            }
        }

        public int Int32Value
        {
            get
            {
                if (this.Size != 4)
                    throw new InvalidOperationException();

                var value = new BinaryReader(new MemoryStream(this.Bytes)).ReadInt32();

                return value;
            }

            set
            {
                var m = new MemoryStream();
                var w = new BinaryWriter(m);

                w.Write(value);

                this.Bytes = m.ToArray();
            }
        }

        public IntPtr IntPtrValue
        {
            get
            {
                return (IntPtr)this.Int64Value;
            }
            set
            {
                this.Int64Value = value.ToInt64();
            }
        }

        public static IntPtr GetIntPtrValue(IntPtrInfo e)
        {
            return e.IntPtrValue;
        }

        public static void SetIntPtrValue(IntPtrInfo e, IntPtr value)
        {
            e.IntPtrValue = value;
        }


        public long Int64Value
        {
            get
            {
                if (this.Size != 8)
                    throw new InvalidOperationException();

                var value = new BinaryReader(new MemoryStream(this.Bytes)).ReadInt64();

                return value;
            }

            set
            {
                var m = new MemoryStream();
                var w = new BinaryWriter(m);

                w.Write(value);

                this.Bytes = m.ToArray();
            }
        }

        public string StringValue
        {
            get
            {
                var x = this.Bytes;
                var z = new byte[x.Length - 1];

                Array.Copy(x, z, z.Length);

                return Encoding.ASCII.GetString(z);
            }
            set
            {
                this.Bytes = GetBytes(value);
            }
        }

        public static void Dispose(IntPtrInfo e)
        {
            e.Dispose();
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(this.Pointer);
        }

        public static implicit operator IntPtr(IntPtrInfo e)
        {
            return e.Pointer;
        }

        public static implicit operator IntPtrInfo(string value)
        {
            var Bytes = GetBytes(value);

            return new IntPtrInfo(Bytes.Length)
            {
                Bytes = Bytes
            };
        }

        private static byte[] GetBytes(string value)
        {
            var x = Encoding.ASCII.GetBytes(value);

            var m = new MemoryStream();
            var w = new BinaryWriter(m);

            w.Write(x);
            w.Write((byte)0);

            var Bytes = m.ToArray();
            return Bytes;
        }
    }

}
