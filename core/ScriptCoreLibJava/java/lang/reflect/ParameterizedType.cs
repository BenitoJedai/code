using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;

namespace java.lang.reflect
{
    // http://docs.oracle.com/javase/1.5.0/docs/api/java/lang/reflect/ParameterizedType.html
    // http://developer.android.com/reference/java/lang/reflect/ParameterizedType.html
    [Script(IsNative = true)]
    public interface ParameterizedType :  Type
    {
        Type[] getActualTypeArguments();
    }
}
