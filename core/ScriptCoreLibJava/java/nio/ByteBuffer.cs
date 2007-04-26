using ScriptCoreLib;

namespace java.nio
{
    [Script(IsNative=true)]
    public abstract class ByteBuffer : Buffer
    {
        #region methods
        /// <summary>
        /// Allocates a new byte buffer.
        /// </summary>
        public static ByteBuffer allocate(int capacity)
        {
            return default(ByteBuffer);
        }

        /// <summary>
        /// Allocates a new direct byte buffer.
        /// </summary>
        public static ByteBuffer allocateDirect(int capacity)
        {
            return default(ByteBuffer);
        }

        /// <summary>
        /// Returns the byte array that backs this buffer  <i>(optional operation)</i>.
        /// </summary>
        public sbyte[] array()
        {
            return default(sbyte[]);
        }

        /// <summary>
        /// Returns the offset within this buffer's backing array of the first element of the buffer  <i>(optional operation)</i>.
        /// </summary>
        public int arrayOffset()
        {
            return default(int);
        }

        /// <summary>
        /// Creates a view of this byte buffer as a char buffer.
        /// </summary>
        public abstract CharBuffer asCharBuffer();

        /// <summary>
        /// Creates a view of this byte buffer as a double buffer.
        /// </summary>
        public abstract DoubleBuffer asDoubleBuffer();

        /// <summary>
        /// Creates a view of this byte buffer as a float buffer.
        /// </summary>
        public abstract FloatBuffer asFloatBuffer();

        /// <summary>
        /// Creates a view of this byte buffer as an int buffer.
        /// </summary>
        public abstract IntBuffer asIntBuffer();

        /// <summary>
        /// Creates a view of this byte buffer as a long buffer.
        /// </summary>
        public abstract LongBuffer asLongBuffer();

        /// <summary>
        /// Creates a new, read-only byte buffer that shares this buffer's content.
        /// </summary>
        public abstract ByteBuffer asReadOnlyBuffer();

        /// <summary>
        /// Creates a view of this byte buffer as a short buffer.
        /// </summary>
        public abstract ShortBuffer asShortBuffer();

        /// <summary>
        /// Compacts this buffer  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer compact();

        /// <summary>
        /// Compares this buffer to another object.
        /// </summary>
        public int compareTo(object ob)
        {
            return default(int);
        }

        /// <summary>
        /// Creates a new byte buffer that shares this buffer's content.
        /// </summary>
        public abstract ByteBuffer duplicate();

        /// <summary>
        /// Relative <i>get</i> method.
        /// </summary>
        public abstract sbyte get();

        /// <summary>
        /// Relative bulk <i>get</i> method.
        /// </summary>
        public ByteBuffer get(sbyte[] dst)
        {
            return default(ByteBuffer);
        }

        /// <summary>
        /// Relative bulk <i>get</i> method.
        /// </summary>
        public ByteBuffer get(sbyte[] dst, int offset, int length)
        {
            return default(ByteBuffer);
        }

        /// <summary>
        /// Absolute <i>get</i> method.
        /// </summary>
        public abstract sbyte get(int index);

        /// <summary>
        /// Relative <i>get</i> method for reading a char value.
        /// </summary>
        public abstract char getChar();

        /// <summary>
        /// Absolute <i>get</i> method for reading a char value.
        /// </summary>
        public abstract char getChar(int index);

        /// <summary>
        /// Relative <i>get</i> method for reading a double value.
        /// </summary>
        public abstract double getDouble();

        /// <summary>
        /// Absolute <i>get</i> method for reading a double value.
        /// </summary>
        public abstract double getDouble(int index);

        /// <summary>
        /// Relative <i>get</i> method for reading a float value.
        /// </summary>
        public abstract float getFloat();

        /// <summary>
        /// Absolute <i>get</i> method for reading a float value.
        /// </summary>
        public abstract float getFloat(int index);

