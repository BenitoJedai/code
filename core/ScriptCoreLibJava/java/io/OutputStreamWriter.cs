using java.util;
using java.lang;

using ScriptCoreLib;

namespace java.io
{
    /// <summary>
    /// public class OutputStreamWriter
    /// </summary>
    [Script(IsNative = true)]
    public class OutputStreamWriter : Writer
    {
        // Field Summary
        // Fields inherited from class java.io.Writer

        public OutputStreamWriter()
        {
        }

        // Constructor Summary
        /// <summary>
        /// Create an OutputStreamWriter that uses the default character encoding.
        /// </summary>
        public OutputStreamWriter(OutputStream _out)
        {
        }

        ///// <summary>
        ///// Create an OutputStreamWriter that uses the given charset.
        ///// </summary>
        //public OutputStreamWriter(OutputStream _out, Charset cs)
        //{
        //}

        ///// <summary>
        ///// Create an OutputStreamWriter that uses the given charset encoder.
        ///// </summary>
        //public OutputStreamWriter(OutputStream _out, CharsetEncoder enc)
        //{
        //}

        ///// <summary>
        ///// Create an OutputStreamWriter that uses the named charset.
        ///// </summary>
        //public OutputStreamWriter(OutputStream _out, string charsetName)
        //{
        //}

        // Method Summary
        /// <summary>
        /// Close the stream.
        /// </summary>
        public  override void  close ()
        {
            return ;
        }

        /// <summary>
        /// Flush the stream.
        /// </summary>
        public override void flush()
        {
            return ;
        }

        /// <summary>
        /// Return the name of the character encoding being used by this stream.
        /// </summary>
        public   string   getEncoding ()
        {
            return default( string  );
        }

        /// <summary>
        /// Write a portion of an array of characters.
        /// </summary>
        public override void write(char[] cbuf, int off, int len)
        {
            return;
        }

        /// <summary>
        /// Write a single character.
        /// </summary>
        public  void write(int c)
        {
            return;
        }

        /// <summary>
        /// Write a portion of a string.
        /// </summary>
        public   void  write (string  str, int off, int len)
        {
            return;
        }

        // Methods inherited from class java.io.Writer
        // Methods inherited from class java.lang.Object

    }
}

