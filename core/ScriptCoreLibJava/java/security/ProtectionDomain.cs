using ScriptCoreLib;

namespace java.security
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/security/ProtectionDomain.html
    // http://developer.android.com/reference/java/security/ProtectionDomain.html
    [Script(IsNative=true)]
    public class ProtectionDomain
    {
        public CodeSource getCodeSource()
        {
            return default(CodeSource);
        }
    }
}
