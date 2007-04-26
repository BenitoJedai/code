using ScriptCoreLib;

namespace java.io
{
    /// <summary>
    /// http://java.sun.com/j2se/1.4.2/docs/api/java/io/FileInputStream.html
    /// </summary>
    [Script(IsNative = true)]
    public class FileInputStream : InputStream
    {
        public FileInputStream(File file)
        {
        }

        public FileInputStream(string name)
        {
        }

        public override int read()
        {
            return default(int);
        }
    }
}
