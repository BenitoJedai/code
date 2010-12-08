using ScriptCoreLib;

namespace java.security
{
    [Script(IsNative=true)]
    public class ProtectionDomain
    {
        public CodeSource getCodeSource()
        {
            return default(CodeSource);
        }
    }
}
