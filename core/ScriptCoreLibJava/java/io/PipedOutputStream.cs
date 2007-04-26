using ScriptCoreLib;

namespace java.io
{
    [Script(IsNative = true)]
    public class PipedOutputStream : OutputStream
    {
        #region methods

        /// <summary>
        /// Connects this piped output stream to a receiver.
        /// </summary>
        public void connect(PipedInputStream snk)
        {
        }

        #endregion

    }
}
