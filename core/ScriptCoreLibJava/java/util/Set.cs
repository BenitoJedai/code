using ScriptCoreLib;

namespace java.util
{
    /// <summary>
    /// http://java.sun.com/j2se/1.4.2/docs/api/java/util/Set.html
    /// </summary>
    [Script(IsNative = true)]
    public interface Set
    {
        object[] toArray();

        object[] toArray(object[] e);
    }
}
