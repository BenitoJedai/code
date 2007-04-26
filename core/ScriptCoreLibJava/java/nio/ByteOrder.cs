
using ScriptCoreLib;

namespace java.nio
{
    [Script(IsNative = true)]
    public class ByteOrder
    {
        #region methods
        /// <summary>
        /// Retrieves the native byte order of the underlying platform.
        /// </summary>
        public static ByteOrder nativeOrder()
        {
            return default(ByteOrder);
        }

        #endregion

    }
}
