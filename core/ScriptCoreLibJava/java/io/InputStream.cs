using ScriptCoreLib;

namespace java.io
{
    [Script(IsNative = true)]
    public abstract class InputStream
    {

        #region methods
        /// <summary>
        /// Returns the number of bytes that can be read (or skipped over) from this input stream without blocking by the next caller of a method for this input stream.
        /// </summary>
        public int available()
        {
            return default(int);
        }

        /// <summary>
        /// Closes this input stream and releases any system resources associated with the stream.
        /// </summary>
        public void close()
        {
        }

        /// <summary>
        /// Marks the current position in this input stream.
        /// </summary>
        public void mark(int readlimit)
        {
        }

        /// <summary>
        /// Tests if this input stream supports the <code>mark</code> and <code>reset</code> methods.
        /// </summary>
        public bool markSupported()
        {
            return default(bool);
        }

        /// <summary>
        /// Reads the next byte of data from the input stream.
        /// </summary>
        public abstract int read();

        /// <summary>
        /// Reads some number of bytes from the input stream and stores them into the buffer array <code>b</code>.
        /// </summary>
        public int read(sbyte[] b)
        {
            return default(int);
        }

        /// <summary>
        /// Reads up to <code>len</code> bytes of data from the input stream into an array of bytes.
        /// </summary>
        public int read(sbyte[] b, int off, int len)
        {
            return default(int);
        }

        /// <summary>
        /// Repositions this stream to the position at the time the <code>mark</code> method was last called on this input stream.
        /// </summary>
        public void reset()
        {
        }

        /// <summary>
        /// Skips over and discards <code>n</code> bytes of data from this input stream.
        /// </summary>
        public long skip(long n)
        {
            return default(long);
        }

        #endregion

    }
}
