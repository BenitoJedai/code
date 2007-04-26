using ScriptCoreLib;

namespace java.io
{
    /// <summary>
    /// http://java.sun.com/j2se/1.4.2/docs/api/java/io/BufferedReader.html
    /// </summary>
    [Script(IsNative = true)]
    public class BufferedReader : Reader
    {
        public BufferedReader(InputStreamReader e)
        {
        }

        #region methods
        /// <summary>
        /// Close the stream.
        /// </summary>
        public override void close()
        {
        }

        /// <summary>
        /// Read characters into a portion of an array.
        /// </summary>
        public override int read(char[] cbuf, int off, int len)
        {
            return default(int);
        }

        /// <summary>
        /// Read a line of text.
        /// </summary>
        public string readLine()
        {
            return default(string);
        }

      

        #endregion

    }
}
