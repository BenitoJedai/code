using ScriptCoreLib;

namespace java.lang
{
    [Script(IsNative=true)]
    public interface Cloneable
    {
        object clone();
    }
}
