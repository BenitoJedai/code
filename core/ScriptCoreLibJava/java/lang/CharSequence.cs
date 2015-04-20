using ScriptCoreLib;

namespace java.lang
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/lang/CharSequence.html
    // http://developer.android.com/reference/java/lang/CharSequence.html
    [Script(IsNative = true)]
    public interface CharSequence
    {
        int length();
    }
}