        /// <summary>
        /// Relative <i>get</i> method for reading an int value.
        /// </summary>
        public abstract int getInt();

        /// <summary>
        /// Absolute <i>get</i> method for reading an int value.
        /// </summary>
        public abstract int getInt(int index);

        /// <summary>
        /// Relative <i>get</i> method for reading a long value.
        /// </summary>
        public abstract long getLong();

        /// <summary>
        /// Absolute <i>get</i> method for reading a long value.
        /// </summary>
        public abstract long getLong(int index);

        /// <summary>
        /// Relative <i>get</i> method for reading a short value.
        /// </summary>
        public abstract short getShort();

        /// <summary>
        /// Absolute <i>get</i> method for reading a short value.
        /// </summary>
        public abstract short getShort(int index);

        /// <summary>
        /// Tells whether or not this buffer is backed by an accessible byte array.
        /// </summary>
        public bool hasArray()
        {
            return default(bool);
        }

        /// <summary>
        /// Tells whether or not this byte buffer is direct.
        /// </summary>
        public abstract bool isDirect();

        /// <summary>
        /// Retrieves this buffer's byte order.
        /// </summary>
        public ByteOrder order()
        {
            return default(ByteOrder);
        }

        /// <summary>
        /// Modifies this buffer's byte order.
        /// </summary>
        public ByteBuffer order(ByteOrder bo)
        {
            return default(ByteBuffer);
        }

        /// <summary>
        /// Relative <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer put(sbyte b);

        /// <summary>
        /// Relative bulk <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public ByteBuffer put(sbyte[] src)
        {
            return default(ByteBuffer);
        }

        /// <summary>
        /// Relative bulk <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public ByteBuffer put(sbyte[] src, int offset, int length)
        {
            return default(ByteBuffer);
        }

        /// <summary>
        /// Relative bulk <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public ByteBuffer put(ByteBuffer src)
        {
            return default(ByteBuffer);
        }

        /// <summary>
        /// Absolute <i>put</i> method  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer put(int index, sbyte b);

        /// <summary>
        /// Relative <i>put</i> method for writing a char value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putChar(char value);

        /// <summary>
        /// Absolute <i>put</i> method for writing a char value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putChar(int index, char value);

        /// <summary>
        /// Relative <i>put</i> method for writing a double value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putDouble(double value);

        /// <summary>
        /// Absolute <i>put</i> method for writing a double value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putDouble(int index, double value);

        /// <summary>
        /// Relative <i>put</i> method for writing a float value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putFloat(float value);

        /// <summary>
        /// Absolute <i>put</i> method for writing a float value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putFloat(int index, float value);

        /// <summary>
        /// Relative <i>put</i> method for writing an int value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putInt(int value);

        /// <summary>
        /// Absolute <i>put</i> method for writing an int value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putInt(int index, int value);

        /// <summary>
        /// Absolute <i>put</i> method for writing a long value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putLong(int index, long value);

        /// <summary>
        /// Relative <i>put</i> method for writing a long value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putLong(long value);

        /// <summary>
        /// Absolute <i>put</i> method for writing a short value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putShort(int index, short value);

        /// <summary>
        /// Relative <i>put</i> method for writing a short value  <i>(optional operation)</i>.
        /// </summary>
        public abstract ByteBuffer putShort(short value);

        /// <summary>
        /// Creates a new byte buffer whose content is a shared subsequence of this buffer's content.
        /// </summary>
        public abstract ByteBuffer slice();

        /// <summary>
        /// Wraps a byte array into a buffer.
        /// </summary>
        public static ByteBuffer wrap(sbyte[] array)
        {
            return default(ByteBuffer);
        }

        /// <summary>
        /// Wraps a byte array into a buffer.
        /// </summary>
        public static ByteBuffer wrap(sbyte[] array, int offset, int length)
        {
            return default(ByteBuffer);
        }

        #endregion

    }
}
