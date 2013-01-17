
using ScriptCoreLib;

namespace java.nio
{
    [Script(IsNative = true)]
    public abstract class IntBuffer : Buffer
    {
        public static IntBuffer wrap(int[] value)
        {
            return default(IntBuffer);
        }
    }
}
