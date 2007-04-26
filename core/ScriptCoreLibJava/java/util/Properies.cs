using ScriptCoreLib;

using java.io;

namespace java.util
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/util/Properties.html
    [Script(IsNative = true)]
    public class Properties
    {
        public string getProperty(string key)
        {
            return default(string);
        }

        public void load(InputStream inStream)
        {
        }
    }
}
