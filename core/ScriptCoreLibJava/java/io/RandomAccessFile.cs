
using ScriptCoreLib;

namespace java.io
{
    /// <summary>
    /// http://java.sun.com/j2se/1.4.2/docs/api/java/io/RandomAccessFile.html
    /// </summary>
    [Script(IsNative = true)]
    public class RandomAccessFile
    {
        public RandomAccessFile(File file, string mode)
        {
        }

        public RandomAccessFile(string file, string mode)
        {
        }

        #region methods
        /// <summary>
        /// Closes this random access file stream and releases any system  resources associated with the stream.
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Returns the unique <A HREF="../../java/nio/channels/FileChannel.html" title="class in java.nio.channels"><CODE>FileChannel</CODE></A> object associated with this file.
        /// </summary>
        public FileChannel getChannel()
        {
            return default(FileChannel);
        }

        /// <summary>
        /// Returns the opaque file descriptor object associated with this stream.
        /// </summary>
        public FileDescriptor getFD()
        {
            return default(FileDescriptor);
        }

        /// <summary>
        /// Returns the current offset in this file.
        /// </summary>
        public long getFilePointer()
        {
            return default(long);
        }

        /// <summary>
        /// Returns the length of this file.
        /// </summary>
        public long length()
        {
            return default(long);
        }

        /// <summary>
        /// Reads a byte of data from this file.
        /// </summary>
        public int read()
        {
            return default(int);
        }

        /// <summary>
        /// Reads up to <code>b.length</code> bytes of data from this file  into an array of bytes.
        /// </summary>
        public int read(sbyte[] b)
        {
            return default(int);
        }

        /// <summary>
        /// Reads up to <code>len</code> bytes of data from this file into an  array of bytes.
        /// </summary>
        public int read(sbyte[] b, int off, int len)
        {
            return default(int);
        }

        /// <summary>
        /// Reads a <code>boolean</code> from this file.
        /// </summary>
        public bool readBoolean()
        {
            return default(bool);
        }

        /// <summary>
        /// Reads a signed eight-bit value from this file.
        /// </summary>
        public sbyte readByte()
        {
            return default(sbyte);
        }

        /// <summary>
        /// Reads a Unicode character from this file.
        /// </summary>
        public char readChar()
        {
            return default(char);
        }

        /// <summary>
        /// Reads a <code>double</code> from this file.
        /// </summary>
        public double readDouble()
        {
            return default(double);
        }

        /// <summary>
        /// Reads a <code>float</code> from this file.
        /// </summary>
        public float readFloat()
        {
            return default(float);
        }

        /// <summary>
        /// Reads <code>b.length</code> bytes from this file into the byte  array, starting at the current file pointer.
        /// </summary>
        public void readFully(sbyte[] b)
        {
        }

        /// <summary>
        /// Reads exactly <code>len</code> bytes from this file into the byte  array, starting at the current file pointer.
        /// </summary>
        public void readFully(sbyte[] b, int off, int len)
        {
        }

        /// <summary>
        /// Reads a signed 32-bit integer from this file.
        /// </summary>
        public int readInt()
        {
            return default(int);
        }

        /// <summary>
        /// Reads the next line of text from this file.
        /// </summary>
        public string readLine()
        {
            return default(string);
        }

        /// <summary>
        /// Reads a signed 64-bit integer from this file.
        /// </summary>
        public long readLong()
        {
            return default(long);
        }

        /// <summary>
        /// Reads a signed 16-bit number from this file.
        /// </summary>
        public short readShort()
        {
            return default(short);
        }

        /// <summary>
        /// Reads an unsigned eight-bit number from this file.
        /// </summary>
        public int readUnsignedByte()
        {
            return default(int);
        }

        /// <summary>
        /// Reads an unsigned 16-bit number from this file.
        /// </summary>
        public int readUnsignedShort()
        {
            return default(int);
        }

        /// <summary>
        /// Reads in a string from this file.
        /// </summary>
        public string readUTF()
        {
            return default(string);
        }

        /// <summary>
        /// Sets the file-pointer offset, measured from the beginning of this  file, at which the next read or write occurs.
        /// </summary>
        public void seek(long pos)
        {
        }

        /// <summary>
        /// Sets the length of this file.
        /// </summary>
        public void setLength(long newLength)
        {
        }

        /// <summary>
        /// Attempts to skip over <code>n</code> bytes of input discarding the  skipped bytes.
        /// </summary>
        public int skipBytes(int n)
        {
            return default(int);
        }

        /// <summary>
        /// Writes <code>b.length</code> bytes from the specified byte array  to this file, starting at the current file pointer.
        /// </summary>
        public void write(sbyte[] b)
        {
        }

        /// <summary>
        /// Writes <code>len</code> bytes from the specified byte array  starting at offset <code>off</code> to this file.
        /// </summary>
        public void write(sbyte[] b, int off, int len)
        {
        }

        /// <summary>
        /// Writes the specified byte to this file.
        /// </summary>
        public void write(int b)
        {
        }

        /// <summary>
        /// Writes a <code>boolean</code> to the file as a one-byte value.
        /// </summary>
        public void writeBoolean(bool v)
        {
        }

        /// <summary>
        /// Writes a <code>byte</code> to the file as a one-byte value.
        /// </summary>
        public void writeByte(int v)
        {
        }

        /// <summary>
        /// Writes the string to the file as a sequence of bytes.
        /// </summary>
        public void writeBytes(string s)
        {
        }

        /// <summary>
        /// Writes a <code>char</code> to the file as a two-byte value, high byte first.
        /// </summary>
        public void writeChar(int v)
        {
        }

        /// <summary>
        /// Writes a string to the file as a sequence of characters.
        /// </summary>
        public void writeChars(string s)
        {
        }

        /// <summary>
        /// Converts the double argument to a <code>long</code> using the  <code>doubleToLongBits</code> method in class <code>Double</code>,  and then writes that <code>long</code> value to the file as an  eight-byte quantity, high byte first.
        /// </summary>
        public void writeDouble(double v)
        {
        }

        /// <summary>
        /// Converts the float argument to an <code>int</code> using the  <code>floatToIntBits</code> method in class <code>Float</code>,  and then writes that <code>int</code> value to the file as a  four-byte quantity, high byte first.
        /// </summary>
        public void writeFloat(float v)
        {
        }

        /// <summary>
        /// Writes an <code>int</code> to the file as four bytes, high byte first.
        /// </summary>
        public void writeInt(int v)
        {
        }

        /// <summary>
        /// Writes a <code>long</code> to the file as eight bytes, high byte first.
        /// </summary>
        public void writeLong(long v)
        {
        }

        /// <summary>
        /// Writes a <code>short</code> to the file as two bytes, high byte first.
        /// </summary>
        public void writeShort(int v)
        {
        }

        /// <summary>
        /// Writes a string to the file using UTF-8 encoding in a  machine-independent manner.
        /// </summary>
        public void writeUTF(string str)
        {
        }

        #endregion

    }

}
