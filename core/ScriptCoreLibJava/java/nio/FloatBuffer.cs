
using ScriptCoreLib;

namespace java.nio
{
    [Script(IsNative = true)]
    public abstract class FloatBuffer : Buffer
    {
        public static FloatBuffer wrap(float[] value)
        {
            return default(FloatBuffer);
        }
    }
}
