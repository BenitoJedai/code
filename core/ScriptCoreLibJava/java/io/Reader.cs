using ScriptCoreLib;

namespace java.io
{
    [Script(IsNative=true)]
    public abstract class Reader
    {
        #region methods
        /// <summary>
        /// Close the stream.
        /// </summary>
        public abstract void close();

        /// <summary>
        /// Mark the present position in the stream.
        /// </summary>
        public void mark(int readAheadLimit)
        {
        }

        /// <summary>
        /// Tell whether this stream supports the mark() operation.
        /// </summary>
        public bool markSupported()
        {
            return default(bool);
        }

        /// <summary>
        /// Read a single character.
        /// </summary>
        public int read()
        {
            return default(int);
        }

        /// <summary>
        /// Read characters into an array.
        /// </summary>
        public int read(char[] cbuf)
        {
            return default(int);
        }

        /// <summary>
        /// Read characters into a portion of an array.
        /// </summary>
        public abstract int read(char[] cbuf, int off, int len);

        /// <summary>
        /// Tell whether this stream is ready to be read.
        /// </summary>
        public bool ready()
        {
            return default(bool);
        }

        /// <summary>
        /// Reset the stream.
        /// </summary>
        public void reset()
        {
        }

        /// <summary>
        /// Skip characters.
        /// </summary>
        public long skip(long n)
        {
            return default(long);
        }

        #endregion

    }
}
