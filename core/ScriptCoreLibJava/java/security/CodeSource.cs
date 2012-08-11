using ScriptCoreLib;
using java.net;

namespace java.security
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/security/CodeSource.html
    // http://developer.android.com/reference/java/security/CodeSource.html
    [Script(IsNative=true)]
    public class CodeSource
    {
        public URL getLocation()
        {
            return default(URL);
        }
    }
}
