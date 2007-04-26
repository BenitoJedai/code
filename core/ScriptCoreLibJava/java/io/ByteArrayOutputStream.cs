using ScriptCoreLib;

namespace java.io
{
    [Script(IsNative = true)]
    public class ByteArrayOutputStream
    {
        //   // Field Summary
        //??? protected  byte[]buf 
        //    The buffer where data is stored.

        //??? protected  intcount 
        //    The number of valid bytes in the buffer.

        // Constructor Summary
        /// <summary>
        /// Creates a new byte array output stream.
        /// </summary>
        public ByteArrayOutputStream()
        {
        }

        /// <summary>
        /// Creates a new byte array output stream, with a buffer capacity of the specified size, in bytes.
        /// </summary>
        public ByteArrayOutputStream(int size)
        {
        }

        // Method Summary
        /// <summary>
        /// Closing a
        /// </summary>
        public void close()
        {
            return;
        }

        /// <summary>
        /// Resets the
        /// </summary>
        public void reset()
        {
            return;
        }

        /// <summary>
        /// Returns the current size of the buffer.
        /// </summary>
        public int size()
        {
            return default(int);
        }

        /// <summary>
        /// Creates a newly allocated byte array.
        /// </summary>
        public sbyte[] toByteArray()
        {
            return default(sbyte[]);
        }

        /// <summary>
        /// Converts the buffer's contents into a string, translating bytes into characters according to the platform's default character encoding.
        /// </summary>
        public string toString()
        {
            return default(string);
        }

        /// <summary>
        /// 
        /// </summary>
        public string toString(int hibyte)
        {
            return default(string);
        }

        /// <summary>
        /// Converts the buffer's contents into a string, translating bytes into characters according to the specified character encoding.
        /// </summary>
        public string toString(string enc)
        {
            return default(string);
        }

        /// <summary>
        /// Writes
        /// </summary>
        public void write(sbyte[] b, int off, int len)
        {
            return;
        }

        /// <summary>
        /// Writes the specified byte to this byte array output stream.
        /// </summary>
        public void write(int b)
        {
            return;
        }

        /// <summary>
        /// Writes the complete contents of this byte array output stream to the specified output stream argument, as if by calling the output stream's write method using
        /// </summary>
        public void writeTo(OutputStream _out)
        {
            return;
        }

        //  // Methods inherited from class java.io.OutputStream
        //??? flush, write

        //  // Methods inherited from class java.lang.Object
        //??? clone, equals, finalize, getClass, hashCode, notify, notifyAll, wait, wait, wait


    }
}
