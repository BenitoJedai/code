using ScriptCoreLib;

namespace java.io
{
    [Script(IsNative = true)]
    public class FilterInputStream : InputStream
    {
        public FilterInputStream()
        {
        }

        public override int read()
        {
            return default(int);
        }
    }
}
