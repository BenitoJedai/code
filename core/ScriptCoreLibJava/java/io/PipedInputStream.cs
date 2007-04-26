using ScriptCoreLib;

namespace java.io
{
    [Script(IsNative = true)]
    public class PipedInputStream : InputStream
    {
        #region methods
       

        /// <summary>
        /// Causes this piped input stream to be connected to the piped  output stream <code>src</code>.
        /// </summary>
        public void connect(PipedOutputStream src)
        {
        }

        /// <summary>
        /// Reads the next byte of data from this piped input stream.
        /// </summary>
        public override int read()
        {
            return default(int);
        }



        /// <summary>
        /// Receives a byte of data.
        /// </summary>
        protected void receive(int b)
        {
        }

        #endregion

    }
}
