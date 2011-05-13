using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace java.lang
{
    // http://download.oracle.com/javase/1.4.2/docs/api/java/lang/Comparable.html
    [Script(IsNative = true)]
    public interface Comparable
    {
        int compareTo(object o);
    }
}
