using System;
using System.Collections.Generic;
using System.Text;
using ScriptCoreLib;

namespace java.awt
{
    // http://java.sun.com/j2se/1.4.2/docs/api/java/awt/Toolkit.html#beep()
    [Script(IsNative = true)]
    public class Toolkit
    {
        public void beep()
        {
        }

        public static Toolkit getDefaultToolkit()
        {
            return default(Toolkit);
        }
    }
}
