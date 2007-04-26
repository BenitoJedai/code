using ScriptCoreLib;

namespace java.io
{
    /// <summary>
    /// public abstract class Writer
    /// </summary>
    [Script(IsNative = true)]
    public abstract class Writer
    {


        // Method Summary
        /// <summary>
        /// Close the stream, flushing it first.
        /// </summary>
        public abstract void close();

        /// <summary>
        /// Flush the stream.
        /// </summary>
        public abstract void flush();

        /// <summary>
        /// Write an array of characters.
        /// </summary>
        public void write(char[] cbuf)
        {
            return;
        }

        /// <summary>
        /// Write a portion of an array of characters.
        /// </summary>
        public abstract void write(char[] cbuf, int off, int len);

        /// <summary>
        /// Write a single character.
        /// </summary>
        public void write(int c)
        {
            return;
        }

        /// <summary>
        /// Write a string.
        /// </summary>
        public void write(string str)
        {
            return;
        }

        /// <summary>
        /// Write a portion of a string.
        /// </summary>
        public void write(string str, int off, int len)
        {
            return;
        }

        // Methods inherited from class java.lang.Object

    }
}

