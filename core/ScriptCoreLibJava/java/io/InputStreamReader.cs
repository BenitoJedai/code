using ScriptCoreLib;

namespace java.io
{
    [Script(IsNative = true)]
    public class InputStreamReader : Reader
    {
        public InputStreamReader(InputStream e)
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
        /// Return the name of the character encoding being used by this stream.
        /// </summary>
        public string getEncoding()
        {
            return default(string);
        }


        /// <summary>
        /// Read characters into a portion of an array.
        /// </summary>
        public override int read(char[] cbuf, int offset, int length)
        {
            return default(int);
        }



        #endregion

    }
}
