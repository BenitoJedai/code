using ScriptCoreLib;

using java.io;
using java.util;

namespace java.lang
{
   
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/lang/Process.html
    [Script(IsNative = true)]
    public abstract class Process
    {
  
        public OutputStream getOutputStream()
        {
            return null;
        }
        public InputStream getInputStream()
        {
            return null;
        }
    }
}

