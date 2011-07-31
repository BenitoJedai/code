
using ScriptCoreLib;

namespace java.nio
{
    [Script(IsNative = true)]
    public sealed class ByteOrder
    {
        #region methods
        /// <summary>
        /// Retrieves the native sbyte order of the underlying platform.
        /// </summary>
        public static ByteOrder nativeOrder()
        {
            return default(ByteOrder);
        }

        #endregion

    }
}
