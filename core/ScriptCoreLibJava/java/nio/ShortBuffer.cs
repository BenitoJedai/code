
using ScriptCoreLib;

namespace java.nio
{
    [Script(IsNative = true)]
    public abstract class ShortBuffer : Buffer
    {
        public static ShortBuffer wrap(short[] value)
        {
            return default(ShortBuffer);
        }
    }
}
